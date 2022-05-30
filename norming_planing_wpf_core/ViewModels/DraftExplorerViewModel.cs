using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Controls;
using System.ComponentModel;

namespace norming_planing_wpf_core
{
    public class DraftExplorerViewModel : ObservableObject, IDisposable
    {
        private AcszmkdbContext db;
        public Draft ExploredDraft { get; set; } = new Draft();
        public BindingList<Mark> Marks { get; set; }
        public ObservableCollection<Material> Materials { get; set; }
        public ObservableCollection<SteelGrade> SteelGrades { get; set; }
        public ObservableCollection<Customer> Customers { get; set; }
        public Customer SelectedCustomer { get; set; }
        public string Title { get; set; } = "Обзор проекта";
       
        public DraftExplorerViewModel()
        {
        }

        public DraftExplorerViewModel(Draft? exploredDraft)
        {
            db = new AcszmkdbContext();
            db.Customers.Load();
            Customers = db.Customers.Local.ToObservableCollection();

            if (exploredDraft == null)
                Title = "Создание проекта";
            else
            {
                ExploredDraft = exploredDraft;
                db.Marks.Where(m => m.DraftId == exploredDraft.Id).Include(m => m.Details).Load();
                db.Materials.Load();
                db.SteelGrades.Load();
                Marks = db.Marks.Local.ToBindingList();
                Materials = db.Materials.Local.ToObservableCollection();
                SteelGrades = db.SteelGrades.Local.ToObservableCollection();

                SelectedCustomer = Customers.Where(c => c.Id == exploredDraft.CustomerId).First();
            }
        }
        #region Commands
        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get => _saveCommand ??= new RelayCommand(p =>
                                 {
                                     db.Update<Draft>(ExploredDraft);
                                     db.SaveChanges();
                                 });
            set
            {
                _saveCommand = value;
            }
        }
        #endregion
        #region Methods
        public void Dispose()
        {
            db.Dispose();
        }
        #endregion
    }
}

