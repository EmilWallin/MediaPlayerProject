using System;
using System.Collections.Generic;
using System.Text;

namespace EmilWallin_Assignment7.Models
{

    //Class used to saved playlists
    [Serializable]
    public class Playlist
    {
        private List<FileInfoHolder> fileList;
        private string name = "";

        public Playlist()
        {
            fileList = new List<FileInfoHolder>();
        }

        //Add fileinfoholder to playlist
        public void AddFile(FileInfoHolder newFile)
        {
            if (newFile != null)
            {
                fileList.Add(newFile);
            }
        }

        //Remove fileinfoholder from playlist
        public void RemoveFile(FileInfoHolder removeFile)
        {
            for(int i = 0; i < fileList.Count; i++)
            {
                if (fileList[i].FilePath == removeFile.FilePath)
                {
                    fileList.RemoveAt(i);
                    break;
                }
            }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<FileInfoHolder> FilesList
        {
            get { return fileList; }
        }
    }
}
