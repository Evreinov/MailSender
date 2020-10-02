using System.ComponentModel;
using WpfTest.ViewModels.Base;

namespace WpfTest.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private string _Title = "Тестовое окно";
        public string Title
        {
            get => _Title;
            //set
            //{
            //    if (_Title == value) return;
            //    _Title = value;
            //    OnPropertyChange();
            //}
            set => Set(ref _Title, value);
        }

        private int _X;
        public int X
        {
            get => _X;
            set => Set(ref _X, value);
        }

    }
}
