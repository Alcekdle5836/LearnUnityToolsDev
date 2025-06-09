using UnityEditor;
using UnityEngine;

public class DrawGizmosTest
{
    //表示物体显示并且被选择的时候，绘制Gizmos
    [DrawGizmo(GizmoType.Active | GizmoType.Selected)]
    //第一个参数需要指定目标类，目标类需要挂载在场景对象中
    private static void MyCustomOnDrawGizmos(GizmosTest target, GizmoType gizmoType)
    {
        var color = Gizmos.color;
        Gizmos.color = Color.yellow;
        //target为挂载该组件的对象
        Gizmos.DrawWireSphere(target.transform.position, 5.0f);
        Gizmos.color = color;
    }
}