
using UnityEngine;

namespace Utilities
{
    public static class VectorExtensions
    {
        public static Vector3 SetX(this Vector3 @this,float xValue)
        {
            return new Vector3(xValue, @this.y, @this.z);
        }

        public static Vector3 SetY(this Vector3 @this,float yValue)
        {
            return new Vector3(@this.x, yValue, @this.z);
        }

        public static Vector3 SetZ(this Vector3 @this,float zValue)
        {
            return new Vector3(@this.x, @this.y, zValue);
        }
        
        public static Vector2 SetX(this Vector2 @this,float xValue)
        {
            return new Vector2(xValue, @this.y);
        }

        public static Vector2 SetY(this Vector2 @this,float yValue)
        {
            return new Vector2(@this.x, yValue);
        }

        public static Vector3 AddX(this Vector3 @this,float xValue)
        {
            return new Vector3(@this.x + xValue, @this.y, @this.z);
        }
        
        public static Vector3 AddZ(this Vector3 @this,float zValue)
        {
            return new Vector3(@this.x, @this.y, @this.z + zValue);
        }
        public static Vector3 AddY(this Vector3 @this,float yValue)
        {
            return new Vector3(@this.x, @this.y + yValue, @this.z);
        }
        public static Vector3 ToVector(this float @thisFloat)
        {
            return new Vector3(@thisFloat, @thisFloat, @thisFloat);
        }
        
    }
}
