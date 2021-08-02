using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = ReverseString("Exemplar");
            //Console.WriteLine(test);

            string ReverseString(string input)
            {
                var temp = default(string);
                for (int i = input.Length - 1; i > -1; i--)
                {
                    temp += input[i];
                }
                return temp;
            }

            var inventors = new List<Inventor>();

            inventors.Add(new Inventor() { 
                first  = "Albert",
                last   = "Einstin",
                year   = 1879,
                passed = 1955
                });
            inventors.Add(new Inventor()
            {
                first  = "Isac",
                last   = "Newton",
                year   = 1643,
                passed = 1727
            });
            inventors.Add(new Inventor()
            {
                first  = "Galileo",
                last   = "Galilei",
                year   = 1564,
                passed = 1642
            });
            inventors.Add(new Inventor()
            {
                first  = "Marie",
                last   = "Curie",
                year   = 1867,
                passed = 1934
            });
            inventors.Add(new Inventor()
            {
                first  = "Johannes",
                last   = "Kepler",
                year   = 1571,
                passed = 1630
            });
            inventors.Add(new Inventor()
            {
                first  = "Nicolaus",
                last   = "Copernicus",
                year   = 1473,
                passed = 1543
            });
            inventors.Add(new Inventor()
            {
                first  = "Max",
                last   = "Planck",
                year   = 1858,
                passed = 1947
            });
            
            var people = new string[] {
                "Beck, Glenn", "Becker, Carl", "Beckett, Samuel",
                "Beddoes, Mick", "Beecher, Henry", "Beethoven, Ludwig",
                "Begin, Menachem", "Belloc, Hilaire", "Bellow, Saul",
                "Benchley, Robert", "Benenson, Peter", "Ben-Gurion, David",
                "Benjamin, Walter", "Benn, Tony", "Bennington, Chester",
                "Benson, Leana", "Bent, Silas", "Bentsen, Lloyd", "Berger, Ric",
                "Bergman, Ingmar", "Berio, Luciano", "Berle, Milton",
                "Berlin, Irving", "Berne, Eric", "Bernhard, Sandra",
                "Berra, Yogi", "Berry, Halle", "Berry, Wendell", "Bethea, Erin",
                "Bevan, Aneurin", "Bevel, Ken", "Biden, Joseph",
                "Bierce, Ambrose", "Biko, Steve", "Billings, Josh",
                "Biondo, Frank", "Birrell, Augustine", "Black, Elk",
                "Blair, Robert", "Blair, Tony", "Blake, William"
            };

            // 1. Filter the list of inventors for those who were born in the 1500's
            var bornAfter1500 = inventors.Where(x => x.year > 1499)
                                         .Where(x => x.year < 1599)
                                         .Select(x => x.year);
            Console.WriteLine("////// Inventors born in 1500's ///////");
            foreach (var inventor in bornAfter1500)
            {
                Console.WriteLine(inventor);
            }
            // 2. Give us an array of the inventory first and last names
            var firstAndLastName = inventors.Select(x => new
            {
                x.first,
                x.last
            });
            Console.WriteLine("////// Inventors First and Last Names ///////");
            foreach (var inventor in firstAndLastName)
            {
                Console.WriteLine(inventor.first);
                Console.WriteLine(inventor.last);
                Console.WriteLine("----------");
            }
            // 3. Sort the inventors by birthdate, oldest to youngest
            var orderedBirthdate = inventors.OrderBy(x => x.year);
            Console.WriteLine("////// Order Inventors by Age ///////");
            foreach (var inventor in orderedBirthdate)
            {
                Console.WriteLine(inventor.first);
            }
            // 4. How many years did all inventors live?
            var allAges = default(int);
            Console.WriteLine("////// All the Inventors years togheter ///////");
            foreach (var item in inventors)
            {
                allAges += item.passed - item.year;
            }
            Console.WriteLine(allAges);
            // 4.1 Average of the inventors age
            Console.WriteLine("////// All Inventors average age ///////");
            var inventorsAge = new List<int>();
            foreach (var item in inventors)
            {
                inventorsAge.Add(item.passed - item.year);
            }
            Console.WriteLine(inventorsAge.Average());
            // 5. Sort the inventors by years lived
            Console.WriteLine("////// Ordered All Inventors average age ///////");
            inventorsAge.Sort();
            for (int i = 0; i < inventorsAge.Count; i++)
            {
                Console.WriteLine(inventorsAge[i]);
            }
            // 6. Create a list of Boulevards in Paris that contain 'de' anywhere in the name
            // https://en.wikipedia.org/wiki/Category:Boulevards_in_Paris
            Console.WriteLine("////// Scraping and Filtering Strings with 'de' ///////");
            var itemSelected = new List<string>();
            var stringToCheck = "de";
            var url = "https://en.wikipedia.org/wiki/Category:Boulevards_in_Paris";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            HtmlNodeCollection parentSelectors = doc.DocumentNode.SelectNodes("//*[@class='mw-category-group']/ul/li/a/text()");
            foreach (var item in parentSelectors)
            {
                var selectors = item.InnerText;
                itemSelected.Add(selectors);
            }
            var filteredItems = itemSelected.Where( x => x.Contains(stringToCheck));
            foreach (var item in filteredItems)
            {
                Console.WriteLine(item);
            }
            // 7. Sort the people alphabetically by last name
            var lastNameAlphabetically = inventors.OrderBy(x => x.last)
                                                  .Select( x => x.last);
            Console.WriteLine("////// Ordered Alphabetically Inventors last Name ///////");
            foreach (var item in lastNameAlphabetically)
            {
                Console.WriteLine(item);
            }
            // 8. Sum up the instances of each of these
            var data = new string[] {
                "car","car", "truck", "truck", "bike", "walk", "car", "van",
                "bike", "walk", "car", "van", "car", "truck"
            };
            var repeatedData = data.GroupBy(x => x);
            Console.WriteLine("////// Finding how many items repeat ///////");
            foreach (var item in repeatedData)
            {
                Console.WriteLine($"{item.Key} has {item.Count()} items");
            }
        }
    }

    public class Inventor
    {
        public string first;
        public string last;
        public int    year;
        public int    passed;
    }
}
