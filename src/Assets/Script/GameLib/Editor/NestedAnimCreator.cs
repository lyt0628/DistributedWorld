using System;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace QS.GameLib.Editor
{
    public class RenameWindow : EditorWindow
    {
        public string Caption { get; set; }
        public string ButtonText { get; set; }
        public string NewName { get; set; }
        public Action<string> OnButtonClicked { get; set; }

        private void OnGUI()
        {
            NewName = EditorGUILayout.TextField(Caption, NewName);
            if (GUILayout.Button(ButtonText))
            {
                OnButtonClicked ?.Invoke(NewName.Trim());

                Close();
                GUIUtility.ExitGUI();

            }
        }
    }


    public class NestedAnimationClipCreator : MonoBehaviour
    {
        [MenuItem("GameLib/Anim/Create/Nested AnimationClip")]
        public static void Create()
        {
            AnimatorController selectedAnimatorController = Selection.activeObject as AnimatorController;
            if (selectedAnimatorController == null)
            {
                Debug.LogWarning("No AnimationController selected.");
                return;
            }
         
            var renameWindow = EditorWindow.GetWindow<RenameWindow>("Create Nested AnimationClip");
            renameWindow.Caption = "New Animation Name";
            renameWindow.NewName = "New Clip";
            renameWindow.ButtonText = "Create";
            renameWindow.OnButtonClicked = (newName) =>
            {
                if (string.IsNullOrEmpty(newName))
                {
                    Debug.LogWarning("Invalid name.");
                    return;
                }

                var animClip = AnimatorController.AllocateAnimatorClip(newName);

                AssetDatabase.AddObjectToAsset(animClip, selectedAnimatorController);
                AssetDatabase.ImportAsset(AssetDatabase.GetAssetOrScenePath(selectedAnimatorController));
            };

        }
    }

}