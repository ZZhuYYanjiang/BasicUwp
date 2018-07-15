using BasicUwp.Models;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace BasicUwp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static Contact _lastSelectedContact;

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var contacts = ContactListView.ItemsSource as List<Contact>;
            if (contacts == null)
            {
                var contactservices = new ContactServices();
                contacts = (await contactservices.ListAsync()).ToList();
                ContactListView.ItemsSource = contacts;
            }
            if(e.Parameter is Contact contact)
            {
                _lastSelectedContact = contacts.FirstOrDefault(p => p.Id == contact.Id);
                FirstNameTextBox.Text = _lastSelectedContact.FirstName;
                LastNameTextBox.Text = _lastSelectedContact.LastName;
            }
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

        private void ContactListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var ClickContent = e.ClickedItem as Contact;
            _lastSelectedContact = ClickContent;
            if(AdaptiveStates.CurrentState==DefaultState)
            {
                FirstNameTextBox.Text = ClickContent.FirstName;
                LastNameTextBox.Text = ClickContent.LastName;
            }
            else
            {
                Frame.Navigate(typeof(DetailPage),ClickContent,new DrillInNavigationTransitionInfo());
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _lastSelectedContact.FirstName = FirstNameTextBox.Text;
            _lastSelectedContact.LastName = LastNameTextBox.Text;
            var contactServices = new ContactServices();
            await contactServices.UpdateAsync(_lastSelectedContact);
        }

        private void cButton_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void AdaptiveStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        //{

        //}
    }
}
