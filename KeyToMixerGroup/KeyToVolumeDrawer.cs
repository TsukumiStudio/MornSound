#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace MornSound
{
    [CustomPropertyDrawer(typeof(KeyToMixerGroup))]
    public class KeyToMixerGroupDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var sceneType = property.FindPropertyRelative("SourceType");
            var scene = property.FindPropertyRelative("MixerGroup");
            EditorGUI.BeginProperty(position, label, property);
            {
                position = EditorGUI.PrefixLabel(position, label);
                var indent = EditorGUI.indentLevel;
                {
                    EditorGUI.indentLevel = 0;
                    var halfWidth = position.width / 2;
                    const float spacing = 5f;
                    var sceneTypeRect = new Rect(position.x, position.y, halfWidth - spacing, position.height);
                    var sceneRect = new Rect(position.x + halfWidth, position.y, halfWidth, position.height);
                    EditorGUI.PropertyField(sceneTypeRect, sceneType, GUIContent.none);
                    EditorGUI.PropertyField(sceneRect, scene, GUIContent.none);
                    EditorGUI.indentLevel = indent;
                }
            }
            EditorGUI.EndProperty();
        }
    }
}
#endif