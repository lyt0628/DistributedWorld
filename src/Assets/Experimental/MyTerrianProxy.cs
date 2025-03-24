using UnityEngine;

namespace QS.Exp
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class MyTerrianProxy : MonoBehaviour
    {

        public MyTerrainData terrainData { get; private set; }
        MeshFilter meshFilter;
        MeshRenderer meshRenderer;


        private void OnEnable()
        {
            terrainData = new MyTerrainData(512, 512, new float[512, 512]);
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();

            meshRenderer.material = new Material(Shader.Find("Standard"));
            GenerateMesh();
        }

        public void GenerateMesh()
        {
            int w = terrainData.width;
            int h = terrainData.height;
            Mesh mesh = new();
            var vertices = new Vector3[w * h];
            var uv = new Vector2[w * h];
            int[] triangles = new int[(w - 1) * (h - 1) * 6];

            int verticeIndex = 0;
            int triangleIndex = 0;

            for (int row = 0; row < h; row++)
            {
                for (int col = 0; col < w; col++)
                {
                    float verticeHeight = 10f;
                    vertices[verticeIndex] = new Vector3(row, verticeHeight, col);
                    uv[verticeIndex] = new Vector2(row / (h - 1), col / (w - 1));
                    verticeIndex++;
                }
            }

            for (int row = 0; row < h - 1; row++)
            {
                for (int col = 0; col < w - 1; col++)
                {
                    int topLeft = row * w + col;
                    int topRight = topLeft + 1;
                    int bottomLeft = (row + 1) * w + col;
                    int bottomRight = bottomLeft + 1;

                    triangles[triangleIndex++] = topLeft;
                    triangles[triangleIndex++] = bottomLeft;
                    triangles[triangleIndex++] = topRight;
                    triangles[triangleIndex++] = topRight;
                    triangles[triangleIndex++] = bottomLeft;
                    triangles[triangleIndex++] = bottomRight;
                }
            }

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            meshFilter.mesh = mesh;
        }
    }
}