using AForge.Imaging;
using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EMedia
{

    public class FFT
    {
        private readonly ComplexImage _image;

        public FFT(Bitmap map) // pobieram bitmape
        {
            _image = ComplexImage.FromBitmap(ConvertToGray(map));
        }

        private Bitmap ConvertToGray(Bitmap bitmap)
        {
           Grayscale filter = new Grayscale(0.3, 0.5, 0.1);
           bitmap = filter.Apply(bitmap);
           return bitmap;
        }

        public Bitmap GetForwardFFT() 
        {
            ComplexImage copy = _image;
            copy.ForwardFourierTransform();
            Bitmap bitmap = copy.ToBitmap();
            return bitmap;

        }

        public Bitmap GetBackFTT()
        {
            ComplexImage copy = _image;
            copy.BackwardFourierTransform();
            Bitmap bitmap = copy.ToBitmap();
            return bitmap;
     }

   }
    //
}