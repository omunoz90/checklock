using App_Control_Parental_Demo.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static App_Control_Parental_Demo.Models.LoginModelo;

namespace App_Control_Parental_Demo
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
            btnRegistro.Clicked += BtnRegistro_Clicked;
            btnLogin.Clicked += BtnLogin_Clicked;

        }
               
        private void BtnLogin_Clicked(object sender, EventArgs e)
        {

            LoginResponse oResponse = new LoginResponse();
            oResponse = LoginCheckLock(entry_Usuario.Text, entry_Pass.Text);

            if (oResponse.CodigoRespuesta == "00")
            {
                ((NavigationPage)this.Parent).PushAsync(new MainAppPage());
            }
            else
            {
                DisplayAlert("Login", oResponse.Mensaje, "Ok");
            }          
        }

        private void BtnRegistro_Clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new Registro());
        }
    }
}