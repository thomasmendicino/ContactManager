using ContactManager.ViewModels;
using System;

namespace ContactManager.Stores
{
    public class NavigationStore
    {
		private ViewModelBase _currentViewModel;

		public ViewModelBase CurrentViewModel
		{
			get { return _currentViewModel; }
			set { 
				_currentViewModel = value;
				OnCurrentViewModelChanged();
			}
		}

		private void OnCurrentViewModelChanged()
		{
			CurrentViewModelChanged?.Invoke();
		}

		public event Action CurrentViewModelChanged;

	}
}
