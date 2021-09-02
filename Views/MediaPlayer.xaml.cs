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
    /// Interaction logic for MediaPlayer.xaml
    /// </summary>
    public partial class MediaPlayer : Window
    {
        private bool isPlayingVideo = false;
        private VideoPlayer openVideoPlayer = null;

        //Initializes view and sets subscribers to events
        public MediaPlayer()
        {
            InitializeComponent();

            MediaPlayerViewModel mediaPlayerViewModel = new MediaPlayerViewModel();
            DataContext = mediaPlayerViewModel;
            mediaPlayerViewModel.TogglePlayEvent += TogglePlay;
            mediaPlayerViewModel.VideoPlayerEvent += SwitchWindow;
            mediaElement.Play();
        }

        //Gets reference of the viewmodel
        public MediaPlayerViewModel GetViewModel() 
        {
            return (MediaPlayerViewModel)DataContext;
        }
        
        //Switches between video and audio, video needs a new window, creates VideoPlayerView
        private void SwitchWindow(object sender, VideoPlayerEvent e)
        {
            if (e.IsVideo)
            {
                if(openVideoPlayer != null)     //Closes openVideoPlayer if it already exists. Dont have time to implement a shift atm
                {
                    openVideoPlayer.Close();
                }

                this.Topmost = false;
                mediaElement.Pause();
                isPlayingVideo = true;
                openVideoPlayer = new VideoPlayer(GetViewModel());
                openVideoPlayer.Show();
            }
            else
            {
                this.Topmost = true;
                isPlayingVideo = false;
                openVideoPlayer?.Close();
                mediaElement.Play();
            }
        }

        //Toggles play from button (which calls Command, which raises event)
        private void TogglePlay(object sender, PlayPauseEvent e)
        {
            if (isPlayingVideo)
                return;

            if (e.IsPlaying == true)
                mediaElement.Play();
            else
                mediaElement.Pause();
        }

        /// <summary>
        ///     Events triggered in window
        /// </summary>

        //Double clicking a listview item plays it
        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = filesListView.Items.IndexOf(((ListViewItem)sender).Content);
            GetViewModel().SetPlayIndex(index);
        }
        //Volume changed (from slider) connects to mediaElement
        private void Volume_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mediaElement != null)
                mediaElement.Volume = e.NewValue;
        }
        //If media ends, it plays next media
        private void Media_Ended(object sender, RoutedEventArgs e)
        {
            ((MediaPlayerViewModel)DataContext).PlayNext();
        }
        //if window closes, the mediaElement stops playing
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mediaElement.Stop();
        }
    }
}
