using UnityEngine;
using FlowPathfinding;

namespace FlowPathfinding
{
    public class FlowField
    {
        public Grid2D<Cell> grid { get; private set; }
        public Vector2Int gridSize { get; private set; }
        public float cellRadius { get; private set; }
        public float cellDiameter => cellRadius * 2f;

        public FlowField(int width, int height, float cellRadius)
        {
            this.gridSize = new Vector2Int(width, height);
            this.cellRadius = cellRadius;
            grid = new Grid2D<Cell>(width, height);
            CreateGrid();
        }

        // Create the empty grid of Cells
        private void CreateGrid()
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    Vector3 worldPos = new Vector3(x * cellDiameter + cellRadius, 0, y * cellDiameter + cellRadius);
                    Cell cell = new Cell(worldPos, new Vector2Int(x, y));
                    grid.SetCell(x, y, cell);
                }
            }
        }

        // Scan the world to determine the cost of cells
        public void CreateCostField(LayerMask impassableLayer, LayerMask mudLayer, LayerMask waterLayer, LayerMask fireLayer)
        {
            // OverlapBox is smaller than cell to avoid hitting walls on borders
            Vector3 halfExtents = new Vector3(cellRadius - 0.1f, cellRadius - 0.1f, cellRadius - 0.1f);

            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    Cell cell = grid.GetCell(x, y);

                    cell.cost = (byte)TerrainCost.NormalGround;

                    // Priority 1 Impassable - Check first, if is a wall, no need to check others
                    if (Physics.CheckBox(cell.worldPos, halfExtents, Quaternion.identity, impassableLayer))
                    {
                        cell.cost = (byte)TerrainCost.Impassable;
                        continue; 
                    }

                    // Priority 2 Fire
                    if (Physics.CheckBox(cell.worldPos, halfExtents, Quaternion.identity, fireLayer))
                    {
                        cell.IncreaseCost((byte)TerrainCost.Fire);
                        continue;
                    }

                    // Priority 3 Water
                    if (Physics.CheckBox(cell.worldPos, halfExtents, Quaternion.identity, waterLayer))
                    {
                        cell.IncreaseCost((byte)TerrainCost.Water);
                        continue;
                    }

                    // Priority 4 Mud
                    if (Physics.CheckBox(cell.worldPos, halfExtents, Quaternion.identity, mudLayer))
                    {
                        cell.IncreaseCost((byte)TerrainCost.Mud);
                        continue;
                    }
                }
            }
        }
    }
}
