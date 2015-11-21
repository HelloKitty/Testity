using MiscUtil;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Testity.EngineMath.UnitTests
{
	[TestFixture]
	public static class GenericVectorTests
	{
		[Test(Author = "Andrew Blakely", Description = "Tests Vector3<TMathType> type intialization/ctor.", TestOf = typeof(Vector3<>))]
		[TestCase(int.MaxValue, int.MinValue, -0)]
		[TestCase(float.NegativeInfinity, float.PositiveInfinity, float.NaN)]
		[TestCase(float.MaxValue, float.MinValue, float.Epsilon)]
		[TestCase(-5.3f, 6.5f, 7.6f)]
		[TestCase(5.3f, 6.5f, 7.6f)]
		[TestCase(1, 3, -4)]
		[TestCase(-1, 3, 4)]
		[TestCase(1, 2, 3)]
		[TestCase(-0, 0, 0)]
		[TestCase(-5.3f, 6.5d, 7.6d)]
		[TestCase(5.3f, 6.5d, -7.6d)]
		[TestCase(1d, 3d, -4d)]
		[TestCase(-1, 3, 4)]
		[TestCase(1, 2, 3d)]
		[TestCase(0d, 0, 0)]
		public static void Test_Vector3_Generic_Init<TMathType>(TMathType a, TMathType b, TMathType c) //NUnit can infer best fit type
			where TMathType : struct
		{
			//arrange
			Vector3<TMathType> vec3 = new Vector3<TMathType>(a, b, c);

			//assert
			Assert.AreEqual(vec3.x, a, nameof(Vector3<TMathType>) + " failed to initialize x value.");
			Assert.AreEqual(vec3.y, b, nameof(Vector3<TMathType>) + " failed to initialize y value.");
			Assert.AreEqual(vec3.z, c, nameof(Vector3<TMathType>) + " failed to initialize z value.");
		}

		[Test(Author = "Andrew Blakely", Description = "Tests Vector3<TMathType> type addition operator.", TestOf = typeof(Vector3<>))]
		[TestCase(double.NegativeInfinity, double.PositiveInfinity, double.NaN)]
		[TestCase(double.MaxValue, double.MinValue, double.Epsilon)]
		[TestCase(int.MaxValue, int.MinValue, -0)]
		[TestCase(float.NegativeInfinity, float.PositiveInfinity, float.NaN)]
		[TestCase(float.MaxValue, float.MinValue, float.Epsilon)]
		[TestCase(-5.3f, 6.5f, 7.6f)]
		[TestCase(5.3f, 6.5f, 7.6f)]
		[TestCase(1, 3, -4)]
		[TestCase(-1, 3, 4)]
		[TestCase(1, 2, 3)]
		[TestCase(-0, 0, 0)]
		[TestCase(-5.3f, 6.5d, 7.6d)]
		[TestCase(5.3f, 6.5d, -7.6d)]
		[TestCase(1d, 3d, -4d)]
		[TestCase(-1, 3, 4)]
		[TestCase(1, 2, 3d)]
		[TestCase(0d, 0, 0)]
		public static void Test_Vector3_Generic_Addition<TMathType>(TMathType a, TMathType b, TMathType c) //NUnit can infer best fit type
			where TMathType : struct
		{
			//arrange
			Vector3<TMathType> vec3One = new Vector3<TMathType>(a, b, c);
			Vector3<TMathType> vec3Two = new Vector3<TMathType>(c, a, b);

			//act
			Vector3<TMathType> resultOne = vec3One + vec3Two;
			Vector3<TMathType> resultTwo = vec3Two + vec3One;

			//assert
			Assert.AreEqual(resultOne, resultTwo);
			//We test a couple variations of the addition. Should be enough.
			Assert.AreEqual(Operator.Add(a, c), resultOne.x);
			Assert.AreEqual(Operator.Add(a, c), resultTwo.x);
			Assert.AreEqual(Operator.Add(b, a), resultOne.y);
			Assert.AreEqual(Operator.Add(b, a), resultTwo.y);
		}

		[Test(Author = "Andrew Blakely", Description = "Tests Vector3<TMathType> type multiplcation dot operator.", TestOf = typeof(Vector3<>))]
		[TestCase(double.NegativeInfinity, double.PositiveInfinity, double.NaN)]
		[TestCase(double.MaxValue, double.MinValue, double.Epsilon)]
		[TestCase(int.MaxValue, int.MinValue, -0)]
		[TestCase(float.NegativeInfinity, float.PositiveInfinity, float.NaN)]
		[TestCase(float.MaxValue, float.MinValue, float.Epsilon)]
		[TestCase(-5.3f, 6.5f, 7.6f)]
		[TestCase(5.3f, 6.5f, 7.6f)]
		[TestCase(1, 3, -4)]
		[TestCase(-1, 3, 4)]
		[TestCase(1, 2, 3)]
		[TestCase(-0, 0, 0)]
		[TestCase(-5.3f, 6.5d, 7.6d)]
		[TestCase(5.3f, 6.5d, -7.6d)]
		[TestCase(1d, 3d, -4d)]
		[TestCase(-1, 3, 4)]
		[TestCase(1, 2, 3d)]
		[TestCase(0d, 0, 0)]
		public static void Test_Vector3_Generic_MultiplicationDot<TMathType>(TMathType a, TMathType b, TMathType c) //NUnit can infer best fit type
			where TMathType : struct
		{
			//arrange
			Vector3<TMathType> vec3One = new Vector3<TMathType>(a, b, c);
			Vector3<TMathType> vec3Two = new Vector3<TMathType>(c, a, b);
			List<TMathType> manualDotProduct = new List<TMathType>(3);

			//manually compute the dot product
			manualDotProduct.Add(Operator.Multiply(a, c));
			manualDotProduct.Add(Operator.Multiply(b, a));
			manualDotProduct.Add(Operator.Multiply(c, b));

			TMathType dotResult = AddAll(manualDotProduct);

			//act
			//tests * operator for dot mutiplication
			TMathType resultOne = vec3One * vec3Two;
			TMathType resultTwo = vec3Two * vec3One;

			//tests static method for dot multiplication.
			TMathType resultThree = Vector3<TMathType>.Dot(vec3One, vec3Two);

			//assert
			Assert.AreEqual(resultOne, resultTwo);
			Assert.AreEqual(dotResult, resultOne);
			Assert.AreEqual(resultThree, dotResult);
		}

		[Test(Author = "Andrew Blakely", Description = "Tests Vector3<TMathType> type multiplication against a scaler.", TestOf = typeof(Vector3<>))]
		[TestCase(double.NegativeInfinity, double.PositiveInfinity, double.NaN)]
		[TestCase(double.MaxValue, double.MinValue, double.Epsilon)]
		[TestCase(int.MaxValue, int.MinValue, -0)]
		[TestCase(float.NegativeInfinity, float.PositiveInfinity, float.NaN)]
		[TestCase(float.MaxValue, float.MinValue, float.Epsilon)]
		[TestCase(-5.3f, 6.5f, 7.6f)]
		[TestCase(5.3f, 6.5f, 7.6f)]
		[TestCase(1, 3, -4)]
		[TestCase(-1, 3, 4)]
		[TestCase(1, 2, 3)]
		[TestCase(-0, 0, 0)]
		[TestCase(-5.3f, 6.5d, 7.6d)]
		[TestCase(5.3f, 6.5d, -7.6d)]
		[TestCase(1d, 3d, -4d)]
		[TestCase(-1, 3, 4)]
		[TestCase(1, 2, 3d)]
		[TestCase(0d, 0, 0)]
		public static void Test_Vector3_Generic_Multiplcation_With_Scalar<TMathType>(TMathType a, TMathType b, TMathType c) //NUnit can infer best fit type
			where TMathType : struct
		{
			//arrange
			Vector3<TMathType> vec3 = new Vector3<TMathType>(a, b, c);
			List<TMathType> parameters = new List<TMathType>() { a, b, c };

			//act
			//test both overloads
			Vector3<TMathType> resultA = a * vec3;
			Vector3<TMathType> resultB = vec3 * b;
			Vector3<TMathType> resultC = c * vec3;

			List<Vector3<TMathType>> results = new List<Vector3<TMathType>>() { resultA, resultB, resultC };

			//assert
			//foreach result we need to manually check the math
			for (int i = 0; i < results.Count; i++)
				for (int index = 0; index < 3; index++)
					Assert.AreEqual(results[i][index], Operator.Multiply(parameters[i], vec3[index]));
		}

		//this is needed to verify that TMathType is being handled correctly.
		[Test(Author = "Andrew Blakely", Description = "Tests Vector<int> non-generic scalar multiplication.", TestOf = typeof(Vector3<>))]
		[TestCase(1, 3, -4, 0, 0, 0, 0)]
		[TestCase(-1, 3, 4, 5, -5, 15, 20)]
		[TestCase(1, 2, 3, 3, 3, 6, 9)]
		[TestCase(0, 0, 0, 3, 0, 0, 0)]
		public static void Test_Vector3_NonGeneric_Multiplication_With_Scalar(int a, int b, int c, int scale, int resultA, int resultB, int resultC)
		{
			//arrange
			Vector3<int> vec3 = new Vector3<int>(a, b, c);
			Vector3<int> expectedResult = new Vector3<int>(resultA, resultB, resultC);

			//act
			Vector3<int> result = scale * vec3;

			//assert
			Assert.AreEqual(result, expectedResult);
			Assert.AreEqual(result.x, scale * a);
			Assert.AreEqual(result.y, scale * b);
			Assert.AreEqual(result.z, scale * c);
		}

		[Test(Author = "Andrew Blakely", Description = "Tests Vector<TMathType> static direction vectors.", TestOf = typeof(Vector3<>))]
		//forward
		[TestCase(0, 0, 1, nameof(Vector3<int>.forward))] //int
		[TestCase(0f, 0f, 1f, nameof(Vector3<float>.forward))] //float
		[TestCase(0d, 0d, 1d, nameof(Vector3<double>.forward))] //double

		//back
		[TestCase(0, 0, -1, nameof(Vector3<int>.back))] //int
		[TestCase(0f, 0f, -1f, nameof(Vector3<float>.back))] //float
		[TestCase(0d, 0d, -1d, nameof(Vector3<double>.back))] //double

		//up
		[TestCase(0, 1, 0, nameof(Vector3<int>.up))] //int
		[TestCase(0f, 1f, 0f, nameof(Vector3<float>.up))] //float
		[TestCase(0d, 1d, 0d, nameof(Vector3<double>.up))] //double

		//down
		[TestCase(0, -1, 0, nameof(Vector3<int>.down))] //int
		[TestCase(0f, -1f, 0f, nameof(Vector3<float>.down))] //float
		[TestCase(0d, -1d, 0d, nameof(Vector3<double>.down))] //double

		//left
		[TestCase(-1, 0, 0, nameof(Vector3<int>.left))] //int
		[TestCase(-1f, 0f, 0f, nameof(Vector3<float>.left))] //float
		[TestCase(-1d, 0d, 0d, nameof(Vector3<double>.left))] //double
	
		//right
		[TestCase(1, 0, 0, nameof(Vector3<int>.right))] //int
		[TestCase(1f, 0f, 0f, nameof(Vector3<float>.right))] //float
		[TestCase(1d, 0d, 0d, nameof(Vector3<double>.right))] //double

		//one (not really a unit direction)
		[TestCase(1, 1, 1, nameof(Vector3<int>.one))] //int
		[TestCase(1f, 1f, 1f, nameof(Vector3<float>.one))] //float
		[TestCase(1d, 1d, 1d, nameof(Vector3<double>.one))] //double
		public static void Test_Vector3_Generic_Directions<TMathType>(TMathType a, TMathType b, TMathType c, string directionName) //pass in expected values
			where TMathType : struct
		{
			//arrange
			//grabs the property that corresponds to the string in the test case
			PropertyInfo vecDirectionProperty = typeof(Vector3<TMathType>).GetProperty(directionName, BindingFlags.Public | BindingFlags.Static
				| BindingFlags.GetProperty);

			if (vecDirectionProperty == null)
				Assert.Fail("Tried to find property named {0} but couldn't. {1} was null.", directionName, nameof(vecDirectionProperty));

			//act
			Vector3<TMathType> vecDirection = (Vector3<TMathType>)vecDirectionProperty.GetValue(null, null);

			//assert
			Assert.AreEqual(vecDirection, new Vector3<TMathType>(a, b, c));
		}

		/*[Test(Author = "Andrew Blakely", Description = "Tests Vector<TMathType> magnitude methods.", TestOf = typeof(Vector3<>))]
		public static void Test_Vector3_NonGeneric_Magnitude<TMathType>(TMathType a, TMathType b, TMathType c, TMathType expectedResult)
			where TMathType : struct
		{
			//arrange
			Vector3<TMathType> vec3 = new Vector3<TMathType>(a, b, c);

			TMathType result = vec3.sqrMagnitude;

			//System.Numerics

			//Assert
			//Assert.AreEqual(result, null, "Failed to compute {0} with {1}:{2}:{3}." + nameof(Vector3<TMathType>.sqrMagnitude), a, b, c);
		}*/

		private static TMathType AddAll<TMathType>(IEnumerable<TMathType> vals)
			where TMathType : struct
		{
			return vals.Aggregate(Operator<TMathType>.Zero, (a, b) => Operator.Add(a, b));
		}
	}
}
