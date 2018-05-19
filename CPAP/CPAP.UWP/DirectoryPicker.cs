using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Pickers;

[assembly: Xamarin.Forms.Dependency (typeof (CPAP.UWP.DirectoryPicker))]
namespace CPAP.UWP
{
    class DirectoryPicker : IDirectoryPicker
    {
        public string PickDirectory()
        {
            FolderPicker _folderPicker = new FolderPicker();
            Windows.Storage.StorageFolder folder = _folderPicker.PickSingleFolderAsync();
            return folder.Path;
        }
    }
}
