using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Entrega_3
{
    public static class StaticPicture
    {
        //Atributes
        public static string namePic;
        public static string location;
        public static string photographer;
        public static string adress;
        public static List<Label> label;
        public static List<Person> persons;
        public static List<Coordenada> etiquetados;
        public static string saturation;
        public static string resolution;
        public static string aspectRatio;
        public static int Calification;
        private static Bitmap bmp;
        public static Bitmap BmpCopy;

        public static Bitmap Bmp { get => bmp; set => bmp = value; }



        //Constructor


        //Methodes 
        //coordenadas default 0,0 por temas practicos

        public static void AddPerson(Person p, int X = 0, int Y = 0)
        {
            persons.Add(p);
            etiquetados.Add(new Coordenada(X, Y, p));
        }
        public static void NewLabel(string labelName)
        {
            Label lbl = new Label(labelName);
            label.Add(lbl);
        }
        public static void AddLabel(Label lbl)
        {
            label.Add(lbl);
        }
        public static List<string> ShowPersonListPic()
        {

            List<string> ppl = new List<string>();


            foreach (Person peolpe in persons)
            {
                string Name = peolpe.Name;
                ppl.Add(Name);

            }
            return ppl;
        }
        public static List<string> ShowLabelListPic()
        {
            List<string> ppl = new List<string>();


            foreach (Label lbl in label)
            {
                string Name = lbl.Name;
                ppl.Add(Name);

            }
            return ppl;
        }
        public static void CopyActualPicture(Picture pic)
        {
            namePic = pic.NamePic;
            location = pic.Location;
            photographer = pic.Photographer;
            adress = pic.Adress;
            //List<Label> label;
            label = new List<Label>();
            foreach (Label lbl in pic.Label)
            {
                label.Add(lbl);
            }
            //List<Person> persons;
            persons = new List<Person>();
            foreach (Person p in pic.Persons)
            {
                persons.Add(p);
            }
            //List<Coordenada> etiquetados;
            etiquetados = new List<Coordenada>();
            foreach (Coordenada Coord in pic.Etiquetados)
            {
                etiquetados.Add(Coord);
            }
            saturation = pic.Saturation;
            resolution = pic.Resolution;
            aspectRatio = pic.AspectRatio;
            bmp = pic.Bitmap;
            BmpCopy = (Bitmap)pic.Bitmap.Clone();
        }
        public static List<string> ShowListPic()

        {

            List<string> SLP = new List<string>();
            SLP.Add($"Location:{ location} ");
            SLP.Add($"Photographer: { photographer } ");
            SLP.Add($"Adress: { adress } ");
            SLP.Add($"Saturation: { saturation } ");
            SLP.Add($"Resolution: { resolution } ");
            SLP.Add($"Aspect Ratio: { aspectRatio }.");

            string personas = "People: ";
            foreach (Person P in persons)
            {
                personas += P.Name + ", ";

            }
            personas += " ";
            SLP.Add(personas);

            string lbl = "Labels: ";
            foreach (Label i in label)
            {
                lbl += i.Name + ", ";

            }
            lbl += " ";

            SLP.Add(lbl);


            return SLP;
        }
        public static string GetInfo()
        {
            string info = "";
            List<string> InfoList = ShowListPic();
            foreach (string i in InfoList)
            {
                info += (i + " ");
            }
            return info;
        }
        public static void Save()
        {
            Picture pic = new Picture("Modified" + namePic, BmpCopy, location, photographer, adress);
            StaticAlbum.AddPic(pic);
            pic.Bitmap.Save(pic.NamePic);
        }
        public static void Update()
        {
            resolution = $"{BmpCopy.Width} X {BmpCopy.Height}";
            Fraccion f = new Fraccion(BmpCopy.Width, BmpCopy.Height);
            aspectRatio = f.simplificar().toString();
        }
        public static void DelateLabel(int index)
        {
            label.RemoveAt(index);
        }
        public static void DelatePerson(int index)
        {
            persons.RemoveAt(index);
        }
    }
}
