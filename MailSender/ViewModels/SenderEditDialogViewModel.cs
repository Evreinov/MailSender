using MailSender.lib.Models;
using MailSender.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MailSender.ViewModels
{
    class SenderEditDialogViewModel : DialogViewModelBase<Sender>
    {
        Sender _Sender;

        public string Name 
        {
            get => _Sender.Name;
            set
            {
                _Sender.Name = value;
            }
        }

        public SenderEditDialogViewModel(Sender sender)
        {
            _Sender = sender;
        }

       
    }
}
