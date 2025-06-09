using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneExtension : MonoBehaviour
{
    
}

#if UNITY_EDITOR
[CustomEditor(typeof(SceneExtension))]
public class SceneExtensionEditor : Editor
{
    private void OnSceneGUI()
    {
        SceneExtension _target = target as SceneExtension;

        Handles.Label(_target.transform.position,_target.transform.name + " : " + _target.transform.position);
    }
}

#endif