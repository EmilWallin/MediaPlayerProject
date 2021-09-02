using EmilWallin_Assignment7.Commands;
using EmilWallin_Assignment7.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace EmilWallin_Assignment7.ViewModels
{
    public abstract class MediaManagerParent : INotifyPropertyChanged
    {
        private ObservableCollection<FileInfoHolder> fileInfo;
        private ObservableCollection<FileInfoHolder> selectedFiles;

        private IComparer<FileInfoHolder> previousSort = null;

        //Base Constructor. Initializes file lists, commands, etc.
        public MediaManagerParent()
        {
            fileInfo = new ObservableCollection<FileInfoHolder>();
            selectedFiles = new ObservableCollection<FileInfoHolder>();

            ClearSelectionCommand = new ClearSelectionCommand(this);
            RemoveFilesCommand = new RemoveFilesCommand(this);
            SortListViewCommand = new SortListViewCommand(this);
        }

        //Add files to lists/collections, gives base implementation, but it is not used
        public virtual void AddToFileList(FileInfo[] infoArray)
        {
            foreach (FileInfo fi in infoArray)
            {
                if (CheckIfFileInCollection(fi.Name))            //If file is already in collection, skip adding this file
                {
                    continue; 
                }
                fileInfo.Add(new FileInfoHolder(fi));
            }

            OnPropertyChanged("FileInfo");
            OnPropertyChanged("NoOfFiles");
            OnPropertyChanged("NoOfVideoFiles");
            OnPropertyChanged("NoOfAudioFiles");
        }

        //Delete file method
        public void DeleteFile(FileInfoHolder fi)
        {
            for (int i = 0; i < fileInfo.Count; i++)
            {
                if (fileInfo[i].FilePath == fi.FilePath)
                    fileInfo.RemoveAt(i);
            }
            OnPropertyChanged("FileInfo");
            OnPropertyChanged("NoOfFiles");
            OnPropertyChanged("NoOfVideoFiles");
            OnPropertyChanged("NoOfAudioFiles");
        }

        //Sort the FileInfo. Copy to list, sort the list, create new observable collection. ObsCollection lacks sorting functions
        public void SortFileInfo(IComparer<FileInfoHolder> comparer)
        {
            List<FileInfoHolder> tempList = new List<FileInfoHolder>(FileInfo);

            if (previousSort == null || previousSort.GetType() != comparer.GetType())
            {
                tempList.Sort(comparer);
                previousSort = comparer;
            }
            else
            {
                tempList.Reverse();
            }
            fileInfo = new ObservableCollection<FileInfoHolder>(tempList);
            OnPropertyChanged("FileInfo");
        }

        //Checks if the fileinfo file is already in the FileInfo collection
        protected bool CheckIfFileInCollection(string fileName)
        {
            foreach(FileInfoHolder fih in FileInfo)
            {
                if (fileName == fih.FileName)
                    return true;
            }
            return false;
        }

        //PROPERTIES
        public ObservableCollection<FileInfoHolder> FileInfo
        {
            get { return fileInfo; }
        }
        public ObservableCollection<FileInfoHolder> SelectedFiles
        {
            get { return selectedFiles; }
            set
            {
                selectedFiles = value;
                OnPropertyChanged("SelectedFiles");
                OnPropertyChanged("NoOfSelectedFiles");
            }
        }


        #region UI File Count Properties
        public int NoOfFiles
        {
            get { return fileInfo.Count; }
        }
        public int NoOfVideoFiles
        {
            get
            {
                int count = 0;

                foreach (FileInfoHolder fi in fileInfo)
                {
                    if (fi.MediaType == MediaType.Video)
                    {
                        count++;
                    }
                }
                return count;
            }
        }
        public int NoOfAudioFiles
        {
            get
            {
                int count = 0;

                foreach (FileInfoHolder fi in fileInfo)
                {
                    if (fi.MediaType == MediaType.Audio)
                    {
                        count++;
                    }
                }
                return count;
            }
        }
        public int NoOfSelectedFiles
        {
            get { return selectedFiles.Count; }
        }
        #endregion

        //Command properties
        public ICommand ClearSelectionCommand
        {
            get;
            private set;
        }
        public ICommand RemoveFilesCommand
        {
            get;
            private set;
        }
        public ICommand SortListViewCommand
        {
            get;
            private set;
        }

        //INotifyPropertyChanged eventhandler and method
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }
}
