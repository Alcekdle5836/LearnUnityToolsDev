using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosFrustum : MonoBehaviour
{
    public Camera _mainCamera;
    private void OnDrawGizmos()
    {
        if(_mainCamera == null)
            _mainCamera = Camera.main;
        Gizmos.color = Color.green;
        
        //设置gizmos的矩阵
        Gizmos.matrix = Matrix4x4.TRS(_mainCamera.transform.position, _mainCamera.transform.rotation, Vector3.one);
        Gizmos.DrawFrustum(Vector3.zero, _mainCamera.fieldOfView, _mainCamera.farClipPlane, _mainCamera.nearClipPlane, _mainCamera.aspect);
    }
}