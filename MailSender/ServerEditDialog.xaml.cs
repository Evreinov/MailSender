using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MailSender
{ 
    public partial class ServerEditDialog
    {
        private ServerEditDialog() => InitializeComponent();

        private void OnPortTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(sender is TextBox text_box) || text_box.Text == "") return;
            e.Handled = !int.TryParse(text_box.Text, out _);
        }
        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = !((Button)e.OriginalSource).IsCancel;
            Close();
        }
        public static bool ShowDialog(
            string Title, 
            ref string Name, 
            ref string Address, 
            ref int Port, 
            ref bool UseSSL, 
            ref string Description, 
            ref string Login,
            ref string Password)
        {
            ServerEditDialog dialog = new ServerEditDialog
            {
                Title = Title,
                ServerName = { Text = Name },
                ServerAddress = { Text = Address },
                ServerPort = { Text = Port.ToString() },
                ServerSSL = { IsChecked = UseSSL },
                Login = { Text = Login },
                Password = { Password = Password },
                ServerDescription = {Text = Description },
                Owner = Application.Current.Windows.Cast<Window>().FirstOrDefault(win => win.IsActive)
            };

            if (dialog.ShowDialog() != true) return false; 

            Name = dialog.ServerName.Text; 
            Address = dialog.ServerAddress.Text; 
            Port = int.Parse(dialog.ServerPort.Text); 
            Login = dialog.Login.Text; 
            Password = dialog.Password.Password; 
            return true;
        }

        public static bool Create(
            out string Name, 
            out string Address, 
            out int Port, 
            out bool UseSSL, 
            out string Description, 
            out string Login,
            out string Password)
        {
            Name = null; 
            Address = null; 
            Port = 25; 
            UseSSL = false; 
            Description = null; 
            Login = null; 
            Password = null;

            return ShowDialog(
                "Создать сервер",
                ref Name,
                ref Address,
                ref Port,
                ref UseSSL,
                ref Description,
                ref Login,
                ref Password);
        }
    }
}
