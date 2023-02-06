using OOORUL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOORUL.Model
{
    public static class PageChangeMediator
    {
        private static IDictionary<string, Action<object>> DictActions
            = new Dictionary<string, Action<object>>();


        public static void AddAction(string Key, Action<object> action)
        {
            if (!DictActions.ContainsKey(Key))
                DictActions.Add(Key, action);
            if(DictActions.ContainsKey(Key))
                DictActions[Key] = action;
        }


        public static void Transit(string Key, object args = null)
        {
            if( DictActions.ContainsKey(Key) )
                DictActions[Key].Invoke(args);
        }
    }
}
