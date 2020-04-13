using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace KobApp.Droid.RestService
{
    public class RestService
    {
        private static RestService instance;
        public RestService() {}

        public static RestService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RestService();
                }
                return instance;
            }
        }
    }
}