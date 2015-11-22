using MiscUtil;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testity.EngineMath.Unity3D.AcceptanceTests
{
	[TestFixture]
	public static class GenericVector3AgainstUnity
	{
		//This is the final test for generic vector. Various methods previously tested will be tested against the Vector3 Unity3D implementation
		//For details on this class look here: http://docs.unity3d.com/ScriptReference/Vector3.html
		//Additionally this will test untested methods that are difficult to pre-compute or setup
		//Might have to use a net35 project to test: http://stackoverflow.com/questions/8795550/casting-a-result-to-float-in-method-returning-float-changes-result

		[Test(Author = "Andrew Blakely", Description = "Tests" + nameof(Vector3<float>) + " project methods using Unity3D as a reference.", TestOf = typeof(Vector3<>))]
		[TestCase(-5.3f, 6.5f, 7.6f, 0.0000235f, 5.654543f, 10.1234234f)]
		[TestCase(5.3f, 6.5f, 7.6f, 6.4f, 67.55f, 8.09f)]
		[TestCase(1, 3, -4, 5.0f, 2, -5.43333f)] //won't be equal but wolfram, Unity and EngineMath all give very close results: http://www.wolframalpha.com/input/?i=SetPrecision%5B%7B1%2C+3%2C+-4%7D+-+%28%28+%28%7B1%2C+3%2C+-4%7D+.+%7B5%2C+2%2C+-5.43333%7D%29+*%7B5%2C+2%2C+-5.43333%7D%29+%2F+%28%7B5%2C+2%2C+-5.43333%7D+.+%7B5%2C+2%2C+-5.43333%7D%29%29%2C+40%5D
		[TestCase(1, 3, -4, 5.000005340001f, 2, -5.43333333f)]
		[TestCase(-1, 3, 4, 0, 0, 0)]
		[TestCase(1, 2, 3, 6.4f, 67.55f, 8.095454f)]
		[TestCase(-0, 0, 0, -5, -100.04332312f, -10.3333f)]
		[TestCase(-5.3f, 6.5f, 7.6f, 6.4f, 67.55f, 8.09f)]
		[TestCase(5.3f, 6.5f, -7.6f, 55, 56, 105.0522f)]
		[TestCase(1f, 3f, -4f, 7, 6, 0)]
		[TestCase(1, 2, 3, 6.4f, 67.55000054f, 8.0900001f)]
		[TestCase(1, 2, 3, 6.4f, 67.55000054f, 8.0900001f)]
		[TestCase(0, 0, 0, 0, 0, 0)]
		public static void Test_Vector3_Generic_Project_Methods_Against_Unity3D(float a, float b, float c, float d, float e, float f)
		{
			//arrange
			Vector3<float> genericVec3 = new Vector3<float>(a, b, c);
			UnityEngine.Vector3 unityVec3 = new UnityEngine.Vector3(a, b, c);

			Vector3<float> genericProjection = new Vector3<float>(d, e, f);
			UnityEngine.Vector3 unityProjection = new UnityEngine.Vector3(d, e, f);

			//act
			Vector3<float> genericProjected = genericVec3.ProjectOnPlane(genericProjection);
			UnityEngine.Vector3 unityProjected = UnityEngine.Vector3.ProjectOnPlane(unityVec3, unityProjection);

			//For testing higher precision
			Vector3<double> doubleTestVec3 = new Vector3<double>(a, b, c);
			Vector3<double> doubleProjection = new Vector3<double>(d, e, f);
			Vector3<double> doubleProjected = doubleTestVec3.ProjectOnPlane(doubleProjection);

			//assert
			for (int index = 0; index < 3; index++)
				Assert.AreEqual(unityProjected[index], genericProjected[index], Vector3<double>.kEpsilon, "Index: {0} was not equivalent. High precision computation was {1}.", index, doubleProjected[index]);
		}


		[Test(Author = "Andrew Blakely", Description = "Tests" + nameof(Vector3<float>) + " clamp magnitude using Unity3D as a reference.", TestOf = typeof(Vector3<>))]
		[TestCase(0.0000235f, 5.654543f, 10.1234234f, 64)]
		[TestCase(5.3f, 6.5f, 7.6f, 8.09f)]
		[TestCase(1, 3, -4, 5.0f)]
		[TestCase(1, 3, -4, 5.000005340001f)]
		[TestCase(-1, 3, 4, 0)]
		[TestCase(1, 2, 3, 6.4f)]
		[TestCase(-0, 0, 0, -5)]
		[TestCase(-5.3f, 6.5f, 7.6f, 6.4f)]
		[TestCase(5.3f, 6.5f, 55, 56)]
		[TestCase(1f, 3f, -4f, 70)]
		[TestCase(1, 2, 3, 6.4f)]
		[TestCase(0, 0, 0, 0)]
		public static void Test_Vector3_Generic_ClampMagnitude_Methods_Against_Unity3D(float a, float b, float c, float clamp)
		{
			//arrange
			Vector3<float> genericVec3 = new Vector3<float>(a, b, c);
			UnityEngine.Vector3 unityVec3 = new UnityEngine.Vector3(a, b, c);

			//act
			Vector3<float> genericVec3Clamped = genericVec3.ClampMagnitude(clamp);
			UnityEngine.Vector3 unityVec3Clamped = UnityEngine.Vector3.ClampMagnitude(unityVec3, clamp);

			//assert
			for (int index = 0; index < 3; index++)
				Assert.AreEqual(unityVec3Clamped[index], genericVec3Clamped[index], UnityEngine.Vector3.kEpsilon, "Index: {0} was incorrect.", index);
		}


		[Test(Author = "Andrew Blakely", Description = "Tests" + nameof(Vector3<float>) + " cross product methods using Unity3D..", TestOf = typeof(Vector3<>))]
		[TestCase(-5.3f, 6.5f, 7.6f, 0.0000235f, 5.654543f, 10.1234234f)]
		[TestCase(5.3f, 6.5f, 7.6f, 6.4f, 67.55f, 8.09f)] //fails
		[TestCase(1, 3, -4, 5.0f, 2, -5.43333f)]
		[TestCase(1, 3, -4, 5.000005340001f, 2, -5.43333333f)]
		[TestCase(-1, 3, 4, 0, 0, 0)]
		[TestCase(1, 2, 3, 6.4f, 67.55f, 8.095454f)]
		[TestCase(-0, 0, 0, -5, -100.04332312f, -10.3333f)]
		[TestCase(-5.3f, 6.5f, 7.6f, 6.4f, 67.55f, 8.09f)] 
		[TestCase(5.3f, 6.5f, -7.6f, 55, 56, 105.0522f)]
		[TestCase(1f, 3f, -4f, 7, 6, 0)]
		[TestCase(1, 2, 3, 6.4f, 67.55000054f, 8.0900001f)]
		[TestCase(1, 2, 3, 6.4f, 67.55000054f, 8.0900001f)]
		[TestCase(0, 0, 0, 0, 0, 0)]
		public static void Test_Vector3_Generic_Cross_Product_Against_Unity3D(float a, float b, float c, float d, float e, float f)
		{
			//arrange
			Vector3<float> genericVec3One = new Vector3<float>(a, b, c);
			Vector3<float> genericVec3Two = new Vector3<float>(d, e, f);

			UnityEngine.Vector3 unityVec3One = new UnityEngine.Vector3(a, b, c);
			UnityEngine.Vector3 unityVec3Two = new UnityEngine.Vector3(d, e, f);

			//act
			Vector3<float> genericVec3Cross = genericVec3One.Cross(genericVec3Two);
			UnityEngine.Vector3 unityVec3Cross = UnityEngine.Vector3.Cross(unityVec3One, unityVec3Two);

			Vector3<float> manualCross = new Vector3<float>(genericVec3One.y * genericVec3Two.z - genericVec3One.z * genericVec3Two.y,
				genericVec3One.z * genericVec3Two.x - genericVec3One.x * genericVec3Two.z, genericVec3One.x * genericVec3Two.y - genericVec3One.y * genericVec3Two.x);

			double termOne = Operator<double>.Multiply((double)genericVec3One.y, (double)genericVec3Two.z);
			double termTwo = Operator<double>.Multiply((double)genericVec3One.z, (double)genericVec3Two.y);

			double termThree = Operator<double>.Multiply((double)genericVec3One.z, (double)genericVec3Two.x);
			double termFour = Operator<double>.Multiply((double)genericVec3One.x, (double)genericVec3Two.z);

			double termFive = Operator<double>.Multiply((double)genericVec3One.x, (double)genericVec3Two.y);
			double termSix = Operator<double>.Multiply((double)genericVec3One.y, (double)genericVec3Two.x);

			double newX = Operator<double>.Subtract((double)termOne, (double)termTwo);
			double newY = Operator<double>.Subtract((double)termThree, (double)termFour);
			double newZ = Operator<double>.Subtract((double)termFive, (double)termSix);

			Vector3<float> operatorGenericCross = new Vector3<float>((float)newX, (float)newY, (float)newZ);

			Assert.AreEqual(Operator<float>.Multiply(0.000000001005f, 0.00000443200050001f), 0.000000001005f * 0.00000443200050001f);

			//assert
			for (int i = 0; i < 3; i++) //we have to accept a higher delta of error since cross product has so much float math.
			{
				Assert.AreEqual(operatorGenericCross[i], manualCross[i]);
				Assert.AreEqual(manualCross[i], unityVec3Cross[i]);
				Assert.AreEqual(unityVec3Cross[i], genericVec3Cross[i], UnityEngine.Vector3.kEpsilon * 5, "Index: {0} failed. Failed to compute cross product.", i);
			}
				
		}
	}
}
