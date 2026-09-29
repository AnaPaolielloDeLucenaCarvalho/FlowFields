using UnityEngine;

namespace FlowPathfinding
{
    // 2D grid structure backed by 1D array
    public class Grid2D<T>
    {
        private T[] gridArray;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Grid2D(int width, int height)
        {
            Width = width;
            Height = height;
            gridArray = new T[width * height];
        }

        // Convert 2D coordinates to 1D array index (y * width + x)
        public int GetIndex(int x, int y)
        {
            return y * Width + x;
        }
        
        public int GetIndex(Vector2Int gridIndex)
        {
            return GetIndex(gridIndex.x, gridIndex.y);
        }

        // Check if x,y is within bounds of grid
        public bool IsValidCoordinate(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }
        
        public bool IsValidCoordinate(Vector2Int gridIndex)
        {
            return IsValidCoordinate(gridIndex.x, gridIndex.y);
        }

        // Get value at (x, y)
        public T GetCell(int x, int y)
        {
            if (IsValidCoordinate(x, y))
            {
                return gridArray[GetIndex(x, y)];
            }
            return default;
        }
        
        public T GetCell(Vector2Int gridIndex)
        {
            return GetCell(gridIndex.x, gridIndex.y);
        }

        // Set value at (x, y)
        public void SetCell(int x, int y, T value)
        {
            if (IsValidCoordinate(x, y))
            {
                gridArray[GetIndex(x, y)] = value;
            }
        }
        
        public void SetCell(Vector2Int gridIndex, T value)
        {
            SetCell(gridIndex.x, gridIndex.y, value);
        }
    }
}
