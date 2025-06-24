using System;
using System.Net.Http.Headers;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace GravitySim
{    
    public struct VectorInt
    {
        public int X;
        public int Y;
        public int Z;

        public float Length => VectorInt.Distance(new VectorInt(0, 0, 0), this);

        public VectorInt(int X, int Y, int Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;

        }


        public VectorInt(Vector3 position)
        {
            this.X = (int)position.X;
            this.Y = (int)position.Y;
            this.Z = (int)position.Z;
        }



        public static bool operator ==(VectorInt a, VectorInt b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        public static bool operator !=(VectorInt a, VectorInt b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z;
        }

        public static VectorInt operator *(VectorInt a, float b)
        {
            return new VectorInt((int)(a.X * b), (int)(a.Y * b), (int)(a.Z * b));
        }


        public static VectorInt operator *(VectorInt a, VectorInt b)
        {
            return new VectorInt(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static VectorInt operator +(VectorInt a, VectorInt b)
        {
            return new VectorInt(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static VectorInt operator -(VectorInt a, VectorInt b)
        {
            return new VectorInt(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public VectorInt Normalized()
        {
            float length = this.Length;

            int newX = (int)(X / length);
            int newY = (int)(Y / length);
            int newZ = (int)(Z / length);

            return new VectorInt(newX, newY, newZ);
        }

        public static float Distance(VectorInt pos1, VectorInt pos2)
        {
            return (int)Math.Sqrt(DistanceSquared(pos1, pos2));
        }

        public static int DistanceSquared(VectorInt pos1, VectorInt pos2)
        {
            return ((pos2.X - pos1.X) * (pos2.X - pos1.X)) + ((pos2.Y - pos1.Y) * (pos2.Y - pos1.Y));
        }

        public override readonly bool Equals(object? obj)
        {
            if (obj is VectorInt v2 && obj != null)
            {
                return v2 == this;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
       
    }
     
}