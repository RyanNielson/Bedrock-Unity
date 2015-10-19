using UnityEngine;
using System;
using System.Collections.Generic;

namespace Bedrock.DataTypes
{
    [Serializable]
    public class Grid<T>
    {
        [SerializeField]
        protected T[] values;

        [SerializeField]
        protected IntVector2 size;
        public IntVector2 Size
        {
            get { return size; }
            protected set { size = value; }
        }

        public int Width
        {
            get { return Size.X; }
        }

        public int Height
        {
            get { return Size.Y; }
        }

        public T this[int x, int y]
        {
            get { return values[y * Size.X + x]; }
            set { values[y * Size.X + x] = value; }
        }

        public Grid(int width, int height)
        {
            Reset(width, height);
        }

        public Grid(int width, int height, T value) : this(width, height)
        {
            Clear(value);
        }

        public void Clear()
        {
            values = new T[Width * Height];
        }

        public void Clear(T value)
        {
            SetRegion(0, 0, Width - 1, Height - 1, value);
        }

        public T Get(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return default(T);

            return this[x, y];
        }

        public bool Set(int x, int y, T value)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                this[x, y] = value;
                return true;
            }

            return false;
        }

        public List<T> AdjacentCells(int x, int y)
        {
            return new List<T>()
            {
                Get(x + 1, y), Get(x + 1, y + 1), Get(x + 1, y - 1),
                Get(x - 1, y), Get(x - 1, y + 1), Get(x - 1, y - 1),
                Get(x, y + 1), Get(x, y - 1)
            };
        }

        public void SetRegion(int x1, int y1, int x2, int y2, T value)
        {
            for (int y = y1; y <= y2; y++)
            {
                for (int x = x1; x <= x2; x++)
                {
                    Set(x, y, value);
                }
            }
        }

        public void SetCircle(int x, int y, int radius, T value)
        {
            int tempX = radius;
            int tempY = 0;
            int radiusError = 1 - tempX;

            while (tempX >= tempY)
            {
                SetRegion(x - tempX, y + tempY, x + tempX, y + tempY, value);
                SetRegion(x - tempY, y + tempX, x + tempY, y + tempX, value);
                SetRegion(x - tempX, y - tempY, x + tempX, y - tempY, value);
                SetRegion(x - tempY, y - tempX, x + tempY, y - tempX, value);

                tempY++;
                if (radiusError < 0)
                {
                    radiusError += 2 * tempY + 1;
                }
                else
                {
                    tempX--;
                    radiusError += 2 * (tempY - tempX) + 1;
                }
            }
        }

        public void SetLine(int x0, int y0, int x1, int y1, T value, int width = 1)
        {
            int xDist = Mathf.Abs(x1 - x0);
            int yDist = -Mathf.Abs(y1 - y0);
            int xStep = (x0 < x1 ? +1 : -1);
            int yStep = (y0 < y1 ? +1 : -1);
            int error = xDist + yDist;

            Set(x0, y0, value);

            while (x0 != x1 || y0 != y1)
            {
                if (2 * error - yDist > xDist - 2 * error)
                {
                    // Horizontal step
                    error += yDist;
                    x0 += xStep;
                }
                else
                {
                    // Vertical step
                    error += xDist;
                    y0 += yStep;
                }

                Set(x0, y0, value);

                if (width == 2)
                {
                    Set(x0 - 1, y0, value);
                    Set(x0 + 1, y0, value);
                }
            }
        }

        public void Resize(int width, int height)
        {
            Grid<T> oldGrid = (Grid<T>)this.MemberwiseClone();

            Reset(width, height);

            for (int y = 0; y < Height && y < oldGrid.Height; y++)
            {
                for (int x = 0; x < Width && x < oldGrid.Width; x++)
                {
                    this[x, y] = oldGrid[x, y];
                }
            }
        }

        private void Reset(int width, int height)
        {
            Size = new IntVector2(width, height);
            values = new T[width * height];
        }
    }
}