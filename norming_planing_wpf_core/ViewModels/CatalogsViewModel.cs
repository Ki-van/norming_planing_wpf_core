using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace norming_planing_wpf_core
{
    public class CatalogsViewModel : ObservableObject, IPageViewModel
    {
        private ICommand? _changeCatalogCommand;
        private IPageViewModel _currentCatalog;
        public string Name => "Каталоги";
        public List<IPageViewModel> Catalogs { get; set; } = new List<IPageViewModel>();
        public CatalogsViewModel()
        {
            Catalogs.Add(new MaterialsViewModel());
            Catalogs.Add(new NormingMapsViewModel());

            CurrentCatalog = Catalogs[0];
        }
        public IPageViewModel CurrentCatalog
        {
            get
            {
                return _currentCatalog;
            }
            set
            {
                if (_currentCatalog != value)
                {
                    _currentCatalog = value;
                    OnPropertyChanged("CurrentCatalog");
                }
            }
        }
        public ICommand ChangeCatalogCommand
        {
            get
            {
                if (_changeCatalogCommand == null)
                {
                    _changeCatalogCommand = new RelayCommand(
                        p =>
                        {
                            var catalog = p as IPageViewModel;

                            if (!Catalogs.Contains(catalog))
                                Catalogs.Add(catalog);

                            CurrentCatalog = Catalogs.FirstOrDefault(c => c == catalog);
                        },
                        p => p is IPageViewModel);
                }

                return _changeCatalogCommand;
            }
        }

    }
}
