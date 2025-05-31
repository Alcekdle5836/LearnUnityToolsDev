using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuItemExample : EditorWindow
{
    // ----------------- 新建菜单 -----------------
    [MenuItem("Tools/Custome Menu Function")]
    public static void NewMenuFunction()
    {
        Debug.Log("Custome Menu Function");
    }

    // Assets
    [MenuItem("Assets/Assets Menu Function")]
    public static void AssetsMenuFunction()
    {
        Debug.Log("Assets Menu Function");
    }

    // Component
    [MenuItem("Component/Component Menu Function")]
    public static void ComponenttMenuFunction()
    {
        Debug.Log("Component Menu Function");
    }

    // ----------------- 优先级设置 -----------------
    // 不填写priority默认为1000，数字越小越靠前
    // 当菜单之间的优先级相差大于11时，菜单之间会出现分隔符
    // Inspector / Hierarchy / GameObject
    [MenuItem("GameObject/GameObject Menu Function", priority = 1000)]
    public static void GameObjectMenuFunction()
    {
        Debug.Log("GameObject Menu Function");
    }

    [MenuItem("GameObject/GameObject Menu Function2", priority = 1011)]
    public static void GameObjectMenuFunction2()
    {
        Debug.Log("GameObject Menu Function");
    }

    // ----------------- 有效性 -----------------
    // 验证函数是否启用
    [MenuItem("Tools/Log1", false, 990)]
    public static void Log1()
    {
        Debug.Log("2");
    }

    [MenuItem("Tools/Log2", false, 1001)]
    public static void Log2()
    {
        Debug.Log("2");
    }

    [MenuItem("Tools/Log2", true, 1001)]
    public static bool ValidateLog2()
    {
        return Selection.activeTransform != null;
    }

    // ----------------- 添加快捷键 -----------------
    [MenuItem("Tools/ShortCut &s")]
    public static void ShortCut()
    {
        Debug.Log("ShortCut!!!");
    }
}
