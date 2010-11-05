using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace iEventViewer
{
    public static class Extensions
    {
        public static void AddRange<T>(this Collection<T> collection, IEnumerable<T> range)
        {
            foreach (T item in range)
                collection.Add(item);
        }
    }
}
