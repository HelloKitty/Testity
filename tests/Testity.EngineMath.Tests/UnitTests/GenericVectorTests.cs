using MiscUtil;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
		[TestCase('a', '0', '&')]
		[TestCase(byte.MaxValue, byte.MinValue, (byte)50)]
		public static void Test_Vector3_Generic_Init<TMathType>(TMathType a, TMathType b, TMathType c) //NUnit can infer best fit type
			where TMathType : struct, IComparable<TMathType>, IEquatable<TMathType>
		{
			//arrange
			Vector3<TMathType> vec3 = new Vector3<TMathType>(a, b, c);
			Vector3<TMathType> vec3Two = new Vector3<TMathType>();
			Vector3<TMathType> vec3Three = new Vector3<TMathType>(a, b);
			Vector3<TMathType> vec3Four = new Vector3<TMathType>();
			vec3Two.Set(vec3.x, vec3.y, vec3.z);
			vec3Four.x = a;
			vec3Four.y = b;
			vec3Four.z = c;

			var vecCollection = new List<Vector3<TMathType>>() { vec3, vec3Two, vec3Four };

			//assert
			foreach(var v in vecCollection)
			{
				//these should all be equal
				foreach(var v2 in vecCollection)
					Assert.IsTrue(v == v2);

				Assert.AreEqual(v.x, a, nameof(Vector3<TMathType>) + " failed to initialize x value.");
				Assert.AreEqual(v.y, b, nameof(Vector3<TMathType>) + " failed to initialize y value.");
				Assert.AreEqual(v.z, c, nameof(Vector3<TMathType>) + " failed to initialize z value.");
			}

			//Make sure last one init to default value
			Assert.AreEqual(Operator<TMathType>.Zero, vec3Three.z);

			//Don't check z
			for (int i = 0; i < 2; i++)
				Assert.AreEqual(vec3[i], vec3Three[i]);
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
			where TMathType : struct, IComparable<TMathType>, IEquatable<TMathType>
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
			where TMathType : struct, IComparable<TMathType>, IEquatable<TMathType>
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
			where TMathType : struct, IComparable<TMathType>, IEquatable<TMathType>
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

		//zero
		[TestCase(0, 0, 0, nameof(Vector3<int>.zero))] //int
		[TestCase(0f, 0f, 0f, nameof(Vector3<float>.zero))] //float
		[TestCase(0d, 0d, 0d, nameof(Vector3<double>.zero))] //double
		[TestCase((char)0, (char)0, (char)0, nameof(Vector3<char>.zero))] //char
		[TestCase((byte)0, (byte)0, (byte)0, nameof(Vector3<byte>.zero))] //byte
		public static void Test_Vector3_Generic_Directions<TMathType>(TMathType a, TMathType b, TMathType c, string directionName) //pass in expected values
			where TMathType : struct, IComparable<TMathType>, IEquatable<TMathType>
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

		[Test(Author = "Andrew Blakely", Description = "Tests Vector<TMathType> magnitude methods.", TestOf = typeof(Vector3<>))]
		[TestCase(1,1,1,3)]
		[TestCase(2, 2, 2, 12)]
		[TestCase(2f, 2f, 2f, 12f)]
		[TestCase(2d, 2d, 2d, 12d)]
		[TestCase(1d, 2.5d, 6.7d, 1d + 2.5d * 2.5d + 6.7d * 6.7d)]
		[TestCase(-1d, -2.5d, -6.7d, 1d + 2.5d * 2.5d + 6.7d * 6.7d)]
		public static void Test_Vector3_Generic_Magnitude<TMathType>(TMathType a, TMathType b, TMathType c, TMathType expectedResult)
			where TMathType : struct, IComparable<TMathType>, IEquatable<TMathType>
		{
			//arrange
			Vector3<TMathType> vec3 = new Vector3<TMathType>(a, b, c);

			//act
			//test both static and instance computations
			TMathType result = vec3.SqrMagnitude;
			TMathType resultTwo = Vector3<TMathType>.SquarMagnitude(vec3);
			double resultSquared = vec3.Magnitude<double>();

			//assert
			Assert.IsTrue(result.Equals(resultTwo));
			Assert.AreEqual(result, expectedResult, "Failed to compute magnitude with {1}:{2}:{3}.", nameof(Vector3<TMathType>.SquarMagnitude), a, b, c);
			Assert.AreEqual(Math.Sqrt((double)Convert.ChangeType(expectedResult, typeof(double))), resultSquared, "Failed to compute magnitude with {1}:{2}:{3}.", nameof(Vector3<TMathType>.SquarMagnitude), a, b, c);
		}

		[Test(Author = "Andrew Blakely", Description = "Tests Vector<TMathType> equivalence methods.", TestOf = typeof(Vector3<>))]
		//test zeros
		[TestCase(0, 0, 0)]
		[TestCase(0f, 0f, 0f)]
		[TestCase(0d, 0d, 0d)]
		//positives
		[TestCase(1, 3, 5)]
		[TestCase(1.3f, 3.7f, 5.999f)]
		[TestCase(1.3d, 3.7d, 5.999d)]
		//negatives
		[TestCase(1, -3, 5)]
		[TestCase(-1.3f, 3.7f, 5.999f)]
		[TestCase(1.3d, 3.7d, -5.999d)]
		//odd cases
		[TestCase(int.MaxValue, int.MinValue, 0)]
		[TestCase(float.Epsilon, float.NaN, float.MaxValue)]
		[TestCase(double.MaxValue, double.Epsilon, double.MinValue)]
		public static void Test_Vector3_Generic_Equals<TMathType>(TMathType a, TMathType b, TMathType c)
			where TMathType : struct, IComparable<TMathType>, IEquatable<TMathType>
		{
			//arrange
			Vector3<TMathType> vec3One = new Vector3<TMathType>(a, b, c);
			Vector3<TMathType> vec3Two = vec3One;

			Assert.AreEqual(vec3One, vec3Two, 
				"Equivalence for {0} is not working for values {0}:{1}:{2}", nameof(Vector3<TMathType>), a, b, c);

			Assert.IsTrue(vec3Two == vec3Two);
			Assert.IsFalse(vec3Two != vec3Two);

            Assert.IsTrue(vec3Two == vec3One);
			Assert.IsFalse(vec3Two != vec3One);

			Assert.IsTrue(vec3Two.Equals(vec3One));
		}


		[Test(Author = "Andrew Blakely", Description = "Tests Vector<TMathType> equivalence methods when not equal.", TestOf = typeof(Vector3<>))]
		[TestCase(0,0,0 ,0,0,1)]
		[TestCase(0, 0, 0, 0, 0, -1)]
		[TestCase(1.5f, 1.3f, 1.5f, 1.2f, 3.6f, 0)]
		public static void Test_Vector3_Generic_Equals_When_Not_Equal<TMathType>(TMathType a, TMathType b, TMathType c, 
			TMathType d, TMathType e, TMathType f)
				where TMathType : struct, IComparable<TMathType>, IEquatable<TMathType>
		{
			//arrange
			Vector3<TMathType> vec3One = new Vector3<TMathType>(a, b, c);
			Vector3<TMathType> vec3Two = new Vector3<TMathType>(d, e, f);

			//assert
			Assert.IsFalse(vec3One == vec3Two);
			Assert.IsFalse(vec3Two == vec3One);
			Assert.IsTrue(vec3Two != vec3One);
			Assert.IsFalse(vec3Two.Equals(vec3One));
		}

		[Test(Author = "Andrew Blakely", Description = "Tests Vector<TMathType> negation methods.", TestOf = typeof(Vector3<>))]
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
		public static void Test_Vector3_Generic_Negate<TMathType>(TMathType a, TMathType b, TMathType c)
			where TMathType : struct, IComparable<TMathType>, IEquatable<TMathType>
		{
			//arrange
			Vector3<TMathType> vec3 = new Vector3<TMathType>(a, b, c);

			//acrt
			Vector3<TMathType> vec3Negated = -vec3;

			//assert
			for (int i = 0; i < 3; i++)
				Assert.AreEqual(Operator.Negate(vec3[i]), vec3Negated[i], "Negation for Type: {0} failed for values {1}:{2}:{3}.", nameof(Vector3<TMathType>), a, b, c);
		}

		//int vectors don't work. Can't normalize them.
		[Test(Author = "Andrew Blakely", Description = "Tests Vector<TMathType> normalization methods.", TestOf = typeof(Vector3<>))]
		[TestCase(1f,2f,3f, null)]
		[TestCase(1.005f, 5.6f, 2.4f, null)]
		[TestCase(1d, 2d, 3d, null)]
		[TestCase(1.33d, 2.5343d, 3.643d, null)]
		[TestCase(0.00000001f, 0.00000001f, 0.00000001f, null)]
		public static void Test_Vector3_Generic_Normalize<TMathType>(TMathType a, TMathType b, TMathType c, TMathType? optionalExpectedValue = null)
			where TMathType : struct, IComparable<TMathType>, IEquatable<TMathType>
		{
			//arrange
			Vector3<TMathType> vec3 = new Vector3<TMathType>(a, b, c);
			Vector3<TMathType> vec3UnNormalized = vec3;

			//act
			Vector3<TMathType> nVec3 = vec3.normalized;
			Vector3<TMathType> nVec3Two = Vector3<TMathType>.Normalize(vec3);
			vec3.Normalize();

			//assert
			Assert.AreEqual(nVec3, nVec3Two);
			Assert.AreEqual(nVec3, vec3);

			//Change for case: [TestCase(0.00000001f, 0.00000001f, 0.00000001f, null)] it can return 0 vector which has 0 magnitude.
			//We had to add an if to test small vector.
			if (Operator<TMathType>.GreaterThan(vec3UnNormalized.Magnitude<TMathType>(), Vector3<TMathType>.kEpsilon))
				Assert.AreEqual(Operator.Convert<TMathType, double>(Operator.AddAlternative(Operator<TMathType>.Zero, 1d)), Operator.Convert<TMathType, double>(nVec3.Magnitude()), Operator.Convert<TMathType, double>(Operator.Convert<double, TMathType>(1E-05f)));
			else
				Assert.AreEqual(Operator<TMathType>.Zero, nVec3.Magnitude<TMathType>()); //if the vector is too small we expect it to become the 0 vector.

			if (optionalExpectedValue.HasValue)
				for (int i = 0; i < 3; i++)
					Assert.AreEqual(Operator.Divide(vec3UnNormalized[i], vec3UnNormalized.Magnitude()), nVec3[i]);
        }

		[Test(Author = "Andrew Blakely", Description = "Tests Vector<TMathType> min method.", TestOf = typeof(Vector3<>))]
		//Don't expect these odd values to compare properly. They represent non-value value types essentially.
		//[TestCase(double.NegativeInfinity, double.PositiveInfinity, double.NaN)]
		//[TestCase(double.MaxValue, double.MinValue, double.Epsilon)]
		//[TestCase(int.MaxValue, int.MinValue, -0)]
		//[TestCase(float.NegativeInfinity, float.PositiveInfinity, float.NaN)]
		//[TestCase(float.MaxValue, float.MinValue, float.Epsilon)]
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
		[TestCase((byte)5, (byte)7, (byte)255)]
		[TestCase('a', '6', '&')]
		public static void Test_Vector3_Generic_Min<TMathType>(TMathType a, TMathType b, TMathType c)
			where TMathType : struct, IComparable<TMathType>, IEquatable<TMathType>
		{

			//arrange
			Vector3<TMathType> vec3One = new Vector3<TMathType>(a, b, c);
			Vector3<TMathType> vec3Two = new Vector3<TMathType>(b, c, a);

			//act
			Vector3<TMathType> minVec3 = Vector3<TMathType>.Min(vec3One, vec3Two);
			//must cast back to TMathType or char fails as it is like a ushort or something.
			Vector3<TMathType> minVec3Manual = new Vector3<TMathType>((TMathType)(dynamic)Math.Min((dynamic)vec3One.x, (dynamic)vec3Two.x),
				(TMathType)(dynamic)Math.Min((dynamic)vec3One.y, (dynamic)vec3Two.y), (TMathType)(dynamic)Math.Min((dynamic)vec3One.z, (dynamic)vec3Two.z));

			//Assert
			//use dynamic DLR to test this without depending on anything else.
			for (int index = 0; index < 3; index++)
				Assert.AreEqual((dynamic)Math.Min((dynamic)vec3One[index], (dynamic)vec3Two[index]), minVec3[index]);
			Assert.AreEqual(minVec3Manual, minVec3);

			//check if equal values first
			for (int index = 0; index < 3; index++)
				if (!Operator.Equal(vec3One[index], vec3Two[index]))
					Assert.AreNotEqual((dynamic)Math.Max((dynamic)vec3One[index], (dynamic)vec3Two[index]), minVec3[index]); //check that the max value isn't equal to the min.x
		}

		[Test(Author = "Andrew Blakely", Description = "Tests Vector<TMathType> max method.", TestOf = typeof(Vector3<>))]
		//Don't expect these odd values to compare properly. They represent non-value value types essentially.
		//[TestCase(double.NegativeInfinity, double.PositiveInfinity, double.NaN)]
		//[TestCase(double.MaxValue, double.MinValue, double.Epsilon)]
		//[TestCase(int.MaxValue, int.MinValue, -0)]
		//[TestCase(float.NegativeInfinity, float.PositiveInfinity, float.NaN)]
		//[TestCase(float.MaxValue, float.MinValue, float.Epsilon)]
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
		[TestCase((byte)5, (byte)7, (byte)255)]
		[TestCase('a', '6', '&')]
		public static void Test_Vector3_Generic_Max<TMathType>(TMathType a, TMathType b, TMathType c)
			where TMathType : struct, IComparable<TMathType>, IEquatable<TMathType>
		{

			//arrange
			Vector3<TMathType> vec3One = new Vector3<TMathType>(a, b, c);
			Vector3<TMathType> vec3Two = new Vector3<TMathType>(b, c, a);

			//act
			Vector3<TMathType> maxVec3 = Vector3<TMathType>.Max(vec3One, vec3Two);
			//must cast back to TMathType or char fails as it is like a ushort or something.
			Vector3<TMathType> maxVec3Manual = new Vector3<TMathType>((TMathType)(dynamic)Math.Max((dynamic)vec3One.x, (dynamic)vec3Two.x),
				(TMathType)(dynamic)Math.Max((dynamic)vec3One.y, (dynamic)vec3Two.y), (TMathType)(dynamic)Math.Max((dynamic)vec3One.z, (dynamic)vec3Two.z));

			//Assert
			//use dynamic DLR to test this without depending on anything else.
			for(int index = 0; index < 3; index++)
				Assert.AreEqual((dynamic)Math.Max((dynamic)vec3One[index], (dynamic)vec3Two[index]), maxVec3[index]);

			Assert.AreEqual(maxVec3Manual, maxVec3);

			//check if equal values first
			for(int index = 0; index < 3; index++)
				if (!Operator.Equal(vec3One[index], vec3Two[index]))
					Assert.AreNotEqual((dynamic)Math.Min((dynamic)vec3One[index], (dynamic)vec3Two[index]), maxVec3[index]); //check that the max value isn't equal to the min.x
		}

		private static TMathType AddAll<TMathType>(IEnumerable<TMathType> vals)
			where TMathType : struct, IComparable<TMathType>, IEquatable<TMathType>
		{
			return vals.Aggregate(Operator<TMathType>.Zero, (a, b) => Operator.Add(a, b));
		}
	}
}
