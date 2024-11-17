using System;
using UnityEngine;

namespace DefaultNamespace

/*
 * Calculated and universal constants used in our projects
 */
{
    public class Constants
    {
        // Schwarzschild radius (km)
        public static double RS = 1.92 * Math.Pow(10, 11);
        public static double RS_Unity = 5;
        // The scale from km to unity (the black hole is always 5 unity in radius)
        public static double SCALE = RS_Unity / RS;
        // universal gravitational constant
        public static double G = 6.67430 * Math.Pow(10, -11);
        public static double G_Unity = G * SCALE;
        public static double M = 1.314 * Math.Pow(10, 41);
        public static double M_Unity = M * SCALE;
        
        public static int v_light = 3 * (int) Math.Pow(10, 8);
        
    }
}
