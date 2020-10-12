using MailSender.lib.Models;
using MailSender.ViewModels.Base;
using MailSender.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using System.Windows;
using MailSender.Interfaces;

namespace MailSender.ViewModels
{
    class SenderEditDialogViewModel : DialogViewModelBase<Sender>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        public SenderEditDialogViewModel(Sender sender)
        {
            if (sender is null)
                return;
            Name = sender.Name;
            Address = sender.Address;
            Description = sender.Description;
        }


    }
}
