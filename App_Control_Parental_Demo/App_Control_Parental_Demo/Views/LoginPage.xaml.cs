using App_Control_Parental_Demo.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App_Control_Parental_Demo.Models;

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
               
        public void BtnLogin_Clicked(object sender, EventArgs e)
        {
            LoginModelo oProceso = new LoginModelo();        
            LoginModelo.LoginResponse oResponse = new LoginModelo.LoginResponse();           
            oResponse = oProceso.LoginCheckLock(entry_Usuario.Text, entry_Pass.Text);

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