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
    public partial class MedicineEntryPage : ContentPage
    {
        public MedicineEntryPage()
        {
            InitializeComponent();
        }

        private async void OnAddButtonClicked(object sender, EventArgs e)
        {


            if (!String.IsNullOrWhiteSpace(Medicine_Name_Entry.Text))
            {
                await App.Database.SaveMedicineAsync(new Medicine
                {
                    Medicine_name = Medicine_Name_Entry.Text,
                    Url = Url_Entry.Text,
                    timing = Timing_Entry.Text,
                });

                await App.Database.SaveUserAsync(new User
                {
                    jikantai = Jikantai_Entry.Text
                });
            }

        }


    }
}