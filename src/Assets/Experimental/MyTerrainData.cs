

namespace QS.Exp
{
    public class MyTerrainData
    {
        public int width;
        public int height;
        public float[,] heightMap;

        public MyTerrainData(int width, int height, float[,] heightMap)
        {
            this.width = width;
            this.height = height;
            this.heightMap = heightMap;
        }
    }
}