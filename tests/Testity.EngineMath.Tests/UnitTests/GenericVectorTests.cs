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
		[Test(Author = "Andrew Blakely", Description = "Tests float type intialization of the generic Vector3.", TestOf = typeof(Vector3<float>) )]
		[TestCase(float.NegativeInfinity, float.PositiveInfinity, float.NaN)]
		[TestCase(float.MaxValue, float.MinValue, float.Epsilon)]
		[TestCase(-5.3f, 6.5f, 7.6f)]
		[TestCase(5.3f, 6.5f, 7.6f)]
		[TestCase(1, 3, -4)]
		[TestCase(-1, 3, 4)]
		[TestCase(1, 2, 3)]
		[TestCase(0, 0, 0)]
		public static void Test_Vector3_Float_Init(float a, float b, float c)
		{
			//arrange
			Vector3<float> vec3 = new Vector3<float>(a, b, c);

			//assert
			Assert.AreEqual(vec3.x, a, nameof(Vector3<float>) + " failed to initialize x value.");
			Assert.AreEqual(vec3.y, b, nameof(Vector3<float>) + " failed to initialize y value.");
			Assert.AreEqual(vec3.z, c, nameof(Vector3<float>) + " failed to initialize z value.");
		}

		[Test(Author = "Andrew Blakely", Description = "Tests double type intialization of the generic Vector3.", TestOf = typeof(Vector3<double>))]
		[TestCase(float.NegativeInfinity, float.PositiveInfinity, float.NaN)]
		[TestCase(float.MaxValue, float.MinValue, float.Epsilon)]
		[TestCase(-5.3f, 6.5f, 7.6f)]
		[TestCase(5.3f, 6.5f, 7.6f)]
		[TestCase(1, 3, -4)]
		[TestCase(-1, 3, 4)]
		[TestCase(1, 2, 3)]
		[TestCase(0, 0, 0)]
		[TestCase(-5.3f, 6.5d, 7.6d)]
		[TestCase(5.3f, 6.5d, 7.6d)]
		[TestCase(1d, 3d, -4d)]
		[TestCase(-1, 3, 4)]
		[TestCase(1, 2, 3d)]
		[TestCase(0d, 0, 0)]
		public static void Test_Vector3_Double_Init(double a, double b, double c)
		{
			//arrange
			Vector3<double> vec3 = new Vector3<double>(a, b, c);

			//assert
			Assert.AreEqual(vec3.x, a, nameof(Vector3<double>) + " failed to initialize x value.");
			Assert.AreEqual(vec3.y, b, nameof(Vector3<double>) + " failed to initialize y value.");
			Assert.AreEqual(vec3.z, c, nameof(Vector3<double>) + " failed to initialize z value.");
		}

		[Test(Author = "Andrew Blakely", Description = "Tests double type intialization of the generic Vector3.", TestOf = typeof(Vector3<double>))]
		[TestCase(int.MaxValue, int.MinValue, -0)]
		[TestCase(1, 3, -4)]
		[TestCase(-1, 3, 4)]
		[TestCase(1, 2, 3)]
		[TestCase(0, 0, 0)]
		[TestCase(1, 3, -4)]
		[TestCase(-1, 3, 4)]
		[TestCase(1, 2, 3)]
		[TestCase(0, 0, 0)]
		public static void Test_Vector3_Int_Init(int a, int b, int c)
		{
			//arrange
			Vector3<int> vec3 = new Vector3<int>(a, b, c);

			//assert
			Assert.AreEqual(vec3.x, a, nameof(Vector3<int>) + " failed to initialize x value.");
			Assert.AreEqual(vec3.y, b, nameof(Vector3<int>) + " failed to initialize y value.");
			Assert.AreEqual(vec3.z, c, nameof(Vector3<int>) + " failed to initialize z value.");
		}
	}
}
