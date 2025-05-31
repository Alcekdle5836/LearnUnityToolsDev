using UnityEditor;
using UnityEngine;

public class ScriptableWizardExample : ScriptableWizard
{
    [MenuItem("Tools/ScriptableWizard Example",priority = 1200)]
    static void ShowWindow()
    {
        // 对话框类型，对话框名字
        ScriptableWizard.DisplayWizard<ScriptableWizardExample>("CSV转fbx", "Convert", "ResetCSVFile");
    }

    [Header("CSV File")]
    public TextAsset csvFile;

    [Header("Mesh Settings")]
    public bool recalculateNormals = false;
    public bool generateCollider = false;

    private void Reset()
    {
        // 自动从Assets/Scripts/model文件夹加载第一个csv文件
        string[] guids = AssetDatabase.FindAssets("t:TextAsset", new[] { "Assets/Scripts" });
        if (guids.Length > 0)
        {
            foreach (var item in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(item);
                if (path.EndsWith(".csv"))
                    csvFile = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
            }
        }
    }

    private void OnWizardCreate()
    {
        Debug.Log("Convert Called");
    }

    private void OnWizardOtherButton()
    {
        Reset();
        Debug.Log("Reset CSVFile Called");
    }

    // 更新时调用
    private void OnWizardUpdate()
    {
        errorString = null;
        helpString = null;

        if (Selection.gameObjects.Length > 0)
        {
            helpString = "当前选择" + Selection.gameObjects.Length + "个物体";
        }
        else
        {
            errorString = "(提示示例) - 请选择至少一个物体";
        }
    }

    private void OnSelectionChange()
    {
        OnWizardUpdate();
        Repaint();
    }
}
