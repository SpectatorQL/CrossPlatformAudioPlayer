using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

[assembly: Xamarin.Forms.Dependency (typeof (CPAP.Droid.Dummy))]
namespace CPAP.Droid
{
    // To coś istnieje tylko po to, żeby DependencyService nie umarł
    class Dummy : IDirectoryPicker
    {
        public ObservableCollection<MusicFile> MusicFiles => throw new NotImplementedException();

        public async Task PickDirectory()
        {
            throw new NotImplementedException();
        }
    }
}