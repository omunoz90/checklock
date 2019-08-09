using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App_Control_Parental_Demo.Models;
using static App_Control_Parental_Demo.Models.AppsModelo;
using System.Data;

namespace App_Control_Parental_Demo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainAppPage : TabbedPage
    {


        public MainAppPage ()
        {
            InitializeComponent();
            btnSalir.Clicked += BtnSalir_Clicked;
            //List<InstalledApps> lista = new List<InstalledApps>();
            //lista = GetInstalledApp();
            //lstApps.ItemsSource = lista;
            DataTable dtApps = new DataTable();
            dtApps = GetInstalledAppTable();
            lstApps.ItemsSource = dtApps.Rows;
            //PackageManager packageManager = GetPackageManager();
            //Intent mainIntent = new Intent(Intent.ActionMain, null);
            //mainIntent.AddCategory(Intent.CategoryLauncher);
        }

        private void BtnSalir_Clicked(object sender, EventArgs e)
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }


        public List<InstalledApps> GetInstalledApp()
        {
            List<InstalledApps> installedApps = new List<InstalledApps>();
            try
            {
                var apps = Android.App.Application.Context.PackageManager.GetInstalledApplications(PackageInfoFlags.MetaData);
                if (apps != null)
                {
                    if (apps.Count > 0)
                    {
                        for (int i = 0; i < apps.Count; i++)
                        {

                            var info = Android.App.Application.Context.PackageManager.GetPackageInfo(apps[i].PackageName, 0);
                            if (IsSystemPackage(info) == false)
                            {
                                InstalledApps installapps = new InstalledApps()
                                {
                                    AppName = apps[i].LoadLabel(Android.App.Application.Context.PackageManager),
                                    PackageName = apps[i].PackageName,
                                    Icon = apps[i].LoadIcon(Android.App.Application.Context.PackageManager).ToString()
                                };
                                installedApps.Add(installapps);
                            }

                        }
                        return installedApps;
                    }
                }


                return installedApps;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetInstalledAppTable()
        {
            DataRow dr = null; 
            DataTable dtApps = new DataTable();
            dtApps.TableName = "Apps";
            dtApps.Columns.Add("AppName", typeof(string));
            //dtApps.Columns.Add("PackageName", typeof(string));
            //dtApps.Columns.Add("Icon", typeof(string));

            try
            {
                var apps = Android.App.Application.Context.PackageManager.GetInstalledApplications(PackageInfoFlags.MetaData);
                if (apps != null)
                {
                    if (apps.Count > 0)
                    {
                        for (int i = 0; i < apps.Count; i++)
                        {

                            var info = Android.App.Application.Context.PackageManager.GetPackageInfo(apps[i].PackageName, 0);
                            if (IsSystemPackage(info) == false)
                            {
                                dr = dtApps.NewRow();
                                dr["AppName"] = apps[i].LoadLabel(Android.App.Application.Context.PackageManager);
                                //dr["PackageName"] = apps[i].PackageName;
                                //dr["Icon"] = apps[i].LoadIcon(Android.App.Application.Context.PackageManager).ToString();
                                dtApps.Rows.Add(dr);
                                //InstalledApps installapps = new InstalledApps()
                                //{
                                //    AppName = apps[i].LoadLabel(Android.App.Application.Context.PackageManager),
                                //    PackageName = apps[i].PackageName,
                                //    Icon = apps[i].LoadIcon(Android.App.Application.Context.PackageManager).ToString()
                                //};
                                //installedApps.Add(installapps);
                            }

                        }
                        return dtApps;
                    }
                }


                return dtApps;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsSystemPackage(PackageInfo pkgInfo)
        {
            return ((pkgInfo.ApplicationInfo.Flags & ApplicationInfoFlags.System) != 0);
        }


        private static void SearchApps()
        {
            
            
            
            //apps[0].LoadLabel(Android.App.Application.Context.PackageManager);
            //string appName = apps[0].PackageName;
            //return appName;
            //apps[0].LoadIcon(Android.App.Application.Context.PackageManager);
            //Intent intent = PackageManager.GetLaunchIntentForPackage(apps[0].PackageName);
            //StartActivity(intent);
            //string searchQuery = "chrome";
            //PackageInfoFlags flag = PackageInfoFlags.Activities;
            //var apps = PackageManager.GetInstalledApplications(flag);
            //foreach (ApplicationInfo app in apps)
            //{
            //    try
            //    {
            //        var appInfo = PackageManager.GetApplicationInfo(app.PackageName, 0);
            //        var appLabel = PackageManager.GetApplicationLabel(appInfo);
            //        if (appLabel.ToLower().Contains(searchQuery.ToLower()))
            //        {
            //            var builder = new AlertDialog.Builder(this);
            //            builder.SetTitle("Found it!");
            //            builder.SetMessage(appLabel + " installed at: " + app.SourceDir);
            //            builder.Show();
            //        }
            //    }
            //    catch (PackageManager.NameNotFoundException e) { continue; }
            //}
            //final PackageManager pm = getActivity().getPackageManager();
            //Intent intent = new Intent(Intent.ActionMain, null);
            //intent.addCategory(Intent.CategoryLauncher);
            //List<ResolveInfo> apps = pm.queryIntentActivities(intent, PackageManager.GET_META_DATA);
            //List<PackageInfo> packs = getActivity().getPackageManager().getInstalledPackages(0);

        }
    }
}