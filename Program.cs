using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidSpreadSimulator
{
    class Program
    {
        static List<Person> persons = new List<Person> { };
        static void Main()
        {
            CreatePersons();
            while (true)
            {
                TryInfect();
            }
        }
        static void CreatePersons()
        {
            for (int i = 0; i < 50; i++)
            {
                bool infected;
                if (i == 0)
                {
                    infected = true;
                }
                else
                {
                    infected = false;
                }
                Person p = new Person(infected, 0, false);
                persons.Add(p);
            }
        }
        static void TryInfect()
        {
            var isInfected = from person in persons
                             where person.infected == true
                             && person.infectedHours < 5
                             select person;
            var isImmune = from person in persons
                           where person.immune == true
                           select person;
            var canSpreadDisease = from person in persons
                                   where person.infected == true
                                   && person.infectedHours < 5
                                   select person;
            Console.WriteLine(persons.Select(isInfected)); ;
            //foreach(Person p in persons)
            //{
            //    if(isInfected.Count() > 0)
            //    { 
            //    
            //    }
            //}
        }
    }
    class Person
    {
        public bool infected;
        public int infectedHours;
        public bool immune;
        public Person(bool infected, int infectedHours, bool immune)
        {
            this.immune = immune;
            this.infected = infected;
            this.infectedHours = infectedHours;
        }

        public bool canSpreadDisease()
        {
            if (this.infected == true && this.infectedHours < 5)
            {
                return true;
            }
            return false;
        }
    }
}
