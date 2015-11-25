﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testity.EngineServices
{
	/// <summary>
	/// Represents the result of an unregisteration operation. This operation can potentially indicate failure to unregister.
	/// </summary>
	/// <typeparam name="TObjectType">Type of object being unregistered.</typeparam>
	public class UnregistrationResult<TObjectType>
		where TObjectType : class
    {
		private readonly bool _Failed;
		/// <summary>
		/// Indicates if unregisteration was sucessful.
		/// </summary>
		public bool Failed
		{
			get { return Failed && Value != null; }
		}

		/// <summary>
		/// The <see cref="TObjectType"/> object that was unregistered. Potentially null. Check <see cref="Failed"/> first.
		/// </summary>
		public TObjectType Value { get; private set; }

		/// <summary>
		/// Creates a result for unregisteration that can potentially fail.
		/// </summary>
		/// <param name="found">Indicates if the object was found.</param>
		/// <param name="val">The object if found. Null is acceptable.</param>
		public UnregistrationResult(bool found, TObjectType val)
		{
			_Failed = found;
			Value = val;
		}
	}
}
