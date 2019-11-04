using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Entrega_3
{
    public class PictureList
    {
        //Atributes
        public List<Picture> picList;
        public Dictionary<string, Picture> album;

        //Constructor
        public PictureList()
        {
            this.picList = new List<Picture>();
            this.album = new Dictionary<string, Picture>();
        }
        //Methods
        public void AddPic(Picture pic)
        {
            picList.Add(pic);
            album.Add(pic.NamePic, pic);
        }
        public List<Picture> Copy()
        {
            List<Picture> copy = new List<Picture>();
            foreach (Picture pic in picList)
            {
                copy.Add(pic);
            }
            return copy;
        }
        public List<string> ShowNames()
        {
            List<string> names = new List<string>();

            foreach (Picture pic in picList)
            {
                names.Add(pic.NamePic);
            }
            return names;
        }
        public void AddListOfPics(List<Picture> LP)
        {
            foreach (Picture pic in LP)
            {
                picList.Add(pic);
                album.Add(pic.NamePic, pic);
            }

        }
        public List<string> ShowPicture(string name)
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
                alb.Add(pic.NamePic);
                return alb;
            }
        }
        public List<Picture> Search(string value)
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
