using EmilWallin_Assignment7.Models;
using EmilWallin_Assignment7.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmilWallin_Assignment7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MediaManager : Window
    {
        public MediaManager()
        {
            InitializeComponent();
            DataContext= new MediaManagerViewModel();
        }

        //Get viewmodel reference method. Casts DataContext to MediaManagerViewModel
        public MediaManagerViewModel GetViewModel()
        {
            return (MediaManagerViewModel)DataContext;
        }

        /// <summary>
        ///     Events raised in the window. Calls the ViewModel's methods/events
        /// </summary>

        //Selection changed => adds selection to observablecollection in viewmodel
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MediaManagerViewModel viewModel = (MediaManagerViewModel)DataContext;

            viewModel.SelectedFiles = new ObservableCollection<FileInfoHolder>(filesListView.SelectedItems.Cast<FileInfoHolder>().ToList());
        }

        //Drag start and stop used to drag files to playlists
        private void MouseDragStart(object sender, MouseButtonEventArgs e)
        {
            GetViewModel().DraggingFiles = true;
        }
        private void MouseDragStop(object sender, MouseButtonEventArgs e)
        {
            GetViewModel().DraggingFiles = false;
        }

        //Executes the ExitProgramCommand which safely closes playlists and shuts down Application
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GetViewModel().ExitProgramCommand.Execute(null);
        }
    }
}
