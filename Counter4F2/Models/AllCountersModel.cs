using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Counter4F2.Models
{
    internal class AllCountersModel
    {
        public static string saveFileName = "counters.xml";

        public static string saveFilePath = Path.Combine(FileSystem.AppDataDirectory, saveFileName);

        public ObservableCollection<CounterModel> AllCountersCollection { get; set; } = new();

        public int AddCounter(string name, int count, string hexColor)
        {
            CounterModel counter = new(count, name, AllCountersCollection.Count, hexColor, count);
            AllCountersCollection.Add(counter);
            SaveAllCounters();
            return AllCountersCollection.Count - 1;
        }

        public static CounterModel LoadCounter(int Id)
        {
            if (File.Exists(saveFilePath))
            {
                XDocument document = XDocument.Load(saveFilePath);

                var _counter = document.Descendants("Counter").FirstOrDefault(x => (int)x.Element("Id") == Id);

                if (_counter == null)
                {
                    
                    throw new Exception($"Counter with ID: {Id} not found in file {saveFilePath}");
                }

                CounterModel counter = new((int)_counter.Element("Count"), (string)_counter.Element("Name"), (int)_counter.Element("Id"), (string)_counter.Element("Color"), (int)_counter.Element("DefaultCount"));

                return counter;
            }
            throw new FileNotFoundException($"No save file found in path: {saveFilePath}");
        }

        public static void DeleteCounter(int Id)
        {
            if (!File.Exists(saveFilePath))
            {
                throw new FileNotFoundException($"No save file found in path: {saveFilePath}");
            }
                

            XDocument document = XDocument.Load(saveFilePath);

            var allCounters = document.Descendants("Counter").ToList();

            var _counter = allCounters.FirstOrDefault(x => (int)x.Element("Id") == Id);

            if (_counter == null)
            {
                throw new Exception($"Counter with ID: {Id} not found in file {saveFilePath}");
            }
                

            _counter.Remove();
            allCounters.Remove(_counter);

            int i = 0;
            foreach (var counter in allCounters)
            {
                counter.SetElementValue("Id", i++);
            }

            document.Save(saveFilePath);

            
        }

        public void LoadAllCounters()
        {
            AllCountersCollection.Clear();
            if (File.Exists(saveFilePath))
            {
                XDocument document = XDocument.Load(saveFilePath);

                var counters = document.Descendants("Counter");

                foreach (var counter in counters) {
                    CounterModel newCounter = new((int)counter.Element("Count"), (string)counter.Element("Name"), (int)counter.Element("Id"), (string)counter.Element("Color"), (int)counter.Element("DefaultCount"));
                    AllCountersCollection.Add(newCounter);
                }      
            }
        }

        public void SaveAllCounters()
        {
            XDocument document = new(
                new XElement("Root",
                from item in AllCountersCollection
                    select new XElement("Counter", 
                        new XElement("Id", item.Id),
                        new XElement("Name", item.Name),
                        new XElement("Count", item.Count),
                        new XElement("Color", item.GetColor()),
                        new XElement("DefaultCount", item.DefaultCount)
                    )
                )
            );

            document.Save(saveFilePath);
        }



        public AllCountersModel()
        {
            LoadAllCounters();
        }
    }
}
