using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVMStudy.Models
{
    class Inventory : INotifyPropertyChanged
    {
        private int _carId;
        public int CarId 
        { 
            get => _carId;
            set 
            {
                if (value == _carId) return;
                _carId = value;
                OnPropertyChanged(nameof(CarId));
            } 
        }
        private string _make;
        public string Make
        { 
            get => _make;
            set
            {
                if (value == _make) return;
                _make = value;
                OnPropertyChanged(nameof(Make));
            } 
        }
        private string _color;
        public string Color
        {
            get => _color;
            set
            {
                if (value == _color) return;
                _color = value;
                OnPropertyChanged(nameof(Color));
            }
        }
        private string _petName;
        public string PetName 
        { 
            get => _petName;
            set
            {
                if (value == _petName) return;
                _petName = value;
                OnPropertyChanged(nameof(Color));
            }
        }
        private bool _isChanged;
        public bool IsChanged
        {
            get => _isChanged;
            set
            {
                if (value == _isChanged) return;
                _isChanged = value;
                OnPropertyChanged(nameof(IsChanged));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName != nameof(IsChanged))
            {
                IsChanged = true;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
