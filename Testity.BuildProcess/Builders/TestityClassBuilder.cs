﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Options;
using System;
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

		public void AddClassField(string fieldName, Type fieldType, bool isPrivate = false, params Attribute[] attris)
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
				//Why? I don't know. I don't understand Rosyln to be honest
				attributeList = attributeList.Add(SyntaxFactory.AttributeList(SyntaxFactory.SingletonSeparatedList(SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(a.GetType().ToString())))));
			}

			//New field using the information above that may be private or public.
			FieldDeclarationSyntax newField = SyntaxFactory.FieldDeclaration(variableSyntax)
				.WithAttributeLists(attributeList)
				.WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(isPrivate ? SyntaxKind.PrivateKeyword : SyntaxKind.PublicKeyword)));

			lock(syncObj)
				memberSyntax.Add(newField);
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
	}
}
