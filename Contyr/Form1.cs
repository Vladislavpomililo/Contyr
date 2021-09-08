using System;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace Contyr
{
    public partial class Form1 : Form
    {
        private Image<Bgr, byte> inputImage = null;
       
        public Form1()
        {
            InitializeComponent();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult res = openFileDialog1.ShowDialog();

                if (res == DialogResult.OK)
                {
                    inputImage = new Image<Bgr, byte>(openFileDialog1.FileName);
                    pictureBox1.Image = inputImage.Bitmap;            
                }
                else
                {
                    MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void найтиКонтурыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Image<Gray, byte> outputImage = inputImage.Convert<Gray, byte>().ThresholdBinary(new Gray(100), new Gray(255));
                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                Mat hierarchy = new Mat();
                CvInvoke.FindContours(outputImage, contours, hierarchy, Emgu.CV.CvEnum.RetrType.Tree,
                    Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);

                if(checkBox1.Checked)
                {
                    Image<Gray, byte> blackBackground = new Image<Gray, byte>(inputImage.Width, inputImage.Height, new Gray(0));
                    CvInvoke.DrawContours(blackBackground, contours, -1, new MCvScalar(250, 0 , 0));

                    pictureBox2.Image = blackBackground.Bitmap;
                }

                else
                {
                    CvInvoke.DrawContours(inputImage, contours, -1, new MCvScalar(250, 0, 0));

                    pictureBox2.Image = inputImage.Bitmap;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
