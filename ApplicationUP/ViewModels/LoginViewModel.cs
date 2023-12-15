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
using System.Windows.Navigation;

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
        private UserModel CurrentUser;
        private Visibility _stackPanelVisibility = Visibility.Hidden;

        private IUserRepository userRepository;
        private IUserRepository userEditor;

        private string currentAccessLevel;
        public Visibility StackPanelVisibility
        {
            get { return _stackPanelVisibility; }
            set
            {
                _stackPanelVisibility = value;
                OnPropertyChanged(nameof(StackPanelVisibility));
            }
        }
        public string CurrentAccessLevel
        {
            get { return currentAccessLevel; }
            set
            {
                currentAccessLevel = value;
                VisibilityEdit();
                OnPropertyChanged("CurrentAccessLevel");
            }
        }

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

        
        
        public ViewModelCommand AddCommand { get; private set; }
        public ViewModelCommand EditCommand { get; private set; }
        public ViewModelCommand LoginCommand { get; private set; }
        public ViewModelCommand SignUpCommand { get; private set; }
        public ViewModelCommand RecoverPasswordCommand { get; private set; }
        public ViewModelCommand ShowPasswordCommand { get; private set; }
        public ViewModelCommand RememberPasswordCommand { get; private set; }

        public LoginViewModel()
        {
            VisibilityEdit();
            EditCommand = new ViewModelCommand(Edit);
            userEditor = new UserRepository();
            Users = new ObservableCollection<UserModel>(dbLog.GetAllUsers());
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            SignUpCommand = new ViewModelCommand(SignUp, CanSignUp);
            AddCommand = new ViewModelCommand(AddUp, CanAdppUp);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverCommand("", ""));
        }
        private void Edit(object parameter)
        {
            userEditor.EditUser(UserName, Password, Email, CurrentAccessLevel);
            string message = "Пользователь был отредактирован.";
            MessageBox.Show(message);
        }

        private void VisibilityEdit()
        {
            if (CurrentAccessLevel == "admin")
            {
                StackPanelVisibility = Visibility.Visible;
            }
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
                CurrentUser = dbLog.GetByUsername(UserName);
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

        private bool CanAdppUp(object parameter)
        {
            return true;
        }
        private void AddUp(object parameter)
        {
            userRepository.CreateUser(UserName, Password, Email);
            string message = "Admin добавил данные пользователя.";
            MessageBox.Show(message);
        }


        private void ExecuteRecoverCommand(string username, string email)
        {
            throw new NotImplementedException();
        }

    }
}
