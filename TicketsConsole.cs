using System;
using System.Collections.Generic;
using System.Linq;
using static TicketsConsole.Program;

/*

Let's say we're running a small entertainment business as a start-up. This means we're selling tickets to live events on a website. An email campaign service is what we are going to make here. We're building a marketing engine that will send notifications (emails, text messages) directly to the client and we'll add more features as we go.

Please, instead of debuging with breakpoints, debug with "Console.Writeline();" for each task because the Interview will be in Coderpad and in that platform you cant do Breakpoints.

*/

namespace TicketsConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*

            1. You can see here a list of events, a customer object. Try to understand the code, make it compile. 

            2.  The goal is to create a MarketingEngine class sending all events through the constructor as parameter and make it print the events that are happening in the same city as the customer. To do that, inside this class, create a SendCustomerNotifications method which will receive a customer as parameter and will mock the Notification Service. Add this ConsoleWriteLine inside the Method to mock the service. Inside this method you can add the code you need to run this task correctly but you cant modify the console writeline: Console.WriteLine($"{customer.Name} from {customer.City} event {e.Name} at {e.Date}");

            3. As part of a new campaign, we need to be able to let customers know about events that are coming up close to their next birthday. You can make a guess and add it to the MarketingEngine class if you want to. So we still want to keep how things work now, which is that we email customers about events in their city or the event closest to next customer's birthday, and then we email them again at some point during the year. The current customer, his birthday is on may. So it's already in the past. So we want to find the next one, which is 23. How would you like the code to be built? We don't just want functionality; we want more than that. We want to know how you plan to make that work. Please code it.

            4. The next requirement is to extend the solution to be able to send notifications for the five closest events to the customer. The interviewer here can paste a method to help you, or ask you to search it. We will attach 2 different ways to calculate the distance. 

            // ATTENTION this row they don't tell you, you can google for it. In some cases, they pasted it so you can use it

            Option 1:
            var distance = Math.Abs(customerCityInfo.X - eventCityInfo.X) + Math.Abs(customerCityInfo.Y - eventCityInfo.Y);

            Option 2:
            private static int AlphebiticalDistance(string s, string t)
            {
                var result = 0;
                var i = 0;
                for(i = 0; i < Math.Min(s.Length, t.Length); i++)
                    {
                        // Console.Out.WriteLine($"loop 1 i={i} {s.Length} {t.Length}");
                        result += Math.Abs(s[i] - t[i]);
                    }
                    for(; i < Math.Max(s.Length, t.Length); i++)
                    {
                        // Console.Out.WriteLine($"loop 2 i={i} {s.Length} {t.Length}");
                        result += s.Length > t.Length ? s[i] : t[i];
                    }
                    
                    return result;
            } 

            Tips of this Task:
            Try to use Lamba Expressions. Data Structures. Dictionary, ContainsKey method.

            5. If the calculation of the distances is an API call which could fail or is too expensive, how will you improve the code written in 4? Think in caching the data which could be code it as a dictionary. You need to store the distances between two cities. Example:

            New York - Boston => 400 
            Boston - Washington => 540
            Boston - New York => Should not exist because "New York - Boston" is already stored and the distance is the same. 

            6. If the calculation of the distances is an API call which could fail, what can be done to avoid the failure? Think in HTTPResponse Answers: Timeoute, 404, 403. How can you handle that exceptions? Code it.

            7.  If we also want to sort the resulting events by other fields like price, etc. to determine whichones to send to the customer, how would you implement it? Code it.
            */

            var events = new List<Event>{
                new Event(1, "Phantom of the Opera", "New York", new DateTime(2023,12,23), 155),
                new Event(2, "Metallica", "Los Angeles", new DateTime(2023,12,02), 139),
                new Event(3, "Metallica", "New York", new DateTime(2023,12,06), 204),
                new Event(4, "Metallica", "Boston", new DateTime(2023,10,23), 300),
                new Event(5, "LadyGaGa", "New York", new DateTime(2023,09,20), 210),
                new Event(6, "LadyGaGa", "Boston", new DateTime(2023,08,01), 166),
                new Event(7, "LadyGaGa", "Chicago", new DateTime(2023,07,04), 200),
                new Event(8, "LadyGaGa", "San Francisco", new DateTime(2023,07,07), 172),
                new Event(9, "LadyGaGa", "Washington", new DateTime(2023,05,22), 183),
                new Event(10, "Metallica", "Chicago", new DateTime(2023,01,01), 213),
                new Event(11, "Phantom of the Opera", "San Francisco", new DateTime(2023,07,04), 208),
                new Event(12, "Phantom of the Opera", "Chicago", new DateTime(2024,05,15), 188)
            };

            var customer = new Customer()
            {
                Id = 1,
                Name = "John",
                City = "New York",
                BirthDate = new DateTime(1995, 05, 10)
            };

            var marketing = new MarketingEngine(events);
            marketing.SendCustomerNotifications(customer);
            //I've chosen (deliberately) to make nearby events public to enable external calls as well
            //marketing.SendCustomerNeabyEvents(customer, 5);
        }
            
        public class Event
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string City { get; set; }
            public DateTime Date { get; set; }
            public double Price { get; set; }

            public Event(int id, string name, string city, DateTime date, double price)
            {
                this.Id = id;
                this.Name = name;
                this.City = city;
                this.Date = date;
                this.Price = price;
            }
        }

        public class Customer
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? City { get; set; }
            public DateTime BirthDate { get; set; }
        }
        
        public static class DistanceHelper{

            private static readonly Dictionary<string,int> cache = new();

            private static int AlphebiticalDistance(string s, string t)
            {
                var result = 0;
                var i = 0;
                for(i = 0; i < Math.Min(s.Length, t.Length); i++)
                    {
                        // Console.Out.WriteLine($"loop 1 i={i} {s.Length} {t.Length}");
                        result += Math.Abs(s[i] - t[i]);
                    }
                    for(; i < Math.Max(s.Length, t.Length); i++)
                    {
                        // Console.Out.WriteLine($"loop 2 i={i} {s.Length} {t.Length}");
                        result += s.Length > t.Length ? s[i] : t[i];
                    }
                    
                    return result;
            } 

            public static int Calculate(string fromCity, string toCity){
                if(!cache.TryGetValue($"{fromCity}{toCity}", out var distance) &&
                   !cache.TryGetValue($"{toCity}{fromCity}", out distance)){
                    try
                    {
                        distance = AlphebiticalDistance(fromCity, toCity);
                        cache.Add($"{fromCity}{toCity}", distance);
                    }
                    //simulating HTTPResponse fail by using a try catch, it could have been a simple
                    //if(response.StatusCode != 200)
                    catch(Exception e){ 
                        //writing to console but instead it would go to an ILogger/Serilog or similar
                        //logger.Log(LogLevel.Error, ..., $"{fromCity} {toCity} {response.StatusCode}");
                        Console.WriteLine($"{fromCity} {toCity} {e.Message}");
                        return int.MaxValue; //return int.MaxValue to make it last in order by, could be made a parameter distanceOnError
                    }
                }
                return distance;
            }
        }

        public class MarketingEngine
        {
            private readonly List<Event> Events;

            public MarketingEngine(List<Event> events){
                Events = events;
            }

            public void SendCustomerNeabyEvents(Customer customer, int eventCount){
                var sortedEvents = (from e in Events
                                    where e.Date > DateTime.Now
                                    orderby DistanceHelper.Calculate(e.City, customer.City ?? string.Empty)
                                    select e).ToList();
                if(sortedEvents == null || sortedEvents.Count() == 0)
                    Console.WriteLine($"{customer.Name} from {customer.City} No events available");
                else{
                    if(sortedEvents.Count > eventCount)
                        sortedEvents.RemoveRange(5, sortedEvents.Count - eventCount);
                    
                    //final list sorted by ascending price, enabling data to be sorted by whatever...
                    var events = (from e in sortedEvents
                                  orderby e.Price
                                  select e).ToArray();

                    Console.WriteLine("\n== Nearby events ==");

                    for(int i = 0; i< Math.Min(events.Count(), eventCount) ;i++)
                        Console.WriteLine($"{customer.Name} from {customer.City} event {events[i].Name} at {events[i].Date} for {events[i].Price:C}");
                }
            }

            public void SendCustomerNotifications(Customer customer){

                var e = (from d in Events 
                         where d.Date > customer.BirthDate && d.Date > DateTime.Now
                         orderby d.Date
                         select d).FirstOrDefault(new Event(0, "No events available", string.Empty, DateTime.Today, 0));
                
                Console.WriteLine("\n== Next event ==");
                Console.WriteLine($"{customer.Name} from {customer.City} event {e.Name} at {e.Date}");

                SendCustomerNeabyEvents(customer, 5);
            }
        }
       
        /*-------------------------------------
        Coordinates are roughly to scale with miles in the USA
           2000 +----------------------+  
                |                      |  
                |                      |  
             Y  |                      |  
                |                      |  
                |                      |  
                |                      |  
                |                      |  
             0  +----------------------+  
                0          X          4000
        ---------------------------------------*/

    }
}

