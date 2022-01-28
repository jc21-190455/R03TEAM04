using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NavPageSample.notification;
using SQLite;

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
                    jikantai = Jikantai_Entry.Text
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
            using (var connection = new SQLiteConnection("R03TEAM04.db"))
            {
                // テーブルの作成(あればスキップ)
                connection.CreateTable<User>();

                // データ数が0件の場合
                if (connection.Table<User>().Count() == 0)
                {
                    // データINSERT
                    connection.Insert(new User("taro", 42));
                    connection.Insert(new User("jiro", 23));
                    connection.Insert(new User("hanako", 31));
                }

                // データSELECT（Id）
                var userList = connection.Query<User>("SELECT * FROM User WHERE Day_breakfast = ?", 3);
                foreach (User user in userList)
                {
                    Console.WriteLine("Id:{0}, Name:{1}, Age:{2}", user.Id, user.Name, user.Age);
                }

                // データSELECT（LIKe）
                userList = connection.Query<User>("SELECT * FROM User WHERE Name LIKE '%a%'");
                foreach (User user in userList)
                {
                    Console.WriteLine("Id:{0}, Name:{1}, Age:{2}", user.Id, user.Name, user.Age);
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