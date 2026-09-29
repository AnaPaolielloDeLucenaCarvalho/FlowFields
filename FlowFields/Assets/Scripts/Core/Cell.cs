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
        public int cost; // Traverse cost (1 for ground, 255 for wall)
        public int bestCost; // Integration cost, distance to goal
        public Vector2 bestDirection;// Vector Field, direction to neighbour with lowest bestCost

        public Cell(Vector3 _worldPos, Vector2Int _gridIndex)
        {
            worldPos = _worldPos;
            gridIndex = _gridIndex;
            
            // Default
            cost = 1;
            bestCost = int.MaxValue;
            bestDirection = Vector2.zero;
        }

        // Helper to increase cost, max = 255 (Impassable)
        public void IncreaseCost(int amount)
        {
            if (cost == int.MaxValue) return;
            
            if (amount + cost >= int.MaxValue)
            {
                cost = int.MaxValue;
            }
            else
            {
                cost += (int)amount;
            }
        }
    }
}
