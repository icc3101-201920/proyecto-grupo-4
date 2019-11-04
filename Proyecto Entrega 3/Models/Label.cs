using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Entrega_3
{
    public class Label
    {
        //Atributes
        private string name;
        public string Name { get => name; set => name = value; }
        //Constructors
        public Label(string LabelName)
        {
            this.Name = LabelName;

        }

    }
}
