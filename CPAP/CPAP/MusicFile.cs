using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace CPAP
{
    public class MusicFile
    {
        public string Path { get; }
        public string Name { get; set; }
        public SafeFileHandle FileHandle { get; set; }

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
        }
    }
}
