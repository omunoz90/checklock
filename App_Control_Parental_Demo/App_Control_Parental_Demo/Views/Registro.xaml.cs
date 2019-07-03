using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_Control_Parental_Demo.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Registro : ContentPage
	{
		public Registro ()
		{
			InitializeComponent ();
            btnRegistrar.Clicked += BtnRegistrar_Clicked;

        }

        private void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Registro", "Registro realizado con éxito.", "Ok");
            ((NavigationPage)this.Parent).PushAsync(new LoginPage());
        }
    }
}