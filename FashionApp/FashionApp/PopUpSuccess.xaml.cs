using FashionApp.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FashionApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpSuccess 
    {
        public PopUpSuccess()
        {
            InitializeComponent();
             ShowMessege();
            MessagingCenter.Subscribe<ShopPage>(this, "Hi", (sender) => {

                clospage();
            });
        }
        async void ShowMessege()
        {
            AnimationView.IsVisible = true;
            AnimationView1.IsVisible = false;
           await Task.Delay(200);
            AnimationView.IsVisible = false;
            AnimationView1.IsVisible = true;
        }
        async void clospage()
        {
            
            await PopupNavigation.Instance.PopAsync();
        }
    }
}