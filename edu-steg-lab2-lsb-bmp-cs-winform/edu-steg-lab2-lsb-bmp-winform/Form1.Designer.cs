namespace edu_steg_lab2_lsb_bmp_winform
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.img_original = new System.Windows.Forms.PictureBox();
            this.img_new = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.img_path = new System.Windows.Forms.TextBox();
            this.btn_browse = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.msg_text = new System.Windows.Forms.TextBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_embed = new System.Windows.Forms.Button();
            this.enc_key = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ste_key = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.img_text = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.iv_b64 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.img_original)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img_new)).BeginInit();
            this.SuspendLayout();
            // 
            // img_original
            // 
            this.img_original.Location = new System.Drawing.Point(12, 182);
            this.img_original.Name = "img_original";
            this.img_original.Size = new System.Drawing.Size(425, 296);
            this.img_original.TabIndex = 0;
            this.img_original.TabStop = false;
            // 
            // img_new
            // 
            this.img_new.Location = new System.Drawing.Point(452, 182);
            this.img_new.Name = "img_new";
            this.img_new.Size = new System.Drawing.Size(425, 296);
            this.img_new.TabIndex = 1;
            this.img_new.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 163);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Original image";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(449, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Steganography image";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Image";
            // 
            // img_path
            // 
            this.img_path.Enabled = false;
            this.img_path.Location = new System.Drawing.Point(12, 26);
            this.img_path.Name = "img_path";
            this.img_path.ReadOnly = true;
            this.img_path.Size = new System.Drawing.Size(238, 20);
            this.img_path.TabIndex = 5;
            // 
            // btn_browse
            // 
            this.btn_browse.Location = new System.Drawing.Point(256, 21);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(107, 29);
            this.btn_browse.TabIndex = 6;
            this.btn_browse.Text = "Browse...";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Click += new System.EventHandler(this.btn_browse_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Text to embed";
            // 
            // msg_text
            // 
            this.msg_text.Location = new System.Drawing.Point(12, 69);
            this.msg_text.Name = "msg_text";
            this.msg_text.Size = new System.Drawing.Size(425, 20);
            this.msg_text.TabIndex = 8;
            this.msg_text.Text = "Example text to embed";
            this.msg_text.TextChanged += new System.EventHandler(this.msg_text_TextChanged);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(770, 484);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(107, 29);
            this.btn_save.TabIndex = 9;
            this.btn_save.Text = "Save file...";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_embed
            // 
            this.btn_embed.Location = new System.Drawing.Point(443, 64);
            this.btn_embed.Name = "btn_embed";
            this.btn_embed.Size = new System.Drawing.Size(107, 29);
            this.btn_embed.TabIndex = 10;
            this.btn_embed.Text = "Embed";
            this.btn_embed.UseVisualStyleBackColor = true;
            this.btn_embed.Click += new System.EventHandler(this.btn_embed_Click);
            // 
            // enc_key
            // 
            this.enc_key.Location = new System.Drawing.Point(650, 30);
            this.enc_key.Name = "enc_key";
            this.enc_key.Size = new System.Drawing.Size(227, 20);
            this.enc_key.TabIndex = 12;
            this.enc_key.Text = "exampleKey123";
            this.enc_key.TextChanged += new System.EventHandler(this.enc_key_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(647, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Encryption key";
            // 
            // ste_key
            // 
            this.ste_key.Location = new System.Drawing.Point(650, 73);
            this.ste_key.Name = "ste_key";
            this.ste_key.Size = new System.Drawing.Size(227, 20);
            this.ste_key.TabIndex = 14;
            this.ste_key.Text = "exampleKey321";
            this.ste_key.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(647, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Steganography key";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // img_text
            // 
            this.img_text.Location = new System.Drawing.Point(12, 117);
            this.img_text.Name = "img_text";
            this.img_text.ReadOnly = true;
            this.img_text.Size = new System.Drawing.Size(425, 20);
            this.img_text.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Text from image";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(443, 112);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 29);
            this.button1.TabIndex = 17;
            this.button1.Text = "Read";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(657, 484);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 29);
            this.button2.TabIndex = 18;
            this.button2.Text = "Load file...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // iv_b64
            // 
            this.iv_b64.Location = new System.Drawing.Point(595, 121);
            this.iv_b64.Name = "iv_b64";
            this.iv_b64.Size = new System.Drawing.Size(282, 20);
            this.iv_b64.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(592, 105);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Initialization vector";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 522);
            this.Controls.Add(this.iv_b64);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.img_text);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ste_key);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.enc_key);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_embed);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.msg_text);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_browse);
            this.Controls.Add(this.img_path);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.img_new);
            this.Controls.Add(this.img_original);
            this.Name = "Form1";
            this.Text = "STEG LAB2 LSB BMP";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.img_original)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img_new)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox img_original;
        private System.Windows.Forms.PictureBox img_new;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox img_path;
        private System.Windows.Forms.Button btn_browse;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox msg_text;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_embed;
        private System.Windows.Forms.TextBox enc_key;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ste_key;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox img_text;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox iv_b64;
        private System.Windows.Forms.Label label8;
    }
}

