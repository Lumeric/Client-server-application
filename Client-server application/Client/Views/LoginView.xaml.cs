using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public event System.ComponentModel.CancelEventHandler Validating;

        private readonly string regexIP = @"/^(?:(?:2[0-4]\d|25[0-5]|1\d{2}|[1-9]?\d)\.){3}(?:2[0-4]\d|25[0-5]|1\d{2}|[1-9]?\d)(?:\:(?:\d|[1-9]\d{1,3}|[1-5]\d{4}|6[0-4]\d{3}|65[0-4]\d{2}|655[0-2]\d|6553[0-5]))?$/";
        private readonly string regexUsername = @"\w";
        private string _username;

        public LoginView()
        {
            InitializeComponent();
        }

       /* #region usernameValidation
        private void TextBoxValidating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string errorMsg;
            

            if (!ValidUsernameAddress(Username, out errorMsg))
            {
                e.Cancel = true;
                MessageBox.Show(errorMsg);
            }
        }

        private void TextBoxValidated(object sender, System.EventArgs e)
        {
            //
        }

        public bool ValidUsername(string username, out string errorMessage)
        {
            if (username.Length == 0)
            {
                errorMessage = "Username is required.";
                return false;
            }

            if (Regex.IsMatch(username, regexUsername))
            {
                    errorMessage = "";
                    return true;
            }

            errorMessage = "Username must be valid username  format.\n" +
               "For example 'Hasagi' ";
            return false;
        }
        #endregion*/

        public void ValidIP()
        {

        }

    }
}
