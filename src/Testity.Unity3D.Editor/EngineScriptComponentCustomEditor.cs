using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Testity.EngineComponents;
using Testity.EngineServices.Unity3D;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Testity.Unity3D.Editor
{
	[UnityEditor.CustomEditor(typeof(TestityBehaviour), true, isFallback = false)]
	public class EngineScriptComponentCustomEditor : UnityEditor.Editor
	{
		private List<Action> actionsToDo = new List<Action>();

		public override void OnInspectorGUI()
		{
			EditorGUIUtility.LookLikeControls();
			serializedObject.Update();
			EngineScriptComponent testityBehaviour = GetScriptComponent(target);

			if (GetScriptComponent(target) != null)
				Debug.Log(target.GetType() + " was found.");
			else
				Debug.Log(target.GetType() + " not found.");

			/*foreach (FieldInfo fi in testityBehaviour.GetType().GetFields())
			{
				if (fi.FieldType == typeof(string))
				{
					fi.SetValue(testityBehaviour, EditorGUILayout.TextField(fi.Name + "Reflected:", (string)fi.GetValue(testityBehaviour)));
				}
			}*/

			//SerializedProperty uEvent = serializedObject.FindProperty("testEvent");

			//if(uEvent != null)
			//	EditorGUILayout.PropertyField(uEvent);

			foreach (FieldInfo fi in testityBehaviour.GetType().GetFields())
			{
				if (fi.FieldType == typeof(TestUnityEventInterface))
				{
					
					//UnityEvent uEvent = ((UnityEventContainer)fi.GetValue(testityBehaviour)).ContainedEvent;
					//SerializedProperty uEvent = null;

					//if (serializedObject.FindProperty("_InternalSerializableComponent").FindPropertyRelative(fi.Name) != null)
					//	uEvent = serializedObject.FindProperty("_InternalSerializableComponent").FindPropertyRelative(fi.Name).FindPropertyRelative(nameof(UnityEventContainer.ContainedEvent));
					//else
					//	Debug.Log("Interface was null.");

					//EditorGUILayout.PropertyField(uEvent);
				}
			}


			/*if (actionsToDo.Count() == 0)
			{
				EngineScriptComponent testityBehaviour = GetScriptComponent(target);

				if (GetScriptComponent(target) != null)
					Debug.Log(target.GetType() + " was found.");
				else
					Debug.Log(target.GetType() + " not found.");

				foreach (FieldInfo fi in testityBehaviour.GetType().GetFields())
				{
					if (fi.FieldType == typeof(string))
					{
						actionsToDo.Add(() => fi.SetValue(testityBehaviour, EditorGUILayout.TextField(fi.Name + "Reflected:", (string)fi.GetValue(testityBehaviour))));
					}
				}

				foreach (FieldInfo fi in testityBehaviour.GetType().GetFields())
				{
					if (fi.FieldType == typeof(TestUnityEventInterface))
					{
						actionsToDo.Add(() =>
						{
							SerializedProperty uEvent = serializedObject.FindProperty("_InternalSerializableComponent").FindPropertyRelative(fi.Name).FindPropertyRelative(nameof(UnityEventContainer.ContainedEvent));

							EditorGUILayout.PropertyField(uEvent);
						});
					}
				}
			}
			else
				foreach (Action a in actionsToDo)
					a.Invoke();*/




			serializedObject.ApplyModifiedProperties();

			/*EditorGUIUtility.LookLikeControls();
			serializedObject.Update();

			if (actionsToDo.Count() == 0)
			{
				EngineScriptComponent testityBehaviour = GetScriptComponent(target);

				if (GetScriptComponent(target) != null)
					Debug.Log(target.GetType() + " was found.");
				else
					Debug.Log(target.GetType() + " not found.");

				foreach (FieldInfo fi in testityBehaviour.GetType().GetFields())
				{
					if (fi.FieldType == typeof(string))
					{
						fi.SetValue(testityBehaviour, EditorGUILayout.TextField(fi.Name + "Reflected:", (string)fi.GetValue(testityBehaviour)));
					}
				}

				foreach (FieldInfo fi in testityBehaviour.GetType().GetFields())
				{
					if (fi.FieldType == typeof(UnityEvent))
					{
						SerializedProperty uEvent = null;
						try
						{
							//EditorGUILayout.
							uEvent = serializedObject.FindProperty("_InternalSerializableComponent").FindPropertyRelative(fi.Name);
						}
						catch (Exception)
						{
							Debug.Log("Failed to find property: " + fi.Name);
							throw;
						}

						try
						{

							EditorGUILayout.PropertyField(uEvent);
						}
						catch (Exception)
						{
							Debug.Log("Failed to write propertyfield.");
						}
					}
				}
			}
			else
				foreach (Action a in actionsToDo)
					a.Invoke();

			


			serializedObject.ApplyModifiedProperties();*/

			base.OnInspectorGUI();
		}

		private EngineScriptComponent GetScriptComponent(UnityEngine.Object objectToScrape)
		{
			return objectToScrape.GetType().GetProperty(nameof(ITestityBehaviour<EngineScriptComponent>.ScriptComponent), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.Public)
				.GetValue(objectToScrape, null) as EngineScriptComponent;
		}
	}
}
