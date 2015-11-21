using MiscUtil;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
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
	}
}
