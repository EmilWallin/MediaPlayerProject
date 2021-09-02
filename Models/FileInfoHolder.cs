using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace EmilWallin_Assignment7.Models
{
    [Serializable]
    public class FileInfoHolder : INotifyPropertyChanged
    {
        private bool isPlaying = false;

        private MediaType mediaType;

        private string fileName;
        private string filePath;
        private string directoryPath;
        private string fileType;

        private long fileSize;
        private string fileSizeString;
        private TimeSpan durationSpan;
        private string durationString;

        //Constructor, initialises everything based on the C# FileInfo class (with additional info)
        public FileInfoHolder(FileInfo info)
        {
            fileName = info.Name;
            filePath = info.FullName;
            fileSize = info.Length;
            fileType = info.Extension;
            mediaType = MediaChecks.CheckMediaType(fileType);

            CheckMediaLength();

            directoryPath = info.DirectoryName.Substring(info.DirectoryName.LastIndexOf('\\'));

            FileSizeToString();
        }

        //Check media length algorithm, saves to TimeSpan and to a formatted string
        private void CheckMediaLength()
        {
            if (mediaType != MediaType.Unknown)
            {
                ShellFile sf = ShellFile.FromFilePath(filePath);
                double length = 0;              //Length in nanoseconds

                double.TryParse(sf.Properties.System.Media.Duration.Value.ToString(), out length);

                if (length > 0)
                {
                    double durationSeconds = length / 10000000;     //Convert to seconds
                    durationSpan = TimeSpan.FromSeconds(durationSeconds);
                    
                    if (durationSpan.Hours == 0)
                        durationString = durationSpan.ToString(@"mm\.ss");
                    else
                        durationString = durationSpan.ToString(@"hh\:mm\.ss");
                }
            }
        }

        //Calls FileSizeToString static method that converts file size to relevantly formatted string
        private void FileSizeToString()
        {
            fileSizeString = FileSizeToStringConverter.FileSizeToString(fileSize);
        }

        //Properties
        public string FileName
        {
            get { return fileName.Substring(0, fileName.Length - fileType.Length); }
        }
        public string FilePath
        {
            get { return filePath; }
        }
        public string DirectoryPath
        {
            get { return directoryPath; }
        }
        public string FileType
        {
            get { return fileType; }
        }
        public long FileSize
        {
            get { return fileSize; }
        }
        public string FileSizeString
        {
            get { return fileSizeString; }
        }
        public MediaType MediaType
        {
            get { return mediaType; }
            set { mediaType = value; }
        }
        public string DurationString
        {
            get { return durationString; }
        }
        public TimeSpan DurationSpan
        {
            get { return durationSpan; }
        }

        public bool IsPlaying
        {
            get { return isPlaying; }
            set 
            { 
                isPlaying = value;
                OnPropertyChanged("IsPlaying");
            }
        }


        #region INotifyPropertyChanged EventHandler and method
        [field:NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
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

