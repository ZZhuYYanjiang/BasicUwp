using BasicUwp.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace BasicUwp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void RefreshContainer_RefreshRequested(RefreshContainer sender, RefreshRequestedEventArgs args)
        {
            using (var deferral = args.GetDeferral())
            {
                var contactservices = new ContactServices();
                var contacts = (await contactservices.ListAsync()).ToList();
                ContactListView.ItemsSource = contacts;
            }              
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshContainer.RequestRefresh();
        }
    }
}
