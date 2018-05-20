using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CPAP
{
    public interface IDirectoryPicker
    {
        ObservableCollection<MusicFile> MusicFiles { get; }
        Task PickDirectory();
    }
}
