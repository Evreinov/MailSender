using System;
using System.Collections.Generic;
using System.Text;
using MailSender.ViewModels.Base;

namespace MailSender.ViewModels
{
    class StatisticViewModel : ViewModelBase
    {
        #region Счетчик отправленных сообщений

        private int _SendMessagesCout;

        public int SendMessagesCount { get => _SendMessagesCout; private set => Set(ref _SendMessagesCout, value); }

        public void MessageSended() => SendMessagesCount++;

        #endregion
    }
}
