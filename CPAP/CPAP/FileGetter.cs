using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace CPAP
{
    public class FileGetter
    {
        public ObservableCollection<MusicFile> GetMusicFiles(string searchDirectory)
        {
            string[] mp3s = Directory.GetFiles(searchDirectory, "*.mp3", SearchOption.TopDirectoryOnly);
            string[] wavs = Directory.GetFiles(searchDirectory, "*.wav", SearchOption.TopDirectoryOnly);

            List<MusicFile> files = new List<MusicFile>();
            foreach (var file in mp3s)
            {
                files.Add(new MusicFile(file, Path.GetFileNameWithoutExtension(file)));
            }
            foreach (var file in wavs)
            {
                files.Add(new MusicFile(file, Path.GetFileNameWithoutExtension(file)));
            }

            return new ObservableCollection<MusicFile>(files.OrderBy(file => file.Name));
        }
    }
}
