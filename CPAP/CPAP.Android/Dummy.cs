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

[assembly: Xamarin.Forms.Dependency (typeof (CPAP.IDirectoryPicker))]
namespace CPAP.Droid
{
    class Dummy : IDirectoryPicker
    {
        // to coś istnieje tylko po to, żeby DependencyService nie umarł na UWP
        public string UWPDirectory => throw new NotImplementedException();

        public void PickDirectory()
        {
            throw new NotImplementedException();
        }
    }
}