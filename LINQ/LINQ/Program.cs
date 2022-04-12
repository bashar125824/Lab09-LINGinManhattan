using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace LINQ
{
    class Program
    {

        public class properties
        {
            public string zip { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string address { get; set; }
            public string borough { get; set; }
            public string neighborhood { get; set; }
            public string county { get; set; }

        }

        public class geometry {

            public string type { get; set; }
            public List<double> coordinates { get; set; }


        }


        public class features
        {
            public string type { set; get; }

            public geometry g { set; get; }
            
            public properties p { set; get; }
        }

        public class parent { 

            public string type { set; get; }

            public List<features> f { get; set; }


        }
        static void Main(string[] args)
        {

           
                parent info = JsonConvert.DeserializeObject<parent>(File.ReadAllText("./data.json"));

                countNeighbors(info);
                filterNeighbors(info);
                removeDuplicates(info);
                rewriteQueriesInOneQuery(info);
                opposingMethod(info);
            
            
        }
        public static void countNeighbors(parent p) 
        {

            int sum = 0;
            var queryOne = from features in p.f
                           select features.p.neighborhood;

            
            Console.WriteLine(" Neighbors : ");
            foreach (var n in queryOne)
            {
                Console.WriteLine(sum);

                sum++;
            }

        }

        public static void filterNeighbors(parent p2)
        {

            int sum = 0;
            var queryTwo = from features in p2.f
                           where features.p.neighborhood != ""
                           select features.p.neighborhood;
            Console.WriteLine(" Neighbors with no names : ");
            foreach (var n in queryTwo)
            {
                Console.WriteLine(sum); 
                sum++;
            }

        }
        public static void removeDuplicates(parent p3)
        {
            int sum = 0;
            var queryThree = (from features in p3.f
                      where features.p.neighborhood != ""
                      select features.p.neighborhood).Distinct();
           
            Console.WriteLine(" neighbors without duplicates : ");
            foreach (var n in queryThree)
            {
                Console.WriteLine(sum); 
                sum++;
            }
        }
        public static void rewriteQueriesInOneQuery(parent p4)
        {
            int sum = 0;
            var queryFour = p4.f
                .Select(features => new { features.p.neighborhood })
                .Where(features => features.neighborhood != "");

           
            Console.WriteLine(" Neighbors with no names : ");
            foreach (var element in queryFour)
            {
                Console.WriteLine(sum); 
                sum++;
            }
        }
        public static void opposingMethod(parent p5)
        {
            int sum = 0;
            var queryFive = p5.f
              
                .Select(f => new { f.p.neighborhood }); // lambda statement 
           
            Console.WriteLine(" neighbors : ");
            foreach (var n in queryFive)
            {
                Console.WriteLine(sum); 
                sum++;
            }
        }
        





    }
    }

