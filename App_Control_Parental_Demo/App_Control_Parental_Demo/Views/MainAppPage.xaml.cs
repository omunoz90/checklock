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
    public partial class MainAppPage : TabbedPage
    {
        public MainAppPage ()
        {
            InitializeComponent();
            //PackageManager packageManager = GetPackageManager();
            //Intent mainIntent = new Intent(Intent.ActionMain, null);
            //mainIntent.AddCategory(Intent.CategoryLauncher);
        }
    }
}