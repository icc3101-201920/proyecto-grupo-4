using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Entrega_3
{
    class Busqueda
    {
        static Busqueda()
        {
        }

        public static (string, List<Picture>) Buscar(string Search)
        {
            string[] ASearchs = Search.Split(' ');
            List<string> searchs = new List<string>();
            foreach (string s in ASearchs)
            {
                searchs.Add(s);
            }

            int c = 0;
            List<Picture> search;
            PictureList temporalpiclist = new PictureList();
            List<string> notFound = new List<string>();
            notFound.Add("Resultados no encontrados");
            foreach (string lb in searchs)
            {
                if (c == 0)
                {
                    search = StaticAlbum.Search(lb);
                    if (search.Count == 0)
                    {
                        notFound.Add(lb);
                        continue;
                    }
                    temporalpiclist.AddListOfPics(search);
                    c++;
                }
                search = temporalpiclist.Search(lb);
                if (search.Count == 0)
                {
                    notFound.Add(lb);
                    continue;
                }
                temporalpiclist = new PictureList();
                temporalpiclist.AddListOfPics(search);
            }
            search = temporalpiclist.Copy();

            string Message = "";
            foreach (string word in notFound)
            {
                Message += (word + " ");
            }

            if (notFound.Count() == 1)
            {
                Message = "";
            }
            return (Message, search);

        }
    }
}