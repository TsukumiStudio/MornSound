#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace MornSound
{
    [CustomPropertyDrawer(typeof(KeyToVolume))]
    public class KeyToVolumeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var mixerKeys = property.FindPropertyRelative("MixerKeys");
            if (mixerKeys.isExpanded)
            {
                return EditorGUI.GetPropertyHeight(mixerKeys, true);
            }
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var volumeType = property.FindPropertyRelative("VolumeType");
            var mixerKeys = property.FindPropertyRelative("MixerKeys");
            
            EditorGUI.BeginProperty(position, label, property);
            {
                // ラベルを描画
                position = EditorGUI.PrefixLabel(position, label);
                
                var indent = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;
                
                // VolumeTypeを左側に描画
                var halfWidth = position.width / 2;
                const float spacing = 5f;
                var volumeTypeRect = new Rect(position.x, position.y, halfWidth - spacing, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(volumeTypeRect, volumeType, GUIContent.none);
                
                // MixerKeys配列を右側に描画
                var mixerKeysRect = new Rect(position.x + halfWidth, position.y, halfWidth, EditorGUI.GetPropertyHeight(mixerKeys, true));
                EditorGUI.PropertyField(mixerKeysRect, mixerKeys, GUIContent.none, true);
                
                EditorGUI.indentLevel = indent;
            }
            EditorGUI.EndProperty();
        }
    }
}
#endif