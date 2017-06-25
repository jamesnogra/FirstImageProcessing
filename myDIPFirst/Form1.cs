using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myDIPFirst
{
    public partial class Form1 : Form
    {
        Bitmap source, result;

        public Form1()
        {
            InitializeComponent();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            result = new Bitmap(source.Width, source.Height); //initalize the image
            Color pixel;
            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    pixel = source.GetPixel(x, y);
                    result.SetPixel(x, y, pixel);
                }
            }
            outputPic.Image = result;
        }

        //open image dialog
        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile.ShowDialog();
        }

        private void graysacleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            result = new Bitmap(source.Width, source.Height); //initalize the image
            Color pixel;
            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    pixel = source.GetPixel(x, y);
                    //int grey = (int)(pixel.R + pixel.G + pixel.B) / 3;
                    int grey = (int)(pixel.R*0.33 + pixel.G*0.33 + pixel.B*0.34);
                    Color averageGray = Color.FromArgb(grey, grey, grey);
                    result.SetPixel(x, y, averageGray);
                }
            }
            outputPic.Image = result;
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            result = new Bitmap(source.Width, source.Height); //initalize the image
            Color pixel;
            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    pixel = source.GetPixel(x, y);
                    Color averageGray = Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B);
                    result.SetPixel(x, y, averageGray);
                }
            }
            outputPic.Image = result;
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            result = new Bitmap(source.Width, source.Height); //initalize the image
            Color pixel;
            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    pixel = source.GetPixel(x, y);
                    result.SetPixel((source.Width-1)-x, y, pixel);
                }
            }
            outputPic.Image = result;
        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            result = new Bitmap(source.Width, source.Height); //initalize the image
            Color pixel;
            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    pixel = source.GetPixel(x, y);
                    result.SetPixel(x, (source.Height - 1) - y, pixel);
                }
            }
            outputPic.Image = result;
        }

        private void bothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            result = new Bitmap(source.Width, source.Height); //initalize the image
            Color pixel;
            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    pixel = source.GetPixel(x, y);
                    result.SetPixel((source.Width-1)-x, (source.Height - 1) - y, pixel);
                }
            }
            outputPic.Image = result;
        }

        private void thresholdingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string promptValue = Prompt.ShowDialog("Enter the threshold value (0-255)", "Threshold");
            result = new Bitmap(source.Width, source.Height); //initalize the image
            Color pixel;
            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    pixel = source.GetPixel(x, y);
                    int grey = (int)(pixel.R * 0.33 + pixel.G * 0.33 + pixel.B * 0.34);
                    int threshold = Convert.ToInt32(promptValue);
                    Color tempColor = Color.FromArgb(255, 255, 255);
                    if (grey < threshold)
                    {
                        tempColor = Color.FromArgb(0, 0, 0);
                    }
                    result.SetPixel(x, y, tempColor);
                }
            }
            outputPic.Image = result;
        }

        private void rotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //newX = (int)((x) * Math.Cos(rotation) + (y) * (-Math.Sin(rotation)));
            //newY = (int)(((x) * Math.Sin(rotation) + (y) * Math.Cos(rotation)));
            double rotation = Math.PI / 180 * Convert.ToDouble(Prompt.ShowDialog("How many degrees you want to rotate the image?", "Rotation"));
            result = new Bitmap(source.Width, source.Height); //initalize the image
            Color pixel;
            int newX, newY, transX, transY;
            for (int x = 0; x < source.Width/2; x++)
            {
                for (int y = 0; y < source.Height/2; y++)
                {
                    //quadrant 1
                    pixel = source.GetPixel(x, y);
                    newX = (int)((x - source.Width / 2) * Math.Cos(rotation) + (y - source.Height / 2) * (-Math.Sin(rotation)));
                    newY = (int)(((x - source.Width / 2) * Math.Sin(rotation) + (y - source.Height / 2) * Math.Cos(rotation)));
                    transX = newX + source.Width / 2;
                    transY = newY + source.Height / 2;
                    if (transX >= 0 && transX < source.Width && transY >= 0 && transY < source.Height)
                    {
                        result.SetPixel(transX, transY, pixel);
                    }
                }
            }
            for (int x = source.Width / 2; x < source.Width; x++)
            {
                for (int y = source.Height / 2; y < source.Height; y++)
                {
                    //quadrant 4
                    pixel = source.GetPixel(x, y);
                    newX = (int)((x - source.Width / 2) * Math.Cos(rotation) + (y - source.Height / 2) * (-Math.Sin(rotation)));
                    newY = (int)(((x - source.Width / 2) * Math.Sin(rotation) + (y - source.Height / 2) * Math.Cos(rotation)));
                    transX = newX + source.Width / 2;
                    transY = newY + source.Height / 2;
                    if (transX >= 0 && transX < source.Width && transY >= 0 && transY < source.Height)
                    {
                        result.SetPixel(transX, transY, pixel);
                    }
                }
            }
            for (int x = 0; x < source.Width/2; x++)
            {
                for (int y = source.Height / 2; y < source.Height; y++)
                {
                    //quadrant 3
                    pixel = source.GetPixel(x, y);
                    newX = (int)((x - source.Width / 2) * Math.Cos(rotation) + (y - source.Height / 2) * (-Math.Sin(rotation)));
                    newY = (int)(((x - source.Width / 2) * Math.Sin(rotation) + (y - source.Height / 2) * Math.Cos(rotation)));
                    transX = newX + source.Width / 2;
                    transY = newY + source.Height / 2;
                    if (transX >= 0 && transX < source.Width && transY >= 0 && transY < source.Height)
                    {
                        result.SetPixel(transX, transY, pixel);
                    }
                }
            }
            for (int x = source.Width / 2; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height/2; y++)
                {
                    //quadrant 2
                    pixel = source.GetPixel(x, y);
                    newX = (int)((x - source.Width / 2) * Math.Cos(rotation) + (y - source.Height / 2) * (-Math.Sin(rotation)));
                    newY = (int)(((x - source.Width / 2) * Math.Sin(rotation) + (y - source.Height / 2) * Math.Cos(rotation)));
                    transX = newX + source.Width / 2;
                    transY = newY + source.Height / 2;
                    if (transX >= 0 && transX < source.Width && transY >= 0 && transY < source.Height)
                    {
                        result.SetPixel(transX, transY, pixel);
                    }
                }
            }
            //MessageBox.Show("DONE rotating.");
            outputPic.Image = result;
        }

        private void resizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int tempNewWidth = Convert.ToInt32(Prompt.ShowDialog("Enter the new width", "Width"));
            int tempNewHeight = Convert.ToInt32(Prompt.ShowDialog("Enter the new height", "Height"));
            result = new Bitmap(tempNewWidth, tempNewHeight); //initalize the image
            Color pixel;
            for (int x = 0; x < tempNewWidth; x++)
            {
                for (int y = 0; y < tempNewWidth; y++)
                {
                    pixel = source.GetPixel(x * source.Width / tempNewWidth, y * source.Height / tempNewHeight);
                    result.SetPixel(x, y, pixel);
                }
            }
            outputPic.Image = result;

        }

        //after clicking the open in the open file dialog
        private void openFile_FileOk(object sender, CancelEventArgs e)
        {
            source = new Bitmap(openFile.FileName);
            myPic.Image = source;
        }
    }

    //taken from https://stackoverflow.com/questions/5427020/prompt-dialog-in-windows-forms
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 155,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text, Width = 450 };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 80, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }

}
