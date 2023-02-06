using OOORUL.Model.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOORUL.Model.Core
{
    internal static class ObservableCollectionConverter
    {
        public static ObservableCollection<Product> ToProductObsarvableCollection(this List<Product> list)
        {
             return new ObservableCollection<Product>(list);
        }
    }
}

