using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelectionExample : EditorWindow
{
    [MenuItem("Tools/GetSelectionLength")]
    static void GetSelectionLength()
    {
        Debug.Log(Selection.objects.Length);
    }

    [MenuItem("Tools/GetActiveGameObject")]
    static void GetActiveGameObject()
    {
        Debug.Log(Selection.activeTransform);
    }
}
