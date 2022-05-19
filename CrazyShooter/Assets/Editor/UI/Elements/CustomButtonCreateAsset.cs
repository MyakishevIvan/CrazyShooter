using System.Reflection;
using CrazyShooter.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace CrayzShooter.Editor.UI
{
    public class CustomButtonCreateAsset : UnityEditor.Editor
    {
        public static string ContainerFieldName = "container";
        public static string TextFieldName = "textObject";
        public static string DisableParentScrollFieldName = "disableParentScroll";
        public static string IsCloseButtonFieldName = "isCloseButton";
        public static string NoBounceAnimationFieldName = "noBounceAnimation";
        
        [MenuItem("GameObject/CobraKai/UI/Button", false, -1)]
        public static void CreateButton()
        {
            // create button
            var buttonGo = new GameObject("Button");

            buttonGo.transform.parent = Selection.activeTransform;
            buttonGo.transform.localScale = Vector3.one;
            buttonGo.transform.localPosition = Vector3.zero;
            
            var buttonImage = buttonGo.AddComponent<Image>();
            buttonImage.color = Color.clear;
            
            var button = buttonGo.AddComponent<CustomButton>();
            button.targetGraphic = buttonImage;
            button.transition = Selectable.Transition.None;

            Selection.activeTransform = buttonGo.transform;

            // create inner container and image
            var container = new GameObject("Container").AddComponent<RectTransform>();
            container.SetParent(button.transform);
            container.transform.localScale = Vector3.one;
            container.transform.localPosition = Vector3.zero;
            container.anchorMin = Vector2.zero;
            container.anchorMax = Vector2.one;
            container.offsetMin = container.offsetMax = Vector2.zero;
            
            var image = new GameObject("Image").AddComponent<Image>();
            image.transform.SetParent(container);
            image.transform.localScale = Vector3.one;
            image.transform.localPosition = Vector3.zero;
            image.raycastTarget = false;

            var imageRect = image.gameObject.GetComponent<RectTransform>();
            imageRect.anchorMin = Vector2.zero;
            imageRect.anchorMax = Vector2.one;
            imageRect.offsetMin = imageRect.offsetMax = Vector2.zero;

            // setup inner image field
            var info = button.GetType().GetField(ContainerFieldName,
                BindingFlags.Public
                | BindingFlags.NonPublic
                | BindingFlags.Instance);

            if (info == null)
            {
                Debug.LogError($"Can't find field with name {ContainerFieldName}");
                return;
            }
            
            info.SetValue(button, container);

            // mark scene as dirty
            var scene = EditorSceneManager.GetActiveScene();
            EditorSceneManager.MarkSceneDirty(scene);
        }
    }
}