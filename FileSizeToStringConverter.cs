using System;
using System.Collections.Generic;
using System.Text;

namespace EmilWallin_Assignment7
{
    class FileSizeToStringConverter
    {
        //Converts filesize(long) to string, up to GB. Similar to window OS it chooses Bytes, KB, MB, GB based on its size
        public static string FileSizeToString(long fileSize)
        {
            string outString = "";
            float workingSize = fileSize;
            string numSize = fileSize.ToString();

            if (numSize.Length <= 3)
            {
                outString = String.Format("{0} {1}", workingSize, " Bytes");
            } else if (numSize.Length <= 6) {
                outString = String.Format("{0:0.#} {1}", (workingSize / 1000), "KB");
            } else if (numSize.Length <= 9)
            {
                outString = String.Format("{0:0.##} {1}", (workingSize / 1000000), "MB");
            } else
            {
                outString = String.Format("{0:0.##} {1}", (workingSize / 1000000000), "GB");
            }

            return outString;
        }
    }
}
