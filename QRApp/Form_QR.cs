using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//---------------------
using ZXing;
using ZXing.Common;
using ZXing.QrCode.Internal;
using ZXing.Rendering;

namespace QRApp
{
    public partial class Form_QR : Form
    {
        public Form_QR()
        {
            InitializeComponent();
            txtOutput.Visible = false;
            btnReader.Visible = false;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if(txtInput.Text != String.Empty)
            {

                BarcodeWriter barcodeWriter = new BarcodeWriter();
                EncodingOptions encodingOptions = new EncodingOptions()
                {
                    Width = 400,
                    Height = 400,
                    Margin = 0,
                    PureBarcode = false
                };
                encodingOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
                barcodeWriter.Renderer = new BitmapRenderer();
                barcodeWriter.Options = encodingOptions;
                barcodeWriter.Format = BarcodeFormat.QR_CODE;
                Bitmap bitmap = barcodeWriter.Write(txtInput.Text);
                Bitmap logo = new Bitmap($"{ Application.StartupPath}/jetpacktocat.png");
                Graphics g = Graphics.FromImage(bitmap);
                g.DrawImage(logo, new Point((bitmap.Width - logo.Width) / 2, (bitmap.Height - logo.Height) / 2));
                pictureBox1.Image = bitmap;
                //------------
                txtOutput.Visible = true;
                btnReader.Visible = true;

            }
            else
            {
                //If inpute text Empty show message
                MessageBox.Show("Enter Text in Text Box", "Message Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReader_Click(object sender, EventArgs e)
        {
            BarcodeReader barcodeReader = new BarcodeReader();
            var result = barcodeReader.Decode(new Bitmap(pictureBox1.Image));
            if (result != null)
            {
                txtOutput.Text = result.Text;
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            String message = "QR Code With a Custom Logo Inside in c# \n You can Download from github \n https://www.github.com/hassanentabi/QRApp  ";
            String title = "Information";
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
