using OOORUL.Model.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOORUL.Model.Core
{
    internal static class DataMediator
    {
        public static User user;


        public static void deleteData()
        {
            user = null;
        }
    }
}
