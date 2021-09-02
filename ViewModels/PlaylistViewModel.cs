using EmilWallin_Assignment7.Commands;
using EmilWallin_Assignment7.Events;
using EmilWallin_Assignment7.Models;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace EmilWallin_Assignment7.ViewModels
{
    public class PlaylistViewModel : MediaManagerParent
    {
        public event EventHandler<HoverFormEvent> Hover;
        public event EventHandler<DropEvent> DropFiles;

        private Playlist playlist;
        private string filePath = null;
        
        //Empty constructor for new playlists
        public PlaylistViewModel() : base()
        {
            playlist = new Playlist();
            SetupCommands();
        }

        //Constructor with the opened playlist (from file) and the filepath from where it was opened
        public PlaylistViewModel(Playlist playlist, string filePath) : base()
        {
            this.filePath = filePath;
            this.playlist = playlist;

            string tempStr = "";

            foreach(FileInfoHolder fih in playlist.FilesList)
            {
                if (File.Exists(fih.FilePath))                  //Check if file exists before adding it to the filelist
                    FileInfo.Add(fih);
                else
                    tempStr += fih.FileName + "\n";
            }

            if (tempStr != "")
                MessageBox.Show("These files could not be found:\n" + tempStr, "Could Not Find Files");

            SetupCommands();
        }

        //Setup Commands and initial OnPropertyChangeds. Called in constructors
        private void SetupCommands()
        {
            PlayPlaylistCommand = new PlayPlaylistCommand(this);
            ClosePlaylistCommand = new ClosePlaylistCommand(this);

            OnPropertyChanged("PlaylistName");
            OnPropertyChanged("NoOfFiles");
            OnPropertyChanged("TotalDuration");
        }

        //Adds file to the FileInfo list and playlist through adding a FileInfoHolder instead of a FileInfo object
        public void AddToFileList(FileInfoHolder fileToAdd)
        {
            if (CheckIfFileInCollection(fileToAdd.FileName))       //Currently the playlists dont support multiples of the same file
            {
                foreach(FileInfoHolder fih in FileInfo)
                {
                    if (fileToAdd.FilePath == fih.FilePath)
                        return;
                }
            }

            FileInfo.Add(fileToAdd);
            playlist.AddFile(fileToAdd);
            OnPropertyChanged("FileInfo");
            OnPropertyChanged("NoOfFiles");
            OnPropertyChanged("TotalDuration");
        }

        //Called by view to create drag and drop functionality. Lets 
        public void TriggerHover(bool isHovering)
        {
            HoverFormEvent hoverEvent = new HoverFormEvent(isHovering);
            Hover(this, hoverEvent);
        }

        //Called by view to create drag and drop functionality (Triggers "dropping" files from MediaManagerView)
        //Triggers DropFiles Event
        public void TriggerDropFiles()
        {
            DropFiles(this, new DropEvent());
            Mouse.OverrideCursor = Cursors.Arrow;
        }
        

        //Get total duration for FileInfo, formats TimeSpan
        public string TotalDuration
        {
            get 
            {
                TimeSpan totalDuration = new TimeSpan();
                foreach(FileInfoHolder fih in FileInfo)
                {
                    totalDuration += fih.DurationSpan;
                }
                return "Playlist Duration: " + totalDuration.ToString(@"hh\:mm\:ss");
            }
        }
        //Playlistname bound to textbox in window
        public string PlaylistName
        {
            get { return playlist.Name; ; }
            set 
            { 
                playlist.Name = value;
                OnPropertyChanged("PlaylistName");
            }
        }
        //Property for the filepath
        public string FilePath
        {
            set { filePath = value; }
            get { return filePath; }
        }
        //Playlist get
        public Playlist Playlist
        {
            get { return playlist; }
        }

        //Commands
        public ICommand PlayPlaylistCommand
        {
            get;
            private set;
        }
        public ICommand ClosePlaylistCommand
        {
            get;
            private set;
        }
    }
}
