using DeluxeEdit.Actions;
using DeluxeEdit.Actions.Actions;
using DeluxeEdit.Actions.Types.Interfaces;
using System;
using System.Collections.Generic;

namespace DeluxeEdit.Shared
{
    public static class ActionList
    {
//todo: use variation MVC whith long running state</li>
//todo: use notepad++-pluging</li>
//  todo: Create INamedActiom
        public static List<INamedAction> GetAllActions()
        {
            var result = new List<INamedAction>()
           {
               new FileOpenAction{ HotKeys=new List<char> {SystemConstants.ControlKey,'o'  }   }

           };
            return result;
            
        }

    }
}
