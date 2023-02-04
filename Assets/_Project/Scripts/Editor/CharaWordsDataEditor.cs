using EtienneEditor;
using UnityEditor;
using UnityEngine;

namespace Route69
{
    [CustomEditor(typeof(CharaWordsData))]
    public class CharaWordsDataEditor : Editor<CharaWordsData>
    {
        bool isDirty = false;
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginDisabledGroup(!isDirty);
            if (GUILayout.Button("Save")) Save();
            EditorGUI.EndDisabledGroup();
            EditorGUI.BeginChangeCheck();
            base.OnInspectorGUI();
            if (EditorGUI.EndChangeCheck()) SetDirtyInternal();
        }

        private void SetDirtyInternal()
        {
            EditorUtility.SetDirty(Target);
            isDirty = true;
        }

        private void Save()
        {
            AssetDatabase.SaveAssetIfDirty(Target);
            isDirty = false;
        }

        private void OnEnable()
        {
            isDirty = false;
        }

        private void OnDisable()
        {
            if (!isDirty) return;
            if (EditorUtility.DisplayDialog($"{Target.name} has unsaved changes.", "Do you want to save?", "Save", "Discard"))
            {
                Save();
            }
        }
    }
}
