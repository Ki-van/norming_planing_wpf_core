using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Controls;

namespace norming_planing_wpf_core
{
    public class DraftExplorerViewModel : ObservableObject, IDisposable
    {
        private Control _currentExplorerView;
        private object selectedItemTreeViewSs;
        private AcszmkdbContext db;
        private DetailExploreView detailExploreView;
        private MarkExploreView markExploreView;
        public Draft ExploredDraft { get; set; } = new Draft();
        public ObservableCollection<Customer> Customers { get; set; }
   
        public Customer SelectedCustomer { get; set; }
        public string Title { get; set; } = "Обзор проекта";
        public object SelectedItemTreeViewSs
        {
            get
            {
                return selectedItemTreeViewSs;
            }
            set
            {
                selectedItemTreeViewSs = value;
                OnPropertyChanged(nameof(SelectedItemTreeViewSs));
                ChangeExplorerCommand.Execute(value);
            }
        }
        public Control CurrentExplorerView
        {
            get
            {
                return _currentExplorerView;
            }
            set
            {
                if (_currentExplorerView != value)
                {
                    _currentExplorerView = value;
                    OnPropertyChanged("CurrentExplorerView");
                }
            }
        }
        #region Commands
        private ICommand? _changeExplorerCommand;
        public ICommand ChangeExplorerCommand
        {
            get
            {
                if (_changeExplorerCommand == null)
                {
                    _changeExplorerCommand = new RelayCommand(
                        p => ChangeExplorerView(p));
                }

                return _changeExplorerCommand;
            }
        }
        #endregion
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
                db.Drafts.Where(d => d.Id == exploredDraft.Id)
                    .Include(d => d.Marks)
                    .ThenInclude(m => m.Details).Load();
                ExploredDraft = db.Drafts.Local.Where(d => d.Id == exploredDraft.Id).First();
                SelectedCustomer = Customers.Where(c => c.Id == exploredDraft.CustomerId).First();
            }
        }

        #region Methods
        public void ChangeExplorerView(object p)
        {
            if (p is Detail)
            {
                var detail = (Detail)p;
                if (detailExploreView == null)
                {
                    detailExploreView = new DetailExploreView();
                    db.Materials.Load();
                    db.SteelGrades.Load();
                    detailExploreView.MaterialComboBox.ItemsSource = db.Materials.Local.ToList();
                    detailExploreView.SteelGradeComboBox.ItemsSource = db.SteelGrades.Local.ToList();
                }

                var query = db.Materials.Local.Where(m => m.Id == detail.Material?.Id);
                detailExploreView.MaterialComboBox.SelectedItem = query.Any() ? query.First(): null;

                query = db.Materials.Local.Where(m => m.Id == detail.SteelGrade?.Id);
                detailExploreView.SteelGradeComboBox.SelectedItem = query.Any() ? query.First() : null;

                detailExploreView.DataContext = detail;
                CurrentExplorerView = detailExploreView;
            }
            else if (p is Mark)
            {
                var mark = (Mark)p;
                if(markExploreView == null)
                    markExploreView = new();
                markExploreView.DataContext = mark;
                CurrentExplorerView = markExploreView;
            }
                
            
        }
        public void Dispose()
        {
            db.Dispose();
        }
        #endregion
    }
}

