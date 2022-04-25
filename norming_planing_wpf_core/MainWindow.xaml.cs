using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using norming_planing_wpf_core.Models;

namespace norming_planing_wpf_core
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using (AcszmkdbContext db = new AcszmkdbContext())
            {
                // создаем два объекта User
                Draft user1 = new Draft { Name = "Tom" };
                Draft user2 = new Draft { Name = "Alice" };

                // добавляем их в бд
                db.Drafts.AddRange(user1, user2);
                db.SaveChanges();
            }
            // получение данных
            using (AcszmkdbContext db = new AcszmkdbContext())
            {
                // получаем объекты из бд и выводим на консоль
                var users = db.Drafts.ToList();
                Console.WriteLine("Drafts list:");
                foreach (Draft u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name}");
                }
            }
        }
    }
}
