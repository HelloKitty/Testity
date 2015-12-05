using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Fasterflect;

namespace Testity.Serialization
{
	public static class SerializedMemberParser<TMemberInfoType, TTypeToParse>
		where TMemberInfoType : MemberInfo where TTypeToParse : class
    {
		/// <summary>
		/// Caches the functional 1 to 1 relationship between a Type and its <see cref="MemberTypes"/>.
		/// </summary>
		private static readonly IDictionary<Type, MemberTypes> cacheTypeMemberMap
			= new Dictionary<Type, MemberTypes>(Enum.GetValues(typeof(MemberTypes)).Length)
			{
				{typeof(FieldInfo), MemberTypes.Field},
				{typeof(PropertyInfo), MemberTypes.Property}
			};

		//Fasterflect will cache
		/*//the reason we don't use MemberTypes is because enums that aren't ints cause boxing.
		//This causes GC issues in Unity.
		/// <summary>
		/// Caches the results of parsing a Type for <see cref="ExposeDataMemeberAttribute"/>.
		/// </summary>
		private static readonly IDictionary<Type, IDictionary<Type, IEnumerable<TMemberInfoType>>>
			cachedMemberDataMap = new Dictionary<Type, IDictionary<Type, IEnumerable<TMemberInfoType>>>();

		static SerializedMemberParser()
		{
			cachedMemberDataMap.Add(typeof(TTypeToParse), new Dictionary<Type, IEnumerable<TMemberInfoType>>(3));
        }

		public static IEnumerable<TMemberInfoType> Parse()
		{
			//If we don't have the type cached then we'll need to add it and add the dictionary
			if (cachedMemberDataMap.ContainsKey(typeof(TTypeToParse)))//if we have the key of the memberinfo then we can return the enumerable																	 
				if (cachedMemberDataMap[typeof(TTypeToParse)].ContainsKey(typeof(TMemberInfoType))) //otherwise we need to add it.
					return cachedMemberDataMap[typeof(TTypeToParse)][typeof(TMemberInfoType)];
				else
					cachedMemberDataMap[typeof(TTypeToParse)].Add(typeof(TMemberInfoType), GenerateSerializableMemberInfos());
			else
				cachedMemberDataMap.Add(typeof(TTypeToParse), new Dictionary<Type, IEnumerable<TMemberInfoType>>(3));

			//call recursively. Will eventually reach base-cse of the return.
			return Parse();
        }

		/// <summary>
		/// Creates a collection of <see cref="MemberInfo"/>s that are of 
		/// the type <typeparamref name="TMemberInfoType "/> that aren't null and are marked with
		/// 
		/// </summary>
		/// <returns></returns>
		private static IEnumerable<TMemberInfoType> GenerateSerializableMemberInfos()
		{

			return typeof(TTypeToParse).MembersWith<ExposeDataMemeberAttribute>(cacheTypeMemberMap[typeof(TMemberInfoType)], Flags.InstanceAnyVisibility) //get all members from current and base classes
				.Cast<TMemberInfoType>();
		}*/

		/// <summary>
		/// Creates a collection of <see cref="IEnumerable{TMemberInfoType}"/> that reference the <see cref="TTypeToParse"/> members.
		/// These members will also be decorated by the <see cref="ExposeDataMemeberAttribute"/>.
		/// </summary>
		/// <returns>A collection of <typeparamref name="MemberInfoType"/></returns>
		private static IEnumerable<TMemberInfoType> Parse()
		{
			return typeof(TTypeToParse).MembersWith<ExposeDataMemeberAttribute>(cacheTypeMemberMap[typeof(TMemberInfoType)], Flags.InstanceAnyVisibility) //get all members from current and base classes
				.Cast<TMemberInfoType>();
		}
	}
}
