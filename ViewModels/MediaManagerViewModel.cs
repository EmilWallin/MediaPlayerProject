using EmilWallin_Assignment7.Commands;
using EmilWallin_Assignment7.Models;
using EmilWallin_Assignment7;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using EmilWallin_Assignment7.Events;

namespace EmilWallin_Assignment7.ViewModels
{
    public class MediaManagerViewModel : MediaManagerParent
    {
        private ObservableCollection<PlaylistViewModel> openPlaylists;
        private bool isDraggingFiles = false;

        //Constructor. Initializes openPlaylists list obscollection and commands necessary for MediaManager
        public MediaManagerViewModel() : base()
        {
            openPlaylists = new ObservableCollection<PlaylistViewModel>();

            OpenFilesCommand = new OpenFilesCommand(this);
            OpenFolderCommand = new OpenFolderCommand(this);
            NewPlaylistCommand = new NewPlaylistCommand(this);
            OpenPlaylistCommand = new OpenPlaylistCommand(this);
            ExitProgramCommand = new ExitProgramCommand(this);

            OpenHelpWindowCommand = new OpenHelpWindowCommand();
        }

        //Add files to lists/collections, Override of the base implementation to check files' MediaTypes and prompt user if they want to add it
        public override void AddToFileList(FileInfo[] infoArray)
        {
            foreach (FileInfo fi in infoArray)
            {
                if (base.CheckIfFileInCollection(fi.Name))
                    return;

                if (MediaChecks.CheckMediaType(fi.Extension) != MediaType.Unknown ||
                    MessageBox.Show("Unknown File Type Found.\nDo you still want to add it to the list?\nThese files cannot be added to playlists.", "Unknown File Type", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    FileInfo.Add(new FileInfoHolder(fi));
                }
            }

            OnPropertyChanged("FileInfo");
            OnPropertyChanged("NoOfFiles");
            OnPropertyChanged("NoOfVideoFiles");
            OnPropertyChanged("NoOfAudioFiles");
        }

        //Open playlist based on already created viewmodel
        public void OpenPlaylistView(PlaylistViewModel viewModel)
        {
            openPlaylists.Add(viewModel);
            openPlaylists[openPlaylists.Count - 1].Hover += HoverPlaylist;

            foreach(FileInfoHolder fih in viewModel.FileInfo)
            {
                bool alreadyInMediaManager = false;
                foreach(FileInfoHolder fih2 in FileInfo)
                {
                    if (fih2.FilePath == fih.FilePath)
                    {
                        alreadyInMediaManager = true;
                        break;
                    }
                }
                if (!alreadyInMediaManager)
                {
                    FileInfo.Add(fih);
                    OnPropertyChanged("FileInfo");
                }
            }

            //If it is a new playlist (aka no files in FileInfo), add the files marked in the media manager when opening it
            if (viewModel.FileInfo.Count == 0)
            {
                foreach (FileInfoHolder fih in SelectedFiles)
                {
                    viewModel.AddToFileList(fih);
                }
            }



            OnPropertyChanged("NoOfFiles");
            OnPropertyChanged("NoOfVideoFiles");
            OnPropertyChanged("NoOfAudioFiles");
        }

        //Hoverplaylist subscriber. 
        private void HoverPlaylist(object sender, HoverFormEvent e)
        {
            if (!e.IsHovering)
            {
                ((PlaylistViewModel)sender).DropFiles -= DropFiles;

            }
            else
            {
                ((PlaylistViewModel)sender).DropFiles += DropFiles;

            }
        }

        //Called upon DropFiles event triggered from playlistviewmodel
        private void DropFiles(object sender, DropEvent e)
        {
            if (isDraggingFiles)
            {
                string unknownFileTypeNames = "";

                foreach (FileInfoHolder fih in SelectedFiles)
                {
                    if (fih.MediaType == MediaType.Unknown)
                        unknownFileTypeNames += fih.FileName + "\n";
                    else
                        ((PlaylistViewModel)sender).AddToFileList(fih);
                }
                isDraggingFiles = false;
                ClearSelectionCommand.Execute(null);

                if (unknownFileTypeNames != "")             //Guard clause if there are no files that could not be added (due to their filetype being neither audio/video)
                {
                    MessageBox.Show("These files of unkown filetype were not added:\n" + unknownFileTypeNames, "Files Not Added");
                }
            }
        }

        //Sets dragfiles bool (done from view)
        public bool DraggingFiles
        {
            set 
            { 
                isDraggingFiles = value;
                if (isDraggingFiles)
                    Mouse.OverrideCursor = Cursors.Hand;
                else
                    Mouse.OverrideCursor = Cursors.Arrow;
            }
        }

        //PROPERTIES
        //Command properties
        public ICommand OpenFilesCommand
        {
            get;
            private set;
        }
        public ICommand OpenFolderCommand
        {
            get;
            private set;
        }
        public ICommand NewPlaylistCommand
        {
            get;
            private set;
        }
        public ICommand OpenPlaylistCommand
        {
            get;
            private set;
        }
        public ICommand ExitProgramCommand
        {
            get;
            private set;
        }
        public ICommand OpenHelpWindowCommand
        {
            get;
            private set;
        }
    }
}
