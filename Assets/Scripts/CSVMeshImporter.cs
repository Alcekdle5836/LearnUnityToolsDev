using UnityEngine;
using System.Collections.Generic;
using System.IO;

// TODO:还没写完
public class CSVMeshImporter : MonoBehaviour
{
    [Header("CSV File")]
    [ContextMenuItem("Reset CSV File", "Reset")] // Removed invalid attribute
    public TextAsset csvFile;

    [Header("Mesh Settings")]
    public bool recalculateNormals = true;
    public bool generateCollider = true;

    private void Reset()
    {
        // 自动从Assets/Scripts/model文件夹加载第一个csv文件
        string[] guids = UnityEditor.AssetDatabase.FindAssets("t:TextAsset", new[] { "Assets/Scripts" });
        if (guids.Length > 0)
        {
            foreach (var item in guids)
            {
                string path = UnityEditor.AssetDatabase.GUIDToAssetPath(item);
                if (UnityEditor.AssetDatabase.GUIDToAssetPath(item).EndsWith(".csv"))
                    csvFile = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(path);
            }
        }
    }

    [ContextMenu("Generate Mesh")]
    public void GenerateMeshFromCSV()
    {
        if (csvFile == null)
        {
            Debug.LogError("CSV file not assigned!");
            return;
        }

        Mesh mesh = new Mesh();
        mesh.name = "ImportedMesh";

        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<Color> colors = new List<Color>();
        List<int> triangles = new List<int>();

        bool inVertexSection = false;
        bool inIndexSection = false;

        using (StringReader reader = new StringReader(csvFile.text))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                // 检测数据段开始
                if (line.StartsWith("VTX,"))
                {
                    inVertexSection = true;
                    inIndexSection = false;
                    continue;
                }
                else if (line.StartsWith("IDX,"))
                {
                    inVertexSection = false;
                    inIndexSection = true;
                    continue;
                }

                string[] values = line.Split(',');

                if (inVertexSection)
                {
                    // 解析顶点数据 (示例格式：位置,法线,UV,颜色)
                    if (values.Length >= 8) // 至少包含位置(x,y,z)和法线(x,y,z)
                    {
                        // 坐标系转换：RenderDoc(Y-up) -> Unity(Z-up)
                        Vector3 position = new Vector3(
                            float.Parse(values[1]),  // X
                            float.Parse(values[3]),  // Z (原Y)
                            float.Parse(values[2])); // Y (原Z)

                        Vector3 normal = new Vector3(
                            float.Parse(values[4]),
                            float.Parse(values[6]),
                            float.Parse(values[5]));

                        Vector2 uv = values.Length > 7 ?
                            new Vector2(float.Parse(values[7]), float.Parse(values[8])) :
                            Vector2.zero;

                        Color color = values.Length > 9 ?
                            new Color(
                                float.Parse(values[9]),
                                float.Parse(values[10]),
                                float.Parse(values[11]),
                                float.Parse(values[12])) :
                            Color.white;

                        vertices.Add(position);
                        normals.Add(normal);
                        uvs.Add(uv);
                        colors.Add(color);
                    }
                }
                else if (inIndexSection)
                {
                    // 解析索引数据（三角形列表）
                    if (values.Length >= 4) // 三个索引值 + 类型标识
                    {
                        triangles.Add(int.Parse(values[1]));
                        triangles.Add(int.Parse(values[2]));
                        triangles.Add(int.Parse(values[3]));
                    }
                }
            }
        }

        // 设置网格数据
        mesh.SetVertices(vertices);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
        mesh.SetColors(colors);
        mesh.SetTriangles(triangles, 0);

        if (recalculateNormals)
        {
            mesh.RecalculateNormals();
        }

        // 添加组件
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        if (meshFilter == null) meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        if (meshRenderer == null) meshRenderer = gameObject.AddComponent<MeshRenderer>();

        if (generateCollider)
        {
            MeshCollider collider = gameObject.GetComponent<MeshCollider>();
            if (collider == null) collider = gameObject.AddComponent<MeshCollider>();
            collider.sharedMesh = mesh;
        }
        Debug.Log("Mesh generated successfully! Vertex count: " + vertices.Count);
    }
}