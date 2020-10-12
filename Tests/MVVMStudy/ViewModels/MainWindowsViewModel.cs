using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MVVMStudy.Commands;
using MVVMStudy.Models;

namespace MVVMStudy.ViewModels
{
    public class MainWindowsViewModel
    {
        public IList<Inventory> Cars { get; } = new ObservableCollection<Inventory>();
        public MainWindowsViewModel()
        {
            Cars.Add(new Inventory { CarId = 1, Color = "Blue", Make = "Chevy", PetName = "Kit", IsChanged = false });
            Cars.Add(new Inventory { CarId = 2, Color = "Red", Make = "Ford", PetName = "Red Reider", IsChanged = false });
        }

        private ICommand _changeColorCommand = null;
        public ICommand ChangeColorCommand => _changeColorCommand ?? (_changeColorCommand = new ChangeColorCommand());

        private ICommand _addCarCommand = null;
        public ICommand AddCarCommand => _addCarCommand ?? (_addCarCommand = new AddCarCommand());

        private RelayCommand<Inventory> _deleteCarCommand = null;
        public RelayCommand<Inventory> DeleteCarCommand => _deleteCarCommand ?? (_deleteCarCommand = new RelayCommand<Inventory>(DeleteCar, CanDeleteCar));

        private bool CanDeleteCar(Inventory сar) => сar != null;

        private void DeleteCar(Inventory car)
        {
            Cars.Remove(car);
        }
    }
}
