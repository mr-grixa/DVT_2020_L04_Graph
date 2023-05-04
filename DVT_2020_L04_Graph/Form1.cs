using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SharpGL;
using QuickGraph;
using QuickGraph.Graphviz;
using QuickGraph.Graphviz.Dot;

namespace DVT_2020_L04_Graph
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            radioButtonCube.Checked = true;
        }
        OpenGL gl;
        private void openGLControl1_OpenGLDraw(object sender, RenderEventArgs args)
        {
            // Создаем экземпляр
            gl = this.openGLControl1.OpenGL;
            // Очистка экрана и буфера глубин
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            // Сбрасываем модельно-видовую матрицу
            gl.LoadIdentity();
            // Двигаем перо вглубь экрана
            double camX = (double)numericUpDownX.Value;
            double camY = (double)numericUpDownY.Value;
            double camZ = (double)numericUpDownZ.Value;
            gl.Translate(camX, camY, -camZ);
            // Вращаем точки 
            double angleX = (double)numericUpDownRX.Value /** Math.PI / 180.0f*/;
            double angleY = (double)numericUpDownRY.Value /** Math.PI / 180.0f*/;
            double angleZ = (double)numericUpDownRZ.Value /** Math.PI / 180.0f*/;
            gl.Rotate(angleX, 1.0f, 0.0f, 0.0f);
            gl.Rotate(angleY, 0.0f, 1.0f, 0.0f);
            gl.Rotate(angleZ, 0.0f, 0.0f, 1.0f);
            gl.Begin(OpenGL.GL_POINTS);
            if (points != null)
            {
                foreach (Point3D point in points)
                {
                    gl.Color(1 - point.x, 1 - point.y, 1 - point.z);
                    gl.Vertex(point.x, point.y, point.z);
                }
            }
            // Завершаем работу
            gl.End();

        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "Табличные данные (*.bmp)|*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<Point3D> data = new List<Point3D>();
                using (FileStream fs = new FileStream(openFileDialog.FileName,FileMode.Open))
                {
                    byte[] bytes = new byte[fs.Length];
                    int bytesRead = 0;
                    while ((bytesRead = fs.Read(bytes, 0, bytes.Length)) > 0)
                    {
                        //for (int i = 0; i < bytesRead; i++)
                        //{
                        //    byte b = buffer[i];
                        //    Console.Write("{0:X2} ", b); // вывод байта в шестнадцатеричном формате
                        //}
                    }
                    
                    byte[] frame = new byte[19];
                    List<CANDumpData> frames=new List<CANDumpData>();
                    int I = 0;
                    for (int i =0; i < bytes.Length; i++)
                    {
                        if (bytes[i] == 255 && bytes[i + 1] == 255)
                        {
                            I = 0;
                            i++;
                            BitConverter.ToInt32(bytes,0);
                            UInt32 time = (UInt32)(frame[2]*255* 255+frame[1] * 255 + frame[0]);
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
                                frame[16],
                                frame[16],
                                frame[16]));
                        }
                        else
                        {
                            frame[I]= bytes[i];
                            I++;

                        }
                    }
                    dataGridView1.DataSource = dataTable(frames);
                }
            }
        }


        

        private DataTable dataTable(List<CANDumpData> frames)
        {
            // Создание таблицы
            DataTable table = new DataTable("CANDumpData");

            // Добавление колонок в таблицу
            table.Columns.Add("TickStamp", typeof(uint));
            table.Columns.Add("Priority", typeof(byte));
            table.Columns.Add("EDP", typeof(byte));
            table.Columns.Add("DP", typeof(byte));
            table.Columns.Add("PDUFormat", typeof(byte));
            table.Columns.Add("PDUSpecific", typeof(byte));
            table.Columns.Add("SourceAddress", typeof(byte));
            table.Columns.Add("DLC", typeof(byte));
            table.Columns.Add("b1", typeof(byte));
            table.Columns.Add("b2", typeof(byte));
            table.Columns.Add("b3", typeof(byte));
            table.Columns.Add("b4", typeof(byte));
            table.Columns.Add("b5", typeof(byte));
            table.Columns.Add("b6", typeof(byte));
            table.Columns.Add("b7", typeof(byte));
            table.Columns.Add("b8", typeof(byte));

            // Добавление данных в таблицу
            foreach (var frame in frames)
            {
                DataRow row = table.NewRow();
                row["TickStamp"] = frame.TickStamp;
                row["Priority"] = frame.Priority;
                row["EDP"] = frame.EDP;
                row["DP"] = frame.DP;
                row["PDUFormat"] = frame.PDUFormat;
                row["PDUSpecific"] = frame.PDUSpecific;
                row["SourceAddress"] = frame.SourceAddress;
                row["DLC"] = frame.DLC;
                row["b1"] = frame.b1;
                row["b2"] = frame.b2;
                row["b3"] = frame.b3;
                row["b4"] = frame.b4;
                row["b5"] = frame.b5;
                row["b6"] = frame.b6;
                row["b7"] = frame.b7;
                row["b8"] = frame.b8;
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
        Point3D[] points;
        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            if (radioButtonCube.Checked)
            {
                points = Generate.RandomPoints((int)UpDownCout.Value);
            }
            else if (radioButtonSpin.Checked)
            {
                points = Generate.SpinPoints((int)UpDownCout.Value);
            }
            else if (radioButton_Dot.Checked)
            {
                points = Generate.DotPoints((int)UpDownCout.Value);
            }
            points = Generate.RandomaisePoints(points, (double)trackBar1.Value / 100);
            Plotnost();
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
                Bitmap bmp = new Bitmap(openGLControl1.Width, openGLControl1.Height);
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
    }
}
