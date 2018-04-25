using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace edu_steg_lab4_jpeg_cs_winform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = imageFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filepath = imageFile.FileName;
                textBoxFilepath.Text = filepath;

                var analyzer = JPEGAnalyzer.Instance(filepath);
                pictureBox.Image = analyzer.GetRawBmp();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            JPEGAnalyzer analyzer = JPEGAnalyzer.Instance();
            Bitmap compareBMP = analyzer.Process();

            pictureBox.Image = compareBMP;

            var stringBuilder = new StringBuilder();

            foreach(Point point in analyzer.ChangedPoints)
            {
                stringBuilder.AppendFormat(point.X.ToString() + ", " + point.Y.ToString() + "{0}", Environment.NewLine);
            }

            labelCount.Text = analyzer.ChangedPointsSize.ToString();
            textBoxPoints.Text = stringBuilder.ToString();

        }
    }
}
