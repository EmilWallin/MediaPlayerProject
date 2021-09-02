using EmilWallin_Assignment7.Events;
using EmilWallin_Assignment7.ViewModels;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for VideoPlayer.xaml. Uses same viewmodel as MediaPlayer.xaml
    /// </summary>
    public partial class VideoPlayer : Window
    {
        private bool isVideoPlaying = true;

        //No parameter-less constructor as this view is always built upon the viewmodel of an existing mediaplayer
        public VideoPlayer(MediaPlayerViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            viewModel.TogglePlayEvent += TogglePlay;

            mediaElement.Play();
        }

        //TogglePlay method, subscriber of toggleplay of mediaplayer
        private void TogglePlay(object sender, PlayPauseEvent e)
        {
            if (!isVideoPlaying)
                return;

            if (e.IsPlaying == true)
                mediaElement.Play();
            else
                mediaElement.Pause();
        }

        /// <summary>
        ///     Events raised in window
        /// </summary>

        //VolumeChanged follows slider in window
        private void Volume_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(mediaElement != null)
                mediaElement.Volume = e.NewValue;
        }
        //On media ended, play next
        private void Media_Ended(object sender, RoutedEventArgs e)
        {
            ((MediaPlayerViewModel)DataContext).PlayNext();
        }
        //If window closes, pauses the media
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isVideoPlaying = false;
            mediaElement.Pause();
            ((MediaPlayerViewModel)DataContext).TogglePlayEvent -= TogglePlay;
        }
        //If the window is loaded, it plays the loaded media instantly
        private void Window_Loaded(object sender, EventArgs e)
        {
            isVideoPlaying = true;
            mediaElement.Play();
        }
    }
}
