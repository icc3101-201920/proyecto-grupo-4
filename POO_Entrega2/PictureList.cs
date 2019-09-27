using System;
using System.Collections.Generic;

namespace POO_Entrega2
{
    public static class TemporalPictureList
    {
        //Atributes
        public static List<Picture> picList;
        public static Dictionary<string, Picture> album;

        //Constructor, no hay


        //Methods
        public static void AddPic(Picture pic)
        {
            picList.Add(pic);
            album.Add(pic.namePic, pic);
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
        public static void Default()
        {
            int counter = 0;
            while (counter <= 50)
            {
                counter++;
                Picture pic = new Picture($"IMG{counter}");
                picList.Add(pic);

                album.Add(pic.namePic, pic);
            }
        }

        public static List<string> ShowNames()
        {
            List<string> names = new List<string>();

            foreach (Picture pic in picList)
            {
                names.Add(pic.namePic);
            }
            return names;
        }
        public static void AddListOfPics(List<Picture> LP)
        {
            foreach (Picture pic in LP)
            {
                picList.Add(pic);
                album.Add(pic.namePic, pic);
            }

        }


        public static List<string> ShowPicture(string name)
        {
            if (album.ContainsKey(name))
            {
                List<string> alb = album[name].ShowListPic();
                return alb;
            }
            else
            {
                List<string> alb = album[name].ShowListPic();
                Picture pic = new Picture("NONE");
                alb.Add(pic.namePic);
                return alb;
            }
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
    }
}
