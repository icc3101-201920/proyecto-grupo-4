using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Entrega_3
{
    public static class Filters
    {
        static Filters()
        {
        }

        public static Bitmap TagPerson(Coordenada Coords, Bitmap bmp, int dist = 80)
        {
            int pointx = Coords.X;
            int pointy = Coords.Y;
            string name = Coords.person.Name;
            Graphics gr = Graphics.FromImage(bmp);
            Pen pe = new Pen(Brushes.Black, 3);
            gr.DrawLines(pe, new Point[] { new Point(pointx - dist, pointy - dist), new Point(pointx + dist, pointy - dist), new Point(pointx + dist, pointy + dist), new Point(pointx - dist, pointy + dist), new Point(pointx - dist, pointy - dist) });
            gr.DrawString(name, new Font(new FontFamily("Times New Roman"), 20, FontStyle.Bold), Brushes.Black, new PointF(pointx - dist, pointy + dist)); ;
            return bmp;

        }
        public static Bitmap Collage(List<Bitmap> temporal)//------------------------------------------
        {
            int dimensions = 200;
            int lenght = 2000;
            int high = 1000;

            Bitmap bmp = new Bitmap(lenght, high);
            Graphics g = Graphics.FromImage(bmp);
            int len = temporal.Count();

            foreach (int x in Enumerable.Range(0, lenght / dimensions))
            {
                foreach (int y in Enumerable.Range(0, high / dimensions))
                {
                    int ran = new Random().Next(1, len);
                    Bitmap img = temporal[ran];
                    g.DrawImage(img, x * dimensions, y * dimensions, dimensions, dimensions);
                }
            }

            return bmp;

        }
        public static Bitmap RandomCollage(List<Bitmap> temporal)//------------------------------------------
        {

            int lenght = 1920;
            int high = 1080;

            Bitmap bmp = new Bitmap(lenght, high);
            Graphics g = Graphics.FromImage(bmp);

            int rand = new Random().Next(0, 100);
            g.DrawImage(temporal[0], 0, 0, lenght, high);
            foreach (int x in Enumerable.Range(0, 20))
            {
                foreach (Bitmap img in temporal)
                {

                    int randimention = new Random().Next(20, 40);
                    int ranx = new Random(rand * x).Next(0, lenght);
                    int rany = new Random(rand * 2 * x).Next(0, high);
                    double ranangle = new Random(rand + x).NextDouble();

                    g.RotateTransform(380.7f * (float)ranangle);

                    g.DrawImage(img, ranx, rany, randimention * img.Width / (100), randimention * img.Height / (100));
                }
            }
            return bmp;
        }
        public static Bitmap FibonachiCollage(List<Bitmap> ListOfBitmaps) // recibe una lista de bitmaps con 7 bitmap, em caso sde que sean mas, solo seran los primeros 7 

        {
            while (ListOfBitmaps.Count() < 7)
            {
                List<Bitmap> NewList = new List<Bitmap>();
                foreach (Bitmap b in ListOfBitmaps)
                {
                    NewList.Add(b);

                }
                foreach (Bitmap b in ListOfBitmaps)
                {
                    NewList.Add(b);

                }
                ListOfBitmaps = NewList;
            }
            Bitmap bmp = new Bitmap(2100, 1300);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            g.DrawImage(ListOfBitmaps[0], 505, 305, 90, 90);
            g.DrawImage(ListOfBitmaps[1], 505, 405, 90, 90);
            g.DrawImage(ListOfBitmaps[2], 605, 305, 190, 190);
            g.DrawImage(ListOfBitmaps[3], 505, 5, 290, 290);
            g.DrawImage(ListOfBitmaps[4], 5, 5, 490, 490);
            g.DrawImage(ListOfBitmaps[5], 5, 505, 790, 790);
            g.DrawImage(ListOfBitmaps[6], 805, 5, 1290, 1290);
            return bmp;
        }
        public static Bitmap RotateFlipX(Bitmap img)
        {
            Bitmap copy = (Bitmap)img.Clone();
            copy.RotateFlip(RotateFlipType.RotateNoneFlipX);
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(copy, 0, 0, img.Width, img.Height);
            return bmp;
        }
        public static Bitmap RotateFlipY(Bitmap img)
        {
            Bitmap copy = (Bitmap)img.Clone();
            copy.RotateFlip(RotateFlipType.RotateNoneFlipY);
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(copy, 0, 0, img.Width, img.Height);
            return bmp;
        }
        public static Bitmap Rotate90(Bitmap img)
        {
            Bitmap copy = (Bitmap)img.Clone();
            copy.RotateFlip(RotateFlipType.Rotate90FlipNone);
            Bitmap bmp = new Bitmap(copy.Width, copy.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(copy, 0, 0, copy.Width, copy.Height);
            return bmp;
        }
        public static Bitmap FlipY(Bitmap img)
        {
            Bitmap copy = (Bitmap)img.Clone();
            Bitmap copy2 = (Bitmap)img.Clone();
            copy2.RotateFlip(RotateFlipType.RotateNoneFlipX);
            Bitmap bmp = new Bitmap(img.Width * 2, img.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(copy, 0, 0, img.Width, img.Height);
            g.DrawImage(copy2, img.Width, 0, img.Width, img.Height);
            return bmp;
        }
        public static Bitmap FlipX(Bitmap img)
        {
            Bitmap copy = (Bitmap)img.Clone();
            Bitmap copy2 = (Bitmap)img.Clone();
            copy2.RotateFlip(RotateFlipType.RotateNoneFlipY);
            Bitmap bmp = new Bitmap(img.Width, img.Height * 2);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(copy, 0, 0, img.Width, img.Height);
            g.DrawImage(copy2, 0, img.Height, img.Width, img.Height);
            return bmp;
        }
        public static Bitmap FlipXY(Bitmap img)
        {
            Bitmap imgX = FlipX(img);
            Bitmap imgXY = FlipY(imgX);
            return imgXY;
        }
        public static Bitmap ReSize(Bitmap img, int Height, int Width)
        {
            Bitmap copy = (Bitmap)img.Clone();
            Bitmap bmp = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(copy, 0, 0, Width, Height);
            return bmp;

        }
        public static Bitmap PercentageReSize(Bitmap img, int p)
        {

            p = 100 + p;

            Bitmap copy = (Bitmap)img.Clone();
            int Height = copy.Height * p / 100;
            int Width = copy.Width * p / 100;
            Bitmap bmp = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(copy, 0, 0, Width, Height);
            return bmp;

        }
        public static Bitmap MergedBitmaps(Bitmap bmp1, Bitmap bmp2)
        {
            Bitmap result = new Bitmap(Math.Max(bmp1.Width, bmp2.Width),
                                       Math.Max(bmp1.Height, bmp2.Height));
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp2, Point.Empty);
                g.DrawImage(bmp1, Point.Empty);
            }
            return result;
        }
        public static Bitmap Combine(List<Bitmap> images) //------------------------------------------
        {
            Bitmap finalImage;
            int width = 0;
            int height = images[0].Height;
            foreach (Bitmap bmp in images)
            {
                width += bmp.Width;
            }

            finalImage = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(finalImage))
            {
                g.Clear(Color.White);
                int offset = 0;
                foreach (Bitmap image in images)
                {
                    g.DrawImage(image, new Rectangle(offset, 0, image.Width, image.Height));
                    offset += image.Width;
                }
            }
            return finalImage;

        }
        public static Bitmap Cut(Bitmap img, int x, int y, int Height, int Width)
        {
            Bitmap copy = (Bitmap)img.Clone();
            Bitmap bmp = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(copy, -x, -y, img.Width, img.Height);
            return bmp;

        }
        public static Bitmap PixelArt(Bitmap bmp, int dimensions = 25)
        {

            Bitmap bmp1 = ReSize(bmp, bmp.Height / dimensions, bmp.Width / dimensions);
            Bitmap bmp2 = new Bitmap(bmp1.Width * dimensions, bmp1.Height * dimensions);
            Graphics g = Graphics.FromImage(bmp2);

            foreach (int x in Enumerable.Range(0, bmp1.Width))
            {
                foreach (int y in Enumerable.Range(0, bmp1.Height))
                {
                    Bitmap img = new Bitmap(dimensions, dimensions);
                    Graphics g2 = Graphics.FromImage(img);
                    Color clr = bmp1.GetPixel(x, y);
                    g2.Clear(clr);
                    g.DrawImage(img, x * dimensions, y * dimensions, dimensions, dimensions);
                }
            }
            return bmp2;
        }
        public static Bitmap AutoMosaic(Bitmap bmp, int dimensions = 25)
        {

            Bitmap bmp1 = ReSize(bmp, bmp.Height / dimensions, bmp.Width / dimensions);
            Bitmap bmp2 = new Bitmap(bmp1.Width * dimensions, bmp1.Height * dimensions);
            bmp = ReSize(bmp, dimensions, dimensions);
            Graphics g = Graphics.FromImage(bmp2);

            foreach (int x in Enumerable.Range(0, bmp1.Width))
            {
                foreach (int y in Enumerable.Range(0, bmp1.Height))
                {

                    Color clr = bmp1.GetPixel(x, y);
                    Bitmap img = Tools.AplyColor(clr, bmp);
                    g.DrawImage(img, x * dimensions, y * dimensions, dimensions, dimensions);
                }
            }
            return bmp2;
        }
        static int GetMiniumDistance(Color c1, Color c2)
        {
            int d1 = c1.R - c2.R;
            int d2 = c1.G - c2.G;
            int d3 = c1.B - c2.B;
            int distance = Convert.ToInt32(Math.Sqrt((d1 * d1) + (d2 * d2) + (d3 * d3)));
            return distance;
        }
        public static Bitmap Mosaic(Bitmap bmp, List<Bitmap> randomList, int dimensions = 25)
        {

            Bitmap bmp1 = ReSize(bmp, bmp.Height / dimensions, bmp.Width / dimensions);
            Bitmap bmp2 = new Bitmap(bmp1.Width * dimensions, bmp1.Height * dimensions);
            foreach (int b in Enumerable.Range(0, randomList.Count()))
            {
                randomList[b] = ReSize(randomList[b], dimensions, dimensions);
            }

            int len = randomList.Count();
            bmp = ReSize(bmp, dimensions, dimensions);
            Graphics g = Graphics.FromImage(bmp2);

            foreach (int x in Enumerable.Range(0, bmp1.Width))
            {
                foreach (int y in Enumerable.Range(0, bmp1.Height))
                {
                    int index = 0;
                    int dist = 442;
                    Color clr = bmp1.GetPixel(x, y);
                    foreach (int i in Enumerable.Range(0, randomList.Count()))
                    {
                        Color clr2 = Tools.getDominantColor(randomList[i]);
                        int tdist = GetMiniumDistance(clr, clr2);

                        if (dist >= tdist)
                        {
                            dist = tdist;
                            index = i;
                        }

                    }
                    Bitmap img = randomList[index];
                    g.DrawImage(img, x * dimensions, y * dimensions, dimensions, dimensions);
                }
            }
            return bmp2;
        }
        public static Bitmap TrapMosaic(Bitmap bmp, List<Bitmap> randomList, int dimensions = 10)
        {

            Bitmap bmp1 = ReSize(bmp, bmp.Height / dimensions, bmp.Width / dimensions);
            Bitmap bmp2 = new Bitmap(bmp1.Width * dimensions, bmp1.Height * dimensions);
            foreach (int b in Enumerable.Range(0, randomList.Count()))
            {
                randomList[b] = ReSize(randomList[b], dimensions, dimensions);
            }

            int len = randomList.Count();
            bmp = ReSize(bmp, dimensions, dimensions);
            Graphics g = Graphics.FromImage(bmp2);

            foreach (int x in Enumerable.Range(0, bmp1.Width))
            {
                foreach (int y in Enumerable.Range(0, bmp1.Height))
                {
                    int index = 0;
                    int dist = 442;
                    Color clr = bmp1.GetPixel(x, y);
                    foreach (int i in Enumerable.Range(0, randomList.Count()))
                    {
                        Color clr2 = Tools.getDominantColor(randomList[i]);
                        int tdist = GetMiniumDistance(clr, clr2);
                        if (dist >= tdist)
                        {
                            dist = tdist;
                            index = i;
                        }
                    }
                    Bitmap img = randomList[index];
                    img = Tools.AplyColor(clr, img);
                    g.DrawImage(img, x * dimensions, y * dimensions, dimensions, dimensions);
                }
            }
            return bmp2;
        }
    }
}
