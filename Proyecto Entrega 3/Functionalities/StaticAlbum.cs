using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Entrega_3
{
    static class StaticAlbum
    {
        //Atributes
        public static List<Picture> picList;
        public static Dictionary<string, Picture> album;
        public static List<Picture> selectedPicList;
        public static List<Label> labelList;
        public static List<Person> peopleList;

        static StaticAlbum()
        {
            picList = new List<Picture>();
            album = new Dictionary<string, Picture>();
            selectedPicList = new List<Picture>();
            labelList = new List<Label>();
            peopleList = new List<Person>();
        }
        //Methods
        public static void AddPic(Picture pic)
        {
            string name = pic.NamePic;
            int c = 1;
            while (true)
            {
                if (album.ContainsKey(pic.NamePic))
                {
                    pic.NamePic = $"({c}){name}";
                    c++;
                    continue;
                }

                break;

            }

            picList.Add(pic);
            album.Add(pic.NamePic, pic);
        }
        public static List<Picture> Copy()
        {
            List<Picture> copy = new List<Picture>();
            foreach (Picture pic in picList)
            {
                copy.Add(pic);
            }
            return copy;
        }
        public static List<string> ShowNames()
        {
            List<string> names = new List<string>();

            foreach (Picture pic in picList)
            {
                names.Add(pic.NamePic);
            }
            return names;
        }
        public static List<string> ShowSelectedNames()
        {
            List<string> names = new List<string>();

            foreach (Picture pic in selectedPicList)
            {
                names.Add(pic.NamePic);
            }
            return names;
        }
        public static List<string> ShowPeopleNames()
        {
            List<string> names = new List<string>();

            foreach (Person p in peopleList)
            {
                names.Add(p.Name);
            }
            return names;
        }
        public static List<string> ShowLabelNames()
        {
            List<string> names = new List<string>();

            foreach (Label lbl in labelList)
            {
                names.Add(lbl.Name);
            }
            return names;
        }
        public static Label SearchLabel(string labelName)
        {
            foreach (Label lbl in labelList)
            {
                if (lbl.Name == labelName)
                {
                    return lbl;
                }
            }
            return new Label(labelName);
        }
        public static Person SearchPerson(string labelName)
        {
            foreach (Person p in peopleList)
            {
                if (p.Name == labelName)
                {
                    return p;
                }
            }
            return new Person(labelName, "Non");
        }
        public static void AddNewLabel(Label lbl)
        {
            labelList.Add(lbl);
        }
        public static void AddNewPerson(Person p)
        {
            peopleList.Add(p);
        }
        public static List<Picture> Search(string value)
        {
            List<Picture> list = new List<Picture>();
            foreach (Picture i in picList)
            {
                if (i.LookUp(value))
                {
                    list.Add(i);
                }
            }
            return list;
        }
        public static List<Bitmap> GetSelectedBitmaps()
        {
            List<Bitmap> BmpList = new List<Bitmap>();
            if (selectedPicList.Count == 0)
            {
                BmpList.Add(StaticPicture.BmpCopy);
                BmpList.Add(StaticPicture.BmpCopy);
                return BmpList;
            }


            BmpList = new List<Bitmap>();
            foreach (Picture pic in selectedPicList)
            {
                BmpList.Add((Bitmap)pic.Bitmap.Clone());
            }

            return BmpList;
        }
        public static void AddSelecctedPic(string name)
        {
            Picture pic = GetPicture(name);
            selectedPicList.Add(pic);
        }
        public static Picture GetPicture(string name)
        {
            return album[name];
        }
        public static void DelatePic(int index)
        {
            selectedPicList.RemoveAt(index);
        }
        public static void DelateLabel(int index)
        {
            labelList.RemoveAt(index);
        }
        public static void DelatePerson(int index)
        {
            peopleList.RemoveAt(index);
        }
    }
}
