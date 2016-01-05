using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testity.EngineComponents;

namespace Testity.BuildProcess
{
	public class TestityClassBuilder<TType> : IClassBuilder
		where TType : EngineScriptComponent
	{
		private readonly object syncObj = new object();

		private CompilationUnitSyntax rosylnCompilationUnit;

		private ClassDeclarationSyntax rosylnClassUnit;

		private IList<MemberDeclarationSyntax> memberSyntax;

		private bool hasBaseclass = false;

		public TestityClassBuilder()
		{
			rosylnCompilationUnit = SyntaxFactory.CompilationUnit();
			rosylnClassUnit = SyntaxFactory.ClassDeclaration(typeof(TType).Name + "Script");
			memberSyntax = new List<MemberDeclarationSyntax>();
        }

		public void AddDLLReference(string usingString)
		{
			UsingDirectiveSyntax usingUnit = SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName(usingString));

			lock(syncObj)
				rosylnCompilationUnit = rosylnCompilationUnit.AddUsings(usingUnit);
        }

		public void AddBaseClass<TClassType>()
			where TClassType : class
		{
			TypeSyntax typeSyn = SyntaxFactory.ParseTypeName((typeof(TClassType).FullName));

			lock(syncObj)
			{
				//Check if there is already a baseclass
				if (hasBaseclass && typeof(TClassType).IsClass)
					throw new InvalidOperationException("A type may only derive from a single base class.");
				else
				{
					rosylnClassUnit = rosylnClassUnit.AddBaseListTypes(SyntaxFactory.SimpleBaseType(typeSyn));

					hasBaseclass = hasBaseclass || typeof(TClassType).IsClass;
                }			
			}
		}

		//TODO: Support property fields and merge duplicate code
		public void AddClassField(IMemberImplementationProvider implementationProvider)
        {
			VariableDeclarationSyntax variableSyntax = SyntaxFactory.VariableDeclaration(implementationProvider.MemberType)
				.AddVariables(SyntaxFactory.VariableDeclarator(implementationProvider.MemberName));

			//New field using the information above that may be private or public.
			FieldDeclarationSyntax newField = SyntaxFactory.FieldDeclaration(variableSyntax)
				.WithAttributeLists(implementationProvider.ParameterlessAttributes)
				.WithModifiers(implementationProvider.Modifiers);

			lock (syncObj)
				memberSyntax.Add(newField);
		}

		public void AddMemberMethod(IMemberImplementationProvider implementationProvider, IBlockBodyProvider blockProvider, IParameterImplementationProvider parametersProvider = null)// params ParameterData[] typeArgs)
		{
			if (implementationProvider == null)
				throw new ArgumentNullException(nameof(implementationProvider), "The member implementation provider must not be null.");

			if(blockProvider == null)
				throw new ArgumentNullException(nameof(blockProvider), "The member method body block provider must not be null.");

			MethodDeclarationSyntax methodSyntax = SyntaxFactory.MethodDeclaration(implementationProvider.MemberType, implementationProvider.MemberName)
				.WithModifiers(implementationProvider.Modifiers)
				.WithAttributeLists(implementationProvider.ParameterlessAttributes)
				.WithBody(blockProvider.Block);
			
			//Not all methods have parameters so we don't want to require a provider
			if (parametersProvider != null)
				methodSyntax = methodSyntax.WithParameterList(parametersProvider.Parameters);

			lock (syncObj)
				memberSyntax.Add(methodSyntax);
        }

		public string Compile()
		{
			lock(syncObj)
			{
				//don't mutate the class fields
				//We should do it without changing them
				CompilationUnitSyntax compileUnit = rosylnCompilationUnit.AddMembers(rosylnClassUnit.AddMembers(memberSyntax.ToArray()));

				StringBuilder sb = new StringBuilder();
				using (StringWriter writer = new StringWriter(sb))
				{
					Formatter.Format(compileUnit, new AdhocWorkspace()).WriteTo(writer);
				}

				return sb.ToString();
			}
		}

		public Task<string> CompileAsync()
		{

			CompilationUnitSyntax compileUnit = null;
            lock (syncObj)
				//don't mutate the class fields
				//We should do it without changing them
				compileUnit = rosylnCompilationUnit.AddMembers(rosylnClassUnit.AddMembers(memberSyntax.ToArray()));

				StringBuilder sb = new StringBuilder();

				return Task.Factory.StartNew(
					() =>
					{
						using (StringWriter writer = new StringWriter(sb))
						{
							Formatter.Format(compileUnit, new AdhocWorkspace()).WriteTo(writer);
							return sb.ToString();
						}
					});
		}
	}
}
