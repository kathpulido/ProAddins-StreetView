using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Web.WebView2.Core;

namespace ProAddins
{
    /// <summary>
    /// Interaction logic for StreetViewDockpaneView.xaml
    /// </summary>
    public partial class StreetViewDockpaneView : UserControl
    {
        public StreetViewDockpaneView()
        {
            InitializeComponent();
            Loaded += StreetViewDockpaneView_Loaded;
        }

        private async void StreetViewDockpaneView_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= StreetViewDockpaneView_Loaded;

            try
            {
                // WebView2 defaults to creating its user-data folder next to the HOST
                // executable (ArcGISPro.exe, under Program Files), which a normal user
                // account can't write to, and fails silently if it can't. Use a folder
                // under the user's own LocalAppData instead, and create it up front so
                // any permission problem surfaces here instead of failing silently later.
                string userDataFolder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "ProAddins", "WebView2UserData");
                Directory.CreateDirectory(userDataFolder);

                var env = await CoreWebView2Environment.CreateAsync(userDataFolder: userDataFolder);
                await StreetViewBrowser.EnsureCoreWebView2Async(env);

                // Only surface navigation problems; a successful navigation should not
                // clobber the "Lat/Lon: ..." text that ShowAndNavigate already set.
                StreetViewBrowser.CoreWebView2.NavigationCompleted += (s, ev) =>
                {
                    if (!ev.IsSuccess)
                        SetStatus("Error al cargar la página (código " + ev.WebErrorStatus + ").");
                };

                StreetViewBrowser.CoreWebView2.ProcessFailed += (s, ev) =>
                    SetStatus("El proceso de WebView2 falló (" + ev.ProcessFailedKind + "). Cierre y vuelva a abrir el panel.");
            }
            catch (Exception ex)
            {
                SetStatus("No se pudo inicializar WebView2: " + ex.Message);
            }
        }

        private void SetStatus(string text)
        {
            Dispatcher.Invoke(() =>
            {
                if (DataContext is StreetViewDockpaneViewModel vm)
                    vm.StatusText = text;
            });
        }

        // WPF's Hyperlink does not open a browser by itself; launch the system
        // default browser explicitly via the shell.
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                SetStatus("No se pudo abrir el enlace: " + ex.Message);
            }
            e.Handled = true;
        }
    }
}
