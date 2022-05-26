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
            db = new AcszmkdbContext();
            db.Drafts.Load();
            Drafts = db.Drafts.Local.ToObservableCollection();
        }

        
        public Draft? SelectedDraft { get { return selectedDraft; } 
            set
            {
                selectedDraft = value;
                ExploreDraftCommand.Execute(selectedDraft);
            } 
        }

        #region Commands
        private RelayCommand exploreDraftCommand;
        public RelayCommand ExploreDraftCommand
        {
            get {
                if (exploreDraftCommand == null)
                    exploreDraftCommand = new RelayCommand(draft =>
                    {
                        DraftExplorerView draftExplorerView = new DraftExplorerView(draft as Draft);
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
