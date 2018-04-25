using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace edu_steg_lab2_lsb_bmp_winform
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_browse_Click(object sender, EventArgs e)
        {

            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                VarStore.Global.fileOrgPath = openFileDialog1.FileName;
                img_path.Text = openFileDialog1.FileName;

                try
                {
                    Image image = Image.FromFile(file);
                    Bitmap bmp = new Bitmap(image);

                    VarStore.Global.fileBitmapOrgImg = bmp;
                    img_original.Image = image;
                }
                catch (IOException)
                {
                }
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void btn_embed_Click(object sender, EventArgs e)
        {
            /* Initialize AES crypto provider */
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();

            /* Creating key bytes array for AES */
            byte[] keyBytes = Static.GetEncryptionKey(enc_key.Text);

            /* Creating text bytes array for AES */
            byte[] textBytes = Encoding.UTF8.GetBytes(msg_text.Text);

            /* Text message AES Encryption */
            aes.GenerateIV();

            byte[] iv = aes.IV;
            iv_b64.Text = Convert.ToBase64String(iv);

            aes.Key = keyBytes;

            var cryptor = aes.CreateEncryptor();
            byte[] encryptedBytes = cryptor.TransformFinalBlock(textBytes, 0, textBytes.Length);

            string encryptedTextAsBase64 = Convert.ToBase64String(encryptedBytes);

            /* Embed */
            Bitmap new_image = Steganography.embedMessage(VarStore.Global.fileBitmapOrgImg, encryptedTextAsBase64, Static.GetSteganographyKey(ste_key.Text));
            VarStore.Global.fileBitmapNewImg = new_image;
            
            /* Display new image */
            img_new.Image = VarStore.Global.fileBitmapNewImg;
        }

        private void msg_text_TextChanged(object sender, EventArgs e)
        {

        }

        private void enc_key_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bmp = VarStore.Global.fileBitmapNewImg;

            string message = Steganography.fetchMessage(bmp, Static.GetSteganographyKey(ste_key.Text));
            byte[] fromBase64ToBytes = Convert.FromBase64String(message);

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();

            try {
                byte[] iv2 = Convert.FromBase64String(iv_b64.Text);
                var decryptor = aes.CreateDecryptor(Static.GetEncryptionKey(enc_key.Text), iv2);

                byte[] decryptedBytes = decryptor.TransformFinalBlock(fromBase64ToBytes, 0, fromBase64ToBytes.Length);
                string decrypted = Encoding.UTF8.GetString(decryptedBytes);

                img_text.Text = decrypted;
            } catch(Exception exception)
            {
                MessageBox.Show("Decryption fail.", "Error occurred!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                img_text.Text = "";
            }

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Bitmap|*.bmp";
            saveFileDialog1.Title = "Save File";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                ImageConverter converter = new ImageConverter();
                byte[] new_image_bytes = (byte[])converter.ConvertTo(VarStore.Global.fileBitmapNewImg, typeof(byte[]));

                File.WriteAllBytes(saveFileDialog1.FileName, new_image_bytes);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog2.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog2.FileName;

                try
                {
                    Image image = Image.FromFile(file);
                    Bitmap bmp = new Bitmap(image);

                    
                    img_new.Image = image;
                    VarStore.Global.fileBitmapNewImg = bmp;

                    img_original.Image = null;
                }
                catch (IOException)
                {
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}

namespace VarStore
{
    static class Global
    {
        public static int header_bytes_length = 4;

        private static string _ivAsBase64;
        private static string _keyBase64;

        private static int _steg_key;

        private static string _org_img_path = "";
        private static Bitmap _org_img;
        private static Bitmap _new_img;

        public static string ivBase64
        {
            get { return _ivAsBase64; }
            set { _ivAsBase64 = value; }
        }

        public static string keyBase64
        {
            get { return _keyBase64; }
            set { _keyBase64 = value; }
        }

        public static string fileOrgPath
        {
            get { return _org_img_path; }
            set { _org_img_path = value; }
        }

        public static Bitmap fileBitmapOrgImg
        {
            get { return _org_img; }
            set { _org_img = value; }
        }

        public static Bitmap fileBitmapNewImg
        {
            get { return _new_img; }
            set { _new_img = value; }
        }

        public static int stegKey
        {
            get { return _steg_key; }
            set { _steg_key = value; }
        }
    }
}