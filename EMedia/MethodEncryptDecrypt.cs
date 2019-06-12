using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace EMedia
{
     
    public class MethodEncryptDecrypt
    {
        private readonly Image _image;
        private readonly string _filename;

        public MethodEncryptDecrypt(string filename)
        {
            _filename = filename;
            _image = Image.FromFile(_filename);

        }

      public byte[] HexDecode(string tmp)
        {
            string[] _string = tmp.Split('-');
            byte[] array = new byte[_string.Length];

            for(int i=0; i<_string.Length; i++)
            {
                array[i] = Convert.ToByte(_string[i], 16);
                
            }
            return array;
        }

        public bool IfIsPrime(int tmp)
        {
            if(tmp < 2)
            {
                return false;
            }
            if(tmp % 2 == 0)
            {
                return tmp == 2;
            }

            int sqrt = (int)Math.Sqrt((double)tmp);

            for(int i=3; i<=sqrt; i+=2)
            {
                if(tmp % i == 0)
                   return false;
 
            }
            return true;
        }

        public Bitmap ConvertToImage(byte[] vs)
        {
            return (new Bitmap(Image.FromStream(new MemoryStream(vs))));

        }

        public byte[] ConvertToByte(Image image)
        {
            MemoryStream memoryStream = new MemoryStream();

            new Bitmap(image).Save(memoryStream, ImageFormat.Jpeg);
            byte[] vs = new byte[] { 255, 216 };
            vs = memoryStream.ToArray();

            return vs;

        }
    }
}
