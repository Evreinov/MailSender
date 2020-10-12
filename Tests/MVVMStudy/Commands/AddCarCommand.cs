using MVVMStudy.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace MVVMStudy.Commands
{
    public class AddCarCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return parameter != null && parameter is ObservableCollection<Inventory>;
        }

        public override void Execute(object parameter)
        {
            if (parameter is ObservableCollection<Inventory> cars)
            {
                var maxCount = cars?.Max(x => x.CarId) ?? 0;
                cars?.Add(new Inventory { CarId = ++maxCount, Color = "Yeloow", Make = "VM", PetName = "Birdie", IsChanged = false });
            }
        }
    }
}
