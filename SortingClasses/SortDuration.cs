using EmilWallin_Assignment7.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace EmilWallin_Assignment7.SortingClasses
{
    class SortDuration : IComparer<FileInfoHolder>
    {
        public int Compare([AllowNull] FileInfoHolder x, [AllowNull] FileInfoHolder y)
        {
            return x.DurationSpan.TotalSeconds.CompareTo(y.DurationSpan.TotalSeconds);
        }
    }
}
