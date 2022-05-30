using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using norming_planing_wpf_core;

namespace norming_planing_wpf_core
{
    public class ApplicationViewModel : ObservableObject
    {
        #region Fields

        private ICommand? _changePageCommand;
        private ICommand? _newDraftCommand;

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        #endregion

        public ApplicationViewModel()
        {
            // Add available pages
            PageViewModels.Add(new HomeViewModel());
            PageViewModels.Add(new TPViewModel());
            PageViewModels.Add(new PlanningViewModel());
            PageViewModels.Add(new CatalogsViewModel());

            // Set starting page
            CurrentPageViewModel = PageViewModels[0];
        }

        #region Properties / Commands
        public ICommand NewDraftCommand
        {
            get
            {
                if (_newDraftCommand == null)
                {
                    _newDraftCommand = new RelayCommand( p =>
                    {
                        DraftExplorerView newDraftView = new DraftExplorerView(null);
                        newDraftView.ShowDialog();
                    });
                }

                return _newDraftCommand;
            }
        }

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel);
                }

                return _changePageCommand;
            }
        }
        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

        #endregion

        #region Methods
        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels.FirstOrDefault(vm => vm == viewModel);
        }

        #endregion
    }
}

