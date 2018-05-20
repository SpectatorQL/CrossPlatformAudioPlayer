using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;

[assembly: Xamarin.Forms.Dependency (typeof (CPAP.UWP.DirectoryPicker))]
namespace CPAP.UWP
{
    class DirectoryPicker : IDirectoryPicker
    {
        private StorageFolder _folder;

        public ObservableCollection<MusicFile> MusicFiles { get; private set; }

        public DirectoryPicker()
        {
            MusicFiles = new ObservableCollection<MusicFile>();
        }

        public async Task PickDirectory()
        {
            FolderPicker _folderPicker = new FolderPicker()
            {
                SuggestedStartLocation = PickerLocationId.Desktop
            };
            _folderPicker.FileTypeFilter.Add("*");
            _folder = await _folderPicker.PickSingleFolderAsync();
            StorageApplicationPermissions.FutureAccessList.Add(_folder);
            await GetFilesWithHandles();
        }

        private async Task GetFilesWithHandles()
        {
            IReadOnlyList<StorageFile> files = await _folder.GetFilesAsync();
            foreach (var file in files)
            {
                MusicFiles.Add(new MusicFile(file.Path, file.Name, file.CreateSafeFileHandle(FileAccess.Read)));
            }
        }
    }
}
