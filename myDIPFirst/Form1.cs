using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DITDIP;

namespace myDIPFirst
{
    public partial class Form1 : Form
    {
        Bitmap source, source1, result;
        bool aboutToImageSubtract;
        int tempVal;

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
            int newX, newY, transX, transY, cX, cY;
            for (int x = 0; x < source.Width / 2; x++)
            {
                for (int y = 0; y < source.Height / 2; y++)
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
                    //quadrant 2
                    cX = source.Width / 2 + x;
                    cY = y;
                    pixel = source.GetPixel(cX, cY);
                    newX = (int)((cX - source.Width / 2) * Math.Cos(rotation) + (cY - source.Height / 2) * (-Math.Sin(rotation)));
                    newY = (int)(((cX - source.Width / 2) * Math.Sin(rotation) + (cY - source.Height / 2) * Math.Cos(rotation)));
                    transX = newX + source.Width / 2;
                    transY = newY + source.Height / 2;
                    if (transX >= 0 && transX < source.Width && transY >= 0 && transY < source.Height)
                    {
                        result.SetPixel(transX, transY, pixel);
                    }
                    //quadrant 3
                    cX = x;
                    cY = source.Height / 2 + y;
                    pixel = source.GetPixel(cX, cY);
                    newX = (int)((cX - source.Width / 2) * Math.Cos(rotation) + (cY - source.Height / 2) * (-Math.Sin(rotation)));
                    newY = (int)(((cX - source.Width / 2) * Math.Sin(rotation) + (cY - source.Height / 2) * Math.Cos(rotation)));
                    transX = newX + source.Width / 2;
                    transY = newY + source.Height / 2;
                    if (transX >= 0 && transX < source.Width && transY >= 0 && transY < source.Height)
                    {
                        result.SetPixel(transX, transY, pixel);
                    }
                    //quadrant 4
                    cX = source.Width / 2 + x;
                    cY = source.Height / 2 + y;
                    pixel = source.GetPixel(cX, cY);
                    newX = (int)((cX - source.Width / 2) * Math.Cos(rotation) + (cY - source.Height / 2) * (-Math.Sin(rotation)));
                    newY = (int)(((cX - source.Width / 2) * Math.Sin(rotation) + (cY - source.Height / 2) * Math.Cos(rotation)));
                    transX = newX + source.Width / 2;
                    transY = newY + source.Height / 2;
                    if (transX >= 0 && transX < source.Width && transY >= 0 && transY < source.Height)
                    {
                        result.SetPixel(transX, transY, pixel);
                    }
                }
            }
            outputPic.Image = result;
        }

        private void resizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int tempNewWidth = Convert.ToInt32(Prompt.ShowDialog("Enter the new width", "Width"));
            int tempNewHeight = Convert.ToInt32(Prompt.ShowDialog("Enter the new height", "Height"));
            resizeImage(ref source, ref result, tempNewWidth, tempNewHeight);
            outputPic.Image = result;

        }

        private void contrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap tempImage = source;
            int contrastValue = Convert.ToInt32(Prompt.ShowDialog("Enter contrast value", "Contrast"));
            result = new Bitmap(source.Width, source.Height); //initalize the image
            ImageProcess.EqualisationColored(tempImage, ref source, ref result, contrastValue);
            outputPic.Image = result;
        }

        private void mergeImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutToImageSubtract = true;
            tempVal = Convert.ToInt32(Prompt.ShowDialog("Enter value threshold for color subtraction. Then select a color from the first image.", "Image Subtraction"));
        }

        public void resizeImage(ref Bitmap source, ref Bitmap result, int width, int height)
        {
            result = new Bitmap(width, height); //initalize the image
            Color pixel;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    pixel = source.GetPixel(x * source.Width / width, y * source.Height / height);
                    result.SetPixel(x, y, pixel);
                }
            }
        }

        private void myPic_MouseUp(object sender, MouseEventArgs e)
        {
            if (aboutToImageSubtract)
            {
                aboutToImageSubtract = false;
                Bitmap resizedSource1 = new Bitmap(source.Width, source.Height);
                Color colorSelected = source.GetPixel(e.X, e.Y);
                string tempLog = "";
                resizeImage(ref source1, ref resizedSource1, source.Width, source.Height);
                ImageProcess.SubtractCustom(ref source, ref resizedSource1, ref result, colorSelected, tempVal, ref tempLog);
                outputPic.Image = result;
                //MessageBox.Show(tempLog);
            }
        }

        //open 2nd file dialog
        private void open2ndImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile1.ShowDialog();
        }

        private void openFile1_FileOk(object sender, CancelEventArgs e)
        {
            source1 = new Bitmap(openFile1.FileName);
            outputPic.Image = source1;
        }

        //open image dialog
        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile.ShowDialog();
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
