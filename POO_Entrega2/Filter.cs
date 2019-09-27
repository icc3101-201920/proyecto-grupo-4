using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace POO_Entrega2
{
    public static class Filter
    {
        public static void Etiq(string name, int pointx, int pointy, Image img)
        {

            Graphics gr = Graphics.FromImage(img);
            Pen pe = new Pen(Brushes.Black, 3);
            int dist = 200;
            gr.DrawLines(pe, new Point[] { new Point(pointx - dist, pointy - dist), new Point(pointx + dist, pointy - dist), new Point(pointx + dist, pointy + dist), new Point(pointx - dist, pointy + dist), new Point(pointx - dist, pointy - dist) });
            gr.DrawString(name, new Font(new FontFamily("Times New Roman"), 20, FontStyle.Bold), Brushes.LightBlue, new PointF(pointx, pointy)); ;
        }


        public static byte[] GetImageBytes(Bitmap imag, ImageLockMode lockMode, out BitmapData bmpData)
        {
            bmpData = imag.LockBits(new Rectangle(0, 0, imag.Width, imag.Height),
                                     lockMode, imag.PixelFormat);

            byte[] imageBytes = new byte[bmpData.Stride * imag.Height];
            Marshal.Copy(bmpData.Scan0, imageBytes, 0, imageBytes.Length);

            return imageBytes;
        }

        public static Bitmap CreateGrayScaleBitmap(Bitmap source, int iii)
        {
            Bitmap target = new Bitmap(source.Width, source.Height, source.PixelFormat);
            BitmapData targetData, sourceData;

            byte[] sourceBytes = GetImageBytes(source, ImageLockMode.ReadOnly, out sourceData);
            byte[] targetBytes = GetImageBytes(target, ImageLockMode.ReadWrite, out targetData);

            //recorrer los pixeles
            for (int i = 0; i < sourceBytes.Length; i += 3)
            {
                //ignorar el padding, es decir solo procesar los bytes necesarios
                if ((i + 3) % (source.Width * 3) > 0)
                {
                    //Hallar tono gris
                    byte y = (byte)(sourceBytes[i + 2] * 0.3f
                                  + sourceBytes[i + 1] * 0.59f
                                  + sourceBytes[i] * 0.11f);

                    //Asignar tono gris a cada byte del pixel
                    switch (iii)
                    {
                        case 1://rojo
                            targetBytes[i + 2] = y;
                            break;
                        case 2://verde
                            targetBytes[i + 1] = y;
                            break;
                        case 3://azul
                            targetBytes[i] = y;
                            break;
                        case 4://amarillo
                            targetBytes[i + 2] = targetBytes[i + 1] = y;
                            break;
                        case 5://morado
                            targetBytes[i + 2] = targetBytes[i] = y;
                            break;
                        case 6://celeste
                            targetBytes[i] = targetBytes[i + 1] = y;
                            break;
                        case 7://blanco y negro
                            targetBytes[i + 2] = targetBytes[i + 1] = targetBytes[i] = y;
                            break;

                        default:
                            break;
                    }

                }

            }

            Marshal.Copy(targetBytes, 0, targetData.Scan0, targetBytes.Length);
            Console.WriteLine(iii);
            source.UnlockBits(sourceData);
            target.UnlockBits(targetData);

            return target;
        }


        public static Bitmap ResizeImage(Image imagen, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(imagen.HorizontalResolution, imagen.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(imagen, destRect, 0, 0, imagen.Width, imagen.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }




        public static Bitmap Collage(List<Bitmap> temporal)
        {
            int dimensions = 25;
            int lenght = 2000;
            int high = 1000;

            Bitmap bmp = new Bitmap(lenght, high);
            Graphics g = Graphics.FromImage(bmp);
            int len = temporal.Count();

            foreach (int x in Enumerable.Range(1, lenght / dimensions))
            {
                foreach (int y in Enumerable.Range(1, high / dimensions))
                {
                    int ran = new Random().Next(0, len);
                    Bitmap img = temporal[ran];
                    g.DrawImage(img, x * dimensions, y * dimensions, dimensions, dimensions);
                }
            }

            return bmp;

        }


    }
}
