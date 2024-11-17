using UnityEngine;

namespace DefaultNamespace

/*
 * Calculated and universal constants used in our projects
 */
{
    public class Constants
    {
        // Schwarzschild radius (km)
        public static float RS = 1.92f * Mathf.Pow(10, 11);
        // The scale from km to unity (the black hole is always 5 unity in radius)
        public static float SCALE = 5 / RS;
        // universal gravitational constant
        public static double G = 6.67430 * Mathf.Pow(10, -11);
        
        public static int v_light = 3 * 10 ^ 8;
    }
}
