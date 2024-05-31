using ObjectPooling;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PoolingItemSO))]
public class CustomPoolingItemSO : Editor
{
    private SerializedProperty _enumNameProp;
    private SerializedProperty _poolingNameProp;
    private SerializedProperty _description;
    private SerializedProperty _poolCountProp;
    private SerializedProperty _prefabProp;

    private GUIStyle _textAreaStyle;

    private void OnEnable()
    {
        GUIUtility.keyboardControl = 0;

        _enumNameProp = serializedObject.FindProperty("enumName");
        _poolingNameProp = serializedObject.FindProperty("poolingName");
        _description = serializedObject.FindProperty("description");
        _poolCountProp = serializedObject.FindProperty("poolCount");
        _prefabProp = serializedObject.FindProperty("prefab");
    }

    private void StyleSetup()
    {
        if(_textAreaStyle == null)
        {
            _textAreaStyle = new GUIStyle(EditorStyles.textArea);
            _textAreaStyle.wordWrap = true;
        }
    }

    public override void OnInspectorGUI()
    {
        StyleSetup();
        serializedObject.Update(); // 변화가 생긴걸 로드

        EditorGUILayout.BeginHorizontal("HelpBox");
        {
            EditorGUILayout.BeginVertical();
            {
                EditorGUI.BeginChangeCheck(); // 변경을 체크함
                string prevName = _enumNameProp.stringValue;
                EditorGUILayout.DelayedTextField(_enumNameProp);
                if (EditorGUI.EndChangeCheck())
                {
                    string assetPath = AssetDatabase.GetAssetPath(target);
                    string newName = $"Pool_{_enumNameProp.stringValue}";
                    serializedObject.ApplyModifiedProperties();


                    string msg = AssetDatabase.RenameAsset(assetPath, newName);

                    if (string.IsNullOrEmpty(msg))
                    {
                        target.name = msg;
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();
                        return;
                    }
                    _enumNameProp.stringValue = prevName;
                }
                EditorGUILayout.PropertyField(_poolingNameProp);

                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.LabelField("설명");
                    _description.stringValue = EditorGUILayout.TextArea(_description.stringValue, _textAreaStyle, GUILayout.Height(70));
                }
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.PrefixLabel("PoolSettings");
                    EditorGUILayout.PropertyField(_poolCountProp, GUIContent.none);
                    EditorGUILayout.PropertyField(_prefabProp, GUIContent.none);
                }
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties(); // 내가 변경한걸 반영
    }
}
