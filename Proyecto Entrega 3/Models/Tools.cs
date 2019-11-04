using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Proyecto_Entrega_3
{
    static class Tools
    {
        public static Color getDominantColor(Bitmap bmp)
        {
            //Used for tally
            int r = 0;
            int g = 0;
            int b = 0;
            int total = 0;
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color clr = bmp.GetPixel(x, y);
                    r += clr.R;
                    g += clr.G;
                    b += clr.B;
                    total++;
                }
            }
            //Calculate average
            r /= total;
            g /= total;
            b /= total;
            return Color.FromArgb(r, g, b);
        }
        public static Bitmap AplyColor(Color color, Bitmap Img)
        {
            float R = color.R / 255.0f;
            float G = color.G / 255.0f;
            float B = color.B / 255.0f;
            float[][] Values = {
                new float[] { R*3.0f, 0, 0, 0, 0 },
                new float[] { 0, G * 3.0f, 0, 0, 0 },
                new float[] { 0, 0, B * 3.0f, 0, 0 },
                new float[] { 0, 0, 0, 1, 0 },
                new float[] { 0, 0, 0, 0, 1 }};
            System.Drawing.Imaging.ColorMatrix Matrix = new System.Drawing.Imaging.ColorMatrix(Values);
            System.Drawing.Imaging.ImageAttributes IA = new System.Drawing.Imaging.ImageAttributes();
            IA.SetColorMatrix(Matrix);
            Bitmap Effect = (Bitmap)Img.Clone();
            using (Graphics g = Graphics.FromImage(Effect))
            {
                g.DrawImage(Img, new Rectangle(0, 0, Effect.Width, Effect.Height), 0, 0, Effect.Width, Effect.Height, GraphicsUnit.Pixel, IA);
            }
            Img = Effect;
            return Img;
        }
        public static Bitmap SlideShowRandomCollage(List<Bitmap> temporal, Bitmap Actualpicture)
        {

            int lenght = Actualpicture.Width;
            int high = Actualpicture.Height;
            Bitmap bmp = new Bitmap(lenght, high);
            Graphics g = Graphics.FromImage(bmp);
            int rand = new Random().Next(0, 100);
            g.DrawImage(Actualpicture, 0, 0, lenght, high);
            Bitmap img = temporal[new Random().Next(0, temporal.Count())];
            int randimention = new Random().Next(20, 40);
            int ranx = new Random(rand * rand).Next(0, lenght);
            int rany = new Random(rand * 5).Next(0, high);
            g.DrawImage(img, ranx, rany, randimention * img.Width / (100), randimention * img.Height / (100));

            return bmp;
        }
        public static Bitmap SlideShowFusion(Bitmap bmp, int cont)
        {
            Bitmap bmp2 = Filters.Cut(bmp, cont * 2, 0, bmp.Height, 1080);
            return bmp2;
        }
        public static Bitmap Fusion(List<Bitmap> ListOfBitmap)
        {

            int whidth = 1;
            int height = 1;
            foreach (int i in Enumerable.Range(0, ListOfBitmap.Count()))
            {
                if (whidth < ListOfBitmap[i].Width)
                {
                    whidth = ListOfBitmap[i].Width;
                }
                if (height < ListOfBitmap[i].Height)
                {
                    height = ListOfBitmap[i].Height;
                }

            }
            Bitmap bmp = new Bitmap(whidth, height);
            foreach (int x in Enumerable.Range(0, whidth))
            {
                foreach (int y in Enumerable.Range(0, height))
                {
                    int total = 0;
                    int r = 0;
                    int g = 0;
                    int b = 0;
                    foreach (Bitmap bit in ListOfBitmap)
                    {
                        if ((x < bit.Width) && (y < bit.Height) && (x > 0) && (y > 0))
                        {
                            Color clr = bit.GetPixel(x, y);
                            r += clr.R;
                            g += clr.G;
                            b += clr.B;
                            total++;
                        }


                    }
                    if (total == 0) { total++; }

                    r /= total;
                    g /= total;
                    b /= total;
                    Color color = Color.FromArgb(r, g, b);

                    bmp.SetPixel(x, y, color);

                }

            }

            return bmp;

        }
    }
}
