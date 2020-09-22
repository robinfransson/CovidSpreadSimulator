using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidSpreadSimulator
{
        class Person
    {
        public int Iteration { get; set; }
        public bool IsInfected { get; set; }
    public int InfectedHours { get; set; }
        public bool Immune { get; set; }


        public Person(bool infected, bool immune, int id)
        {
            this.Immune = immune;
            this.IsInfected = infected;
            this.InfectedHours = 0;
            this.Iteration = id;
        }

        public bool CanSpreadDisease() // om personen är infekterad och inte immun kan den sprida sjukdom
        {
            if (this.IsInfected && !this.Immune)
            {
                return true;
            }
            return false;
        }
    }
}
