using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT_2020_L04_Graph
{
    internal static class Data
    {
    }

    internal class Point3D
    {
        public double x;
        public double y;
        public double z;
        public Point3D(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public double[] xyz()
        {
            double[] XYZ = new double[3] { x, y, z };
            return XYZ;
        }
    }
    internal static class Generate
    {
        public static Point3D[] DotPoints(int numPoints)
        {
            Point3D[] points = new Point3D[numPoints];
            for (int i = 0; i < numPoints; i++)
            {
                points[i] = new Point3D(0, 0, 0);
            }
            return points;
        }
        public static Point3D[] RandomPoints(int numPoints)
        {
            Random random = new Random();
            Point3D[] points = new Point3D[numPoints];
            for (int i = 0; i < numPoints; i++)
            {
                double x = random.NextDouble() * 2 - 1;
                double y = random.NextDouble() * 2 - 1;
                double z = random.NextDouble() * 2 - 1;
                points[i] = new Point3D(x, y, z);
            }
            return points;
        }

        public static Point3D[] SpinPoints(int numPoints)
        {
            Random random = new Random();
            Point3D[] points = new Point3D[numPoints];
            for (int i = 0; i < numPoints; i++)
            {
                double x = 0.7 * Math.Cos(6 * Math.PI * ((double)i / (double)numPoints));
                double y = 0.6 * Math.Sin(4 * Math.PI * ((double)i / (double)numPoints));
                double z = -1 + 2 * ((double)i / (double)numPoints);
                points[i] = new Point3D(x, y, z);
            }
            return points;
        }

        public static Point3D[] RandomaisePoints(Point3D[] points, double stdDev)
        {
            Random rand = new Random();
            foreach (Point3D point3D in points)
            {
                point3D.x += RandomNormal(0, stdDev, rand);
                point3D.y += RandomNormal(0, stdDev, rand);
                point3D.z += RandomNormal(0, stdDev, rand);
            }
            return points;
        }
        public static double RandomNormal(double mean, double stdDev, Random rand)
        {
            double u1 = 1.0 - rand.NextDouble(); // случайное число от 0 до 1
            double u2 = 1.0 - rand.NextDouble(); // случайное число от 0 до 1
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                Math.Sin(2.0 * Math.PI * u2); // применяем формулу Бокса-Мюллера
            double randNormal = mean + stdDev * randStdNormal;
            return randNormal;
        }
    }
}
