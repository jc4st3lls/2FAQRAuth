using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace _2FAQRAuthApp
{
    public partial class App : Application
    {
        public static string PORTAL_SCHEMA { get; private set; }
        public static string PORTAL_HOST { get; private set; }
        public static string PORTAL_PORT { get; private set; }


        public static readonly string EXAMPLE_USR = "jc4st3lls";
        public static readonly string EXAMPLE_PWD= "p4ssw0rd";

        public App()
        {
            InitializeComponent();          
           
            PORTAL_SCHEMA = "http";
            PORTAL_HOST = "192.168.1.46";
            PORTAL_PORT = "5000";
                       
            //PORTAL_SCHEMA = "https";
            //PORTAL_HOST = "192.168.1.46";
            //PORTAL_PORT = "5001";

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
