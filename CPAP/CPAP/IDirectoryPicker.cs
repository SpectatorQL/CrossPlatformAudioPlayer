using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CPAP
{
    public interface IDirectoryPicker
    {
        Task<ObservableCollection<MusicFile>> GetFilesWithHandles();
    }
}
