﻿using MiscUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testity.EngineMath;

namespace Testity.EngineMath
{
	//For information on how and why this is efficient see: http://stackoverflow.com/questions/1348594/is-there-a-c-sharp-generic-constraint-for-real-number-types
	//A library by the legendary Jon Skeet and Marc Gravell cannot be dismissed. It works.
	/// <summary>
	/// Generic vector type with 3 components. Allows for use of int, float and double types.
	/// Based on Unity3D's: http://docs.unity3d.com/ScriptReference/Vector3.html
	/// </summary>
	/// <typeparam name="TMathType">Value type of the vector that must overload math operators (Ex. int, float, double).</typeparam>
	public struct Vector3<TMathType>
		where TMathType : struct
    {
		public static TMathType kEpsilon = (TMathType)Convert.ChangeType(1E-05f, typeof(TMathType));
		private static TMathType validCompareError = (TMathType)Convert.ChangeType(9.99999944E-11f, typeof(TMathType));

		/// <summary>
		///   <para>X component of the vector.</para>
		/// </summary>
		public TMathType x;

		/// <summary>
		///   <para>Y component of the vector.</para>
		/// </summary>
		public TMathType y;

		/// <summary>
		///   <para>Z component of the vector.</para>
		/// </summary>
		public TMathType z;

		/// <summary>
		/// A cache of the <see cref="TMathType"/> value that represents 1.
		/// </summary>
		private static TMathType OneValue = (TMathType)Convert.ChangeType(1, typeof(TMathType));

		/// <summary>
		///   <para>Shorthand for writing Vector3<TMathType>(0, 0, -1).</para>
		/// </summary>
		public static Vector3<TMathType> back
		{
			get
			{
				return new Vector3<TMathType>(Operator<TMathType>.Zero, Operator<TMathType>.Zero, 
					Operator.Subtract(Operator<TMathType>.Zero, Vector3<TMathType>.OneValue));
			}
		}

		/// <summary>
		///   <para>Shorthand for writing Vector3<TMathType>(0, -1, 0).</para>
		/// </summary>
		public static Vector3<TMathType> down
		{
			get
			{
				return new Vector3<TMathType>(Operator<TMathType>.Zero, Operator.Subtract(Operator<TMathType>.Zero, Vector3<TMathType>.OneValue),
					Operator<TMathType>.Zero);
			}
		}

		/// <summary>
		///   <para>Shorthand for writing Vector3<TMathType>(0, 0, 1).</para>
		/// </summary>
		public static Vector3<TMathType> forward
		{
			get
			{
				return new Vector3<TMathType>(Operator<TMathType>.Zero, Operator<TMathType>.Zero, Vector3<TMathType>.OneValue);
			}
		}

		/// <summary>
		///   <para>Shorthand for writing Vector3<TMathType>(-1, 0, 0).</para>
		/// </summary>
		public static Vector3<TMathType> left
		{
			get
			{
				return new Vector3<TMathType>(Operator.Subtract(Operator<TMathType>.Zero, Vector3<TMathType>.OneValue), Operator<TMathType>.Zero,
					Operator<TMathType>.Zero);
			}
		}

		/// <summary>
		///   <para>Shorthand for writing Vector3<TMathType>(1, 1, 1).</para>
		/// </summary>
		public static Vector3<TMathType> one
		{
			get
			{
				return new Vector3<TMathType>(Vector3<TMathType>.OneValue, Vector3<TMathType>.OneValue, Vector3<TMathType>.OneValue);
			}
		}

		/// <summary>
		///   <para>Shorthand for writing Vector3<TMathType>(1, 0, 0).</para>
		/// </summary>
		public static Vector3<TMathType> right
		{
			get
			{
				return new Vector3<TMathType>(Vector3<TMathType>.OneValue, Vector3<TMathType>.OneValue, Vector3<TMathType>.OneValue);

			}
		}

		/// <summary>
		///   <para>Shorthand for writing Vector3<TMathType>(0, 1, 0).</para>
		/// </summary>
		public static Vector3<TMathType> up
		{
			get
			{
				return new Vector3<TMathType>(Operator<TMathType>.Zero, Vector3<TMathType>.OneValue, Operator<TMathType>.Zero);
			}
		}

		/// <summary>
		///   <para>Shorthand for writing Vector3<TMathType>(0, 0, 0).</para>
		/// </summary>
		public static Vector3<TMathType> zero
		{
			get
			{
				return new Vector3<TMathType>(Operator<TMathType>.Zero, Operator<TMathType>.Zero, Operator<TMathType>.Zero);
			}
		}

		public TMathType this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						{
							return this.x;
						}
					case 1:
						{
							return this.y;
						}
					case 2:
						{
							return this.z;
						}
				}
				throw new IndexOutOfRangeException("Invalid " + nameof(Vector3<TMathType>) + " index!");
			}
			set
			{
				switch (index)
				{
					case 0:
						{
							this.x = value;
							break;
						}
					case 1:
						{
							this.y = value;
							break;
						}
					case 2:
						{
							this.z = value;
							break;
						}
					default:
						{
							throw new IndexOutOfRangeException("Invalid " + nameof(Vector3<TMathType>) + " index!");
						}
				}
			}
		}

		/// <summary>
		///   <para>Returns the length of this vector (Read Only).</para>
		/// </summary>
		public TMathType magnitude
		{
			get
			{
				//Hopefully this gets inlined to reduce copying.
				return MathT.Sqrt(sqrMagnitude);
			}
		}

		/// <summary>
		///   <para>Returns this vector with a magnitude of 1 (Read Only).</para>
		/// </summary>
		public Vector3<TMathType> normalized
		{
			get
			{
				return Vector3<TMathType>.Normalize(this);
			}
		}

		/// <summary>
		///   <para>Returns the squared length of this vector (Read Only).</para>
		/// </summary>
		public TMathType sqrMagnitude
		{
			get
			{
				TMathType firstTerm = Operator.Add(Operator.Multiply(this.x, this.x),  Operator.Multiply(this.y, this.y));
				return Operator.Add(firstTerm, Operator.Multiply(this.z, this.z));
			}
		}

		/// <summary>
		///   <para>Creates a new vector with given x, y, z components.</para>
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public Vector3(TMathType x, TMathType y, TMathType z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		/// <summary>
		///   <para>Creates a new vector with given x, y components and sets z to zero.</para>
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public Vector3(TMathType x, TMathType y)
			: this(x, y, Operator<TMathType>.Zero)
		{

        }

		/// <summary>
		///   <para>Returns a copy of vector with its magnitude clamped to maxLength.</para>
		/// </summary>
		/// <param name="vector"></param>
		/// <param name="maxLength"></param>
		public static Vector3<TMathType> ClampMagnitude(Vector3<TMathType> vector, TMathType maxLength)
		{
			if (Operator.LessThanOrEqual(vector.sqrMagnitude, Operator.Multiply(maxLength, maxLength)))
			{
				return vector;
			}

			return vector.normalized * maxLength;
		}

		/// <summary>
		///   <para>Cross Product of two vectors.</para>
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		public static Vector3<TMathType> Cross(Vector3<TMathType> lhs, Vector3<TMathType> rhs)
		{
			TMathType newX = Operator.Subtract(Operator.Multiply(lhs.y, rhs.z), Operator.Multiply(lhs.z, rhs.y));
			TMathType newY = Operator.Subtract(Operator.Multiply(lhs.z, rhs.x), Operator.Multiply(lhs.x, rhs.z));
			TMathType newZ = Operator.Subtract(Operator.Multiply(lhs.x, rhs.y), Operator.Multiply(lhs.y, rhs.x));

			return new Vector3<TMathType>(newX, newY, newZ);
		}

		/// <summary>
		///   <para>Returns the distance between a and b.</para>
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		public static TMathType Distance(Vector3<TMathType> a, Vector3<TMathType> b)
		{
			Vector3<TMathType> vec3 = new Vector3<TMathType>(Operator.Subtract(a.x, b.x), Operator.Subtract(a.x, b.x), Operator.Subtract(a.z, b.z));
			return vec3.magnitude;
		}

		/// <summary>
		///   <para>Dot Product of two vectors.</para>
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		public static TMathType Dot(Vector3<TMathType> lhs, Vector3<TMathType> rhs)
		{
			TMathType firstTerm = Operator.Add(Operator.Multiply(lhs.x, rhs.x), Operator.Multiply(lhs.y, rhs.y));
			return Operator.Add(firstTerm, Operator.Multiply(lhs.z, rhs.z));
		}

		public override bool Equals(object other)
		{
			if (!(other is Vector3<TMathType>))
			{
				return false;
			}

			Vector3<TMathType> Vector3 = (Vector3<TMathType>)other;
			return (!this.x.Equals(Vector3.x) || !this.y.Equals(Vector3.y) ? false : this.z.Equals(Vector3.z));
		}

		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2;
		}

		public static TMathType Magnitude(Vector3<TMathType> a)
		{
			return a.magnitude;
		}

		/// <summary>
		///   <para>Returns a vector that is made from the largest components of two vectors.</para>
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		public static Vector3<TMathType> Max(Vector3<TMathType> lhs, Vector3<TMathType> rhs)
		{
			return new Vector3<TMathType>(MathT.Max(lhs.x, rhs.x), MathT.Max(lhs.y, rhs.y), MathT.Max(lhs.z, rhs.z));
		}

		/// <summary>
		///   <para>Returns a vector that is made from the smallest components of two vectors.</para>
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		public static Vector3<TMathType> Min(Vector3<TMathType> lhs, Vector3<TMathType> rhs)
		{
			return new Vector3<TMathType>(MathT.Min(lhs.x, rhs.x), MathT.Min(lhs.y, rhs.y), MathT.Min(lhs.z, rhs.z));
		}

		/// <summary>
		///   <para></para>
		/// </summary>
		/// <param name="value"></param>
		public static Vector3<TMathType> Normalize(Vector3<TMathType> value)
		{
			TMathType single = Vector3<TMathType>.Magnitude(value);

			if (Operator.LessThanOrEqual(single, kEpsilon))
			{
				return Vector3<TMathType>.zero;
			}

			return value / single;
		}

		/// <summary>
		///   <para>Makes this vector have a magnitude of 1.</para>
		/// </summary>
		public void Normalize()
		{
			TMathType single = Vector3<TMathType>.Magnitude(this);

			if (Operator.LessThanOrEqual(single, kEpsilon))
			{
				this = Vector3<TMathType>.zero;
			}
			else
			{
				this = this / single;
			}
		}

		#region Operator Overloads
		public static Vector3<TMathType> operator +(Vector3<TMathType> a, Vector3<TMathType> b)
		{
			return new Vector3<TMathType>(Operator.Add(a.x, b.x), Operator.Add(a.y, b.y), 
				Operator.Add(a.z, b.z));
		}

		public static Vector3<TMathType> operator /(Vector3<TMathType> a, TMathType d)
		{
			return new Vector3<TMathType>(Operator.Divide(a.x, d), Operator.Divide(a.y, d), Operator.Divide(a.z, d));
		}

		public static bool operator ==(Vector3<TMathType> lhs, Vector3<TMathType> rhs)
		{
			return Operator.LessThan(Vector3<TMathType>.SqrMagnitude(lhs - rhs), Vector3<TMathType>.validCompareError);
		}

		public static bool operator !=(Vector3<TMathType> lhs, Vector3<TMathType> rhs)
		{
			return Operator.GreaterThanOrEqual(Vector3<TMathType>.SqrMagnitude(lhs - rhs), Vector3<TMathType>.validCompareError);
		}

		public static Vector3<TMathType> operator *(Vector3<TMathType> a, TMathType d)
		{
			return new Vector3<TMathType>(Operator.Multiply(a.x, d), Operator.Multiply(a.y, d), Operator.Multiply(a.z, d));
		}

		public static Vector3<TMathType> operator *(TMathType d, Vector3<TMathType> a)
		{
			return new Vector3<TMathType>(Operator.Multiply(a.x, d), Operator.Multiply(a.y, d), Operator.Multiply(a.z, d));
		}

		public static Vector3<TMathType> operator -(Vector3<TMathType> a, Vector3<TMathType> b)
		{
			return new Vector3<TMathType>(Operator.Subtract(a.x, b.x), Operator.Subtract(a.y, b.y), Operator.Subtract(a.z, b.z));
		}

		public static Vector3<TMathType> operator -(Vector3<TMathType> a)
		{
			return new Vector3<TMathType>(Operator.Negate(a.x), Operator.Negate(a.y), Operator.Negate(a.z));
		}
		#endregion

		/// <summary>
		///   <para>Projects a vector onto another vector.</para>
		/// </summary>
		/// <param name="vector"></param>
		/// <param name="onNormal"></param>
		public static Vector3<TMathType> Project(Vector3<TMathType> vector, Vector3<TMathType> onNormal)
		{
			TMathType single = Vector3<TMathType>.Dot(onNormal, onNormal);

			if (Operator.LessThanOrEqual(single, kEpsilon))
			{
				return Vector3<TMathType>.zero;
			}

			return (onNormal * Vector3<TMathType>.Dot(vector, onNormal)) / single;
		}

		/// <summary>
		///   <para>Projects a vector onto a plane defined by a normal orthogonal to the plane.</para>
		/// </summary>
		/// <param name="vector"></param>
		/// <param name="planeNormal"></param>
		public static Vector3<TMathType> ProjectOnPlane(Vector3<TMathType> vector, Vector3<TMathType> planeNormal)
		{
			return vector - Vector3<TMathType>.Project(vector, planeNormal);
		}

		/// <summary>
		///   <para>Reflects a vector off the plane defined by a normal.</para>
		/// </summary>
		/// <param name="inDirection"></param>
		/// <param name="inNormal"></param>
		public static Vector3<TMathType> Reflect(Vector3<TMathType> inDirection, Vector3<TMathType> inNormal)
		{
			TMathType twoTimesDotNDir = Operator.Multiply(Operator.Negate(Operator.Add(Vector3<TMathType>.OneValue, Vector3<TMathType>.OneValue)), Vector3<TMathType>.Dot(inNormal, inDirection));
			return (twoTimesDotNDir * inNormal) + inDirection;
		}

		/// <summary>
		///   <para>Multiplies two vectors component-wise.</para>
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		public static Vector3<TMathType> Scale(Vector3<TMathType> a, Vector3<TMathType> b)
		{
            return new Vector3<TMathType>(Operator.Multiply(a.x, b.x), Operator.Multiply(a.y, b.y), Operator.Multiply(a.z, b.z));
		}

		/// <summary>
		///   <para>Multiplies every component of this vector by the same component of scale.</para>
		/// </summary>
		/// <param name="scale"></param>
		public void Scale(Vector3<TMathType> scale)
		{
			Vector3<TMathType> vector3 = this;
			vector3.x = Operator.Multiply(vector3.x, scale.x);
			Vector3<TMathType> vector31 = this;
			vector31.y = Operator.Multiply(vector31.y, scale.y);
			Vector3<TMathType> vector32 = this;
			vector32.z = Operator.Multiply(vector32.z, scale.z);
		}

		/// <summary>
		///   <para>Set x, y and z components of an existing Vector3<TMathType>.</para>
		/// </summary>
		/// <param name="new_x"></param>
		/// <param name="new_y"></param>
		/// <param name="new_z"></param>
		public void Set(TMathType new_x, TMathType new_y, TMathType new_z)
		{
			this.x = new_x;
			this.y = new_y;
			this.z = new_z;
		}

		public static TMathType SqrMagnitude(Vector3<TMathType> a)
		{
			return a.sqrMagnitude;
		}

		/*
		/// <summary>
		///   <para>Rotates a vector current towards target.</para>
		/// </summary>
		/// <param name="current"></param>
		/// <param name="target"></param>
		/// <param name="maxRadiansDelta"></param>
		/// <param name="maxMagnitudeDelta"></param>
		public static Vector3<TMathType> RotateTowards(Vector3<TMathType> current, Vector3<TMathType> target, float maxRadiansDelta, float maxMagnitudeDelta)
		{
			return Vector3<TMathType>.INTERNAL_CALL_RotateTowards(ref current, ref target, maxRadiansDelta, maxMagnitudeDelta);
		}

		/// <summary>
		///   <para>Spherically interpolates between two vectors.</para>
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="t"></param>
		public static Vector3<TMathType> Slerp(Vector3<TMathType> a, Vector3<TMathType> b, float t)
		{
			return Vector3<TMathType>.INTERNAL_CALL_Slerp(ref a, ref b, t);
		}

		/// <summary>
		///   <para>Spherically interpolates between two vectors.</para>
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="t"></param>
		public static Vector3<TMathType> SlerpUnclamped(Vector3<TMathType> a, Vector3<TMathType> b, float t)
		{
			return Vector3<TMathType>.INTERNAL_CALL_SlerpUnclamped(ref a, ref b, t);
		}

		/*public static Vector3<TMathType> SmoothDamp(Vector3<TMathType> current, Vector3<TMathType> target, ref Vector3<TMathType> currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float single = 2f / smoothTime;
			float single1 = single * deltaTime;
			float single2 = 1f / (1f + single1 + 0.48f * single1 * single1 + 0.235f * single1 * single1 * single1);
			Vector3<TMathType> Vector3<TMathType> = current - target;
			Vector3<TMathType> Vector3<TMathType>1 = target;
			Vector3<TMathType> = Vector3<TMathType>.ClampMagnitude(Vector3<TMathType>, maxSpeed * smoothTime);
			target = current - Vector3<TMathType>;
			Vector3<TMathType> Vector3<TMathType>2 = (currentVelocity + (single * Vector3<TMathType>)) * deltaTime;
			currentVelocity = (currentVelocity - (single * Vector3<TMathType>2)) * single2;
			Vector3<TMathType> Vector3<TMathType>3 = target + ((Vector3<TMathType> + Vector3<TMathType>2) * single2);
			if (Vector3<TMathType>.Dot(Vector3<TMathType>1 - current, Vector3<TMathType>3 - Vector3<TMathType>1) > 0f)
			{
				Vector3<TMathType>3 = Vector3<TMathType>1;
				currentVelocity = (Vector3<TMathType>3 - Vector3<TMathType>1) / deltaTime;
			}
			return Vector3<TMathType>3;
		}

		/// <summary>
		///   <para>Moves a point current in a straight line towards a target point.</para>
		/// </summary>
		/// <param name="current"></param>
		/// <param name="target"></param>
		/// <param name="maxDistanceDelta"></param>
		public static Vector3<TMathType> MoveTowards(Vector3<TMathType> current, Vector3<TMathType> target, float maxDistanceDelta)
		{
			Vector3<TMathType> Vector3<TMathType> = target - current;
			float single = Vector3<TMathType>.magnitude;

			if (single <= maxDistanceDelta || single == 0f)
			{
				return target;
			}
			return current + ((Vector3<TMathType> / single) * maxDistanceDelta);
		}

		/// <summary>
		///   <para>Returns the angle in degrees between from and to.</para>
		/// </summary>
		/// <param name="from">The angle extends round from this vector.</param>
		/// <param name="to">The angle extends round to this vector.</param>
		public static float Angle(Vector3<TMathType> from, Vector3<TMathType> to)
		{
			return Mathf.Acos(Mathf.Clamp(Vector3<TMathType>.Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f;
		}

		[Obsolete("Use Vector3<TMathType>.Angle instead. AngleBetween uses radians instead of degrees and was deprecated for this reason")]
		public static float AngleBetween(Vector3<TMathType> from, Vector3<TMathType> to)
		{
			return Mathf.Acos(Mathf.Clamp(Vector3<TMathType>.Dot(from.normalized, to.normalized), -1f, 1f));
		}

		[Obsolete("Use Vector3<TMathType>.ProjectOnPlane instead.")]
		public static Vector3<TMathType> Exclude(Vector3<TMathType> excludeThis, Vector3<TMathType> fromThat)
		{
			return fromThat - Vector3<TMathType>.Project(fromThat, excludeThis);
		}

		[WrapperlessIcall]
		private static extern void INTERNAL_CALL_Internal_OrthoNormalize2(ref Vector3<TMathType> a, ref Vector3<TMathType> b);

		[WrapperlessIcall]
		private static extern void INTERNAL_CALL_Internal_OrthoNormalize3(ref Vector3<TMathType> a, ref Vector3<TMathType> b, ref Vector3<TMathType> c);

		[WrapperlessIcall]
		private static extern Vector3<TMathType> INTERNAL_CALL_RotateTowards(ref Vector3<TMathType> current, ref Vector3<TMathType> target, float maxRadiansDelta, float maxMagnitudeDelta);

		[WrapperlessIcall]
		private static extern Vector3<TMathType> INTERNAL_CALL_Slerp(ref Vector3<TMathType> a, ref Vector3<TMathType> b, float t);

		[WrapperlessIcall]
		private static extern Vector3<TMathType> INTERNAL_CALL_SlerpUnclamped(ref Vector3<TMathType> a, ref Vector3<TMathType> b, float t);

		private static void Internal_OrthoNormalize2(ref Vector3<TMathType> a, ref Vector3<TMathType> b)
		{
			Vector3<TMathType>.INTERNAL_CALL_Internal_OrthoNormalize2(ref a, ref b);
		}

		private static void Internal_OrthoNormalize3(ref Vector3<TMathType> a, ref Vector3<TMathType> b, ref Vector3<TMathType> c)
		{
			Vector3<TMathType>.INTERNAL_CALL_Internal_OrthoNormalize3(ref a, ref b, ref c);
		}

		/// <summary>
		///   <para>Linearly interpolates between two vectors.</para>
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="t"></param>
		public static Vector3<TMathType> Lerp(Vector3<TMathType> a, Vector3<TMathType> b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector3<TMathType>(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
		}

		/// <summary>
		///   <para>Linearly interpolates between two vectors.</para>
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="t"></param>
		public static Vector3<TMathType> LerpUnclamped(Vector3<TMathType> a, Vector3<TMathType> b, float t)
		{
			return new Vector3<TMathType>(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
		}

		public static void OrthoNormalize(ref Vector3<TMathType> normal, ref Vector3<TMathType> tangent)
		{
			Vector3<TMathType>.Internal_OrthoNormalize2(ref normal, ref tangent);
		}

		public static void OrthoNormalize(ref Vector3<TMathType> normal, ref Vector3<TMathType> tangent, ref Vector3<TMathType> binormal)
		{
			Vector3<TMathType>.Internal_OrthoNormalize3(ref normal, ref tangent, ref binormal);
		}*/
	}
}