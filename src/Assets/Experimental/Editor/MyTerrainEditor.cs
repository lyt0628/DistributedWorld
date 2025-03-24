

using UnityEditor;
using UnityEngine;

namespace QS.Exp
{
    [CustomEditor(typeof(MyTerrianProxy))]
    public class MyTerrainEditor : Editor
    {
        MyTerrianProxy m_terrainProxy;
        MyTerrainData m_terrainData;
        MyTerrainBrush m_Brush;

        private void OnEnable()
        {
            m_terrainProxy = (MyTerrianProxy)target;
            m_terrainData = m_terrainProxy.terrainData;
            m_Brush = new MyTerrainBrush(m_terrainData);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Generate Mesh"))
            {
                m_terrainProxy.GenerateMesh();
            }
        }
    }
}