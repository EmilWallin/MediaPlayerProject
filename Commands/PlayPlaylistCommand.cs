using EmilWallin_Assignment7.ViewModels;
using EmilWallin_Assignment7.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace EmilWallin_Assignment7.Commands
{
    class PlayPlaylistCommand : ICommand
    {
        private MediaManagerParent viewModel;

        //Constructor, gets viewmodel reference
        public PlayPlaylistCommand(MediaManagerParent viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //CanExecute if more than one file is in filelist
        public bool CanExecute(object parameter)
        {
            return viewModel.FileInfo.Count > 0;
        }

        //Execute opens a mediaplayer, unless there is already a mediaplayer window open, in which case it uses the existing one
        public void Execute(object parameter)
        {
            MediaPlayer mediaPlayer = null;
            foreach(Window w in Application.Current.Windows)
            {
                if (w.GetType() == typeof(MediaPlayer))
                {
                    mediaPlayer = (MediaPlayer)w;
                    break;
                }
            }

            if (mediaPlayer == null)
            {
                mediaPlayer = new MediaPlayer();
            }

            mediaPlayer.GetViewModel().SetPlayList(viewModel.FileInfo);
            mediaPlayer.Show();
        }
    }

}
