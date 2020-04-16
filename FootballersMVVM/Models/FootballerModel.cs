using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballersMVVM.Models
{
    /// <summary>
    /// Class represents a single footballer.
    /// </summary>
    public class FootballerModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Weight { get; set; }
        public int Age { get; set; }
        public string FullContent
        {
            get
            {
                return ToString();
            }
        }

        public FootballerModel()
        {
            FirstName = "";
            LastName = "";
            Weight = 1;
            Age = 1;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="firstname">Footballer firstname.</param>
        /// <param name="lastname">Footballer lastname.</param>
        /// <param name="weight">Footballer weight.</param>
        /// <param name="age">Footballer age.</param>
        public FootballerModel(string firstname, string lastname, int weight, int age)
        {
            FirstName = firstname;
            LastName = lastname;
            Weight = weight;
            Age = age;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, weight: {Weight}kg, age: {Age}";
        }
    }
}
