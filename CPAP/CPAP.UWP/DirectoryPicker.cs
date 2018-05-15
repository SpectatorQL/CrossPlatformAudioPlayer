using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Pickers;

[assembly: Xamarin.Forms.Dependency (typeof (CPAP.IDirectoryPicker))]
namespace CPAP.UWP
{
    class DirectoryPicker : IDirectoryPicker
    {
        public string UWPDirectory { get; private set; }

        public async void PickDirectory()
        {
            FolderPicker _folderPicker = new FolderPicker();
            Windows.Storage.StorageFolder folder = await _folderPicker.PickSingleFolderAsync();
            UWPDirectory = folder.Path;
        }
    }
}
