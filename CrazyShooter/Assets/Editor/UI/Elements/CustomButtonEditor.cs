using CrazyShooter.UI;
using UnityEditor;
using UnityEditor.UI;

namespace CrayzShooter.Editor.UI
{
    [CustomEditor(typeof(CustomButton), false)]
    [CanEditMultipleObjects]
    public class CustomButtonEditor : ButtonEditor
    {
        private SerializedProperty _containerProperty;
        private SerializedProperty _textProperty;
        private SerializedProperty _disableParentScrollProperty;
        private SerializedProperty _isCloseButtonProperty;
        private SerializedProperty _noBounceAnimationProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            _containerProperty = serializedObject.FindProperty(CustomButtonCreateAsset.ContainerFieldName);
            _textProperty = serializedObject.FindProperty(CustomButtonCreateAsset.TextFieldName);
            _disableParentScrollProperty = serializedObject.FindProperty(CustomButtonCreateAsset.DisableParentScrollFieldName);
            _isCloseButtonProperty = serializedObject.FindProperty(CustomButtonCreateAsset.IsCloseButtonFieldName);
            _noBounceAnimationProperty = serializedObject.FindProperty(CustomButtonCreateAsset.NoBounceAnimationFieldName);
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Custom properties");
            serializedObject.Update();
            EditorGUILayout.PropertyField(_containerProperty);
            EditorGUILayout.PropertyField(_textProperty);
            EditorGUILayout.PropertyField(_disableParentScrollProperty);
            EditorGUILayout.PropertyField(_isCloseButtonProperty);
            EditorGUILayout.PropertyField(_noBounceAnimationProperty);
            serializedObject.ApplyModifiedProperties();
            
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Unity Button properties");
            base.OnInspectorGUI();
        }
    }
}