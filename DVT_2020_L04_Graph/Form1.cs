using SharpGL;
using SharpGL.Enumerations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;

namespace DVT_2020_L04_Graph
{
    public partial class Form1 : Form
    {
        List<Point3D> points = new List<Point3D>();
        DataTable CANdataTable;
        DataTable dataTable;
        int[,] histogramXY;
        Bitmap PlotBox;
        Bitmap Line;
        Bitmap allLine;
        public Form1()
        {
            InitializeComponent();

            comboBoxX.SelectedIndex = 1;
            comboBoxY.SelectedIndex = 2;
            comboBoxZ.SelectedIndex = 3;
        }
        OpenGL gl;
        private void openGLControl1_OpenGLDraw(object sender, RenderEventArgs args)
        {
            // Создаем экземпляр
            gl = this.openGLControl.OpenGL;


            // Очистка экрана и буфера глубин
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            // Сбрасываем модельно-видовую матрицу
            gl.MatrixMode(MatrixMode.Projection);
            gl.LoadIdentity();
            // Двигаем перо вглубь экрана
            double camX = (double)numericUpDownX.Value;
            double camY = (double)numericUpDownY.Value;
            double camZ = -(double)numericUpDownZ.Value;

            if (checkBox_perspective.Checked)
            {
                gl.Perspective((double)numericUpDown_Fov.Value, (double)openGLControl.Width / (double)openGLControl.Height, 0.01, 10000);
            }
            else
            {
                double w = (double)openGLControl.Width / (double)openGLControl.Height;
                double h = 1.0;
                h = h * camZ;
                w = w * camZ;
                gl.Ortho(-w, w, -h, h, -10000, 10000);
            }

            gl.Translate(camX, camY, camZ);
            // Вращаем точки 
            float angleX = (float)numericUpDownRX.Value /** Math.PI / 180.0f*/;
            float angleY = (float)numericUpDownRY.Value /** Math.PI / 180.0f*/;
            float angleZ = (float)numericUpDownRZ.Value /** Math.PI / 180.0f*/;
            gl.Rotate(angleX, angleY, angleZ);



            gl.Begin(OpenGL.GL_POINTS);
            if (points != null)
            {
                foreach (Point3D point in points)
                {
                    gl.Color(1 - point.x, 1 - point.y, 1 - point.z);
                    gl.Vertex(point.x, point.y, point.z);
                }
            }
            gl.End();
            gl.Color(1d, 1d, 1d);
            Draw.Rectangle(gl, 0, 0, 0, 1, 1, 1);

            if (checkBox_plane.Checked && histogramXY != null)
            {
                Draw.Histogram(gl, histogramXY);
            }

            //Draw.SquarePlane(gl, 2, 1.25, 0, 0, "x", Draw2D.Triangle(Color.Red));
            if (PlotBox != null && checkBox_boxPlot.Checked)
            {
                Draw.SquarePlane(gl, 2, 1.25, 0, 0, "x", PlotBox);
            }
            if (Line != null && checkBox_boxPlot.Checked)
            {
                Draw.SquarePlane(gl, 2, 0, 0, 1.25, "z", Line);
            }
            if (allLine != null && checkBox_boxPlot.Checked)
            {
                Draw.SquarePlane(gl, 2, 0, -1.25, 0, "y", allLine);
            }
            //Draw.SquarePlane(gl, 2, 0, 0, 1.25, "z", Draw2D.Triangle(Color.Blue));
            //Draw.SquarePlane(gl, 2, 0, 1.25, 0, "y", PlotBox);
            if (checkBoxXYZ.Checked) Draw.XYZ(gl);


            gl.Finish();

        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "Табличные данные (*.bmp)|*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    byte[] bytes = new byte[fs.Length];
                    int bytesRead = 0;
                    while ((bytesRead = fs.Read(bytes, 0, bytes.Length)) > 0)
                    {
                    }

                    byte[] frame = new byte[19];
                    List<CANDumpData> frames = new List<CANDumpData>();
                    int I = 0;
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        if (bytes[i] == 255 && bytes[i + 1] == 255)
                        {
                            I = 0;
                            i++;
                            BitConverter.ToInt32(bytes, 0);
                            UInt32 time = (UInt32)(frame[3] * 256 * 256 + 256 + frame[2] * 256 * 256 + frame[1] * 256 + frame[0]);
                            frames.Add(new CANDumpData(time,
                                frame[4],
                                frame[5],
                                frame[6],
                                frame[7],
                                frame[8],
                                frame[9],
                                frame[10],
                                frame[11],
                                frame[12],
                                frame[13],
                                frame[14],
                                frame[15],
                                frame[16]));
                        }
                        else
                        {
                            frame[I] = bytes[i];
                            I++;

                        }
                    }
                    //dataGridView1.DataSource = dataTable(frames);
                    CANdataTable = dataTableFromCAN(frames);
                    dataTable = new DataTable();
                    dataTable.Columns.Add("TickStamp", typeof(UInt32));
                    // Получаем все уникальные значения столбца "Name"
                    var uniqueNames = CANdataTable.AsEnumerable().Select(row => row.Field<UInt32>("TickStamp")).Distinct();
                    foreach (UInt32 time in uniqueNames)
                    {
                        DataRow[] rows = CANdataTable.Select($"TickStamp = '" + time + "'");

                        DataRow newRow = dataTable.NewRow();
                        newRow["TickStamp"] = time;
                        foreach (DataRow row in rows)
                        {
                            string name = row["Source"] + "=>" + row["Dest"];
                            if (!dataTable.Columns.Contains(name))
                            {
                                dataTable.Columns.Add(name, typeof(byte));
                            }
                            newRow[name] = row["b1"];
                        }
                        dataTable.Rows.Add(newRow);
                    }
                    dataGridView.DataSource = dataTable;
                    dataGridView.Columns["TickStamp"].Width = 80;
                    dataGridView.Columns["2=>0"].Width = 40;
                    dataGridView.Columns["1=>31"].Width = 40;
                    dataGridView.Columns["31=>1"].Width = 40;
                    dataGridView.Columns["57=>1"].Width = 40;
                    dataGridView.Columns["25=>1"].Width = 40;
                    dataGridView.RowHeadersWidth = 30;
                    // DrawCAN(frames);

                }
            }
        }

        private void DrawCAN(List<CANDumpData> frames)
        {
            points.Clear();
            foreach (CANDumpData dumpData in frames)
            {
                double X = ((double)dumpData.Dest / 255) - 1;
                double Y = ((double)dumpData.Source / 255) - 1;
                double Z = ((double)dumpData.b1 / 255) - 1;

                points.Add(new Point3D(X, Y, Z));

            }
        }


        private DataTable dataTableFromCAN(List<CANDumpData> frames)
        {
            DataTable table = new DataTable();
            table.Columns.Add("TickStamp", typeof(UInt32));
            //table.Columns.Add("Prefix", typeof(byte));
            //table.Columns.Add("Format", typeof(byte));
            table.Columns.Add("Dest", typeof(byte));
            table.Columns.Add("Source", typeof(byte));
            //table.Columns.Add("DLC", typeof(byte));
            table.Columns.Add("b1", typeof(byte));
            //table.Columns.Add("b2", typeof(byte));
            //table.Columns.Add("b3", typeof(byte));
            //table.Columns.Add("b4", typeof(byte));
            //table.Columns.Add("b5", typeof(byte));
            //table.Columns.Add("b6", typeof(byte));
            //table.Columns.Add("b7", typeof(byte));
            //table.Columns.Add("b8", typeof(byte));

            foreach (CANDumpData data in frames)
            {
                DataRow row = table.NewRow();
                row["TickStamp"] = data.TickStamp;
                //row["Prefix"] = data.Prefix;
                //row["Format"] = data.Format;
                row["Dest"] = data.Dest;
                row["Source"] = data.Source;
                //row["DLC"] = data.DLC;
                row["b1"] = data.b1;
                //row["b2"] = data.b2;
                //row["b3"] = data.b3;
                //row["b4"] = data.b4;
                //row["b5"] = data.b5;
                //row["b6"] = data.b6;
                //row["b7"] = data.b7;
                //row["b8"] = data.b8;
                table.Rows.Add(row);
            }
            return table;
        }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "data";
            saveFileDialog.Filter = "Табличные данные (*.CSV)|*.CSV";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (points != null)
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        foreach (Point3D point in points)
                        {
                            writer.WriteLine($"{point.x};{point.y};{point.z}");
                        }
                    }
                }
            }
        }

        private Point startPoint;
        private decimal RX;
        private decimal RY;
        private void openGLControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint = e.Location;
                RX = numericUpDownRX.Value;
                RY = numericUpDownRY.Value;
            }
        }

        private void openGLControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                numericUpDownRY.Value += ((decimal)(startPoint.X - e.X) * (decimal)0.1);
                numericUpDownRX.Value += ((decimal)(startPoint.Y - e.Y) * (decimal)0.1);
                startPoint = e.Location;
            }
        }

        private void openGLControl1_MouseUp(object sender, MouseEventArgs e)
        {
            startPoint = Point.Empty;
            RX = 0;
            RY = 0;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void buttonSaveImg_Click(object sender, EventArgs e)
        {
            if (gl != null)
            {
                // создаем новый объект Bitmap с размерами окна
                Bitmap bmp = new Bitmap(openGLControl.Width, openGLControl.Height);
                gl.ReadPixels(0, 0, bmp.Width, bmp.Height, OpenGL.GL_BGRA, OpenGL.GL_UNSIGNED_BYTE, bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb).Scan0);

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PNG файлы (*.png)|*.png";
                saveFileDialog.FileName = "img";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    bmp.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }
        private void Plotnost()
        {
            int[,,] ints = new int[10, 10, 10];
            foreach (Point3D point in points)
            {
                if (Math.Abs(point.x) <= 1 &&
                    Math.Abs(point.y) <= 1 &&
                    Math.Abs(point.z) <= 1)
                {
                    ints[(int)point.x * 5 + 5,
                         (int)point.z * 5 + 5,
                         (int)point.z * 5 + 5]++;
                }
            }
        }

       


        private void comboBoxXYZ_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataTable != null)
            {
                points.Clear();
                string Xname = comboBoxX.SelectedItem?.ToString();
                string Yname = comboBoxY.SelectedItem?.ToString();
                string Zname = comboBoxZ.SelectedItem?.ToString();
                double Xmax = 255;
                double Xmin = 0;
                double Ymax = 255;
                double Ymin = 0;
                double Zmax = 255;
                double Zmin = 0;
                if (checkBox_normalize.Checked)
                {
                    Xmax = (double)(byte)dataTable.Select().Min(row1 => row1[Xname]);
                    Xmin = (double)(byte)dataTable.Select().Max(row1 => row1[Xname]);
                    Ymax = (double)(byte)dataTable.Select().Min(row1 => row1[Yname]);
                    Ymin = (double)(byte)dataTable.Select().Max(row1 => row1[Yname]);
                    Zmax = (double)(byte)dataTable.Select().Min(row1 => row1[Zname]);
                    Zmin = (double)(byte)dataTable.Select().Max(row1 => row1[Zname]);
                }
                List<double[]> lineXY = new List<double[]>();
                if (checkBox_all.Checked)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        DataToPoint(row);
                    }
                }
                else
                {
                    DataTable SelectTable = new DataTable();
                    foreach (DataGridViewColumn column in dataGridView.Columns)
                    {
                        SelectTable.Columns.Add(column.HeaderText, column.ValueType);
                    }
                    foreach (DataGridViewRow row in dataGridView.SelectedRows)
                    {
                        DataRow newRow = SelectTable.Rows.Add();
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            newRow[cell.ColumnIndex] = cell.Value;
                        }
                    }
                    foreach (DataRow row in SelectTable.Rows)
                    {
                        DataToPoint(row);
                    }
                }

                void DataToPoint(DataRow row)
                {
                    double X = 0;
                    double Y = 0;
                    double Z = 0;
                    if (Xname != null)
                    {
                        X = (((double)(byte)row[Xname] - Xmin) / (Xmax - Xmin)) * 2 - 1;
                    }
                    if (Yname != null)
                    {
                        Y = (((double)(byte)row[Yname] - Ymin) / (Ymax - Ymin)) * 2 - 1;
                    }
                    if (Zname != null)
                    {
                        Z = (((double)(byte)row[Zname] - Zmin) / (Zmax - Zmin)) * 2 - 1;
                    }
                    points.Add(new Point3D(X, Y, Z));
                }
                histogramXY = new int[10, 10];
                foreach (Point3D point in points)
                {
                    int z = (int)((point.z + 1) * 5);
                    int x = (int)((point.x + 1) * 5);
                    if (z < 10 && z >= 0 &&
                        x < 10 && x >= 0)
                    {
                        histogramXY[x, z]++;
                    }
                }
                PlotBox = BoxPlot();
                Line = Projection();
                allLine = AllLine();
            }

        }

        private Bitmap AllLine()
        {
            // Создаем новый объект Chart
            var chart = new Chart();

            Series boxPlotSeries = new Series("BoxPlotSeries");
            boxPlotSeries.ChartType = SeriesChartType.Line;
            chart.Series.Add(boxPlotSeries);
            boxPlotSeries.BorderWidth = 3;
            boxPlotSeries.Color = Color.DarkOrange;

            string Zname = comboBoxZ.SelectedItem?.ToString();
            if (checkBox_all.Checked)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    AddData(row);
                }
            }
            else
            {
                DataTable SelectTable = new DataTable();
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    SelectTable.Columns.Add(column.HeaderText, column.ValueType);
                }
                foreach (DataGridViewRow row in dataGridView.SelectedRows)
                {
                    DataRow newRow = SelectTable.Rows.Add();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        newRow[cell.ColumnIndex] = cell.Value;
                    }
                }
                foreach (DataRow row in SelectTable.Rows)
                {
                    AddData(row);
                }
            }

            void AddData(DataRow row)
            {
                boxPlotSeries.Points.Add(new DataPoint((double)(UInt32)row["TickStamp"], (double)(byte)row[Zname]));
            }

            chart.ChartAreas.Add("ChartArea1");
            chart.ChartAreas[0].Visible = true;
            chart.ChartAreas[0].Position.Auto = true;

            // Настраиваем общие параметры графика
            chart.Size = new Size(256, 256);
            //chart.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            //chart.ChartAreas[0].AxisY.LabelStyle.Enabled = false;

            //chart.ChartAreas[0].AxisY.Minimum = -1;
            //chart.ChartAreas[0].AxisY.Maximum = 1;
            //chart.ChartAreas[0].AxisX.Minimum = -1;
            //chart.ChartAreas[0].AxisX.Maximum = 1;

            // Генерируем изображение графика
            Bitmap bitmap = new Bitmap(256, 256);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.Black);
            chart.DrawToBitmap(bitmap, new Rectangle(0, 0, chart.Width, chart.Height));

            bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);

            return bitmap;

        }
        private Bitmap Projection()
        {
            // Создаем новый объект Chart
            var chart = new Chart();

            Series boxPlotSeries = new Series("BoxPlotSeries");
            boxPlotSeries.ChartType = SeriesChartType.Line;
            chart.Series.Add(boxPlotSeries);
            boxPlotSeries.BorderWidth = 3;
            boxPlotSeries.Color = Color.DarkOrange;
            foreach (Point3D point in points)
            {
                if (point.y > -1&& point.y > -1)
                {
                    boxPlotSeries.Points.Add(new DataPoint(point.x, point.y));
                }

            }
            chart.ChartAreas.Add("ChartArea1");
            chart.ChartAreas[0].Visible = true;
            chart.ChartAreas[0].Position.Auto = true;

            // Настраиваем общие параметры графика
            chart.Size = new Size(256, 256);
            chart.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            chart.ChartAreas[0].AxisY.LabelStyle.Enabled = false;

            chart.ChartAreas[0].AxisY.Minimum = -1;
            chart.ChartAreas[0].AxisY.Maximum = 1;
            chart.ChartAreas[0].AxisX.Minimum = -1;
            chart.ChartAreas[0].AxisX.Maximum = 1;

            // Генерируем изображение графика
            Bitmap bitmap = new Bitmap(256, 256);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.Black);
            chart.DrawToBitmap(bitmap, new Rectangle(0, 0, chart.Width, chart.Height));

            bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);

            return bitmap;

        }


        private Bitmap BoxPlot()
        {
            List<double>[] listsY = new List<double>[10];
            for (int i = 0; i < listsY.Length; i++)
            {
                listsY[i] = new List<double>();
            }
            foreach (Point3D point in points)
            {
                int z = (int)((-point.z + 1) * 5);
                if (z == 10) { z = 9; }
                listsY[z].Add(point.y);
            }


            List<double[]> yValues = new List<double[]>();

            for (int i = 0; i < 10; i++)
            {
                var listY = listsY[i];
                listY.Sort();
                int count = listY.Count;
                int medianIndex = count / 2;
                int q1Index = count / 4;
                int q3Index = count - q1Index - 1;
                if (listY.Count != 0)
                {
                    double min = listY.Min();
                    double max = listY.Max();
                    double average = listY.Average();
                    double median = count % 2 == 0 ? (listY[medianIndex] + listY[medianIndex - 1]) / 2 : listY[medianIndex];
                    double Q1 = count % 4 == 0 ? (listY[q1Index] + listY[q1Index - 1]) / 2 : listY[q1Index];
                    double Q3 = count % 4 == 0 ? (listY[q3Index] + listY[q3Index + 1]) / 2 : listY[q3Index];
                    yValues.Add(new double[]{
                            min,max,
                            Q1, Q3,
                            average,median});
                }
                else
                {
                    yValues.Add(new double[] { 0, 0, 0, 0, 0, 0 });
                }

            }

            // Создаем новый объект Chart
            var chart = new Chart();

            Series boxPlotSeries = new Series("BoxPlotSeries");
            boxPlotSeries.ChartType = SeriesChartType.BoxPlot;
            chart.Series.Add(boxPlotSeries);
            boxPlotSeries.BorderWidth = 3;
            boxPlotSeries.Color = Color.DarkOrange;
            for (int i = 0; i < yValues.Count; i++)
            {
                boxPlotSeries.Points.Add(new DataPoint(i, yValues[i]));
            }
            chart.ChartAreas.Add("ChartArea1");
            chart.ChartAreas[0].Visible = true;
            chart.ChartAreas[0].Position.Auto = true;

            // Настраиваем общие параметры графика
            chart.Size = new Size(256, 256);
            chart.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            chart.ChartAreas[0].AxisY.LabelStyle.Enabled = false;

            chart.ChartAreas[0].AxisY.Minimum = -1;
            chart.ChartAreas[0].AxisY.Maximum = 1;
            chart.ChartAreas[0].AxisX.Minimum = 0;
            chart.ChartAreas[0].AxisX.Maximum = 10;

            // Генерируем изображение графика
            Bitmap bitmap = new Bitmap(256, 256);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.Black);
            chart.DrawToBitmap(bitmap, new Rectangle(0, 0, chart.Width, chart.Height));

            bitmap.RotateFlip(RotateFlipType.Rotate270FlipX);

            return bitmap;
        }
    }
}
