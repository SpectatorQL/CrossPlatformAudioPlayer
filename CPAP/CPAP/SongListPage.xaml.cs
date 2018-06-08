using System;
using System.Collections.ObjectModel;
using System.Linq;
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

        public void GetFiles()
        {
            _musicFiles = _fileGetter.GetMusicFiles(MusicFilesDirectory);
            SetSongs();
        }
        
        private async void SetSongs()
        {
            MyListView.ItemsSource = _musicFiles;
            if (_musicFiles.Count == 0 || _musicFiles == null)
            {
                _parent.CurrentSong = null;
                await DisplayAlert("No files!", "It looks like you chose a wrong directory.", "OK");
            }
            else
            {
                _parent.CurrentSong = _musicFiles.ElementAt(0);
            }
        }

        public void ChangeSong(Song s)
        {
            try
            {
                int i = _musicFiles.IndexOf(_parent.CurrentSong);
                MusicFile f = _musicFiles.ElementAt((int)(i + s));
                _parent.CurrentSong = f;
            }
            catch (ArgumentOutOfRangeException)
            {
                // No action is necessary. Playing is stopped without any side effects
            }
            finally
            {
                _parent.UpdateData();
            }
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
            _parent.Reset();
            if (Device.RuntimePlatform == Device.Android)
            {
                await Navigation.PushAsync(new FolderPage(this));
            }
            else if (Device.RuntimePlatform == Device.UWP)
            {
                IDirectoryPicker picker = DependencyService.Get<IDirectoryPicker>();
                _musicFiles = await picker.GetFilesWithHandles();
                SetSongs();
            }
        }
    }
}
