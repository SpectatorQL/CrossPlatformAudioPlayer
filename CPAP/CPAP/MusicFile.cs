using Microsoft.Win32.SafeHandles;

namespace CPAP
{
    public class MusicFile
    {
        bool _addRefSuccess;

        public string Path { get; }
        public string Name { get; }
        public SafeFileHandle FileHandle { get; }

        public MusicFile(string path, string name)
        {
            Path = path;
            Name = name;
        }

        public MusicFile(string path, string name, SafeFileHandle handle)
        {
            Path = path;
            Name = name;
            FileHandle = handle;
            FileHandle.DangerousAddRef(ref _addRefSuccess);
        }

        ~MusicFile()
        {
            if (_addRefSuccess)
                FileHandle.DangerousRelease();
        }
    }
}
