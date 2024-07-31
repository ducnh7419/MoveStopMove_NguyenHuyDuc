// using UnityEngine;
// using UnityEditor;

// [CustomPropertyDrawer(typeof(ConditionalHideAttribute))]
// public class ConditionalHidePropertyDrawer : PropertyDrawer
// {
//     public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//     {
//         ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
//         bool enabled = GetConditionalHideOrShowAttributeResult(condHAtt, property);
 
//         bool wasEnabled = GUI.enabled;
//         GUI.enabled = enabled;
//         if (enabled)
//         {
//             EditorGUI.PropertyField(position, property, label, true);
//         }
 
//         GUI.enabled = wasEnabled;
//     }
 
//     public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//     {
//         ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
//         bool enabled = GetConditionalHideOrShowAttributeResult(condHAtt, property);
 
//         if (enabled)
//         {
//             return EditorGUI.GetPropertyHeight(property, label);
//         }
//         else
//         {
//             return -EditorGUIUtility.standardVerticalSpacing;
//         }
//     }
 
//     private bool GetConditionalHideOrShowAttributeResult(ConditionalHideAttribute condHAtt, SerializedProperty property)
//     {
//         int value;
//         string propertyPath = property.propertyPath; //returns the property path of the property we want to apply the attribute to
//         string conditionPath = propertyPath.Replace(property.name, condHAtt.Etype); //changes the path to the conditionalsource property path
//         SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);
//         SerializedProperty serializedProperty= property.serializedObject.FindProperty(propertyPath);
//         if (sourcePropertyValue != null)
//         {
//             value = sourcePropertyValue.enumValueFlag;
//             if(value == condHAtt.Value){
//                 return true;
//             }
            
//         }
//         else
//         {
//             Debug.LogWarning("Attempting to use a ConditionalHideOrShowAttribute but no matching SourcePropertyValue found in object: " + condHAtt.ConditionalSourceField);
//         }
//         serializedProperty.enumValueFlag=0;
//         return false;
//     }
// }