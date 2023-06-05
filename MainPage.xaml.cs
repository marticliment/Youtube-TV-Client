using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Input.Preview.Injection;
using Windows.UI.WebUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Youtube_TV
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        SystemNavigationManager currentView;
        public const uint GW_CHILD = 5;
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);
        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);


        public MainPage()
        {
            this.InitializeComponent();
            currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            currentView.BackRequested += CurrentView_BackRequested;
            loadYoutube();
            webView.Focus(FocusState.Programmatic);
        }

        private void CurrentView_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (webView.CanGoBack)
            {
                webView.GoBack();
            }
            else
            {
                webView.Source = new Uri("https://www.youtube.com/tv#/");
            }
            webView.Focus(FocusState.Programmatic);
        }

        async void loadYoutube()
        {
            await webView.EnsureCoreWebView2Async();
            webView.CoreWebView2.Settings.UserAgent = "Mozilla/5.0 (WebOS; SmartTV) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.5283.0 Safari/537.36";
            webView.Source = new Uri("https://www.youtube.com/tv#/");
            webView.CoreWebView2.NavigationCompleted += (s, e) => { focusYoutube(); };
            webView.CoreWebView2.WindowCloseRequested += (s, e) => { Application.Current.Exit(); };
            webView.CoreWebView2.ContextMenuRequested += (s, e) => { };
            webView.Focus(FocusState.Programmatic);
        }

        void focusYoutube() {
            webView.Focus(FocusState.Programmatic);
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

        }
    }

}
