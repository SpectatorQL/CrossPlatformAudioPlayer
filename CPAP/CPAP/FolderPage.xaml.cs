using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CPAP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FolderPage : ContentPage
	{
        static string _currentDirectory;
        string _defaultDirectory = "/storage/emulated/0";
        SongListPage _parent;

        public FolderPage(SongListPage parent)
		{
			InitializeComponent();
            _parent = parent;
            _currentDirectory = _currentDirectory ?? _defaultDirectory;
            GetDirectories();
        }

        private void GetDirectories()
        {
            string[] localDirectories = System.IO.Directory.GetDirectories(_currentDirectory);
            FormatPaths(localDirectories);
            foldersListView.ItemsSource = localDirectories;
            pathInfo.Text = _currentDirectory;
        }

        private void FormatPaths(string[] directories)
        {
            for (var i = 0; i < directories.Length; ++i)
            {
                string[] path = directories[i].Split('/');
                directories[i] = path[path.Length - 1];
            }
        }

        private void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var tappedDirectory = foldersListView.SelectedItem;
            GoToDirectory(tappedDirectory.ToString());
            foldersListView.SelectedItem = null;
        }

        private void GoToDirectory(string selectedDirectory)
        {
            string newDirectory;
            if (_currentDirectory != "/")
            {
                newDirectory = _currentDirectory + "/" + selectedDirectory;
            }     
            else
            {
                newDirectory = _currentDirectory + selectedDirectory;
            }
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
            _parent.GetFiles(_currentDirectory);
            await Navigation.PopAsync();
        }
	}
}