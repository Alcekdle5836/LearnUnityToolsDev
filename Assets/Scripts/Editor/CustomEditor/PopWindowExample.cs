using UnityEngine;
using UnityEditor;
 
public class PopWindowExample : EditorWindow
{
    private Rect buttonRect;
    PopupExample example = new PopupExample();
 
    [MenuItem("Tools/PopupWindow Example",priority = 1202)]
    static void Open()
    {
        EditorWindow.GetWindow<PopWindowExample>().Show();
    }
 
    private void OnGUI()
    {
        GUILayout.Label("Popup example Derived From EditorWindow ", EditorStyles.boldLabel);       
        if (GUILayout.Button("File Option Popup", GUILayout.Width(200)))
        {
            PopupWindow.Show(buttonRect, example);
        }
        GUILayout.Label( "Current Selection: " + example.Str);
        if (Event.current.type == EventType.Repaint)
        {
            buttonRect = GUILayoutUtility.GetLastRect();
        }
    }
 
    //继承 PopupWindowContent
    public class PopupExample : PopupWindowContent
    {
        private bool canRead = true;
        private bool canWrite = true;
        private bool canSave = true;
 
        // 通过bool动态生成一个字符串，用于自动记录用户选择
        public string Str
        {
            // 属性 Str 提供了一个只读接口，用于外部访问用户的选择结果
            // get 是 C# 属性的访问器，用于定义属性的读取逻辑
            get
            {
                string str = "";
                if (canRead) { str += "R/"; }
                if (canWrite) { str += "W/"; }
                if (canSave) { str += "S"; }
                
                if (str == "")
                {
                    str = "Empty";
                }
                return str;
            }
        }
 
        public override Vector2 GetWindowSize()
        {
            return new Vector2(200, 150);
        }
        
        public override void OnGUI(Rect rect)
        {
            GUILayout.Label("Custom Popup Options", EditorStyles.boldLabel);
            canRead = EditorGUILayout.Toggle("canRead", canRead);
            canWrite = EditorGUILayout.Toggle("canWrite", canWrite);
            canSave = EditorGUILayout.Toggle("canSave", canSave);
        }
 
        //打开
        public override void OnOpen()
        {
            Debug.Log("Popup opened: " + this);
        }
 
        //关闭
        public override void OnClose()
        {
            Debug.Log("Popup closed: " + this);
        }
    }
}