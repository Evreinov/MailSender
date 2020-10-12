using MVVMStudy.Models;
using MVVMStudy.Commands;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;


namespace MVVMStudy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICommand _changeColorCommand = null;
        public ICommand ChangeColorCommand => _changeColorCommand ?? (_changeColorCommand = new ChangeColorCommand());

        private ICommand _addCarCommand = null;
        public ICommand AddCarCommand => _addCarCommand ?? (_addCarCommand = new AddCarCommand());

        private RelayCommand<Inventory> _deleteCarCommand = null;
        public RelayCommand<Inventory> DeleteCarCommand => _deleteCarCommand ?? (_deleteCarCommand = new RelayCommand<Inventory>(DeleteCar, CanDeleteCar));

        private bool CanDeleteCar(Inventory сar) => сar != null;

        private void DeleteCar(Inventory car)
        {
            _cars.Remove(car);
        }

        readonly IList<Inventory> _cars = new ObservableCollection<Inventory>();
        public MainWindow()
        {
            InitializeComponent();
            _cars.Add(new Inventory { CarId = 1, Color = "Blue", Make = "Chevy", PetName = "Kit", IsChanged=false });
            _cars.Add(new Inventory { CarId = 2, Color = "Red", Make = "Ford", PetName = "Red Reider", IsChanged=false });
            cboCars.ItemsSource = _cars;
        }

        private void BtnAddCar_OnClick(object sender, RoutedEventArgs e)
        {
            var maxCount = _cars?.DefaultIfEmpty().Max(x => x.CarId) ?? 0;
            _cars?.Add(new Inventory { CarId = ++maxCount, Color = "Yeloow", Make = "VM", PetName = "Birdie", IsChanged = false });
        }
    }
}
