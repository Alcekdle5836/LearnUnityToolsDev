using UnityEditor;
using UnityEngine;

public class EditorWindowExample : EditorWindow
{
    // CSV File
    private Object csvFile;

    // Mesh Settings
    private bool recalculateNormals = false;
    private bool generateCollider = false;

    // 这里的方法名可以换成其他任何，只需保证方法为静态，且返回类型为void即可完成MenuItem绑定
    // MenuItem要求绑定的办法是静态的
    [MenuItem("Tools/EditorWindow Example",priority = 1201)]
    static void ShowWindow()
    {
        EditorWindowExample window = GetWindow<EditorWindowExample>();          // 获取窗口实例
        window.titleContent = new GUIContent("EditorWindow Example");        // 设置窗口标题
        window.Show();
    }

    private void OnGUI()
    {
        // Script field (disabled)
        EditorGUI.BeginDisabledGroup(true);        // true表示启用禁用状态，组内的空间变为只读，无法编辑
        // ObjectField，在编辑器中绘制一个对象选择字段。允许用户在编辑器中选择特定类型的对象
        EditorGUILayout.ObjectField("Script", MonoScript.FromScriptableObject(this), typeof(MonoScript), false);
        EditorGUI.EndDisabledGroup();
        
        // 这段代码的作用是显示一个只读的对象字段，字段中显示的是当前脚本文件（EditorWindowExample 的脚本资源）。通过 BeginDisabledGroup 和 EndDisabledGroup 包裹，这个字段是不可编辑的，仅用于展示脚本信息
        // MonoScript.FromScriptableObject(this):
            // MonoScript 是 Unity 中表示脚本资源的类。
            // FromScriptableObject(this) 将当前 EditorWindow 实例转换为对应的脚本资源。
        // typeof(MonoScript):限制字段只能接受 MonoScript 类型的对象。
        // false:是否允许拖放场景中的对象。false 表示不允许。
            
        EditorGUILayout.Space();
    
        // CSV File section
        EditorGUILayout.LabelField("CSV File", EditorStyles.boldLabel);
        csvFile = EditorGUILayout.ObjectField("Csv File", csvFile, typeof(Object), false);
    
        EditorGUILayout.Space();
    
        // Mesh Settings section
        EditorGUILayout.LabelField("Mesh Settings", EditorStyles.boldLabel);
        recalculateNormals = EditorGUILayout.Toggle("Recalculate Normals", recalculateNormals);
        generateCollider = EditorGUILayout.Toggle("Generate Collider", generateCollider);
    }
}
