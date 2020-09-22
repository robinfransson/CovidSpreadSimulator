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

                int numberOfInfected = persons.Count(x => x.IsInfected); // hur många som är infekterade
                numberOfImmune = persons.Count(x => x.Immune); // hur många som är immuna
                int numberOfNotInfected = persons.Count(x => !x.IsInfected);// hur många som inte är infekterade
                toInfect = numberOfInfected; // här sätts variabeln för hur många som ska infekteras nästa loop
                Console.Clear();
                Console.WriteLine($"{hoursPassed} hours has passed, there are {numberOfInfected - numberOfImmune} infected among us, {numberOfImmune}" +
                    $" people are immune.\nThere are {numberOfNotInfected} people not infected");
                Thread.Sleep(200);

                foreach (var person in persons)
                {
                    if (toInfect > numberOfNotInfected) // om det är fler att infektera än vad det är icke infekterade sätts variablen till antalet icke infekterade
                    {
                        toInfect = numberOfNotInfected;
                    }
                    if (person.Immune) //om personen är immun ska nästa iteration påbörjas
                    {
                        continue;
                    }
                    if (person.CanSpreadDisease())
                    {
                        if (CheckInfectedHours(person) == 4) // efter 4 timmar blir personen immun
                        {
                            person.Immune = true;
                        }
                        else
                        {
                            person.InfectedHours++;
                        }
                    }
                    else if (!person.CanSpreadDisease() && toInfect >= 1) // om personen inte kan sprida sjukdom ska personen bli infekterad om det finns kvar 
                    {                                                     //personer som kan infektera denna loop 
                        person.IsInfected = true;
                        toInfect--;
                        if (toInfect == 0 && numberOfNotInfected > 0) // om det är den sista som ska infekteras behövs inte loopen köras längre
                        {
                            break;
                        }
                    }
                }
                hoursPassed++;
                
            }
            Console.ReadLine();
        }

        static int CheckInfectedHours(Person p)
        {
            return p.InfectedHours;
        }
    }

}
