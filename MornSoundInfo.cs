using System;
using UnityEngine;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MornSound
{
    [Serializable]
    internal class MornSoundInfo
    {
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] [Range(0, 1)] private float _volumeRate = 1;
        [SerializeField] private float _semitoneDownRange;
        [SerializeField] private float _semitoneUpRange;
        public AudioClip AudioClip => _audioClip;
        public float VolumeRate => _volumeRate;
        public float Pitch => 1 * Mathf.Pow(2, Random.Range(_semitoneDownRange, _semitoneUpRange) / 12f);
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(MornSoundInfo))]
    public class MornSoundInfoDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            {
                var clip = property.FindPropertyRelative("_audioClip");
                var volumeRate = property.FindPropertyRelative("_volumeRate");
                var semitoneDownRange = property.FindPropertyRelative("_semitoneDownRange");
                var semitoneUpRange = property.FindPropertyRelative("_semitoneUpRange");
                
                var rect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(rect, clip);
                rect.y += EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(rect, volumeRate);
                rect.y += EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(rect, semitoneDownRange);
                rect.y += EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(rect, semitoneUpRange);
                
            }
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 4;
        }
    }
#endif
}