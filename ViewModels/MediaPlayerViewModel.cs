using EmilWallin_Assignment7.Commands;
using EmilWallin_Assignment7.Events;
using EmilWallin_Assignment7.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EmilWallin_Assignment7.ViewModels
{
    public class MediaPlayerViewModel : MediaManagerParent
    {
        private Uri audioSource;
        private Uri videoSource;
        private bool isPlaying = true;
        private int playIndex = 0;
        private bool isShuffle = false;
        private string totalDurationString = "";

        public EventHandler<PlayPauseEvent> TogglePlayEvent;
        public EventHandler<VideoPlayerEvent> VideoPlayerEvent;

        public MediaPlayerViewModel() : base()
        {
            TogglePlayCommand = new TogglePlayCommand(this);
            NewSongCommand = new NewSongCommand(this);
        }

        //Toggle play on and off
        public void TogglePlay()
        {
            isPlaying = !isPlaying;

            TogglePlayEvent.Invoke(this, new PlayPauseEvent(isPlaying));
            
            OnPropertyChanged("PlayText");
            UpdateTotalDuration();
        }

        //Plays next song. Sets isplaying of previous and next song. Randomizes a number (which cannot be the same index as the previous one) to use as playindex
        public void PlayNext()
        {
            if (playIndex == FileInfo.Count - 1 && !isShuffle)
                return;

            FileInfo[playIndex].IsPlaying = false;

            if (isShuffle)
            {
                Random rnd = new Random();
                int previousIndex = playIndex;
                
                while (previousIndex == playIndex)
                {
                    playIndex = rnd.Next(0, FileInfo.Count - 1);
                }
            }
            else
                playIndex++;

            SetSource();

            FileInfo[playIndex].IsPlaying = true;

            OnPropertyChanged("PlayIndex");
            //NewSongEvent.Invoke(this, new Events.NewSongEvent(new Uri(FileInfo[playIndex].FilePath)));
        }

        //Plays previous song. sets up the IsPlaying and sets the mediasource. does not work when shuffling
        public void PlayPrevious()
        {
            if (playIndex <= 0)
                return;

            FileInfo[playIndex].IsPlaying = false;

            playIndex--;
            SetSource();

            FileInfo[playIndex].IsPlaying = true;

            OnPropertyChanged("PlayIndex");
            //NewSongEvent.Invoke(this, new Events.NewSongEvent(new Uri(FileInfo[playIndex].FilePath)));
        }

        //Sets a new playindex (through user input)
        public void SetPlayIndex(int index)
        {
            if (index < 0 || index > FileInfo.Count - 1)            //Index guard clause
                return;

            FileInfo[playIndex].IsPlaying = false;

            playIndex = index;

            SetSource();

            FileInfo[playIndex].IsPlaying = true;

            OnPropertyChanged("PlayIndex");
        }

        //Sets the playlist of the mediaplayer
        public void SetPlayList(ObservableCollection<FileInfoHolder> fileInfo)
        {
            FileInfo.Clear();

            foreach(FileInfoHolder fih in fileInfo)
            {
                fih.IsPlaying = false;
                this.FileInfo.Add(fih);
            }
            playIndex = 0;

            SetSource();

            OnPropertyChanged("PlayIndex");
            FileInfo[playIndex].IsPlaying = true;

            UpdateTotalDuration();
        }

        private void UpdateTotalDuration()
        {
            TimeSpan totalDuration = new TimeSpan();
            foreach (FileInfoHolder fih in FileInfo)
            {
                totalDuration += TimeSpan.FromSeconds(fih.DurationSpan.TotalSeconds);
            }

            totalDurationString =  totalDuration.ToString(@"hh\:mm\.ss");

            OnPropertyChanged("TotalDuration");
        }

        private void SetSource()
        {
            if (FileInfo[playIndex].MediaType == MediaType.Audio)
            {
                VideoSource = null;
                AudioSource = new Uri(FileInfo[playIndex].FilePath);
                VideoPlayerEvent.Invoke(this, new Events.VideoPlayerEvent(false));
            }
            else if (FileInfo[playIndex].MediaType == MediaType.Video)
            {
                AudioSource = null;
                VideoSource = new Uri(FileInfo[playIndex].FilePath);
                VideoPlayerEvent.Invoke(this, new Events.VideoPlayerEvent(true));
            }
            OnPropertyChanged("TotalDuration");
        }

        public void ClearPlaylist()
        {

        }


        //Get total duration for FileInfo
        public string TotalDuration
        {
            get
            {
                return "Total Duration: " + totalDurationString;
            }
        }

        public bool IsPlaying
        {
            get { return isPlaying; }
        }

        public int PlayIndex
        {
            get { return playIndex; }
        }

        public Uri AudioSource
        {
            get { return audioSource; }
            set {
                audioSource = value;
                OnPropertyChanged("AudioSource");
            }
        }

        public Uri VideoSource
        {
            get { return videoSource; }
            set
            {
                videoSource = value;
                OnPropertyChanged("VideoSource");
            }
        }

        public bool IsShuffle
        {
            get { return isShuffle; }
            set { 
                isShuffle = value;
                OnPropertyChanged("IsShuffle");
            }
        }

        public string PlayText
        {
            get
            {
                return (isPlaying ? "Pause" : "Play");
            }
        }


        public ICommand TogglePlayCommand { get; private set; }
        public ICommand NewSongCommand { get; private set; }
    }
}
