using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;

namespace MVVMStudy.Models
{
    partial class Inventory : INotifyPropertyChanged
    {
        private int _carId;
        [Required]
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
        [Required]
        [StringLength(50)]
        public string Make
        { 
            get => _make;
            set
            {
                if (value == _make) return;
                _make = value;
                if (Make == "ModelT")
                {
                    AddError(nameof(Make), "Too Old");
                }
                else
                {
                    ClearErrors(nameof(Make));
                }
                OnPropertyChanged(nameof(Make));
            } 
        }
        private string _color;
        [Required]
        [StringLength(50)]
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
        [StringLength(50)]
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
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        }
    }
}
 