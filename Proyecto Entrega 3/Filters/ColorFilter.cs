using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Entrega_3
{
    public static class ColorFilter
    {
        static ColorFilter()
        {

        }
        // codigo sacado de https://code.msdn.microsoft.com/windowsdesktop/ColorMatrix-Image-Filters-f6ed20ae
        // el codigo se sacó de la pagina, pero se modificó para aplicarlo de mejor manera
        public static Bitmap Transform(Bitmap Img, string filter)
        {
            float[][] Values = {
                    new float[] { 1, 0, 0, 0, 0 },
                    new float[] { 0, 1, 0, 0, 0 },
                    new float[] { 0, 0, 1, 0, 0 },
                    new float[] { 0, 0, 0, 1, 0 },
                    new float[] { 0, 0, 0, 0, 1 }};
            switch (filter)
            {
                case "sepia":
                    float[][] sepiaValues = {
                    new float[]{.393f, .349f, .272f, 0, 0},
                    new float[]{.769f, .686f, .534f, 0, 0},
                    new float[]{.189f, .168f, .131f, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 0, 0, 1}};
                    Values = sepiaValues;

                    break;
                case "greyscale":
                    float[][] greyscaleValues = {
                    new float[]{.299f, .299f, .299f, 0, 0},
                    new float[]{.587f, .587f, .587f, 0, 0},
                    new float[]{.114f, .114f, .114f, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 0, 0, 1}};
                    Values = greyscaleValues;
                    break;


                case "negative":
                    float[][] negativeValues = {
                    new float[] { -1, 0, 0, 0, 0 },
                    new float[] { 0, -1, 0, 0, 0 },
                    new float[] { 0, 0, -1, 0, 0 },
                    new float[] { 0, 0, 0, 1, 0 },
                    new float[] { 1, 1, 1, 1, 1 }};
                    Values = negativeValues;
                    break;

                case "red":
                    float[][] redValues = {
                     new float[] { 1.4f, 0, 0, 0, 0 },
                    new float[] { 0, 0.6f, 0, 0, 0 },
                    new float[] { 0, 0, 0.6f, 0, 0 },
                    new float[] { 0, 0, 0, 1, 0 },
                    new float[] { 0, 0,0, 0, 1 }};
                    Values = redValues;
                    break;
                case "orange":
                    float[][] orangeValues = {
                    new float[]{1.7f, 0, 0, 0, 0},
                    new float[]{0, 0.8f, 0, 0, 0},
                    new float[]{0, 0, 0.2f, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 0, 0, 1}};
                    Values = orangeValues;
                    break;
                case "yellow":
                    float[][] yellowValues = {
                    new float[]{1.5f, 0, 0, 0, 0},
                    new float[]{0, 1.1f, 0, 0, 0},
                    new float[]{0, 0, 0.2f, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 0, 0, 1}};
                    Values = yellowValues;
                    break;
                case "green":
                    float[][] greenValues = {
                    new float[]{0.8f, 0, 0, 0, 0},
                    new float[]{0, 1f, 0, 0, 0},
                    new float[]{0, 0, 0.6f, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 0, 0, 1}};
                    Values = greenValues;
                    break;
                case "lightblue":
                    float[][] lbValues = {
                    new float[]{0.8f, 0, 0, 0, 0},
                    new float[]{0, 0.8f, 0, 0, 0},
                    new float[]{0, 0, 1.9f, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 0, 0, 1}};
                    Values = lbValues;
                    break;
                case "blue":
                    float[][] blueValues = {
                    new float[]{0.5f, 0, 0, 0, 0},
                    new float[]{0, 0.5f, 0, 0, 0},
                    new float[]{0, 0, 1.1f, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 0, 0, 1}};
                    Values = blueValues;
                    break;
                case "purple":
                    float[][] purpleValues = {
                    new float[]{ 1.1f, 0, 0, 0, 0},
                    new float[]{0, 0.5f, 0, 0, 0},
                    new float[]{0, 0, 1.1f, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 0, 0, 1}};
                    Values = purpleValues;
                    break;
                case "frozen":
                    float[][] frozenValues = {
                    new float[]{.393f+0.3f, .349f, .272f, 0, 0},
                    new float[]{.769f, .686f+0.2f, .534f, 0, 0},
                    new float[]{.189f, .168f, .131f+0.9f, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 0, 0, 1}};
                    Values = frozenValues;
                    break;
                case "transparent":
                    float[][] transparentValues = {
                    new float[]{1, 0, 0, 0, 0},
                    new float[]{0, 1, 0, 0, 0},
                    new float[]{0, 0, 1, 0, 0},
                    new float[]{0, 0, 0, 0.2f, 0},
                    new float[]{0, 0, 0, 0, 1}};
                    Values = transparentValues;
                    break;
                default:
                    break;

            }

            System.Drawing.Imaging.ColorMatrix Matrix = new System.Drawing.Imaging.ColorMatrix(Values);
            System.Drawing.Imaging.ImageAttributes IA = new System.Drawing.Imaging.ImageAttributes();
            IA.SetColorMatrix(Matrix);
            Bitmap Effect = (Bitmap)Img.Clone();
            using (Graphics G = Graphics.FromImage(Effect))
            {
                G.DrawImage(Img, new Rectangle(0, 0, Effect.Width, Effect.Height), 0, 0, Effect.Width, Effect.Height, GraphicsUnit.Pixel, IA);
            }
            Img = Effect;
            return Img;
        }
        public static Bitmap Rainbow(Bitmap img)
        {
            int width = img.Width;
            int height = img.Height;
            Bitmap copy = (Bitmap)img.Clone();
            int x = width / 7;
            List<Bitmap> images = new List<Bitmap>() { };
            int count = 0;
            foreach (int index in Enumerable.Range(1, 7))
            {
                images.Add(Filters.Cut(copy, count, 0, height, x));
                count += x;
            }

            images[0] = Transform(images[0], "red");
            images[1] = Transform(images[1], "orange");
            images[2] = Transform(images[2], "yellow");
            images[3] = Transform(images[3], "green");
            images[4] = Transform(images[4], "lightblue");
            images[5] = Transform(images[5], "blue");
            images[6] = Transform(images[6], "purple");


            img = Filters.Combine(images);

            return img;


        }
    }
}
