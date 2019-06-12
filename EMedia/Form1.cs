using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;

namespace EMedia
{
    public partial class Form1 : Form
    {

        private FFT FFT;
        private ReadImage readImage;

        public Form1()
        {
            InitializeComponent();
           
        }
  

        private void button1_Click(object sender, EventArgs e)
        {

            using (var imageStream = File.OpenRead("Lena.jpg")) // pobieram wymiary
            {
                var decoder = BitmapDecoder.Create(imageStream, BitmapCreateOptions.IgnoreColorProfile,
                    BitmapCacheOption.Default);
                var height = decoder.Frames[0].PixelHeight;
                var width = decoder.Frames[0].PixelWidth;
                MessageBox.Show("Wysokosc: " + height + "px "  + " Szerokosc: " + width + "px");
            }
        }

        private void button2_Click(object sender, EventArgs e) // pobieram rozmiar
        {
            var fileLength = new FileInfo("Lena.jpg").Length;
            MessageBox.Show("Rozmiar: " + fileLength);
        }
        

        private void button4_Click_1(object sender, EventArgs e) // Transformata Fouriera (wykres widma FFT)
        {
            readImage = new ReadImage("Lena.jpg");
            
                Bitmap image = new Bitmap(readImage.GetImage(), new Size(256, 256));
                FFT = new FFT(image);
                pictureBox1.Image = FFT.GetForwardFFT();
                label1.Text = "Wykres widma FFT";
                pictureBox2.Image = FFT.GetBackFTT();
                label2.Text = "Obraz po FFT";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }


        private void button5_Click(object sender, EventArgs e)
        {
            readImage = new ReadImage("foto.jpg");
            //textBox1.AppendText(readImage.ListOFAttributes());
            foreach (var item in readImage.ListOFAttributes())
            {
                listBox1.Items.Add(item);
            }
            //listBox1.Items.Add(readImage.ListOFAttributes());
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void eventLog1_EntryWritten(object sender, System.Diagnostics.EntryWrittenEventArgs e)
        {

        }

        private void event_log_box_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
            
