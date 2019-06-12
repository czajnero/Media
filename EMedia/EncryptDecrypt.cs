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

namespace EMedia
{
    public partial class EncryptDecrypt : Form
    {
        static int p = 0;
        static int q = 0;
        static int e = 0;
        static int d = 0;
        static int n = 0;
        static string loadImage = "";
        static string cipher = "";


        private MethodEncryptDecrypt methodEncryptDecrypt;
        
        public string Encryption(string image)
        {
            string hex = image;
            char[] vs = hex.ToCharArray();
            String tmp = "";

            for (int i = 0; i < vs.Length; i++)
            {
                if (tmp == "")
                {
                    tmp = tmp + RSA.Mod(vs[i], e, n);
                }
                else
                {
                    tmp = tmp + "-" + RSA.Mod(vs[i], e, n);
                }
            }
            return tmp;

        }

        public string Decryption(String image)
        {
            char[] vs = image.ToCharArray();
            int i = 0;
            int j = 0;
            string tmp = "";
            string tmp2 = "";
            try
            {
                for (; i < vs.Length; i++)
                {
                    tmp = "";

                    for (j = i; vs[j] != '-'; j++)
                    {
                        tmp = tmp + vs[j];
                    }
                    i = j;

                    tmp2 = tmp2 + ((char)RSA.Mod(Convert.ToInt16(tmp), d, n)).ToString();
                }
            }
            catch (Exception ex)
            { }
            return tmp2;
        }


        public EncryptDecrypt()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs ev)
        {

            if(methodEncryptDecrypt.IfIsPrime(Convert.ToInt16(textBoxP.Text)))
            {
                p = Convert.ToInt16(textBoxP.Text);
            }
            
            if(methodEncryptDecrypt.IfIsPrime(Convert.ToInt16(textBoxQ.Text)))
            {
                q = Convert.ToInt16(textBoxQ.Text);
            }

          
                e = Convert.ToInt16(textBoxE.Text);

            n = RSA.N_val(p, q);
            string tmp = Encryption(loadImage);
            File.WriteAllText("Plik5.txt", tmp);
            MessageBox.Show("Szyfrowanie przeszło pomyślnie");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (methodEncryptDecrypt.IfIsPrime(Convert.ToInt16(textBoxD.Text)))
            {
                d = Convert.ToInt16(textBoxD.Text);
            }

            if(methodEncryptDecrypt.IfIsPrime(Convert.ToInt16(textBoxN.Text)))
            {
                n = Convert.ToInt16(textBoxN.Text);
            }
            cipher = File.ReadAllText("Plik5.txt");
            string tmp = Decryption(cipher);
            pictureBox1.Image = methodEncryptDecrypt.ConvertToImage(methodEncryptDecrypt.HexDecode(tmp));
            pictureBox1.Image.Save("Lenaout.jpeg");
            MessageBox.Show("Deszyfrowanie przeszło pomyślnie");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            methodEncryptDecrypt = new MethodEncryptDecrypt("Lena.jpg");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            pictureBox1.Image = Image.FromFile("Lena.jpg");
            loadImage = BitConverter.ToString(methodEncryptDecrypt.ConvertToByte(pictureBox1.Image));
            MessageBox.Show("Obraz załadowany poprawnie!");
            
        }


    }


}
