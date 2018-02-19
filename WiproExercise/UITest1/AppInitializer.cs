using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest1
{
    public class AppInitializer
    {
        public static string DEVICE_ID = "936798df2122281f";// MI A1

        public static IApp StartApp(Platform platform)
        {
            // TODO: If the iOS or Android app being tested is included in the solution 
            // then open the Unit Tests window, right click Test Apps, select Add App Project
            // and select the app projects that should be tested.
            //
            // The iOS project should have the Xamarin.TestCloud.Agent NuGet package
            // installed. To start the Test Cloud Agent the following code should be
            // added to the FinishedLaunching method of the AppDelegate:
            //
            //    #if DEBUG
            //    Xamarin.Calabash.Start();
            //    #endif
            //
            // For more information please read:
            // http://developer.xamarin.com/guides/testcloud/uitest/adding-uitest/
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android
                    .EnableLocalScreenshots().InstalledApp("WiproExercise.Droid")
                    .DeviceSerial(DEVICE_ID)
                    .StartApp();
            }
            else
            {
                return ConfigureApp
                .iOS
                .StartApp();
            }
        }
    }
}

