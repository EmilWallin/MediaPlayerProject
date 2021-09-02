using EmilWallin_Assignment7.Events;
using EmilWallin_Assignment7.Models;
using EmilWallin_Assignment7.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EmilWallin_Assignment7.Views
{
    /// <summary>
    /// Interaction logic for PlaylistView.xaml
    /// </summary>
    public partial class PlaylistView : Window
    {
        public PlaylistView()
        {
            InitializeComponent();

            DataContext = new PlaylistViewModel();
        }
        //Creates view based on an already existing viewmodel (to open an existing playlist)
        public PlaylistView(PlaylistViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }

        //Gets reference of viewmodel
        public PlaylistViewModel GetViewModel()
        {
            return (PlaylistViewModel)DataContext;
        }

        /// <summary>
        ///     Events raised in the window. Calls the ViewModel's methods/events
        /// </summary>

        //Selection changed => adds selection to observablecollection in viewmodel
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlaylistViewModel viewModel = (PlaylistViewModel)DataContext;

            viewModel.SelectedFiles = new ObservableCollection<FileInfoHolder>(filesListView.SelectedItems.Cast<FileInfoHolder>().ToList());
        }

        //Hover and Drop files events
        private void MouseEnterForm(object sender, MouseEventArgs e)
        {
            GetViewModel().TriggerHover(true);
        }

        private void MouseLeaveForm(object sender, MouseEventArgs e)
        {
            GetViewModel().TriggerHover(false);
        }
        private void MouseLeftUp(object sender, MouseButtonEventArgs e)
        {
            GetViewModel().TriggerDropFiles();
        }
        //Window closing => Calls ClosePlaylistCommand which prompts for saving if the playlist is not empty
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GetViewModel().ClosePlaylistCommand.Execute(null);
        }
    }
}
