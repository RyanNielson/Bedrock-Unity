using System;
using UnityEngine;

namespace RyanNielson.Bedrock
{
    [Serializable]
    public struct IntVector2
    {
        [SerializeField]
        private int x;
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        [SerializeField]
        private int y;
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public IntVector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int SqrMagnitude
        {
            get { return x * x + y * y; }
        }

        public float Magnitude
        {
            get { return Mathf.Sqrt(SqrMagnitude); }
        }

        public static IntVector2 operator +(IntVector2 a, IntVector2 n)
        {
            return new IntVector2(a.X + n.X, a.Y + n.Y);
        }

        public static IntVector2 operator -(IntVector2 a, IntVector2 b)
        {
            return new IntVector2(a.X - b.X, a.Y - b.Y);
        }

        public static IntVector2 operator *(IntVector2 a, int d)
        {
            return new IntVector2(a.X * d, a.Y * d);
        }

        public static IntVector2 operator /(IntVector2 a, int d)
        {
            return new IntVector2(a.X / d, a.Y / d);
        }

        public static explicit operator Vector2(IntVector2 v)
        {
            return new Vector2(v.X, v.Y);
        }

        public static explicit operator Vector3(IntVector2 v)
        {
            return new Vector3(v.X, v.Y, 0f);
        }
    }
}