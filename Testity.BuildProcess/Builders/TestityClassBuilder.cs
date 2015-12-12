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
	public class TestityClassBuilder<TType>
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
		public void AddClassField(string fieldName, Type fieldType, MemberImplementationModifier modifier, params Attribute[] attris)
		{
			//variable details such as: name and type
			VariableDeclarationSyntax variableSyntax = SyntaxFactory
				.VariableDeclaration(
					SyntaxFactory.ParseTypeName(fieldType.ToString()),
					SyntaxFactory.SeparatedList(new[] { SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier(fieldName)) })
					);

			//Generates a new immutable list of attribute syntax data
			SyntaxList<AttributeListSyntax> attributeList;

			foreach(Attribute a in attris)
			{
				//Why is it a list? I think because Rosyln supports adding multiple attributes on a single line
				attributeList = attributeList.Add(SyntaxFactory.AttributeList(SyntaxFactory.SingletonSeparatedList(SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(a.GetType().ToString())))));
			}

			//New field using the information above that may be private or public.
			FieldDeclarationSyntax newField = SyntaxFactory.FieldDeclaration(variableSyntax)
				.WithAttributeLists(attributeList)
				.WithModifiers(SyntaxFactory.TokenList(modifier.ToSyntaxKind().Select(x => SyntaxFactory.Token(x))));

			lock(syncObj)
				memberSyntax.Add(newField);
		}

		public void AddMemberMethod(string methodName, Type returnType, MemberImplementationModifier modifiers, IEnumerable<Type> attributeTypes, params ParameterData[] typeArgs)
		{
			if (attributeTypes == null)
				attributeTypes = Enumerable.Empty<Type>();

			ParameterListSyntax parameters = SyntaxFactory.ParameterList().AddParameters(
				typeArgs.Select(x =>
					SyntaxFactory.Parameter(SyntaxFactory.ParseToken(x.ParameterName))
						.WithType(SyntaxFactory.ParseTypeName(x.ParameterType.FullName))
				).ToArray());

			MethodDeclarationSyntax methodSyntax = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName(returnType.FullName), methodName)
				.WithModifiers(SyntaxFactory.TokenList(modifiers.ToSyntaxKind().Select(x => SyntaxFactory.Token(x))))
				.WithAttributeLists(SyntaxFactory.List(attributeTypes.Select(x => SyntaxFactory.AttributeList(SyntaxFactory.SingletonSeparatedList(SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(x.FullName)))))))
				.WithParameterList(parameters)
				.WithBody(SyntaxFactory.Block());

			lock(syncObj)
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
