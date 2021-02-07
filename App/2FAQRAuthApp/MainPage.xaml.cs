using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2FAQRAuthApp.Services;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace _2FAQRAuthApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void LoginButton_Clicked(System.Object sender, System.EventArgs e)
        {

            var Username = UsernameEntry.Text;
            var Password = PasswordEntry.Text;

            if (Username.Equals(App.EXAMPLE_USR) && Password.Equals(App.EXAMPLE_PWD))
            {
                PasswordEntry.Text = string.Empty;
                var scan = new ZXingScannerPage();

                await Navigation.PushAsync(scan);

                scan.OnScanResult += (result) =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Navigation.PopAsync();

                        var code= result.Text;
                        messageLabel.Text = result.Text;


                        if (!string.IsNullOrEmpty(code))
                        {
                           if (await QRAuthToken.AuthenticateCode(Username, code))
                           {
                                UsernameEntry.Text = string.Empty;

                            }
                            else
                            {
                                messageLabel.Text = "Error";
                            }
                        }
                     });
                };


            }
            else
            {
                messageLabel.Text = "Credentials incorrect!!";
            }
        }
    }
}
