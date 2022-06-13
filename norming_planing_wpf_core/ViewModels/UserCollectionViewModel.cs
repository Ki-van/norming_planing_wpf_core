using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using System.Windows.Data;

namespace norming_planing_wpf_core
{
    public class UserCollectionViewModel : ObservableObject, IPageViewModel, IDisposable
    {
        private AcszmkdbContext db;
        private UserCollectionView view;
        public ObservableCollection<UserCollection> UserCollections { get; set; }  
        public UserCollectionViewModel()
        {
        }

        #region Commands
        private RelayCommand loadModelsCommand;
        public RelayCommand LoadModelsCommand
        {
            get
            {
                return loadModelsCommand ??= new RelayCommand(async (view) =>
                {
                    this.view = view as UserCollectionView;
                    db = new AcszmkdbContext();
                    await db.UserCollections.LoadAsync();
                    UserCollections = db.UserCollections.Local.ToObservableCollection();
                    this.view.UserCollectionDataGrid.ItemsSource = UserCollections;
                    this.view.UserCollectionDataGrid.Items.Refresh();
                });
            }
        }
        #endregion

        public string Name
        {
            get
            {
                return "Коллекции";
            }
        }

       
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
