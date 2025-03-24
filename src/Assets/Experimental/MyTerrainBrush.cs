

namespace QS.Exp
{
    public class MyTerrainBrush
    {
        MyTerrainData terrainData;

        public MyTerrainBrush(MyTerrainData terrainData)
        {
            this.terrainData = terrainData;
        }
        public void Paint(int row, int col, float v, int brushSize)
        {
            int w = terrainData.width;
            int h = terrainData.height;

            for (int brow = -brushSize; brow <= brushSize; brow++)
            {
                for (int bcol = -brushSize; bcol <= brushSize; brushSize++)
                {
                    {
                        int prow = row + brow;
                        int pcol = col + bcol;
                        if (prow >= 0 && prow <= h
                        && pcol >= 0 && pcol <= w)
                        {
                            terrainData.heightMap[prow, pcol] = v;
                        }
                    }
                }
            }
        }
    }
}