using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
namespace POO_Entrega2
{
    public struct Coordenada
    {
        public double X;
        public double Y;
        public Person person;
        public Coordenada(double x, double y, Person person)
        {
            this.X = x;
            this.Y = y;
            this.person = person;
        }
    }
    public class Picture
    {
        //Atributes
        public string namePic;
        public string location;
        public string photographer;
        public string adress;
        public List<Label> label;
        public List<Person> persons;
        public List<Coordenada> Etiquetados;
        public string saturation;
        public string resolution;
        public string aspectRatio;
        public Bitmap image;



        //Constructor
        public Picture(string name)
        { //POR AHORA LO DEJAMOS VACIO, LUEGO IMPLEMENTAREMOS COMO PONER ETIQUETAS A TODAS LAS FOTOS
            this.namePic = name;
            this.location = "";
            this.photographer = "";
            this.adress = "";
            this.saturation = "";
            this.resolution = "";
            this.aspectRatio = "";
            this.label = new List<Label>();
            this.persons = new List<Person>();
            this.Etiquetados = new List<Coordenada>();

        }
        public Picture(Bitmap img)
        { //POR AHORA LO DEJAMOS VACIO, LUEGO IMPLEMENTAREMOS COMO PONER ETIQUETAS A TODAS LAS FOTOS
            this.namePic = img.ToString();
            this.location = "";
            this.photographer = "";
            this.adress = "";
            this.saturation = "";
            this.resolution = "";
            this.aspectRatio = "";
            this.label = new List<Label>();
            this.persons = new List<Person>();
            this.Etiquetados = new List<Coordenada>();

        }
        public Picture(string name, string loc, string ph, string ad, string sat, string res, string asp)
        {
            this.namePic = name;
            this.location = loc;
            this.photographer = ph;
            this.adress = ad;
            this.saturation = sat;
            this.resolution = res;
            this.aspectRatio = asp;
            this.label = new List<Label>();
            this.persons = new List<Person>();
            this.Etiquetados = new List<Coordenada>();

        }
        public Picture(string name, string loc, string ph, string ad)
        {
            this.namePic = name;
            this.location = loc;
            this.photographer = ph;
            this.adress = ad;
            this.saturation = "";
            this.resolution = "";
            this.aspectRatio = "";
            this.label = new List<Label>();
            this.persons = new List<Person>();
            this.Etiquetados = new List<Coordenada>();

        }
        //Methodes 
        public void AddPerson(Person p)//coordenadas default 0,0 por temas practicos
        {
            persons.Add(p);
            Etiquetados.Add(new Coordenada(0.0, 0.0, p));
        }
        public void AddPerson(double X, double Y, Person p)
        {
            persons.Add(p);
            Etiquetados.Add(new Coordenada(X, Y, p));
        }
        public void NewLabel(string labelName)
        {
            Label lbl = new Label(labelName);
            label.Add(lbl);
        }

        public List<string> ShowPersonListPic()
        {
            List<string> ppl = new List<string>();

            ppl.Add("Personas etiquetadas: ");
            foreach (Person peolpe in persons)
            {
                string Name = peolpe.name;
                ppl.Add(Name);

            }
            return ppl;
        }
        public List<string> ShowLabelListPic()
        {
            List<string> lbl = new List<string>();

            lbl.Add("Personas etiquetadas: ");
            foreach (Label lableli in label)
            {
                string Name = lableli.name;
                lbl.Add(Name);

            }
            return lbl;
        }
        public List<string> ShowListPic()
        {

            List<string> SLP = new List<string>();
            SLP.Add("Location: " + location);
            SLP.Add("Photographer: " + photographer);
            SLP.Add("Adress: " + adress);
            SLP.Add("saturation: " + saturation);
            SLP.Add("Resolution: " + resolution);
            SLP.Add("Aspect Ratio: " + aspectRatio);
            SLP.Add("Personas: ");
            foreach (Person P in persons)
            {
                SLP.Add(P.name);
            }

            return SLP;
        }

        public bool LookUp(string value)
        {
            foreach (Person i in persons)
            {
                if (value == i.name)
                {
                    return true;
                }
                if (value == i.sex)
                {
                    return true;
                }

            }
            if (value == namePic)
            {
                return true;
            }
            else if (value == location)
            {
                return true;
            }
            else if (value == photographer)
            {
                return true;
            }
            else if (value == adress)
            {
                return true;
            }
            else if (value == saturation)
            {
                return true;
            }
            else if (value == resolution)
            {
                return true;
            }
            else if (value == aspectRatio)
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