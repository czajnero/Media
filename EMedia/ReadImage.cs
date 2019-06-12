using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMedia
{
    public class ReadImage
    {
        private readonly Image _image;
        private readonly string _filename;


        public ReadImage(string filename)
        {
            _filename = filename;
            _image = Image.FromFile(_filename);

        }
        public Image GetImage()
        {
            return _image;
        }

        public string[] ConvertToHex()
        {
            var tmp = BitConverter.ToString(File.ReadAllBytes(_filename));
            return tmp.Split('-').ToArray();
        }

        List<string> lista = new List<string>();


        private string DecodeHeader()
        {
            var hex = ConvertToHex();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hex.Length; i++)
            {
                if (hex[i] == "FF" && hex[i + 1] == "D8")
                {
                    stringBuilder.Append("Typ pliku: JPG ");
                }

            }
            return stringBuilder.ToString();
        }



        private string DecodePixels()
        {
            var hex = ConvertToHex();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hex.Length; i++)
            {
                if (hex[i] == "FF" && hex[i + 1] == "C0")
                {
                    int section = int.Parse(hex[i + 2] + hex[i + 3], NumberStyles.HexNumber) + 2;
                    int depth = int.Parse(hex[i + 4], NumberStyles.HexNumber);

                    stringBuilder.Append("Liczba bitów na piksel na składnik koloru: " + depth);
                    i = i + section;
                }

            }

            return stringBuilder.ToString();

        }

        private string DecodeHeight()
        {
            var hex = ConvertToHex();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hex.Length; i++)
            {
                if (hex[i] == "FF" && hex[i + 1] == "C0")
                {
                    int section = int.Parse(hex[i + 2] + hex[i + 3], NumberStyles.HexNumber) + 2;
                    int height = int.Parse(hex[i + 5] + hex[i + 6], NumberStyles.HexNumber);
                    
                    stringBuilder.Append("Wysokość: ");
                    stringBuilder.Append(height + " pikseli ");
                    i = i + section;
                }

            }

            return stringBuilder.ToString();

        }

        private string DecodeWidth()
        {
            var hex = ConvertToHex();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hex.Length; i++)
            {
                if (hex[i] == "FF" && hex[i + 1] == "C0")
                {
                    int section = int.Parse(hex[i + 2] + hex[i + 3], NumberStyles.HexNumber) + 2;
                    int width = int.Parse(hex[i + 7] + hex[i + 8], NumberStyles.HexNumber);
                    
                    stringBuilder.Append("Szerokość: ");
                    stringBuilder.Append(width + " pikseli ");
                    i = i + section;
                }

            }

            return stringBuilder.ToString();

        }

        private string DecodeComponents()
        {
            var hex = ConvertToHex();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hex.Length; i++)
            {
                if (hex[i] == "FF" && hex[i + 1] == "C0")
                {
                    int section = int.Parse(hex[i + 2] + hex[i + 3], NumberStyles.HexNumber) + 2;
                    int number_of_components = int.Parse(hex[i + 9], NumberStyles.HexNumber);

                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("Ilosc komponentow: " + number_of_components);
                    i = i + section;
                }

            }

            return stringBuilder.ToString();

        }

        private string DecodeDepth()
        {
            var hex = ConvertToHex();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hex.Length; i++)
            {
                if (hex[i] == "FF" && hex[i + 1] == "C0")
                {
                    int section = int.Parse(hex[i + 2] + hex[i + 3], NumberStyles.HexNumber) + 2;
                    int depth = int.Parse(hex[i + 4], NumberStyles.HexNumber);
                    int number_of_components = int.Parse(hex[i + 9], NumberStyles.HexNumber);

                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("Glebia koloru: " + depth * number_of_components);
                    i = i + section;
                }

            }

            return stringBuilder.ToString();

        }



        private Dictionary<string, string> Attributes = new Dictionary<string, string>()
        {
            {"0x10e"," ImageDescription"},{"0x132"," Data" },{"0x8298", " Copyright" }
        };

        private string GetAttributes()
        {
            PropertyItem[] propertyItem = _image.PropertyItems;
            int i = 0;
            string tmp = "";

            foreach  (var item in propertyItem)
            {
                var encode = new UTF8Encoding();
                string value = encode.GetString(propertyItem[i].Value);
                string code = "0x" + item.Id.ToString("x");
                if (Attributes.ContainsKey(code))
                {
                    string name = Attributes[code];
                    value = value.Replace("\0", "");
                    tmp += $"{name}: {value}";
                }
                i++;
            }
            return tmp; 
        }
        

        public List<string> ListOFAttributes()
        {
            string header = DecodeHeader();
            string width = DecodeWidth();
            string height = DecodeHeight();
            string pixels = DecodePixels();
            string components = DecodeComponents();
            string depth = DecodeDepth();
            lista.Add(header);
            //lista.Add(width);
            //lista.Add(height);
            lista.Add(pixels);
            lista.Add(components);
            lista.Add(depth);

            return lista;
        }



    }
}
