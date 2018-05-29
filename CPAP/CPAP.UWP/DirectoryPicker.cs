using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;

[assembly: Xamarin.Forms.Dependency(typeof(CPAP.UWP.DirectoryPicker))]
namespace CPAP.UWP
{
    class DirectoryPicker : IDirectoryPicker
    {
        StorageFolder _folder;

        public async Task<ObservableCollection<MusicFile>> GetFilesWithHandles()
        {
            await PickDirectory();
            IReadOnlyList<StorageFile> files = await _folder.GetFilesAsync();
            ObservableCollection<MusicFile> musicFiles = new ObservableCollection<MusicFile>();
            foreach (var file in files)
            {
                if (file.FileType == ".mp3" || file.FileType == ".wav")
                {
                    musicFiles.Add(new MusicFile(
                        file.Path,
                        Path.GetFileNameWithoutExtension(file.Path),
                        file.CreateSafeFileHandle(FileAccess.Read)
                        ));
                }
            }
            return new ObservableCollection<MusicFile>(musicFiles.OrderBy(file => file.Name));
        }

        private async Task PickDirectory()
        {
            FolderPicker _folderPicker = new FolderPicker()
            {
                SuggestedStartLocation = PickerLocationId.Desktop
            };
            _folderPicker.FileTypeFilter.Add("*");
            _folder = await _folderPicker.PickSingleFolderAsync();
            StorageApplicationPermissions.FutureAccessList.Add(_folder);
        }
    }
}
