using System;
using System.IO;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest1
{
    [TestFixture(Platform.Android)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        /// <summary>
        /// Test whether the view loaded properly by querying the title of the data.
        /// </summary>
        [Test]
        public void Test_ViewLoadedOrNot()
        {
            Thread.Sleep(1000);
            AppResult[] result = null;
            try
            {
                result = app.Query(c => c.Class("Label").Text("About Canada"));
            }
            catch
            {
                Assert.Fail("View not loaded.");
                return;
            }
            if (result != null && result.Any())
                Assert.Pass("View loaded successfully");
        }

        /// <summary>
        /// Test whether the items are sorted properly when tapping sort button 2 times.
        /// </summary>
        [Test]
        public void Test_SortDescending()
        {
            Thread.Sleep(1000);
            app.Tap(c => c.Button("Sort"));
            Thread.Sleep(300);
            app.Tap(c => c.Button("Sort"));
            AppResult[] result = null;
            try
            {
                result = app.Query(c => c.Class("Label").Text("Meese"));
            }
            catch
            {
                Assert.Fail("Items are not sorted in descending order properly.");
                return;
            }
            if (result != null && result.Any())
                Assert.Pass("Items sorted in descending");
        }

        /// <summary>
        /// Test whether the items are sorted properly when tapping sort button 3 times.
        /// </summary>
        [Test]
        public void Test_SortAscending()
        {
            Thread.Sleep(1000);
            app.Tap(c => c.Button("Sort"));
            Thread.Sleep(300);
            app.Tap(c => c.Button("Sort"));
            Thread.Sleep(300);
            app.Tap(c => c.Button("Sort"));

            AppResult[] result = null;
            try
            {
                result = app.Query(c => c.Class("Label").Text("Geography"));
            }
            catch
            {
                Assert.Fail("Items are not sorted in ascending order properly.");
                return;
            }
            if (result != null && result.Any())
                Assert.Pass("Items are now sorted in ascending");
        }
    }
}

