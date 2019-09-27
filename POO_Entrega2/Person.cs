using System;
using System.Collections.Generic;
using System.Text;

namespace POO_Entrega2
{
    public class Person
    {
        //Atributes
        public string name;
        public string sex;
        public DateTime dateOfBirth; // (MM/DD/YY)

        //Constructor
        public Person(string name, string sex)
        {
            this.name = name;
            this.sex = sex;
        }
        public Person(string name)
        {
            this.name = name;
            this.sex = "";
        }

        //Methods
        public List<string> ShowPerson()
        {
            List<string> SLP = new List<string>();
            DateTime dateTime = dateOfBirth;
            string strDate = Convert.ToDateTime(dateTime).ToString("MMddyy");

            SLP.Add("Nombre: " + name);
            SLP.Add("Fecha nacimiento: " + strDate);
            SLP.Add("Sexo: " + sex);
            return SLP;
        }
    }
}
