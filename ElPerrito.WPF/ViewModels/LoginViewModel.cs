using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ElPerrito.WPF.Commands;
using ElPerrito.WPF.Services;

namespace ElPerrito.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly AuthService _authService;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _hasError;
        private bool _isLoading;

        public LoginViewModel()
        {
            _authService = new AuthService();
            LoginCommand = new RelayCommand(async (param) => await Login(param), CanLogin);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public bool HasError
        {
            get => _hasError;
            set => SetProperty(ref _hasError, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public ICommand LoginCommand { get; }

        private bool CanLogin(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(Email) &&
                   !string.IsNullOrWhiteSpace(Password) &&
                   !IsLoading;
        }

        private async System.Threading.Tasks.Task Login(object? parameter)
        {
            IsLoading = true;
            HasError = false;
            ErrorMessage = string.Empty;

            try
            {
                var (success, message, userType) = await _authService.LoginAsync(Email, Password);

                if (success)
                {
                    // Cerrar la ventana de login
                    if (parameter is PasswordBox passwordBox)
                    {
                        var loginWindow = Window.GetWindow(passwordBox);
                        if (loginWindow != null)
                        {
                            // Si es un diálogo modal, establecer DialogResult
                            if (loginWindow.Owner != null)
                            {
                                loginWindow.DialogResult = true;
                            }
                            else
                            {
                                // Si no es modal, abrir MainWindow y cerrar esta ventana
                                var mainWindow = new MainWindow();
                                mainWindow.Show();
                            }
                            loginWindow.Close();
                        }
                    }
                }
                else
                {
                    HasError = true;
                    ErrorMessage = message;
                }
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMessage = $"Error: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
