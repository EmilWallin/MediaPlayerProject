using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace EmilWallin_Assignment7
{
    class MediaChecks
    {
        //Common audio extensions
        private static string[] audioExtensions =
        {
            ".wav", ".mp3", ".aac", ".ogg", ".aiff", ".m4a", ".flac", ".wma"
        };
        //Common video extensions
        private static string[] videoExtensions =
{
            ".mp4", ".avi", ".divx", ".wmv", ".mov", ".mkv", ".webm", ".m4v", ".m4p", ".mpeg", ".mpg", ".m2v", ".flv"
        };


        //Check if the fileextension is within the arrays, and which mediatype it is
        public static MediaType CheckMediaType(string fileExtension)
        {
            if (-1 != Array.IndexOf(audioExtensions, fileExtension.ToLower()))
            {
                return MediaType.Audio;
            }
            else if (-1 != Array.IndexOf(videoExtensions, fileExtension.ToLower()))
            {
                return MediaType.Video;
            }
            else
            {
                return MediaType.Unknown;
            }
        }
    }
}
