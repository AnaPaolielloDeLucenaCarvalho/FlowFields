using UnityEngine;

namespace FlowPathfinding
{
    // Single tile in grid space
    public class Cell
    {
        // Positional Data
        public Vector2Int gridIndex; // X and Y position in 2D grid array
        public Vector3 worldPos; // Actual world position

        // Flow Field Data
        public byte cost; // Traverse cost (1 for ground, 255 for wall)
        public ushort bestCost; // Integration cost, distance to goal
        public Vector2 bestDirection;// Vector Field, direction to neighbour with lowest bestCost

        public Cell(Vector3 _worldPos, Vector2Int _gridIndex)
        {
            worldPos = _worldPos;
            gridIndex = _gridIndex;
            
            // Default
            cost = (byte)TerrainCost.NormalGround; //normal flat ground (2)
            bestCost = ushort.MaxValue; // 'infinity' (unreached)
            bestDirection = Vector2.zero;
        }

        // Helper to increase cost, max = 255 (Impassable)
        public void IncreaseCost(int amount)
        {
            if (cost == byte.MaxValue) return;
            
            if (amount + cost >= byte.MaxValue)
            {
                cost = byte.MaxValue;
            }
            else
            {
                cost += (byte)amount;
            }
        }
    }
}
