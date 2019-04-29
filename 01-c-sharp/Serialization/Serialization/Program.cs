using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Serialization
{
    class Program
    {
        async static Task Main(string[] args)
        {
            var persons = new List<Person>
            {
                new Person
                {
                    Id = 1,
                    Name = "Nick",
                    Address = new Address
                    {
                        Street = "123 Main St",
                        City = "Dallas",
                        State = "TX"
                    }
                },
                new Person
                {
                    Id = 2,
                    Name = "Fred",
                    Address = new Address
                    {
                        Street = "321 Main St",
                        City = "Fort Worth",
                        State = "TX"
                    }
                }
            };

            var fileName = @"C:\revature\persons.xml";

            persons = DeserializeXMLFromFile(fileName);

            persons[1].Id++;

            // we've seen $ strings
            // @ strings are to disable escape sequences like \n
            SerializeXMLToFile(fileName, persons);

            Task task = SerializeJSONToFileAsync(@"C:\revature\persons.json", persons);
            // if i don't await that returned task...
            // then any code below here will probably run BEFORE
            // the file has been written

            // once i need the result completed, i should await that task
            await task;

            //either use C# 7.1 or up (modifying csproj file)
            // for async Main... or, you can call task.Wait() or task.Result
            // to wait synchronously in Main.
        }

        private async static Task SerializeJSONToFileAsync(string fileName, List<Person> persons)
        {
            string json = JsonConvert.SerializeObject(persons);

            try
            {
                // step 1: when we do disk/network access,
                // look for method that ends in "Async" instead of the regular one.
                // step 2: await the returned task.
                // step 3: this method needs "async" modifier
                // step 4: the return type is wrapped in a Task<...>
                //     or changes from void to Task.
                // step 5: by convention, your async method
                //     should itself be named ".....Async".
                await File.WriteAllTextAsync(fileName, json);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void SerializeXMLToFile(string fileName, List<Person> persons)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<Person>));

            FileStream fileStream = null;

            // how we would have to use IDisposable classes without using blocks.
            try
            {
                fileStream = new FileStream(fileName, FileMode.Create);

                // XMLSerializer will give runtime errors
                // it's can be very picky
                xmlSerializer.Serialize(fileStream, persons);
            }
            catch (IOException ex)
            {
                // the file is locked by another program,
                // we don't have access rights,
                // ran out of disk space, etc
                Console.WriteLine(ex.Message);
            }
            finally
            {
                fileStream?.Dispose();
            }
        }

        private static List<Person> DeserializeXMLFromFile(string fileName)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<Person>));

            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                // how we would have to use IDisposable classes without using blocks.
                try
                {
                    // XMLSerializer will give runtime errors
                    // it's can be very picky
                    // it does not know generics,
                    // so must use explicit cast.
                    return (List<Person>)xmlSerializer.Deserialize(fileStream);
                }
                catch (IOException ex)
                {
                    // the file is locked by another program,
                    // we don't have access rights,
                    // ran out of disk space, etc
                    Console.WriteLine(ex.Message);
                }
            }
            return null;
        }
    }
}
