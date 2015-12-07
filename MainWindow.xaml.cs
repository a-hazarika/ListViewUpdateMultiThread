using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace wpfMultiThreadListViewUpdate
{
    public class Person
    {
        public string Name { get; set; }
    }

    public partial class MainWindow : Window
    {
        private Thread _thread;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            _thread = new Thread(() => showSomePeople(new Random().Next(4, 15), lstvwPeople));
            _thread.Start();
        }

        private void showSomePeople(int numberToGet, ListView listvw)
        {
            List<Person> somePeople = getSomePeople(numberToGet);

            Dispatcher.BeginInvoke(new Action(delegate()
            {
                listvw.ItemsSource = somePeople;
            }));
        }

        private List<Person> getSomePeople(int numberToGet)
        {
            List<Person> peeps = new List<Person>();

            for (int i = 0; i < numberToGet; i++)
            {
                peeps.Add(new Person() { Name = randomName() });
                Thread.Sleep(200);
            }

            return peeps;
        }

        private string randomName()
        {
            const string chrs = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random rdm = new Random();
            return new string(Enumerable.Repeat(chrs, 6)
              .Select(s => s[rdm.Next(s.Length)]).ToArray());
        }
    }
}