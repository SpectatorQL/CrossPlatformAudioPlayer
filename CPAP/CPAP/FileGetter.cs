using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace CPAP
{
    public class FileGetter
    {
        public ObservableCollection<MusicFile> GetMusicFiles(string searchDirectory)
        {
            string[] mp3s = Directory.GetFiles(searchDirectory, "*.mp3", SearchOption.AllDirectories);
            string[] wavs = Directory.GetFiles(searchDirectory, "*.wav", SearchOption.AllDirectories);

            ObservableCollection<MusicFile> files = new ObservableCollection<MusicFile>();
            foreach (var i in mp3s)
            {
                files.Add(new MusicFile(i, Path.GetFileNameWithoutExtension(i)));
            }
            foreach (var i in wavs)
            {
                files.Add(new MusicFile(i, Path.GetFileNameWithoutExtension(i)));
            }

            return files;
        }
    }
}
