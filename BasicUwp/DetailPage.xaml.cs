using BasicUwp.Models;
using BasicUwp.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace BasicUwp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class DetailPage : Page
    {
        public DetailPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var DetailContact=e.Parameter as Contact;
            var backStack = Frame.BackStack;
            var backstackcount = backStack.Count;
            if(backstackcount>0)
            {
                //栈顶
                var masterPageEntry = backStack[backstackcount - 1];
                backStack.RemoveAt(backstackcount - 1);
                //修改原本栈顶信息，再回填
                var modifiedEntry = new PageStackEntry(masterPageEntry.SourcePageType,DetailContact,masterPageEntry.NavigationTransitionInfo);
                backStack.Add(modifiedEntry);  //不回填无法Frame.GoBack
            }
            FirstNameTextBox.Text = DetailContact.FirstName;
            LastNameTextBox.Text = DetailContact.LastName;
        }

        private async void DetailSaveButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage._lastSelectedContact.FirstName = FirstNameTextBox.Text;
            MainPage._lastSelectedContact.LastName = LastNameTextBox.Text;
            var DetailContactServices = new ContactServices();
            await DetailContactServices.UpdateAsync(MainPage._lastSelectedContact);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            Frame.GoBack(new DrillInNavigationTransitionInfo());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            if(ShouldGoToWideState())
            {
                Window.Current.SizeChanged -= Current_SizeChanged;
                NavigateBackForWideState(false);
            }
        }

        private void NavigateBackForWideState(bool usetransition)
        {
            //TODO try to delete this
            NavigationCacheMode = NavigationCacheMode.Disabled;
            if(usetransition)
            {
                Frame.GoBack(new EntranceNavigationTransitionInfo());
            }
            else
            {
                Frame.GoBack(new SuppressNavigationTransitionInfo());
            }
        }

        private bool ShouldGoToWideState()
        {
            return Window.Current.Bounds.Width >= 720;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= Current_SizeChanged;  //-=避免内存泄漏
        }
    }
}
