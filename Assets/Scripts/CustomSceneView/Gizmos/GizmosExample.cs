using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Gizmos.color作为全局的静态变量，为了防止此处修改的color对其他地方产生影响
/// 因此需要保证执行结束时，Gizmos.color 需要复原
/// </summary>
public class GizmosExample : MonoBehaviour
{
    //绘制效果一直显示
    private void OnDrawGizmos()
    {
        var color = Gizmos.color;
        Gizmos.color = Color.white;
        Gizmos.DrawCube(transform.position, Vector3.one);
        Gizmos.color = color;
    }
    
    //绘制效果在选中对象时显示
    private void OnDrawGizmosSelected()
    {
        var color = Gizmos.color;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
        Gizmos.color = color;
    }
}