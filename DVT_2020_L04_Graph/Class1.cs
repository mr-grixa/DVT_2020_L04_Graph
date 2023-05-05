using SharpGL;
using SharpGL.SceneGraph;
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
                gl.TexCoord(0.0f, 0.0f); gl.Vertex(x, y1, z1);
                gl.TexCoord(0.0f, 1.0f); gl.Vertex(x, y1, z2);
                gl.TexCoord(1.0f, 1.0f); gl.Vertex(x, y2, z2);
                gl.TexCoord(1.0f, 0.0f); gl.Vertex(x, y2, z1);
            }
            else if (direction == "y")
            {
                gl.TexCoord(0.0f, 0.0f); gl.Vertex(x1, y, z1);
                gl.TexCoord(0.0f, 1.0f); gl.Vertex(x1, y, z2);
                gl.TexCoord(1.0f, 1.0f); gl.Vertex(x2, y, z2);
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

        public static void Rectangle(OpenGL gl, double x, double y, double z, double widthX, double widthY, double widthZ)
        {
            // Вычисляем координаты углов прямоугольника
            double x1 = x - widthX;
            double y1 = y - widthY;
            double z1 = z - widthZ;
            double x2 = x + widthX;
            double y2 = y + widthY;
            double z2 = z + widthZ;

            // Рисуем прямоугольник
            gl.Begin(OpenGL.GL_LINES);
            ////
            gl.Vertex(x1, y1, z1);
            gl.Vertex(x1, y1, z2);
            gl.Vertex(x1, y2, z1);
            gl.Vertex(x1, y2, z2);
            // Грань X1
            gl.Vertex(x1, y1, z1);
            gl.Vertex(x1, y2, z1);
            gl.Vertex(x1, y2, z2);
            gl.Vertex(x1, y1, z2);
            ////
            gl.Vertex(x2, y2, z1);
            gl.Vertex(x2, y2, z2);
            gl.Vertex(x2, y1, z1);
            gl.Vertex(x2, y1, z2);
            //// Грань X2
            gl.Vertex(x2, y1, z1);
            gl.Vertex(x2, y2, z1);
            gl.Vertex(x2, y2, z2);
            gl.Vertex(x2, y1, z2);
            ////
            gl.Vertex(x1, y1, z1);
            gl.Vertex(x1, y1, z2);
            gl.Vertex(x2, y1, z1);
            gl.Vertex(x2, y1, z2);
            // Грань Y1
            gl.Vertex(x1, y1, z1);
            gl.Vertex(x2, y1, z1);
            gl.Vertex(x2, y1, z2);
            gl.Vertex(x1, y1, z2);
            /////
            gl.Vertex(x1, y2, z1);
            gl.Vertex(x1, y2, z2);
            gl.Vertex(x2, y2, z1);
            gl.Vertex(x2, y2, z2);
            // Грань Y2
            gl.Vertex(x1, y2, z1);
            gl.Vertex(x2, y2, z1);
            gl.Vertex(x2, y2, z2);
            gl.Vertex(x1, y2, z2);
            // Грань Z1
            gl.Vertex(x1, y1, z1);
            gl.Vertex(x1, y2, z1);
            gl.Vertex(x2, y1, z1);
            gl.Vertex(x2, y2, z1);
            ////
            gl.Vertex(x1, y1, z1);
            gl.Vertex(x2, y1, z1);
            gl.Vertex(x2, y2, z1);
            gl.Vertex(x1, y2, z1);
            /////
            gl.Vertex(x1, y1, z2);
            gl.Vertex(x1, y2, z2);
            gl.Vertex(x2, y1, z2);
            gl.Vertex(x2, y2, z2);
            // Грань Z2
            gl.Vertex(x1, y1, z2);
            gl.Vertex(x2, y1, z2);
            gl.Vertex(x2, y2, z2);
            gl.Vertex(x1, y2, z2);

            gl.End();
        }

        public static void RectangleQ(OpenGL gl, double x, double y, double z, double widthX, double widthY, double widthZ)
        {
            // Вычисляем координаты углов прямоугольника
            double x1 = x - widthX;
            double y1 = y - widthY;
            double z1 = z - widthZ;
            double x2 = x + widthX;
            double y2 = y + widthY;
            double z2 = z + widthZ;

            // Рисуем прямоугольник
            gl.Begin(OpenGL.GL_QUADS);

            // Грань X1
            gl.Vertex(x1, y1, z1);
            gl.Vertex(x1, y2, z1);
            gl.Vertex(x1, y2, z2);
            gl.Vertex(x1, y1, z2);
            // Грань X2
            gl.Vertex(x2, y1, z1);
            gl.Vertex(x2, y2, z1);
            gl.Vertex(x2, y2, z2);
            gl.Vertex(x2, y1, z2);
            // Грань Y1
            gl.Vertex(x1, y1, z1);
            gl.Vertex(x2, y1, z1);
            gl.Vertex(x2, y1, z2);
            gl.Vertex(x1, y1, z2);
            // Грань Y2
            gl.Vertex(x1, y2, z1);
            gl.Vertex(x2, y2, z1);
            gl.Vertex(x2, y2, z2);
            gl.Vertex(x1, y2, z2);
            // Грань Z1
            gl.Vertex(x1, y1, z1);
            gl.Vertex(x2, y1, z1);
            gl.Vertex(x2, y2, z1);
            gl.Vertex(x1, y2, z1);
            // Грань Z2
            gl.Vertex(x1, y1, z2);
            gl.Vertex(x2, y1, z2);
            gl.Vertex(x2, y2, z2);
            gl.Vertex(x1, y2, z2);

            gl.End();
        }

        public static void Histogram(OpenGL gl, int[,] histogramXY)
        {
            IEnumerable<int> colNumbs = histogramXY.Cast<int>();
            double max = colNumbs.Max()*2;
            gl.Begin(OpenGL.GL_QUADS);
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    gl.Color((double)histogramXY[x, y] / max, 1 - (double)histogramXY[x, y] / max, 0.5);
                    gl.Vertex(((double)x - 4.5) / 5,
                        ((double)histogramXY[x, y] / max) - 1.5,
                        ((double)y - 4.5) / 5);
                    gl.Color((double)histogramXY[x + 1, y] / max, 1 - (double)histogramXY[x + 1, y] / max, 0.5);
                    gl.Vertex(((double)x - 3.5) / 5,
                        ((double)histogramXY[x + 1, y] / max) - 1.5,
                        ((double)y - 4.5) / 5);
                    gl.Color((double)histogramXY[x + 1, y + 1] / max, 1 - (double)histogramXY[x + 1, y + 1] / max, 0.5);
                    gl.Vertex(((double)x - 3.5) / 5,
                        ((double)histogramXY[x + 1, y + 1] / max) - 1.5,
                        ((double)y - 3.5) / 5);
                    gl.Color((double)histogramXY[x, y + 1] / max, 1 - (double)histogramXY[x, y + 1] / max, 0.5);
                    gl.Vertex(((double)x - 4.5) / 5,
                        ((double)histogramXY[x, y + 1] / max) - 1.5,
                        ((double)y - 3.5) / 5);
                }
            }
            gl.End();
        }

        public static void DrawHistogramXY(OpenGL gl, int[,] histogramXY)
        {
            gl.Begin(OpenGL.GL_QUADS);

            // Размеры столбцов гистограммы
            double columnWidth = 0.1;
            double columnHeight = 0.1;

            // Цвет столбцов гистограммы
            double[] columnColor = { 0.0, 0.0, 1.0 }; // синий

            // Проходим по всем столбцам гистограммы
            for (int x = 0; x < histogramXY.GetLength(0); x++)
            {
                for (int z = 0; z < histogramXY.GetLength(1); z++)
                {
                    int count = histogramXY[x, z];

                    // Координаты вершин столбца гистограммы
                    double x1 = x * columnWidth;
                    double y1 = 0.0;
                    double z1 = z * columnWidth;
                    double x2 = x1 + columnWidth;
                    double y2 = count * columnHeight;
                    double z2 = z1 + columnWidth;

                    // Устанавливаем цвет столбца гистограммы
                    gl.Color(columnColor[0], columnColor[1], columnColor[2]);

                    // Рисуем столбец гистограммы
                    gl.Vertex(x1, y1, z1);
                    gl.Vertex(x2, y1, z1);
                    gl.Vertex(x2, y2, z1);
                    gl.Vertex(x1, y2, z1);
                }
            }

            gl.End();
        }


        public static void XYZ(OpenGL gl)
        {
            // Рисуем оси координат
            gl.Begin(OpenGL.GL_LINES);

            // Ось X - красный цвет
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 0.0f, 0.0f);
            gl.Vertex(1.0f, 0.0f, 0.0f);
            gl.Vertex(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.9f, 0.1f, 0.0f);
            gl.Vertex(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.9f, -0.1f, 0.0f);

            // Ось Y - зеленый цвет
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(0.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Vertex(0.1f, 0.9f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Vertex(-0.1f, 0.9f, 0.0f);

            // Ось Z - синий цвет
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(0.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 0.0f, 1.0f);
            gl.Vertex(0.0f, 0.0f, 1.0f);
            gl.Vertex(0.0f, 0.1f, 0.9f);
            gl.Vertex(0.0f, 0.0f, 1.0f);
            gl.Vertex(0.0f, -0.1f, 0.9f);

            gl.End();
        }
    }
}
