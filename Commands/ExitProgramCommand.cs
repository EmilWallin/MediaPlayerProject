using EmilWallin_Assignment7.Models;
using EmilWallin_Assignment7.ViewModels;
using EmilWallin_Assignment7.Views;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace EmilWallin_Assignment7.Commands
{
    class ExitProgramCommand : ICommand
    {
        private MediaManagerParent viewModel;

        public ExitProgramCommand(MediaManagerParent viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //Can always execute. Only called from closing window
        public bool CanExecute(object parameter)
        {
            return true;
        }

        //Safely closes all playlistviews if the mediamanager is closed
        public void Execute(object parameter)
        {
            List<PlaylistView> playlistViews = new List<PlaylistView>();

            foreach(Window w in Application.Current.Windows)
            {
                if (w.GetType() == typeof(PlaylistView))
                    playlistViews.Add((PlaylistView)w);
            }

            foreach (PlaylistView plv in playlistViews)
            {
                plv.Close();
            }

            Application.Current.Shutdown();
        }

        //Checks if window is of type PlaylistView
        private bool IsWindowPlaylist(Window w)
        {
            try
            {
                PlaylistView plv = (PlaylistView)w;
                return true;
            }

            catch
            {
                return false;
            }
        }
    }
}
