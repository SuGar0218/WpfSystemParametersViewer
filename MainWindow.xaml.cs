using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shell;

namespace SystemParametersViewer;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = ViewModel = new MainViewModel();
        if (Environment.OSVersion.WindowsVersion() >= 10)
        {
            windowChrome.NonClientFrameEdges = NonClientFrameEdges.Left | NonClientFrameEdges.Right | NonClientFrameEdges.Bottom;
        }
    }

    public MainViewModel ViewModel { get; }

    private async void OnRefreshButtonClick(object sender, RoutedEventArgs e)
    {
        await ViewModel.InitializeAsync();
    }

    private void OnSearchTextBoxKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            ViewModel.Search();
            e.Handled = true;
        }
    }

    private void OnHelpHyperlinkClick(object sender, RoutedEventArgs e)
    {
        if (((sender as FrameworkElement)?.DataContext ?? (sender as FrameworkContentElement)?.DataContext) is SystemParameterViewModel parameter && !string.IsNullOrWhiteSpace(parameter.HelpLink))
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = parameter.HelpLink
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
