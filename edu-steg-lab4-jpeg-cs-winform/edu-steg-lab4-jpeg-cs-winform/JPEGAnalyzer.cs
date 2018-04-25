using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edu_steg_lab4_jpeg_cs_winform
{
    class JPEGAnalyzer
    {
        private static JPEGAnalyzer _instance = null;
        private Bitmap _rawBMP;
        private Bitmap _newBMP;
        private Bitmap _compareBMP;

        private List<Point> _changedPoints = new List<Point>();

        private JPEGAnalyzer(string filepath)
        {
            this._rawBMP = new Bitmap(Image.FromFile(filepath));
        }

        public static JPEGAnalyzer Instance(string filepath)
        {
            _instance = new JPEGAnalyzer(filepath);
            return _instance;
        }

        public static JPEGAnalyzer Instance()
        {
            if (_instance == null)
            {
                throw new Exception("Cannot find class instance.");
            } else
            {
                return _instance;
            }
        }


        public Bitmap Process()
        {
            ImageFormat format = ImageFormat.Jpeg;
            ImageCodecInfo jgpEncoder = null;
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    jgpEncoder = codec;
                    break;
                }
            }

            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);

            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 100L); // 50L 100L 0L
            myEncoderParameters.Param[0] = myEncoderParameter;
            MemoryStream JPEGstream = new MemoryStream();

            // Converting BMP to JPEG
            this._rawBMP.Save(JPEGstream, jgpEncoder, myEncoderParameters);

            // Saving JPEG to file
            /*FileStream file_output_stream = new FileStream("test.jpeg", FileMode.Create);
            JPEGstream.WriteTo(file_output_stream);
            file_output_stream.Dispose();
            file_output_stream.Close();*/

            // Converting JPEG to BMP
            Image JPEGimage = Image.FromStream(JPEGstream);
            this._newBMP = new Bitmap(JPEGimage);

            JPEGstream.Dispose();
            JPEGstream.Close();

            // Comparing
            this._compareBMP = new Bitmap(this._rawBMP);

            for (int i = 0; i < this._rawBMP.Height; i++)
            {
                for (int j = 0; j < this._rawBMP.Width; j++)
                {
                    Color raw_pixel = this._rawBMP.GetPixel(j, i);
                    Color new_pixel = this._newBMP.GetPixel(j, i);

                    if (raw_pixel != new_pixel)
                    {
                        _changedPoints.Add(new Point(i, j));
                        this._compareBMP.SetPixel(j, i, Color.Red);
                    }
                }
            }


            return this._compareBMP;
        }

        public Bitmap GetRawBmp()
        {
            return this._rawBMP;
        }

        public List<Point> ChangedPoints
        {
            get { return _instance._changedPoints; }
        }

        public int ChangedPointsSize
        {
            get { return _instance._changedPoints.Count; }
        }
    }
}
