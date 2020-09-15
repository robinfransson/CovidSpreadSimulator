using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CovidSpreadSimulator
{
    class Program
    {
        static List<Person> persons = new List<Person> { };
        static void Main()
        {
            CreatePersons();
            TryInfect();

        }
        static void CreatePersons()
        {
            for (int i = 0; i < 50; i++)
            {
                bool infected;
                if (i == 0) // första ska vara smittsam
                {
                    infected = true;
                }
                else
                {
                    infected = false;
                }
                Person p = new Person(infected, false, i);
                persons.Add(p);
            }
        }
        static void TryInfect()
        {
            int numberOfImmune = 0;
            int toInfect = 0;
            int hoursPassed = 0;
            while (numberOfImmune < 50)
            {
                int numberOfInfected = persons.Where(x => x.isInfected).Count(); // hur många som är infekterade
                numberOfImmune = persons.Where(x => x.immune).Count(); // hur många som är immuna
                int numberOfNotInfected = persons.Where(x => !x.isInfected).Count();// hur många som inte är infekterade
                toInfect = numberOfInfected; // här sätts variabeln för hur många som ska infekteras nästa loop
                foreach (var person in persons)
                {
                    //Console.WriteLine("{0}, {1}",person.iteration, person.infectedHours);
                    Thread.Sleep(50);
                    if (toInfect > numberOfNotInfected) // om det är fler att infektera än vad det är icke infekterade sätts variablen till antalet icke infekterade
                    {
                        toInfect = numberOfNotInfected;
                    }
                    if (!person.immune) // om personen inte är immun
                    {
                        if (person.CanSpreadDisease())
                        {
                            if (person.infectedHours > 3) // efter 4 timmar blir personen immun
                            {
                                person.immune = true;
                            }
                            else
                            {
                                person.infectedHours++;
                            }
                        }
                        else if (!person.CanSpreadDisease() && toInfect >= 1) // om personen inte kan sprida sjukdom ska personen bli infekterad om det finns kvar 
                        {                                                     //personer som kan infektera denna loop 
                            person.isInfected = true;
                            toInfect--;
                            if (toInfect == 0 && numberOfNotInfected > 0) // om det är den sista som ska infekteras behövs inte loopen köras längre
                            {
                                break;
                            }
                        }

                    }

                }
                hoursPassed++;
                Console.Clear();
                Console.WriteLine($"{hoursPassed} hours has passed, there are {numberOfInfected-numberOfImmune} infected among us, {numberOfImmune}" +
                    $" people are immune.\nThere are {numberOfNotInfected} people not infected");
                Thread.Sleep(50);
            }
            Console.ReadLine();
        }
    }
    class Person
    {
        public int iteration;
        public bool isInfected;
        public int infectedHours;
        public bool immune;
        public Person(bool infected, bool immune, int id)
        {
            this.immune = immune;
            this.isInfected = infected;
            this.infectedHours = 0;
            this.iteration = id;
        }

        public bool CanSpreadDisease() // om personen är infekterad och inte immun kan den sprida sjukdom
        {
            if (this.isInfected == true && !this.immune)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
