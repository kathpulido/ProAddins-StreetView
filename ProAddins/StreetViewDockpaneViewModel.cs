using System;
using System.Globalization;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;

namespace ProAddins
{
    /// <summary>
    /// Dockpane that shows Google Street View inside ArcGIS Pro (docked next to the map,
    /// instead of opening a separate browser window).
    /// </summary>
    internal class StreetViewDockpaneViewModel : DockPane
    {
        private const string _dockPaneID = "ProAddins_StreetViewDockpane";

        protected StreetViewDockpaneViewModel() { }

        private Uri _currentUrl = new Uri("about:blank");
        public Uri CurrentUrl
        {
            get => _currentUrl;
            set => SetProperty(ref _currentUrl, value);
        }

        private string _statusText = "Haga clic en el mapa con la herramienta \"Google Streetview\" para ver la imagen aquí.";
        public string StatusText
        {
            get => _statusText;
            set => SetProperty(ref _statusText, value);
        }

        /// <summary>
        /// Shows the dockpane (docking it next to the map the first time) and navigates it
        /// to the Street View panorama for the given WGS84 coordinates. Safe to call from
        /// any thread; marshals to the UI thread internally.
        /// </summary>
        internal static void ShowAndNavigate(double lat, double lon)
        {
            void DoShow()
            {
                var vm = FrameworkApplication.DockPaneManager.Find(_dockPaneID) as StreetViewDockpaneViewModel;
                if (vm == null)
                    return;

                vm.StatusText = string.Format(CultureInfo.InvariantCulture, "Lat/Lon: {0:0.000000}, {1:0.000000}", lat, lon);
                vm.CurrentUrl = new Uri(string.Format(
                    CultureInfo.InvariantCulture,
                    "https://www.google.com/maps/@?api=1&map_action=pano&viewpoint={0}%2C{1}",
                    lat, lon));
                vm.Activate();
            }

            var dispatcher = System.Windows.Application.Current?.Dispatcher;
            if (dispatcher == null || dispatcher.CheckAccess())
                DoShow();
            else
                dispatcher.Invoke(DoShow);
        }
    }

    /// <summary>
    /// Ribbon button to show/activate the Street View dockpane manually (e.g. if the user
    /// closed it and wants it back without clicking the map again).
    /// </summary>
    internal class StreetViewDockpane_ShowButton : Button
    {
        protected override void OnClick()
        {
            FrameworkApplication.DockPaneManager.Find("ProAddins_StreetViewDockpane")?.Activate();
        }
    }
}
