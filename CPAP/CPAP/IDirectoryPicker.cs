using System;
using System.Collections.Generic;
using System.Text;

namespace CPAP
{
    public interface IDirectoryPicker
    {
        string UWPDirectory { get; }
        void PickDirectory();
    }
}
