using EmilWallin_Assignment7.Models;
using EmilWallin_Assignment7.ViewModels;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace EmilWallin_Assignment7.Commands
{
    class ClosePlaylistCommand : ICommand
    {
        private PlaylistViewModel viewModel;

        public ClosePlaylistCommand(PlaylistViewModel viewModel)
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

        //Prompts user for saving, and then saves the file (creates new file if there isn't already one). Uses SavePersistent class
        public void Execute(object parameter)
        {
            if (viewModel.Playlist.FilesList.Count <= 0)
                return;

            try
            {
                //Asks if user wants to save or not
                if(viewModel.FilePath == null)
                {
                    if (MessageBox.Show("Do you want to save the playlist \"" + viewModel.PlaylistName + "\"?", "Save Playlist?", MessageBoxButton.YesNo) == MessageBoxResult.No)
                        return;
                }
                else
                {
                    if (MessageBox.Show("Do you want to save the changes to \"" + viewModel.PlaylistName + "\"?", "Save Playlist?", MessageBoxButton.YesNo) == MessageBoxResult.No)
                        return;
                }

                //User needs to set filepath if there is none
                if (viewModel.FilePath == null)
                {
                    VistaSaveFileDialog saveDialog = new VistaSaveFileDialog();
                    saveDialog.Filter = "Playlist files (*.playlist)|*.playlist";
                    saveDialog.DefaultExt = ".playlist";

                    if (saveDialog.ShowDialog() == true)
                        viewModel.FilePath = saveDialog.FileName;
                    else
                        return;
                }

                if (!viewModel.FilePath.EndsWith(".playlist"))
                {
                    viewModel.FilePath += ".playlist";
                }

                SavingLoading.SavePersistent.SaveBinary<Playlist>(viewModel.FilePath, viewModel.Playlist);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }
    }
}
