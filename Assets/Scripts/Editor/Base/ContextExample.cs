using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ContextExample : EditorWindow
{
    [MenuItem("CONTEXT/CSVMeshImporter/GetScriptInfo")]
    static void GetScriptInfo(MenuCommand menuCommand)
    {
        Debug.Log($"menuCommand.context: {menuCommand.context}");           // Object
        Debug.Log($"menuCommand.context.name: {menuCommand.context.name}"); // Object.name
        Debug.Log($"menuCommand.userData: {menuCommand.userData}");         // 0
    }

    // ----------------- 修改组件值 -----------------
    [MenuItem("CONTEXT/BoxCollider/SetDefaultSize _K", priority = 0)]
    static void SetDefaultSize(MenuCommand menuCommand)
    {
        BoxCollider collider = menuCommand.context as BoxCollider;
        if (collider != null)
        {
            Undo.RecordObject(collider, "Set BoxCollider Size");    // 支持ctrl z撤销
            collider.size = Vector3.one * 3;
            EditorUtility.SetDirty(collider);       // 保存更改
        }
    }
}
