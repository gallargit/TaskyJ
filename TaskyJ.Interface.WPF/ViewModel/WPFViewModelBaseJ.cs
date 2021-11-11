using System.ComponentModel;

namespace TaskyJ.Interface.WPF.ViewModel
{
	public abstract class WPFViewModelBaseJ : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
