using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CPAP
{
    public interface IDirectoryPicker
    {
        ObservableCollection<MusicFile> MusicFiles { get; }
        Task PickDirectory();
    }
}
