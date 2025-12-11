using ElPerrito.WPF.Commands;
using ElPerrito.Core.Configuration;
using System.Windows.Input;
using System.Windows;

namespace ElPerrito.WPF.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly ConfigurationManager _config = ConfigurationManager.Instance;

        private string _applicationName = string.Empty;
        private string _connectionString = string.Empty;
        private int _recordsPerPage = 20;
        private bool _debugMode;
        private int _defaultMinStock = 5;
        private bool _lowStockAlerts = true;
        private bool _emailNotifications;
        private string _exportPath = string.Empty;
        private int _defaultReportFormat;

        public SettingsViewModel()
        {
            // Comandos
            SaveSettingsCommand = new RelayCommand(_ => SaveSettings());
            ResetSettingsCommand = new RelayCommand(_ => ResetSettings());
            BrowseExportPathCommand = new RelayCommand(_ => BrowseExportPath());

            // Cargar configuración actual
            LoadSettings();
        }

        public string ApplicationName
        {
            get => _applicationName;
            set => SetProperty(ref _applicationName, value);
        }

        public string ConnectionString
        {
            get => _connectionString;
            set => SetProperty(ref _connectionString, value);
        }

        public int RecordsPerPage
        {
            get => _recordsPerPage;
            set => SetProperty(ref _recordsPerPage, value);
        }

        public bool DebugMode
        {
            get => _debugMode;
            set => SetProperty(ref _debugMode, value);
        }

        public int DefaultMinStock
        {
            get => _defaultMinStock;
            set => SetProperty(ref _defaultMinStock, value);
        }

        public bool LowStockAlerts
        {
            get => _lowStockAlerts;
            set => SetProperty(ref _lowStockAlerts, value);
        }

        public bool EmailNotifications
        {
            get => _emailNotifications;
            set => SetProperty(ref _emailNotifications, value);
        }

        public string ExportPath
        {
            get => _exportPath;
            set => SetProperty(ref _exportPath, value);
        }

        public int DefaultReportFormat
        {
            get => _defaultReportFormat;
            set => SetProperty(ref _defaultReportFormat, value);
        }

        public ICommand SaveSettingsCommand { get; }
        public ICommand ResetSettingsCommand { get; }
        public ICommand BrowseExportPathCommand { get; }

        private void LoadSettings()
        {
            ApplicationName = _config.GetSetting("ApplicationName", "El Perrito");
            ConnectionString = _config.GetSetting("ConnectionString", "Server=localhost;Database=elperrito;Uid=root;Pwd=;");
            RecordsPerPage = int.Parse(_config.GetSetting("RecordsPerPage", "20"));
            DebugMode = bool.Parse(_config.GetSetting("DebugMode", "false"));
            DefaultMinStock = int.Parse(_config.GetSetting("DefaultMinStock", "5"));
            LowStockAlerts = bool.Parse(_config.GetSetting("LowStockAlerts", "true"));
            EmailNotifications = bool.Parse(_config.GetSetting("EmailNotifications", "false"));
            ExportPath = _config.GetSetting("ExportPath", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            DefaultReportFormat = int.Parse(_config.GetSetting("DefaultReportFormat", "0"));
        }

        private void SaveSettings()
        {
            _config.SetSetting("ApplicationName", ApplicationName);
            _config.SetSetting("ConnectionString", ConnectionString);
            _config.SetSetting("RecordsPerPage", RecordsPerPage.ToString());
            _config.SetSetting("DebugMode", DebugMode.ToString());
            _config.SetSetting("DefaultMinStock", DefaultMinStock.ToString());
            _config.SetSetting("LowStockAlerts", LowStockAlerts.ToString());
            _config.SetSetting("EmailNotifications", EmailNotifications.ToString());
            _config.SetSetting("ExportPath", ExportPath);
            _config.SetSetting("DefaultReportFormat", DefaultReportFormat.ToString());

            MessageBox.Show("Configuración guardada correctamente",
                          "Configuración",
                          MessageBoxButton.OK,
                          MessageBoxImage.Information);
        }

        private void ResetSettings()
        {
            var result = MessageBox.Show("¿Está seguro de restablecer los valores por defecto?",
                                       "Restablecer Configuración",
                                       MessageBoxButton.YesNo,
                                       MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                ApplicationName = "El Perrito";
                ConnectionString = "Server=localhost;Database=elperrito;Uid=root;Pwd=;";
                RecordsPerPage = 20;
                DebugMode = false;
                DefaultMinStock = 5;
                LowStockAlerts = true;
                EmailNotifications = false;
                ExportPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                DefaultReportFormat = 0;
            }
        }

        private void BrowseExportPath()
        {
            // TODO: Abrir diálogo para seleccionar carpeta
            MessageBox.Show("Función de examinar carpeta pendiente de implementación",
                          "Examinar",
                          MessageBoxButton.OK,
                          MessageBoxImage.Information);
        }
    }
}
