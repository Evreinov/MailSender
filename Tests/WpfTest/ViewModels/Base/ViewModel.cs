using System.ComponentModel;

namespace WpfTest.ViewModels.Base
{
    abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);
        protected virtual void OnPropertyChange(string PropertyName )
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
