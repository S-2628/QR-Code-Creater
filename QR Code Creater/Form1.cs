using QRCodeDecoderLibrary;
using QRCodeEncoderLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace QR_Code_Creater
{
    public partial class Form1 : Form
    {
        QRCodeEncoderLibrary.QREncoder Encoder = new QREncoder();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = textBox2.Text;
            if (url == "")
            {
                MessageBox.Show("URLを入力してください","URLが空です",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            string filePath = "";
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "PNGファイル(*.png)|*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
                filePath = dialog.FileName;
            dialog.Dispose();

            if (filePath == "")
                return;

            QREncoder Encoder = new QREncoder();

            Encoder.ErrorCorrection = QRCodeEncoderLibrary.ErrorCorrection.Q;
            Encoder.ModuleSize = 6;
            Encoder.QuietZone = Encoder.ModuleSize * 4;
            Encoder.Encode(url);

            Encoder.SaveQRCodeToPngFile(filePath);

            // 保存されたファイルを開いてPictureBoxに表示させる
            // ただしファイルがロックしないようにコピーを表示させ、ファイルから取得したBitmapはすぐにDisposeする
            Bitmap bitmap = new Bitmap(filePath);
            pictureBox1.Image = new Bitmap(bitmap);
            bitmap.Dispose();

            // 正当性のチェック
            QRDecoder Decoder = new QRDecoder();
            byte[][] DataByteArray = Decoder.ImageDecoder((Bitmap)pictureBox1.Image);
            textBox2.Text = QRDecoder.ByteArrayToStr(DataByteArray[0]);
        }
    }
}

        