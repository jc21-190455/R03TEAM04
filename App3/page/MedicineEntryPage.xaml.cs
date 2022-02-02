using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NavPageSample.notification;
using System.Data.SQLite;

namespace NavPageSample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MedicineEntryPage : ContentPage
    {
        public MedicineEntryPage()
        {
            InitializeComponent();

            // 2021/12/28 吉澤追加分 ここから

            notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
                DisplayAlert("Alert", "TestPage.NotificationReceived", "ok");
            };
            DisplayAlert("Alert", "TestPage.InitializeComponent", "ok");
            // 2021/12/28 吉澤追加分 ここまで
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
                    Jikantai = Jikantai_Entry.Text
                });
            }
            
        }

        // 2021/12/28 吉澤追加分 ここから

        INotificationManager notificationManager;

        int notificationNumber = 0;


        //ボタン押すと即時通知されるやつ
        private void OnNotifyButtonClicked(object sender, EventArgs e)
        {
            notificationNumber++;
            string title = $"Local Notification #{notificationNumber}";
            string message = $"You have now received {notificationNumber} notifications!";
            notificationManager.SendNotification(title, message);
            var msg = new Label()
            {
                Text = $"Notification send:\nTitle: {title}\nMessage: {message}"
            };
            stackLayout.Children.Add(msg);

        }

        //ボタン押すと10秒後に通知がくるやつ
        private void OnScheduleButtonClicked(object sender, EventArgs e)
        {

            //SQLで検索、飲む時間帯が朝か昼か＞薬のタイミングが食前か食後か＞アンケート結果から時間を設定する

            SQLiteConnection con = new SQLiteConnection("Data Source=R03TEAM04.db;Version=3;");
            
            //接続を開く
            con.Open();
            try
            {
                //朝か昼か両方なのか

                // データSELECT（Id）
                string sqlstr = "select Jikantai from User";

                SQLiteCommand com = new SQLiteCommand(sqlstr,con);
                SQLiteDataReader sdr = com.ExecuteReader();

                /*SQLiteParameter param = com.CreateParameter();
                param.ParameterName = "@A";
                param.Direction = System.Data.ParameterDirection.Input;
                param.Value = "朝";
                com.Parameters.Add(param);*/
            }
            catch(SQLiteException ex)
            {

            }
            finally {
                con.Close();
            }

            notificationNumber++;
            string title = $"Local Notification #{notificationNumber}";
            string message = $"You have now received {notificationNumber} notifications!";
            notificationManager.SendNotification(title, message, DateTime.Parse()/*Now.AddSeconds(10))*/;
            var msg = new Label()
            {
                Text = $"Schedule Notification send:\nTitle: {title}\nMessage: {message}"
            };
            stackLayout.Children.Add(msg);
        }

        void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var msg = new Label()
                {
                    Text = $"Notification Received:\nTitle: {title}\nMessage: {message}"
                };
                stackLayout.Children.Add(msg);
            });
        }

        // 2021/12/28 吉澤追加分 ここまで

    }
}