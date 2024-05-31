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

    private static Vector2 editorScrollPosition; //에디터의 스크롤 위치 저장


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
        _selectTexture = new Texture2D(1, 1); //1픽셀짜리 텍스쳐를 만든거야.
        _selectTexture.SetPixel(0, 0, new Color(0.31f, 0.40f, 0.50f));
        _selectTexture.Apply(); //적용

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
            if (_poolTable == null) //현재 해당 파일이 존재하지 않는다.
            {
                //이건 그냥 메모리상에만 만든거야.
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
        // 상단 메뉴 2개 버튼
        EditorGUILayout.BeginHorizontal();
        {
            GUI.color = new Color(0.19f, 0.76f, 0.08f);
            if (GUILayout.Button("Generate Item"))
            {
                Guid guid = Guid.NewGuid(); //고유한 문자열을 생성해준다.

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


        //리스트 내용 출력
        GUI.color = Color.white;
        EditorGUILayout.BeginHorizontal();
        {
            //왼쪽 리스트 출력
            EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(300f));
            {
                EditorGUILayout.LabelField("Pooling List");
                EditorGUILayout.Space(3f);


                scrollPosition[UtilType.Pool] =
                EditorGUILayout.BeginScrollView(scrollPosition[UtilType.Pool], false, true, GUIStyle.none, GUI.skin.verticalScrollbar, GUIStyle.none);
                {
                    //여기에 스크롤 내용
                    foreach (PoolingItemSO item in _poolTable.datas)
                    {
                        GUIStyle style = selectedItem[UtilType.Pool] == item
                                            ? _selectBoxStyle : GUIStyle.none;

                        EditorGUILayout.BeginHorizontal(style, GUILayout.Height(40f));
                        {
                            EditorGUILayout.LabelField(item.enumName,
                                        GUILayout.Width(240f), GUILayout.Height(40f));


                            //삭제버튼
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
                            Event.current.Use(); //이벤트를 소모시켜서 다른애들이 반응하지 않게
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

