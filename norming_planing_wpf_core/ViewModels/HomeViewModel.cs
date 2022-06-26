using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace norming_planing_wpf_core
{
    public class HomeViewModel : ObservableObject, IPageViewModel, IDisposable
    {
        private AcszmkdbContext db;
        private Draft? selectedDraft;
        public ObservableCollection<Draft> Drafts { get; set; }
       
        public HomeViewModel()
        {
            //Init();
            db = new AcszmkdbContext();
            db.Drafts.Include(d => d.Marks).ThenInclude(m => m.TechProcesses).Load();
            Drafts = db.Drafts.Local.ToObservableCollection();
            selectedDraft = Drafts.FirstOrDefault();

        }
        async public void Init()
        {
            db = new AcszmkdbContext();
            await db.Drafts.Include(d => d.Marks).ThenInclude(m => m.Details).LoadAsync();
            Drafts = db.Drafts.Local.ToObservableCollection();
            SelectedDraft = Drafts.FirstOrDefault();
        } 
        
        public Draft? SelectedDraft { get { return selectedDraft; } set { selectedDraft = value; RaisePropertyChanged("SelectedDraft"); } }

        #region Commands
        private RelayCommand exploreDraftCommand;
        public RelayCommand ExploreDraftCommand
        {
            get {
                if (exploreDraftCommand == null)
                    exploreDraftCommand = new RelayCommand(p =>
                    {
                        DraftExplorerView draftExplorerView = new DraftExplorerView(SelectedDraft);
                        draftExplorerView.ShowDialog();
                    });
                return exploreDraftCommand; 
            }
        }
        #endregion

        public string Name
        {
            get
            {
                return "Главная страница";
            }
        }

       
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
