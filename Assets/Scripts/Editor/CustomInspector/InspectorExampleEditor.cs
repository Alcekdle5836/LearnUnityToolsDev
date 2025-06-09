using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 第一种UI绘制方式
/// </summary>
///
// UnityEditor Attribute
// 用于指定自定义编辑器类 InspectorExampleEditor 关联的目标类 InspectorExample。
// 当选中 InspectorExample 类型的对象时，Unity 会使用 InspectorExampleEditor 类来绘制自定义的 Inspector 界面
// 而不是默认的 Inspector 界面。
[CustomEditor(typeof(InspectorExample))]
public class InspectorExampleEditor : UnityEditor.Editor
{
    //target指该编辑器类绘制的目标类，需要将它强转为目标类
    private InspectorExample Target { get { return target as InspectorExample; } }

    public override void OnInspectorGUI()
    {
        // 均可表示未重写状态UI
        // base.OnInspectorGUI();
        // DrawDefaultInspector();
        
        // GUI重写
        Target.floatValue = EditorGUILayout.Slider(new GUIContent("FloatValue"), Target.floatValue, 0, 10);
        EditorGUILayout.BeginHorizontal();
        Target.intValue = EditorGUILayout.IntField("IntValue", Target.intValue);
        Target.boolValue = EditorGUILayout.Toggle("BoolValue", Target.boolValue);
        EditorGUILayout.EndHorizontal();
        Target.floatValue = EditorGUILayout.FloatField("FloatValue", Target.floatValue);
        Target.stringValue = EditorGUILayout.TextField("StringValue", Target.stringValue);
        Target.vector3Value = EditorGUILayout.Vector3Field("Vector3Value", Target.vector3Value);
        Target.enumValue = (Course) EditorGUILayout.EnumPopup("EnumValue", (Course) Target.enumValue);
        Target.colorValue = EditorGUILayout.ColorField(new GUIContent("ColorValue"), Target.colorValue);
        Target.textureValue = (Texture) EditorGUILayout.ObjectField("TextureValue", Target.textureValue, typeof(Texture), true);

        if (GUILayout.Button("ClickMe"))
        {
            Debug.Log("ButtonClicked");
        }
    }
}

/// <summary>
/// 第二种重写UI的方式
/// </summary>
// [CustomEditor(typeof(InspectorExample))]
// public class InspectorExampleEditor : Editor
// {
//     // 定义序列化属性
//     private SerializedProperty _intValue;
//     private SerializedProperty _floatValue;
//     private SerializedProperty _stringValue;
//     private SerializedProperty _boolValue;
//     private SerializedProperty _vector3Value;
//     private SerializedProperty _enumValue;
//     private SerializedProperty _colorValue;
//     private SerializedProperty _textureValue;
//
//     private void OnEnable()
//     {
//         //通过名字查找被序列化属性。
//         _intValue = serializedObject.FindProperty("intValue");
//         _floatValue = serializedObject.FindProperty("floatValue");
//         _stringValue = serializedObject.FindProperty("stringValue");
//         _boolValue = serializedObject.FindProperty("boolValue");
//         _vector3Value = serializedObject.FindProperty("vector3Value");
//         _enumValue = serializedObject.FindProperty("enumValue");
//         _colorValue = serializedObject.FindProperty("colorValue");
//         _textureValue = serializedObject.FindProperty("textureValue");
//     }
//     
//     public override void OnInspectorGUI()
//     {
//         // 更新序列化物体
//         serializedObject.Update();
//         EditorGUILayout.PropertyField(_intValue);
//         EditorGUILayout.PropertyField(_boolValue);
//         EditorGUILayout.PropertyField(_floatValue);
//         EditorGUILayout.PropertyField(_stringValue);
//         EditorGUILayout.PropertyField(_vector3Value);
//         EditorGUILayout.PropertyField(_enumValue);
//         EditorGUILayout.PropertyField(_colorValue);
//         EditorGUILayout.PropertyField(_textureValue);
//         
//         //应用属性值修改，否则Inspector面板的值无法修改
//         serializedObject.ApplyModifiedProperties();
//     }
// }