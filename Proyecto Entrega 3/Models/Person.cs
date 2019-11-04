using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Entrega_3
{
    public class Person
    {
        //Atributes
        private string name;
        private string sex;
        private DateTime dateOfBirth; // (MM/DD/YY)

        public string Name { get => name; set => name = value; }
        public string Sex { get => sex; set => sex = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }

        //Constructor
        public Person(string name, string sex)
        {
            this.Name = name;
            this.sex = sex;
        }
        public Person(string name)
        {
            this.Name = name;
            this.sex = "";
        }

        //Methods
        public string GetPersonInfo()
        {
            string SLP = "";
            DateTime dateTime = dateOfBirth;
            string strDate = Convert.ToDateTime(dateTime).ToString("dd/MM/yyyy");

            SLP += ($"Nombre: { Name}\n");
            SLP += ($"Fecha nacimiento:  {strDate}\n");
            SLP += ($"Sexo:  {sex}\n");
            return SLP;
        }

    }
}
