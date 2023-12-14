using ApplicationUP.Models;
using ApplicationUP.Repositories;
using ApplicationUP.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ApplicationUP.ViewModels
{
    public class LoginViewModel : ViewModelsBase
    {
        private string _userName;
        private string _email;
        private string _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        public UserRepository dbLog = new UserRepository();
        private ObservableCollection<UserModel> users;

        private IUserRepository userRepository;

        public ObservableCollection<UserModel> Users
        {
            get => users;
            set
            {
                users = value;
                OnPropertyChanged(nameof(Users));
            }
        }
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;
            }
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }
        public ViewModelCommand LoginCommand { get; private set; }
        public ViewModelCommand SignUpCommand { get; private set; }
        public ViewModelCommand RecoverPasswordCommand { get; private set; }
        public ViewModelCommand ShowPasswordCommand { get; private set; }
        public ViewModelCommand RememberPasswordCommand { get; private set; }

        public LoginViewModel()
        {
            Users = new ObservableCollection<UserModel>(dbLog.GetAllUsers());
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            SignUpCommand = new ViewModelCommand(SignUp, CanSignUp);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverCommand("", ""));
        }
        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(UserName) || UserName.Length < 3 || Password == null || Password.Length < 3)
            {
                validData = false;
            }
            else
                validData = true;
            return validData;
        }

        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = userRepository.AuthenticateUser(new System.Net.NetworkCredential(UserName, Password));
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(UserName), null);
                MainWindow mainWindow = new MainWindow();
                Application.Current.MainWindow.Close();
                Application.Current.MainWindow = mainWindow;
                mainWindow.Show();
            }
            else
            {
                ErrorMessage = "* Invalid Password or Login";
            }
        }

        private bool CanSignUp(object parameter)
        {
            return true;//!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Email);
        }
        private void SignUp(object parameter)
        {
            userRepository.CreateUser(UserName, Password, Email);
            string message = "Пользователь зарегистрирован. Войдите в аккаунт.";
            MessageBox.Show(message);
        }


        private void ExecuteRecoverCommand(string username, string email)
        {
            throw new NotImplementedException();
        }

    }
}
