using System;
using System.Collections.Generic;
using System.Text;

namespace CPAP
{
    public class MusicFile
    {
        public string Path { get; }
        public string Name { get; set; }

        public MusicFile(string path, string name)
        {
            Path = path;
            Name = name;
        }
    }
}
