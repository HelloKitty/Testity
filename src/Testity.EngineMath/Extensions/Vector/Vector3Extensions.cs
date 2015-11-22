using MiscUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testity.EngineMath
{
	public static class Vector3Extensions
	{
		//This class mostly exists because users can't expect all types of Vector3<T> to offers certain
		//functionality. The best way to seperate it into valid types is to use extension methods

		#region Normalization
		private static Vector3<TMathType> NormalizeInternalGeneric<TMathType>(Vector3<TMathType> value)
			where TMathType : struct, IEquatable<TMathType>, IComparable<TMathType>
		{
			//This is valid because users can't expect to normalize a vector of ints and such.
			//If they're Type permits they can normalize.
			TMathType single = Vector3<TMathType>.Magnitude<TMathType>(value);

			if (Operator.LessThanOrEqual(single, Vector3<TMathType>.kEpsilon))
			{
				return Vector3<TMathType>.zero;
			}

			return value * (Operator.Convert<double, TMathType>(Operator.DivideAlternative(1d, single)));
		}
		/// <summary>
		///   <para>Normalizes the vector to the best precision allowed by <see cref="Vector3{Double}"/>.</para>
		/// Only some Vector types have valid normalizations
		/// </summary>
		public static Vector3<double> Normalized(this Vector3<double> vec)
		{
			return NormalizeInternalGeneric(vec);
			/*double single = vec.Magnitude();

			if (single <= Vector3<double>.kEpsilon)
				return Vector3<double>.zero;
			else
				return vec * (1 / single);*/
		}

		/// <summary>
		///   <para>Normalizes the vector to the best precision allowed by <see cref="Vector3{float}"/>.</para>
		/// Only some Vector types have valid normalizations
		/// /// </summary>
		public static Vector3<float> Normalized(this Vector3<float> vec)
		{
			return NormalizeInternalGeneric(vec);
			/*float single = vec.Magnitude();

			if (single <= Vector3<float>.kEpsilon)
				return Vector3<float>.zero;
			else
				return vec * (1 / single);*/
		}
		#endregion

		#region Double ClampMagnitude
		/// <summary>
		///   <para>Returns a copy of vector with its magnitude clamped to maxLength.</para>
		/// </summary>
		/// <param name="vector">Vector whose magnitude should be clamped.</param>
		/// <param name="maxLength">Value to clamp by.</param>
		public static Vector3<double> ClampMagnitude(this Vector3<double> vector, double maxLength)
		{
			//TODO: Check if this type will provide value results.
			if (vector.SqrMagnitude <= (maxLength * maxLength))
			{
				return vector;
			}

			return vector.Normalized() * maxLength;
		}

		/// <summary>
		///   <para>Returns a copy of vector with its magnitude clamped to maxLength.</para>
		/// </summary>
		/// <param name="vector">Vector whose magnitude should be clamped.</param>
		/// <param name="maxLength">Value to clamp by.</param>
		public static Vector3<double> ClampMagnitude(this Vector3<double> vector, float maxLength)
		{
			//TODO: Check if this type will provide value results.
			if (vector.SqrMagnitude <= (maxLength * maxLength))
			{
				return vector;
			}

			return vector.Normalized() * maxLength;
		}
		#endregion

		#region Float ClampMagnitude
		/// <summary>
		///   <para>Returns a copy of vector with its magnitude clamped to maxLength.</para>
		/// </summary>
		/// <param name="vector">Vector whose magnitude should be clamped.</param>
		/// <param name="maxLength">Value to clamp by.</param>
		public static Vector3<float> ClampMagnitude(this Vector3<float> vector, double maxLength)
		{
			//TODO: Check if this type will provide value results.
			if (vector.SqrMagnitude <= (maxLength * maxLength))
			{
				return vector;
			}

			return vector.Normalized() * (float)maxLength;
		}

		/// <summary>
		///   <para>Returns a copy of vector with its magnitude clamped to maxLength.</para>
		/// </summary>
		/// <param name="vector">Vector whose magnitude should be clamped.</param>
		/// <param name="maxLength">Value to clamp by.</param>
		public static Vector3<float> ClampMagnitude(this Vector3<float> vector, float maxLength)
		{
			//TODO: Check if this type will provide value results.
			if (vector.SqrMagnitude <= (maxLength * maxLength))
			{
				return vector;
			}

			return vector.Normalized() * maxLength;
		}
		#endregion

		#region Projections
		#region Generic Projections
		private static Vector3<TMathType> ProjectInternalGeneric<TMathType>(Vector3<TMathType> vector, Vector3<TMathType> onNormal)
			where TMathType : struct, IEquatable<TMathType>, IComparable<TMathType>
		{
			TMathType single = Vector3<TMathType>.Dot(onNormal, onNormal);

			if (Operator.LessThanOrEqual(single, Vector3<TMathType>.kEpsilon))
			{
				return Vector3<TMathType>.zero;
			}

			return (onNormal * Vector3<TMathType>.Dot(vector, onNormal)) * (Operator.Convert<TMathType, TMathType>(Operator.Convert<double, TMathType>(Operator.DivideAlternative(1d, single))));
		}

		private static Vector3<TMathType> ProjectOnPlaneInternalGeneric<TMathType>(Vector3<TMathType> vector, Vector3<TMathType> planeNormal)
			where TMathType : struct, IEquatable<TMathType>, IComparable<TMathType>
		{
			return vector - ProjectInternalGeneric(vector, planeNormal);
		}
		#endregion

		#region Double Projections
		/// <summary>
		///   <para>Projects a vector onto another vector.</para>
		/// </summary>
		/// <param name="vector"></param>
		/// <param name="onNormal"></param>
		public static Vector3<double> Project(this Vector3<double> vector, Vector3<double> onNormal)
		{
			return ProjectInternalGeneric(vector, onNormal);
			//{1, 3, -4} - (({5.0f, 2, -5.43333f} * ({1, 3, -4} * {5.0f, 2, -5.43333f})) / 
			/*double single = Vector3<double>.Dot(onNormal, onNormal);

			if (single <= Vector3<double>.kEpsilon)
			{
				return Vector3<double>.zero;
			}

			return (onNormal * Vector3<double>.Dot(vector, onNormal)) * (1d / single);*/
		}

		/// <summary>
		///   <para>Projects a vector onto a plane defined by a normal orthogonal to the plane.</para>
		/// </summary>
		/// <param name="vector"></param>
		/// <param name="planeNormal"></param>
		public static Vector3<double> ProjectOnPlane(this Vector3<double> vector, Vector3<double> planeNormal)
		{
			return ProjectOnPlaneInternalGeneric(vector, planeNormal);
			//return vector - vector.Project(planeNormal);
		}
		#endregion

		#region Float Projections
		/// <summary>
		///   <para>Projects a vector onto another vector.</para>
		/// </summary>
		/// <param name="vector"></param>
		/// <param name="onNormal"></param>
		public static Vector3<float> Project(this Vector3<float> vector, Vector3<float> onNormal)
		{
			return ProjectInternalGeneric(vector, onNormal);
			/*float single = Vector3<float>.Dot(onNormal, onNormal);

			if (single < Vector3<float>.kEpsilon)
			{
				return Vector3<float>.zero;
			}

			//Vector3<float> firstTerm = (onNormal * Vector3<float>.Dot(vector, onNormal));

			//return new Vector3<float>(firstTerm.x / single, firstTerm.y / single, firstTerm.z / single);
			return (onNormal * Vector3<float>.Dot(vector, onNormal)) * (1.0f / single);*/
		}

		/// <summary>
		///   <para>Projects a vector onto a plane defined by a normal orthogonal to the plane.</para>
		/// </summary>
		/// <param name="vector"></param>
		/// <param name="planeNormal"></param>
		public static Vector3<float> ProjectOnPlane(this Vector3<float> vector, Vector3<float> planeNormal)
		{
			return ProjectOnPlaneInternalGeneric(vector, planeNormal);
			//return vector - vector.Project(planeNormal);
		}
		#endregion
		#endregion
	}
}
