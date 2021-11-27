using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        Graphics newGraph;
        bool cursorMoving = false;
        bool eraserActivated = false;
        bool fillButtonActivated = false;
        Pen fillFunction;
        Pen pen;
        Pen eraser;
        int cursorX = -1;
        int cursorY = -1;
        string cursorStringX;
        string cursorStringY;
        int brushSize = 5;
        Bitmap surface;



        public Form1()
        {
            InitializeComponent();
            graphics = canvas.CreateGraphics();
            pen = new Pen(Color.Black, 5);
            eraser = new Pen(Color.White, 5);
            fillFunction = new Pen(Color.Black);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            eraser.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            surface = new Bitmap(canvas.Width, canvas.Height);
            newGraph = Graphics.FromImage(surface);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            eraser.Color = Color.White;
            eraserActivated = true;
            fillButtonActivated = false;
        }
        private void fillButton_Click(object sender, EventArgs e)
        {
            fillButtonActivated = true;
            eraserActivated = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            graphics.Clear(Color.White);
        }

        private void blackBox_Click(object sender, EventArgs e)
        {
            PictureBox color = (PictureBox)sender;
            pen.Color = color.BackColor;
            fillFunction.Color = color.BackColor;
            eraserActivated = false;
            fillButtonActivated = false;
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            cursorMoving = true;
            cursorX = e.X;
            cursorY = e.Y;
            cursorStringX = cursorX.ToString();
            cursorStringY = cursorY.ToString();
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            cursorMoving = false;
            cursorX = -1;
            cursorY = -1;
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (cursorX != -1 && cursorY != -1 && cursorMoving == true)
            {
                label1.Text = "X: " + cursorStringX + "  Y: " + cursorStringY;
                if (eraserActivated == true)
                {
                    graphics.DrawLine(eraser, new Point(cursorX, cursorY), e.Location);
                    cursorX = e.X;
                    cursorY = e.Y;
                }
                else
                {
                    graphics.DrawLine(pen, new Point(cursorX, cursorY), e.Location);
                    cursorX = e.X;
                    cursorY = e.Y;
                }
            }
        }

        private void canvas_Click(object sender, EventArgs e)
        {
            if(fillButtonActivated == true)
            {
                canvas.BackColor = fillFunction.Color;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void canvas_MouseEnter(object sender, EventArgs e)
        {
            if (cursorX != -1 && cursorY != -1)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            brushSize++;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            brushSize--;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Png Files (*png) | *.png";
            sfd.DefaultExt = "png";
            sfd.AddExtension = true;

            if(sfd.ShowDialog() == DialogResult.OK)
            {
                surface.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}
