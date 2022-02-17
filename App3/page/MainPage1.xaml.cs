using NavPageSample.page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NavPageSample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage1 : ContentPage
    {
        public MainPage1()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage2());

        }

        public void NameSet(string name)
        {
            label1.Text = name;
            label2.Text = name;


        }
        private void Button5_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DetailPage());

        }



    }
}