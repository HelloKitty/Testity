using MiscUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Testity.EngineMath
{
	public struct Quaternion<TMathType>
		where TMathType : struct, IEquatable<TMathType>, IComparable<TMathType>
	{
		public static TMathType kEpsilon = MathT.GenerateKEpslion<TMathType>();

		/// <summary>
		/// A cache of the <see cref="TMathType"/> value that represents 1.
		/// </summary>
		private static TMathType oneValue = MathT.GenerateOneValue<TMathType>();

		private static TMathType twoValue = MathT.GenerateTwoValue<TMathType>();

		private static TMathType dotCompareVal = GenerateDotCompareValue();
		private static TMathType GenerateDotCompareValue()
		{
			try
			{
				return (TMathType)Convert.ChangeType(0.999999f, typeof(TMathType));
			}
			catch (InvalidCastException)
			{
#if DEBUG || DEBBUGBUILD
				//These are known types that cause issues with quat
				if (typeof(TMathType) != typeof(char))
					throw;
				//These are known types that cause issues with quat
				if (typeof(TMathType) != typeof(byte))
					throw;
				//These are known types that cause issues with quat
				if (typeof(TMathType) != typeof(int))
					throw;
#endif
				return Operator<TMathType>.Zero;
			}
		}


		//This let's us compute dot products with strong typing in another method external from the class.
		private delegate TMathType LowGCDotProductFunc(ref Quaternion<TMathType> a, ref Quaternion<TMathType> b);

		private static LowGCDotProductFunc dotFunc;

		/// <summary>
		///   <para>X component of the Quaternion<TMathType>. Don't modify this directly unless you know Quaternion<TMathType>s inside out.</para>
		/// </summary>
		public TMathType x;

		/// <summary>
		///   <para>Y component of the Quaternion<TMathType>. Don't modify this directly unless you know Quaternion<TMathType>s inside out.</para>
		/// </summary>
		public TMathType y;

		/// <summary>
		///   <para>Z component of the Quaternion<TMathType>. Don't modify this directly unless you know Quaternion<TMathType>s inside out.</para>
		/// </summary>
		public TMathType z;

		/// <summary>
		///   <para>W component of the Quaternion<TMathType>. Don't modify this directly unless you know Quaternion<TMathType>s inside out.</para>
		/// </summary>
		public TMathType w;

		/// <summary>
		///   <para>The identity rotation (Read Only).</para>
		/// </summary>
		public static Quaternion<TMathType> identity { get { return new Quaternion<TMathType>(Operator<TMathType>.Zero, Operator<TMathType>.Zero, Operator<TMathType>.Zero, oneValue); } }


		/// <summary>
		/// Gets and Sets a <see cref="Vector3{TMathType}"/> of the xyz components.
		/// Part of decompiled Unity3D Quat: https://gist.github.com/HelloKitty/91b7af87aac6796c3da9
		/// </summary>
		public Vector3<TMathType> xyz
		{
			set
			{
				x = value.x;
				y = value.y;
				z = value.z;
			}
			get
			{
				return new Vector3<TMathType>(x, y, z);
			}
		}

		public TMathType this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
							return this.x;
					case 1:
							return this.y;
					case 2:
							return this.z;
					case 3:
							return this.w;
					default:
						throw new IndexOutOfRangeException("Invalid " + nameof(Quaternion<TMathType>) + " index!");
				}
			}
			set
			{
				switch (index)
				{
					case 0:
						this.x = value;
						break;
					case 1:
						this.y = value;
						break;
					case 2:
						this.z = value;
						break;
					case 3:
						this.w = value;
						break;
					default:
						throw new IndexOutOfRangeException("Invalid " + nameof(Quaternion<TMathType>) + " index!");
				}
			}
		}

		static Quaternion()
		{
			MethodInfo dotMethodInfo = typeof(QuaternionExtensions)
				.GetMethod("DotRef", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, new Type[] { typeof(Quaternion<TMathType>), typeof(Quaternion<TMathType>) }, null);

			dotFunc = (LowGCDotProductFunc)Delegate.CreateDelegate(typeof(LowGCDotProductFunc), dotMethodInfo, true);
		}

		//Additional constructors added to easily support implementation found here: https://gist.github.com/HelloKitty/91b7af87aac6796c3da9

		/// <summary>
		///   <para>Constructs new Quaternion with given x,y,z,w components.</para>
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="w"></param>
		public Quaternion(TMathType x, TMathType y, TMathType z, TMathType w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		/// <summary>
		/// Construct a new Quaternion from vector and w components
		/// </summary>
		/// <param name="v">The vector part</param>
		/// <param name="w">The w part</param>
		public Quaternion(Vector3<TMathType> v, TMathType w)
		{
			this.x = v.x;
			this.y = v.y;
			this.z = v.z;
			this.w = w;
		}


		/// <summary>
		///   <para>Set x, y, z and w components of an existing Quaternion.</para>
		/// </summary>
		/// <param name="new_x"></param>
		/// <param name="new_y"></param>
		/// <param name="new_z"></param>
		/// <param name="new_w"></param>
		public void Set(TMathType new_x, TMathType new_y, TMathType new_z, TMathType new_w)
		{
			this.x = new_x;
			this.y = new_y;
			this.z = new_z;
			this.w = new_w;
		}

		public override bool Equals(object other)
		{
			if (!(other is Quaternion<TMathType>))
			{
				return false;
			}

			Quaternion<TMathType> quat = (Quaternion<TMathType>)other;
			return (!this.x.Equals(quat.x) || !this.y.Equals(quat.y) || !this.z.Equals(quat.z) ? false : this.w.Equals(quat.w));
		}

		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
		}

		public static bool operator ==(Quaternion<TMathType> lhs, Quaternion<TMathType> rhs)
		{

			return Operator.GreaterThan(dotFunc(ref lhs, ref rhs), dotCompareVal);
        }

		public static bool operator !=(Quaternion<TMathType> lhs, Quaternion<TMathType> rhs)
		{
			return Operator.LessThanOrEqual(dotFunc(ref lhs, ref rhs), dotCompareVal);
		}

		public static Quaternion<TMathType> operator *(Quaternion<TMathType> lhs, Quaternion<TMathType> rhs)
		{
			TMathType newX = Operator.Subtract(Operator.Add(Operator.Add(Operator.Multiply(lhs.w, rhs.x), Operator.Multiply(lhs.x, rhs.w)), Operator.Multiply(lhs.y, rhs.z)), Operator.Multiply(lhs.z, rhs.y));
			TMathType newY = Operator.Subtract(Operator.Add(Operator.Add(Operator.Multiply(lhs.w, rhs.y), Operator.Multiply(lhs.y, rhs.w)), Operator.Multiply(lhs.z, rhs.x)), Operator.Multiply(lhs.x, rhs.z));
			TMathType newZ = Operator.Subtract(Operator.Add(Operator.Add(Operator.Multiply(lhs.w, rhs.z), Operator.Multiply(lhs.z, rhs.w)), Operator.Multiply(lhs.x, rhs.y)), Operator.Multiply(lhs.y, rhs.x));
			TMathType newW = Operator.Subtract(Operator.Subtract(Operator.Subtract(Operator.Multiply(lhs.w, rhs.w), Operator.Multiply(lhs.x, rhs.x)), Operator.Multiply(lhs.y, rhs.y)), Operator.Multiply(lhs.z, rhs.z));

			return new Quaternion<TMathType>(newX, newY, newZ, newW);
        }

		public static Vector3<TMathType> operator *(Quaternion<TMathType> rotation, Vector3<TMathType> point)
		{
			Vector3<TMathType> Vector3 = new Vector3<TMathType>();

			//Decompiled matrix math for quat * vector from Unity3D dll.
			TMathType tMathValueTerm = Operator<TMathType>.Multiply(rotation.x, Operator.Add(oneValue, oneValue));
			TMathType tMathValueTerm1 = Operator<TMathType>.Multiply(rotation.y, Operator.Add(oneValue, oneValue));
			TMathType tMathValueTerm2 = Operator<TMathType>.Multiply(rotation.z, Operator.Add(oneValue, oneValue));

			TMathType tMathValueTerm3 = Operator<TMathType>.Multiply(rotation.x, tMathValueTerm);
			TMathType tMathValueTerm4 = Operator<TMathType>.Multiply(rotation.y, tMathValueTerm1);
			TMathType tMathValueTerm5 = Operator<TMathType>.Multiply(rotation.z, tMathValueTerm2);

			TMathType tMathValueTerm6 = Operator<TMathType>.Multiply(rotation.x, tMathValueTerm1);
			TMathType tMathValueTerm7 = Operator<TMathType>.Multiply(rotation.x, tMathValueTerm2);
			TMathType tMathValueTerm8 = Operator<TMathType>.Multiply(rotation.y, tMathValueTerm2);

			//w
			TMathType tMathValueTerm9 = Operator<TMathType>.Multiply(rotation.w, tMathValueTerm);
			TMathType tMathValueTerm10 = Operator<TMathType>.Multiply(rotation.w, tMathValueTerm1);
			TMathType tMathValueTerm11 = Operator<TMathType>.Multiply(rotation.w, tMathValueTerm2);


            Vector3.x = Operator.Add(Operator.Add(Operator.Multiply(Operator.Subtract(oneValue, Operator.Add(tMathValueTerm4, tMathValueTerm5)), point.x), Operator.Multiply(point.y, Operator.Subtract(tMathValueTerm6, tMathValueTerm11))),
				Operator.Multiply(Operator.Add(tMathValueTerm7, tMathValueTerm10), point.z));//(1f - (single4 + single5)) * point.x + (single6 - single11) * point.y + (single7 + single10) * point.z;
			Vector3.y = Operator.Add(Operator.Add(Operator.Multiply(Operator.Subtract(oneValue, Operator.Add(tMathValueTerm3, tMathValueTerm5)), point.y), Operator.Multiply(point.z, Operator.Subtract(tMathValueTerm8, tMathValueTerm9))),
				Operator.Multiply(Operator.Add(tMathValueTerm6, tMathValueTerm11), point.y));//(single6 + single11) * point.x + (1f - (single3 + single5)) * point.y + (single8 - single9) * point.z;
			Vector3.z = Operator.Add(Operator.Add(Operator.Multiply(Operator.Subtract(oneValue, Operator.Add(tMathValueTerm3, tMathValueTerm4)), point.z), Operator.Multiply(point.x, Operator.Subtract(tMathValueTerm7, tMathValueTerm10))),
				Operator.Multiply(Operator.Add(tMathValueTerm8, tMathValueTerm9), point.y));//(single7 - single10) * point.x + (single8 + single9) * point.y + (1f - (single3 + single4)) * point.z;

			return Vector3;
		}
	}
}
