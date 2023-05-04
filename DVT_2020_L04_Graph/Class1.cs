using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph.Assets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

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
        public static List<Point3D> DotPoints(int numPoints)
        {
            List<Point3D> points = new List<Point3D>();
            for (int i = 0; i < numPoints; i++)
            {
                points.Add(new Point3D(0, 0, 0));
            }
            return points;
        }
        public static List<Point3D> RandomPoints(int numPoints)
        {
            Random random = new Random();
            List<Point3D> points = new List<Point3D>();
            for (int i = 0; i < numPoints; i++)
            {
                double x = random.NextDouble() * 2 - 1;
                double y = random.NextDouble() * 2 - 1;
                double z = random.NextDouble() * 2 - 1;
                points.Add(new Point3D(x, y, z));
            }
            return points;
        }

        public static List<Point3D> SpinPoints(int numPoints)
        {
            Random random = new Random();
            List<Point3D> points = new List<Point3D>();
            for (int i = 0; i < numPoints; i++)
            {
                double x = 0.7 * Math.Cos(6 * Math.PI * ((double)i / (double)numPoints));
                double y = 0.6 * Math.Sin(4 * Math.PI * ((double)i / (double)numPoints));
                double z = -1 + 2 * ((double)i / (double)numPoints);
                points.Add(new Point3D(x, y, z));
            }
            return points;
        }

        public static List<Point3D> RandomaisePoints(List<Point3D> points, double stdDev)
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

    internal static class Draw2D
    {
        public static Bitmap Triangle(Color color)
        {
            int width = 100;
            int height = 100;
            Bitmap bmp = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                Point[] points = new Point[]
                {
                    new Point(0, height),
                    new Point(width / 2, 0),
                    new Point(width, height)
                };

                using (Brush brush = new SolidBrush(color))
                {
                    g.FillPolygon(brush, points);
                }
            }

            return bmp;
        }
    }

    internal static class Draw
    {
        public static void Cube(OpenGL gl, double x, double y, double z, double size)
        {
            double halfSize = size / 2;

            gl.Begin(OpenGL.GL_QUADS);

            //front face
            gl.Vertex(x - halfSize, y - halfSize, z + halfSize);
            gl.Vertex(x + halfSize, y - halfSize, z + halfSize);
            gl.Vertex(x + halfSize, y + halfSize, z + halfSize);
            gl.Vertex(x - halfSize, y + halfSize, z + halfSize);

            //back face
            gl.Vertex(x - halfSize, y - halfSize, z - halfSize);
            gl.Vertex(x - halfSize, y + halfSize, z - halfSize);
            gl.Vertex(x + halfSize, y + halfSize, z - halfSize);
            gl.Vertex(x + halfSize, y - halfSize, z - halfSize);

            //top face
            gl.Vertex(x - halfSize, y + halfSize, z - halfSize);
            gl.Vertex(x - halfSize, y + halfSize, z + halfSize);
            gl.Vertex(x + halfSize, y + halfSize, z + halfSize);
            gl.Vertex(x + halfSize, y + halfSize, z - halfSize);

            //bottom face
            gl.Vertex(x - halfSize, y - halfSize, z - halfSize);
            gl.Vertex(x + halfSize, y - halfSize, z - halfSize);
            gl.Vertex(x + halfSize, y - halfSize, z + halfSize);
            gl.Vertex(x - halfSize, y - halfSize, z + halfSize);

            //left face
            gl.Vertex(x - halfSize, y - halfSize, z - halfSize);
            gl.Vertex(x - halfSize, y + halfSize, z - halfSize);
            gl.Vertex(x - halfSize, y + halfSize, z + halfSize);
            gl.Vertex(x - halfSize, y - halfSize, z + halfSize);

            //right face
            gl.Vertex(x + halfSize, y - halfSize, z - halfSize);
            gl.Vertex(x + halfSize, y + halfSize, z - halfSize);
            gl.Vertex(x + halfSize, y + halfSize, z + halfSize);
            gl.Vertex(x + halfSize, y - halfSize, z + halfSize);

            gl.End();
        }

        public static void SquarePlane(OpenGL gl, double size, double x, double y, double z, string direction, Bitmap bitmap)
        {
            // Вычисление координат вершин плоскости
            double halfSize = size / 2.0;
            double x1 = x - halfSize;
            double x2 = x + halfSize;
            double y1 = y - halfSize;
            double y2 = y + halfSize;
            double z1 = z - halfSize;
            double z2 = z + halfSize;


            // создание текстуры
            uint[] textures = new uint[1];
            gl.GenTextures(1, textures);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, textures[0]);

            // настройка параметров текстуры
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_CLAMP_TO_EDGE);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_CLAMP_TO_EDGE);

            // загрузка данных в текстуру
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            gl.TexImage2D(OpenGL.GL_TEXTURE_2D, 0, OpenGL.GL_RGBA, bitmap.Width, bitmap.Height, 0, OpenGL.GL_BGRA, OpenGL.GL_UNSIGNED_BYTE, data.Scan0);
            bitmap.UnlockBits(data);

            // отрисовка прямоугольника с использованием текстуры
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, textures[0]);
            gl.Begin(OpenGL.GL_QUADS);

            // Задание координат вершин плоскости
            if (direction == "x")
            {
                 gl.TexCoord(0.0f, 0.0f);gl.Vertex(x, y1, z1);
                 gl.TexCoord(0.0f, 1.0f);gl.Vertex(x, y1, z2);
                 gl.TexCoord(1.0f, 1.0f);gl.Vertex(x, y2, z2);
                 gl.TexCoord(1.0f, 0.0f);gl.Vertex(x, y2, z1);
            }
            else if (direction == "y")
            {
                gl.TexCoord(0.0f, 0.0f);gl.Vertex(x1, y, z1);
                gl.TexCoord(0.0f, 1.0f);gl.Vertex(x1, y, z2);
                gl.TexCoord(1.0f, 1.0f);gl.Vertex(x2, y, z2);
                gl.TexCoord(1.0f, 0.0f); gl.Vertex(x2, y, z1);
            }
            else if (direction == "z")
            {
                gl.TexCoord(0.0f, 0.0f); gl.Vertex(x1, y1, z);
                gl.TexCoord(0.0f, 1.0f); gl.Vertex(x1, y2, z);
                gl.TexCoord(1.0f, 1.0f); gl.Vertex(x2, y2, z);
                gl.TexCoord(1.0f, 0.0f); gl.Vertex(x2, y1, z);
            }

            // Конец отрисовки плоскости с текстурой
            gl.End();
            gl.Disable(OpenGL.GL_TEXTURE_2D);
            gl.DeleteTextures(1, textures);
            bitmap.Dispose();

        }
    }
}
