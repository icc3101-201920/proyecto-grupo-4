using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Entrega_3
{
    public class Fraccion
    // formula sacada de http://www.sc.ehu.es/sbweb/fisica/cursoJava/fundamentos/estatico/fraccion/fraccion.htm
    {
        private int num;
        private int den;
        public Fraccion(int x, int y)
        {
            num = x;
            den = y;
        }

        private int mcd()
        {
            int u = Math.Abs(num);
            int v = Math.Abs(den);
            if (v == 0)
            {
                return u;
            }
            int r;
            while (v != 0)
            {
                r = u % v;
                u = v;
                v = r;
            }
            return u;
        }
        public Fraccion simplificar()
        {
            int dividir = mcd();
            num /= dividir;
            den /= dividir;
            return this;
        }
        public String toString()
        {
            String texto = num + " : " + den;
            return texto;
        }
    }
    public struct Coordenada
    {
        public int X;
        public int Y;
        public Person person;
        public Coordenada(int x, int y, Person p)
        {
            this.X = x;
            this.Y = y;
            this.person = p;
        }
    }
    public class Picture
    {
        //Atributes
        private string namePic;
        private string location;
        private string photographer;
        private string adress;
        private List<Label> label;
        private List<Person> persons;
        private List<Coordenada> etiquetados;
        private string saturation;
        private string resolution;
        private string aspectRatio;
        private Bitmap bitmap;
        private int r;
        private int b;
        private int g;
        private int calification;

        public string Adress { get => adress; set => adress = value; }
        public List<Label> Label { get => label; set => label = value; }
        public List<Person> Persons { get => persons; set => persons = value; }
        public List<Coordenada> Etiquetados { get => etiquetados; set => etiquetados = value; }
        public string Saturation { get => saturation; set => saturation = value; }
        public string Resolution { get => resolution; set => resolution = value; }

        public string AspectRatio { get => aspectRatio; set => aspectRatio = value; }
        public string Photographer { get => photographer; set => photographer = value; }
        public string Location { get => location; set => location = value; }
        public string NamePic { get => namePic; set => namePic = value; }
        public Bitmap Bitmap { get => bitmap; set => bitmap = value; }
        public int R { get => r; set => r = value; }
        public int B { get => b; set => b = value; }
        public int G { get => g; set => g = value; }
        public int Calification { get => calification; set => calification = value; }

        //Constructor
        public Picture(string name, Bitmap bmp = null, string loc = "", string ph = "", string ad = "")
        {
            this.NamePic = name;
            this.Location = loc;
            this.Photographer = ph;
            this.Adress = ad;
            Color clr = Tools.getDominantColor(bmp);
            this.saturation = $"R:{clr.R * 100 / 255 }%  G:{clr.G * 100 / 255}%  B:{clr.B * 100 / 255}% ";
            this.resolution = $"{bmp.Width} X {bmp.Height}";
            Fraccion f = new Fraccion(bmp.Width, bmp.Height);
            this.aspectRatio = f.simplificar().toString();
            this.Label = new List<Label>();
            this.Persons = new List<Person>();
            this.Etiquetados = new List<Coordenada>();
            this.R = clr.R;
            this.G = clr.G;
            this.B = clr.B;
            this.calification = 0;

            if (bmp == null)
            {
                this.Bitmap = new Bitmap(100, 100);
            }
            else
            {
                this.Bitmap = bmp;
            }
        }

        //Methodes 
        //coordenadas default 0,0 por temas practicos

        public void AddPerson(Person p, int X = 0, int Y = 0)
        {
            Persons.Add(p);
            Etiquetados.Add(new Coordenada(X, Y, p));
        }
        public void NewLabel(string labelName)
        {
            Label lbl = new Label(labelName);
            Label.Add(lbl);
        }
        public List<string> ShowPersonListPic()
        {
            List<string> ppl = new List<string>();

            ppl.Add("Personas etiquetadas: ");
            foreach (Person peolpe in Persons)
            {
                string Name = peolpe.Name;
                ppl.Add(Name);

            }
            return ppl;
        }
        public List<string> ShowLabelListPic()
        {
            List<string> lbl = new List<string>();

            lbl.Add("Personas etiquetadas: ");
            foreach (Label lableli in Label)
            {
                string Name = lableli.Name;
                lbl.Add(Name);

            }
            return lbl;
        }
        public List<string> ShowListPic()
        {

            List<string> SLP = new List<string>();
            SLP.Add($"Location:{ Location} ");
            SLP.Add($"Photographer: { Photographer } ");
            SLP.Add($"Adress: { Adress } ");
            SLP.Add($"Saturation: { Saturation } ");
            SLP.Add($"Resolution: { Resolution } ");
            SLP.Add($"Aspect Ratio: { AspectRatio } ");

            string personas = "People: ";
            foreach (Person P in Persons)
            {
                personas += P.Name + ", ";

            }
            personas += " ";
            SLP.Add(personas);

            string lbl = "";
            foreach (Label i in label)
            {
                lbl += i.Name + ", ";

            }
            lbl += " ";

            SLP.Add(lbl);


            return SLP;
        }
        public string GetInfo()
        {
            string info = "";
            List<string> InfoList = ShowListPic();
            foreach (string i in InfoList)
            {
                info += (i + " ");
            }
            return info;
        }
        public bool LookUp(string value)
        {
            foreach (Person i in Persons)
            {
                if (value == i.Name)
                {
                    return true;
                }
                if (value == i.Sex)
                {
                    return true;
                }

            }
            foreach (Label i in label)
            {

                if (value == i.Name)
                {
                    return true;
                }

            }
            if (value == NamePic)
            {
                return true;
            }
            else if (value == Location)
            {
                return true;
            }
            else if (value == Photographer)
            {
                return true;
            }
            else if (value == Adress)
            {
                return true;
            }
            else if (value == Saturation)
            {
                return true;
            }
            else if (value == Resolution)
            {
                return true;
            }
            else if (value == AspectRatio)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
