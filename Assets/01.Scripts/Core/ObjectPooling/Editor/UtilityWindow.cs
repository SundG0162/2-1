using ObjectPooling;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public enum UtilType
{
    Pool,
    PowerUp,
    PowerEffect
}

public class UtilityWindow : EditorWindow
{
    private static int toolbarIdx = 0;
    private static Dictionary<UtilType, Vector2> scrollPosition
                                    = new Dictionary<UtilType, Vector2>();
    private static Dictionary<UtilType, Object> selectedItem
                                    = new Dictionary<UtilType, Object>();

    private static Vector2 editorScrollPosition; //�������� ��ũ�� ��ġ ����


    private string[] _toolbarNames;
    private Editor _cachedEditor;
    private Texture2D _selectTexture;
    private GUIStyle _selectBoxStyle;

    private string _poolingFolder = "Assets/08.SO/Pool";
    private PoolingTableSO _poolTable;

    [MenuItem("Tools/Utility")]
    private static void OpenWindow()
    {
        UtilityWindow window = GetWindow<UtilityWindow>("Utility");
        window.minSize = new Vector2(700, 500);
        window.Show();
    }

    private void OnEnable()
    {
        SetupUtility();
    }


    private void OnDisable()
    {
        DestroyImmediate(_cachedEditor);
        DestroyImmediate(_selectTexture);
    }

    private void SetupUtility()
    {
        _selectTexture = new Texture2D(1, 1); //1�ȼ�¥�� �ؽ��ĸ� ����ž�.
        _selectTexture.SetPixel(0, 0, new Color(0.31f, 0.40f, 0.50f));
        _selectTexture.Apply(); //����

        _selectBoxStyle = new GUIStyle();
        _selectBoxStyle.normal.background = _selectTexture;
        _selectTexture.hideFlags = HideFlags.DontSave;

        _toolbarNames = Enum.GetNames(typeof(UtilType));

        foreach (UtilType type in Enum.GetValues(typeof(UtilType)))
        {
            if (scrollPosition.ContainsKey(type) == false)
            {
                scrollPosition[type] = Vector2.zero;
            }
            if (selectedItem.ContainsKey(type) == false)
            {
                selectedItem[type] = null;
            }
        }

        bool isChanged = false;

        if (_poolTable == null)
        {
            _poolTable = AssetDatabase.LoadAssetAtPath<PoolingTableSO>($"{_poolingFolder}/table.asset");
            if (_poolTable == null) //���� �ش� ������ �������� �ʴ´�.
            {
                //�̰� �׳� �޸𸮻󿡸� ����ž�.
                _poolTable = ScriptableObject.CreateInstance<PoolingTableSO>();

                string filename = AssetDatabase.GenerateUniqueAssetPath(
                    $"{_poolingFolder}/table.asset");
                AssetDatabase.CreateAsset(_poolTable, filename);
                Debug.Log($"pooling table created at {filename}");
                isChanged = true;
            }
        }

        if (isChanged)
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    private void OnGUI()
    {
        toolbarIdx = GUILayout.Toolbar(toolbarIdx, _toolbarNames);
        EditorGUILayout.Space(5f);

        DrawContent(toolbarIdx);
    }

    private void DrawContent(int toolbarIdx)
    {
        switch (toolbarIdx)
        {
            case 0:
                DrawPooling();
                break;
        }
    }

    
    private void DrawPooling()
    {
        // ��� �޴� 2�� ��ư
        EditorGUILayout.BeginHorizontal();
        {
            GUI.color = new Color(0.19f, 0.76f, 0.08f);
            if (GUILayout.Button("Generate Item"))
            {
                Guid guid = Guid.NewGuid(); //������ ���ڿ��� �������ش�.

                PoolingItemSO newData = CreateInstance<PoolingItemSO>();
                newData.enumName = guid.ToString();

                AssetDatabase.CreateAsset(newData, $"{_poolingFolder}/Pool_{newData.enumName}.asset");
                _poolTable.datas.Add(newData);

                EditorUtility.SetDirty(_poolTable);
                AssetDatabase.SaveAssets();
            }

            GUI.color = new Color(0.81f, 0.13f, 0.18f);
            if (GUILayout.Button("Generate Enum file"))
            {
                GeneratePoolingEnumFile();
            }
        }
        EditorGUILayout.EndHorizontal();


        //����Ʈ ���� ���
        GUI.color = Color.white;
        EditorGUILayout.BeginHorizontal();
        {
            //���� ����Ʈ ���
            EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(300f));
            {
                EditorGUILayout.LabelField("Pooling List");
                EditorGUILayout.Space(3f);


                scrollPosition[UtilType.Pool] =
                EditorGUILayout.BeginScrollView(scrollPosition[UtilType.Pool], false, true, GUIStyle.none, GUI.skin.verticalScrollbar, GUIStyle.none);
                {
                    //���⿡ ��ũ�� ����
                    foreach (PoolingItemSO item in _poolTable.datas)
                    {
                        GUIStyle style = selectedItem[UtilType.Pool] == item
                                            ? _selectBoxStyle : GUIStyle.none;

                        EditorGUILayout.BeginHorizontal(style, GUILayout.Height(40f));
                        {
                            EditorGUILayout.LabelField(item.enumName,
                                        GUILayout.Width(240f), GUILayout.Height(40f));


                            //������ư
                            EditorGUILayout.BeginVertical();
                            {
                                EditorGUILayout.Space(10f);
                                GUI.color = Color.red;
                                if (GUILayout.Button("X", GUILayout.Width(20f)))
                                {
                                    _poolTable.datas.Remove(item);
                                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(item));
                                    EditorUtility.SetDirty(_poolTable);
                                    AssetDatabase.SaveAssets();
                                }
                                GUI.color = Color.white;
                            }
                            EditorGUILayout.EndVertical();
                        }
                        EditorGUILayout.EndHorizontal();

                        Rect lastRect = GUILayoutUtility.GetLastRect();

                        if (Event.current.type == EventType.MouseDown
                            && lastRect.Contains(Event.current.mousePosition))
                        {
                            editorScrollPosition = Vector2.zero;
                            selectedItem[UtilType.Pool] = item;
                            Event.current.Use(); //�̺�Ʈ�� �Ҹ���Ѽ� �ٸ��ֵ��� �������� �ʰ�
                        }

                        if (item == null)
                        {
                            break;
                        }
                    }
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();
            //End of pool list


            if (selectedItem[UtilType.Pool] != null)
            {
                editorScrollPosition =
                EditorGUILayout.BeginScrollView(editorScrollPosition);
                {
                    EditorGUILayout.Space(2f);
                    Editor.CreateCachedEditor(
                        selectedItem[UtilType.Pool], null, ref _cachedEditor);
                    _cachedEditor.OnInspectorGUI();
                }
                EditorGUILayout.EndScrollView();
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    private void GeneratePoolingEnumFile()
    {
        StringBuilder codeBuilder = new StringBuilder();

        foreach (PoolingItemSO item in _poolTable.datas)
        {
            codeBuilder.Append(item.enumName);
            codeBuilder.Append(", ");
        }

        string code = string.Format(CodeFormat.poolingTypeFormat, codeBuilder.ToString());

        string path = $"{Application.dataPath}/01.Scripts/Core/ObjectPooling";

        File.WriteAllText($"{path}/PoolingType.cs", code);

        AssetDatabase.Refresh();
    }
}

