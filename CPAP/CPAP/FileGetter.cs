using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace CPAP
{
    public class FileGetter
    {
        char _delimeter;

        public FileGetter()
        {
            if (Device.RuntimePlatform == Device.UWP)
                _delimeter = '\\';
            else
                _delimeter = '/';
        }

        public ObservableCollection<MusicFile> GetMusicFiles(string searchDirectory)
        {
            string[] mp3s = Directory.GetFiles(searchDirectory, "*.mp3", SearchOption.AllDirectories);
            string[] wavs = Directory.GetFiles(searchDirectory, "*.wav", SearchOption.AllDirectories);

            ObservableCollection<MusicFile> files = new ObservableCollection<MusicFile>();
            foreach (var i in mp3s)
            {
                files.Add(new MusicFile(i, GetFileNameFromPath(i)));
            }
            foreach (var i in wavs)
            {
                files.Add(new MusicFile(i, GetFileNameFromPath(i)));
            }

            return files;
        }

        private string GetFileNameFromPath(string path)
        {
            string[] splitPath = path.Split(_delimeter);
            path = splitPath[splitPath.Length - 1];
            path = path.Remove(path.LastIndexOf("."));
            return path;
        }
    }
}
