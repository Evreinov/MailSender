using System;
using System.Collections.Generic;
using System.Text;
using MVVMStudy.Models;
using System.Windows.Input;

namespace MVVMStudy.Commands
{
    public class ChangeColorCommand : CommandBase
    {
        public override bool CanExecute(object parameter) => (parameter as Inventory) != null;

        public override void Execute(object parameter)
        {
            ((Inventory)parameter).Color = "Pink";
        }
    }
}
