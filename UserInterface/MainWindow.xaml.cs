using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Threading;
using System.Windows.Media.Animation;

using kdt.ThreeLevelPasswordAuthenticator.Authentication;
using kdt.ThreeLevelPasswordAuthenticator.UserManagement;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User userInfo = new User();
        private User user = new User();

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            captchaNextButton.Click += CaptchaNextButton_Click;
            usernameNextButton.Click += UsernameNextButton_Click;
            passwordNextButton.Click += PasswordNextButton_Click;
            passphraseNextButton.Click += PassphraseNextButton_Click;
        }

        #endregion Constructor

        private void PassphraseNextButton_Click(object sender, RoutedEventArgs e)
        {
            user.Passphrase = passphrasePasswordBox.Password;
            var vt = Authenticator.AuthenticatePassphrase(user);

            if (vt.Result == Token.Matched)
            {
                this.Close();
            }
            else
            {
                string message = "Ooops, you entered a wrong passphrase.";
                MessageBox.Show(message, "Passphrases doesn't match", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                captchaTextBox.Focus();
            }
        }

        private void PasswordNextButton_Click(object sender, RoutedEventArgs e)
        {
            user.Password = passwordPasswordBox.Password;

            Storyboard sb = this.FindResource("passwordVerified") as Storyboard;
            var vt = Authenticator.AuthenticatePassword(user);

            if (vt.Result == Token.Matched)
            {
                sb.Begin();
            }
   
            else
            {
                string message = "Ooops, you entered a wrong password.";
                MessageBox.Show(message, "Passwords doesn't match", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                captchaTextBox.Focus();
            }
        }


        private void UsernameNextButton_Click(object sender, RoutedEventArgs e)
        {
            user.Username = usernameTextBox.Text;

            Storyboard sb = this.FindResource("usernameVerified") as Storyboard;
            var vt = Authenticator.UserExistence(user);
            string filePath = string.Format(@"Users\{0}.usr", user.Username);

            if (vt.Result == Token.Existing)
            {
                User userInfo = UserManager.ReadFromFile(filePath);
                nameTextBlock.Text = userInfo.Name;
                sb.Begin();
            }
            else
            {
                string message = string.Format("Ooops, the username \"{0}\" doesn't exist.", user.Username);
                MessageBox.Show(message, "Captcha Not Matched", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                captchaTextBox.Focus();
            }
        }

        private void CaptchaNextButton_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.FindResource("captchaVerified") as Storyboard;
            ValidationToken vt = Authenticator.Captcha(captchaTextBox.Text);

            if (vt.Result == Token.Matched)
            {
                sb.Begin();
            }
            else
            {
                MessageBox.Show("Oopps, you entered the wrong captcha.", "Captcha Not Matched", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                captchaTextBox.Focus();
            }
        }
    }
}