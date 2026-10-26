using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Counter4F2.Models
{
    internal class CounterModel : INotifyPropertyChanged
    {
        private int defaultCount;
        public int DefaultCount { get => defaultCount; init => defaultCount = value; }

        private int count;
        public int Count { get => count; init => count = value; }

        private Color color;
        public Color Color { get => color; }

        private int r;
        public int R { get => r; set 
            {
                r = value;
                SetColor();
            } 
        }

        private int g;
        public int G
        {
            get => g; set
            {
                g = value;
                SetColor();
            }
        }

        private int b;
        public int B
        {
            get => b; set
            {
                b = value;
                SetColor();
            }
        }

        private string name;
        public string Name 
        { 
            get => name; 
            set 
            { 
                name = value; 
                SaveName();
            } 
        }

        private int id;
        public int Id { get => id; init => id = value; }

        private AllCountersModel parentObject;
        internal AllCountersModel ParentObject { get => parentObject; init => parentObject = value; }

        public void ChangeCount(int value)
        {
            count += value;

            SaveCount();

            //używam OnPropertyChanged aby stosować się do dobrych praktyk MVVM
            OnPropertyChanged(nameof(Count));
        }

        public void ResetCountToDefault()
        {
            count = DefaultCount;
            SaveCount();
            OnPropertyChanged(nameof(Count));
        }

        private void SaveCount()
        {
            string saveFilePath = AllCountersModel.saveFilePath;
            if (File.Exists(saveFilePath))
            {
                XDocument document = XDocument.Load(saveFilePath);

                document.Descendants("Counter").FirstOrDefault(x => (int)x.Element("Id") == Id).SetElementValue("Count", Count);

                document.Save(saveFilePath);
            }
        }

        private void SaveName()
        {
            string saveFilePath = AllCountersModel.saveFilePath;
            if (File.Exists(saveFilePath))
            {

                XDocument document = XDocument.Load(saveFilePath);

                if (document?.Descendants("Counter")?.FirstOrDefault(x => (int)x.Element("Id") == Id) != null)
                {
                    document.Descendants("Counter").FirstOrDefault(x => (int)x.Element("Id") == Id).SetElementValue("Name", Name);
                }

                
                document.Save(saveFilePath);
            }
        }

        private void SaveColor()
        {
            string saveFilePath = AllCountersModel.saveFilePath;
            if (File.Exists(saveFilePath))
            {

                XDocument document = XDocument.Load(saveFilePath);

                if (document?.Descendants("Counter")?.FirstOrDefault(x => (int)x.Element("Id") == Id) != null)
                {
                    document.Descendants("Counter").FirstOrDefault(x => (int)x.Element("Id") == Id).SetElementValue("Color", Color.ToHex());
                }


                document.Save(saveFilePath);
            }
        }

        private void SetColor()
        {
            color = Color.FromRgb(r, g, b);
            SaveColor();
            OnPropertyChanged(nameof(Color));
        }

        public string GetColor()
        {
            return Color.ToHex();
        }



        public CounterModel(int _count, string _name, int _id, string hexColor, int _defaultCount)
        {
            Id = _id;
            Count = _count;
            Name = _name;
            DefaultCount = _defaultCount;

            color = Color.FromArgb(hexColor);
            r = (int)Math.Round(color.Red * 255);
            g = (int)Math.Round(color.Green * 255);
            b = (int)Math.Round(color.Blue * 255);
        }


        //użyłem interfejsu INotifyPropertyChanged mimo że nie było go na lekcjach. Zastosowałem to rozwiązanie aby uczyć się dobrych praktyk MVVM zamiast przypisywać wartości z użyciem .Text 

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //=============================================================================

    }
}
