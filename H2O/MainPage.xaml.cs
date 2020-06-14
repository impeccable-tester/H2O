using System;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using System.Xml.Linq;
using Windows.UI.Notifications;
using Windows.Storage.Streams;
using System.Threading.Tasks;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace H2O
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            //InitializeComponent();
            _ = DoWork();
           
        }

        private static async Task DoWork()
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    DoMoreWork();
                    await Task.Delay(new TimeSpan(1,0,0));
                    
                }
            });
        }

        private static void DoMoreWork()
        {
            ShowToast();
        }

        private static async void ShowToast()
        {
            XmlDocument doc = new XmlDocument();
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Toast.xml", UriKind.Absolute));
            IRandomAccessStream readStream = await file.OpenAsync(FileAccessMode.Read);
            XDocument xmldoc = XDocument.Load(readStream.AsStreamForRead());
            var toastTemplate = xmldoc.ToString();
            doc.LoadXml(toastTemplate);
            var toast = new ToastNotification(doc);
            _ = ToastNotificationManager.CreateToastNotifier();
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
