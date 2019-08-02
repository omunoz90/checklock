using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App_Control_Parental_Demo.Models;

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
            UsuariosModelo oProceso = new UsuariosModelo();
            UsuariosModelo.AltaUsuarioResponse oResponse = new UsuariosModelo.AltaUsuarioResponse();
            oResponse = oProceso.AltaUsuarioCheckLock(entry_Correo.Text, entry_RegPass.Text, entry_Nombre.Text
                                            , entry_SegundoNombre.Text, entry_ApellidoPaterno.Text, entry_ApellidoMaterno.Text
                                            , Convert.ToInt32(entry_Edad.Text), "");

            if (oResponse.CodigoRespuesta == "00")
            {
                DisplayAlert("Registro", "Registro realizado con éxito.", "Ok");
                ((NavigationPage)this.Parent).PushAsync(new LoginPage());
            }
            else
            {
                DisplayAlert("Error", "Ocurrio un error al realizar el registro: " + oResponse.Mensaje, "Ok");
                ((NavigationPage)this.Parent).PushAsync(new LoginPage());
            }

          
        }
    }
}