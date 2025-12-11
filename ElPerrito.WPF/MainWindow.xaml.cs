using System;
using System.Windows;
using ElPerrito.Core.Logging;

namespace ElPerrito.WPF;

/// <summary>
/// Ventana principal de la aplicación
/// </summary>
public partial class MainWindow : Window
{
    private readonly Logger _logger = Logger.Instance;

    public MainWindow()
    {
        InitializeComponent();
        _logger.LogInfo("MainWindow cargada");
    }

    protected override void OnClosed(EventArgs e)
    {
        _logger.LogInfo("MainWindow cerrada");
        base.OnClosed(e);
    }
}
