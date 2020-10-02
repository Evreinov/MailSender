using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using WpfTest.ViewModels.Base;

namespace WpfTest.ViewModels
{
    class CurrentTimeViewModel : ViewModel
    {
        private readonly Timer _Timer;

        public CurrentTimeViewModel()
        {
            _Timer = new Timer(1);
            _Timer.Elapsed += _Timer_Elapsed;
            _Timer.AutoReset = true;
            _Timer.Enabled = true;
        }

        public DateTime CurrentTime => DateTime.Now;


        private void _Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            OnPropertyChange(nameof(CurrentTime));
        }
    }
}
