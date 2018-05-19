using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CPAP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SongListPage : ContentPage
    {
        MainPage _parent;
        ObservableCollection<MusicFile> _musicFiles;
        FileGetter _fileGetter;

        public string MusicFilesDirectory { get; set; }    

        public SongListPage(MainPage parent)
        {
            InitializeComponent();
            _parent = parent;
            _fileGetter = new FileGetter();
        }

        public async void GetFiles()
        {
            _musicFiles = _fileGetter.GetMusicFiles(MusicFilesDirectory);
            if (_musicFiles == null || _musicFiles.Count == 0)
            {
                _parent.Reset();
                _parent.CurrentSong = null;
                await DisplayAlert("No files!", "It looks like you chose a wrong directory.", "OK");
            }
            MyListView.ItemsSource = _musicFiles;
        }

        public void PreviousTrack()
        {
            try
            {
                int i = _musicFiles.IndexOf(_parent.CurrentSong);
                MusicFile previousTrack = _musicFiles.ElementAt(i - 1);
                _parent.CurrentSong = previousTrack;
            }
            // Nie chce mi sie implementować świrowania z indeksami i sprawdzaniem,
            // a to coś działa just fine.
            catch (ArgumentOutOfRangeException) { }
            catch (NullReferenceException) { }
            finally
            {
                _parent.UpdateData();
            }
        }

        public void NextTrack()
        {
            try
            {
                int i = _musicFiles.IndexOf(_parent.CurrentSong);
                MusicFile nextTrack = _musicFiles.ElementAt(i + 1);
                _parent.CurrentSong = nextTrack;
            }
            // Nie chce mi sie implementować świrowania z indeksami i sprawdzaniem,
            // a to coś działa just fine.
            catch (ArgumentOutOfRangeException) { }
            catch (NullReferenceException) { }
            finally
            {
                _parent.UpdateData();
            }
        }

        private string Format(string path)
        {
            string[] splitPath = path.Split('/');
            return splitPath[splitPath.Length - 1];
        }

        private async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            MusicFile song = (MusicFile)MyListView.SelectedItem;
            _parent.CurrentSong = song;
            _parent.UpdateData();
            MyListView.SelectedItem = null;
            await Navigation.PopAsync();
        }

        private async void directoryButton_Clicked(object sender, EventArgs args)
        {           
            if (Device.RuntimePlatform == Device.Android)
            {
                await Navigation.PushAsync(new FolderPage(this));
            }
            else if (Device.RuntimePlatform == Device.UWP)
            {
                System.Diagnostics.Debug.WriteLine("HALO DZIEŃ DOBRY, MY PO FOLDERY");
                IDirectoryPicker picker = DependencyService.Get<IDirectoryPicker>();
                MusicFilesDirectory = picker.PickDirectory();
                GetFiles();
            }
        }
    }
}
