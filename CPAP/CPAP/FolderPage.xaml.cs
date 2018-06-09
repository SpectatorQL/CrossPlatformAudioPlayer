using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CPAP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FolderPage : ContentPage
	{
        static string _currentDirectory;
        SongListPage _parent;
        string _defaultDirectory = "/storage/emulated/0";
        string[] _localDirectories;       

        public FolderPage(SongListPage parent)
		{
			InitializeComponent();
            _parent = parent;
            _currentDirectory = _currentDirectory ?? _defaultDirectory;
            pathInfo.Text = _currentDirectory;
            GetDirectories();
        }

        private void GetDirectories()
        {
            _localDirectories = System.IO.Directory.GetDirectories(_currentDirectory);
            Format();
            foldersListView.ItemsSource = _localDirectories;
            pathInfo.Text = _currentDirectory;
        }

        private void Format()
        {
            for (var i = 0; i < _localDirectories.Length; ++i)
            {
                string[] path = _localDirectories[i].Split('/');
                _localDirectories[i] = path[path.Length - 1];
            }
        }

        private void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = sender as ListView;
            GoToDirectory(listView.SelectedItem.ToString());
            listView.SelectedItem = null;
        }

        private void GoToDirectory(string selectedDirectory)
        {
            string newDirectory;
            if (_currentDirectory != "/")
                newDirectory = _currentDirectory + "/" + selectedDirectory;               
            else
                newDirectory = _currentDirectory + selectedDirectory;

            _currentDirectory = newDirectory;
            GetDirectories();         
        }

        private void backButton_Clicked(object sender, EventArgs args)
        {
            if (_currentDirectory != "/")
            {
                if (_currentDirectory.IndexOf("/") == _currentDirectory.LastIndexOf("/"))
                {
                    _currentDirectory = "/";
                }
                else
                {
                    string previousDirectory = _currentDirectory.Remove(_currentDirectory.LastIndexOf("/"));
                    _currentDirectory = previousDirectory;
                }
                GetDirectories();
            }
        }

        private async void selectButton_Clicked(object sender, EventArgs args)
        {
            _parent.MusicFilesDirectory = _currentDirectory;
            _parent.GetFiles();
            await Navigation.PopAsync();
        }
	}
}