using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testity.Common.Unity3D;
using Microsoft.CodeAnalysis.CSharp;

namespace Testity.BuildProcess.Unity3D
{
	public class UnitySerializedFieldImplementationProvider : IMemberImplementationProvider
	{
		public SyntaxToken MemberName { get; private set; }

		public TypeSyntax MemberType { get; private set; }

		public SyntaxTokenList Modifiers { get; private set; }

		public SyntaxList<AttributeListSyntax> Attributes { get; private set; }

		public UnitySerializedFieldImplementationProvider(string memberName, Type typeOfMember, WiredToAttribute wiredAttribute)
		{
			MemberName = SyntaxFactory.Identifier(memberName);
			MemberType = SyntaxFactory.ParseName(typeOfMember.FullName);

			//These modifiers are the same for all unity members. We make them private because we've no reason to do otherwise
			//Modifiers: Private
			Modifiers = SyntaxFactory.TokenList(MemberImplementationModifier.Private.ToSyntaxKind().Select(x => SyntaxFactory.Token(x)));

			//Unity fields require two attributes.
			//Attributes: SerializeField and WiredToAttribute
			Attributes = GenerateUnityAttributes(wiredAttribute);
	}

		private SyntaxList<AttributeListSyntax> GenerateUnityAttributes(WiredToAttribute wiredAttribute)
	{
			//Code generated from: http://roslynquoter.azurewebsites.net/
			//This is NOT human written. Don't try to read it
			return SyntaxFactory.SingletonList<AttributeListSyntax>(
				SyntaxFactory.AttributeList(
					SyntaxFactory.SeparatedList<AttributeSyntax>(
						new SyntaxNodeOrToken[]{
							SyntaxFactory.Attribute(
								SyntaxFactory.QualifiedName(
									SyntaxFactory.IdentifierName(
										@"UnityEngine"),
									SyntaxFactory.IdentifierName(
										@"SerializeField"))
								.WithDotToken(
									SyntaxFactory.Token(
										SyntaxKind.DotToken)))
							.WithArgumentList(
								SyntaxFactory.AttributeArgumentList()
								.WithOpenParenToken(
									SyntaxFactory.Token(
										SyntaxKind.OpenParenToken))
								.WithCloseParenToken(
									SyntaxFactory.Token(
										SyntaxKind.CloseParenToken))),
							SyntaxFactory.Token(
								SyntaxKind.CommaToken),
							SyntaxFactory.Attribute(
								SyntaxFactory.QualifiedName(
									SyntaxFactory.QualifiedName(
										SyntaxFactory.QualifiedName(
											SyntaxFactory.IdentifierName(
												@"Testity"),
											SyntaxFactory.IdentifierName(
												@"Common"))
										.WithDotToken(
											SyntaxFactory.Token(
												SyntaxKind.DotToken)),
										SyntaxFactory.IdentifierName(
											@"Unity3D"))
									.WithDotToken(
										SyntaxFactory.Token(
											SyntaxKind.DotToken)),
									SyntaxFactory.IdentifierName(
										@"WiredToAttribute"))
								.WithDotToken(
									SyntaxFactory.Token(
										SyntaxKind.DotToken)))
							.WithArgumentList(
								SyntaxFactory.AttributeArgumentList(
									SyntaxFactory.SeparatedList<AttributeArgumentSyntax>(
										new SyntaxNodeOrToken[]{
											SyntaxFactory.AttributeArgument(
												SyntaxFactory.MemberAccessExpression(
													SyntaxKind.SimpleMemberAccessExpression,
													SyntaxFactory.MemberAccessExpression(
														SyntaxKind.SimpleMemberAccessExpression,
														SyntaxFactory.MemberAccessExpression(
															SyntaxKind.SimpleMemberAccessExpression,
															SyntaxFactory.IdentifierName(
																@"System"),
															SyntaxFactory.IdentifierName(
																@"Reflection"))
														.WithOperatorToken(
															SyntaxFactory.Token(
																SyntaxKind.DotToken)),
														SyntaxFactory.IdentifierName(
															@"MemberTypes"))
													.WithOperatorToken(
														SyntaxFactory.Token(
															SyntaxKind.DotToken)),
													SyntaxFactory.IdentifierName(
														wiredAttribute.WiredMemberType.ToString())) //modified from the auto-generated source. Inserts membertype in
												.WithOperatorToken(
													SyntaxFactory.Token(
														SyntaxKind.DotToken))),
											SyntaxFactory.Token(
												SyntaxKind.CommaToken),
											SyntaxFactory.AttributeArgument(
												SyntaxFactory.LiteralExpression(
													SyntaxKind.StringLiteralExpression,
													SyntaxFactory.Literal(wiredAttribute.WiredMemberName)
													/*SyntaxFactory.Literal(
														SyntaxFactory.TriviaList(),
														@"""SomeName""",
														@"""SomeName""",
														SyntaxFactory.TriviaList())*/))}))
								.WithOpenParenToken(
									SyntaxFactory.Token(
										SyntaxKind.OpenParenToken))
								.WithCloseParenToken(
									SyntaxFactory.Token(
										SyntaxKind.CloseParenToken)))}))
				.WithOpenBracketToken(
					SyntaxFactory.Token(
						SyntaxKind.OpenBracketToken))
				.WithCloseBracketToken(
					SyntaxFactory.Token(
						SyntaxKind.CloseBracketToken)));
		}
	}
}
