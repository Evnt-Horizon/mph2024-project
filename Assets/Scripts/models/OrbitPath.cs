using UnityEngine;

namespace DefaultNamespace.models
{
    [System.Serializable]
    public class OrbitPath
    {
        public float xAxis;
        public float zAxis;
        
        public OrbitPath(float xAxis, float zAxis)
        {
            this.xAxis = xAxis;
            this.zAxis = zAxis;
        }
        
        public Vector2 Evaluate(float t)
        {
            float angle = Mathf.Deg2Rad * 360 * t;
            float x = Mathf.Sin(angle) * xAxis;
            float z = Mathf.Cos(angle) * zAxis;
            return new Vector2(x, z);
        }
    }
}
