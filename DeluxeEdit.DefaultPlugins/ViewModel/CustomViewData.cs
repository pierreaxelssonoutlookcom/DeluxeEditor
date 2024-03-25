using Model;
using System;

namespace DeluxeEdit.DefaultPlugins.ViewModel
{
    public enum EventType { NewFile, EditFile }
    public delegate void Cust(EventType type, ContentPath path);

    
    public class CustomEventArgs : EventArgs
    {

        public EventType Type { get; set; }

        public ContentPath Path{ get; set; }
    }
    public class CustomViewData
    {
        public event EventHandler<CustomEventArgs> RaiseEvent;

        public ContentPath NewFile { get; set; }
        public ContentPath EditFile { get; set; }


        private EventType? currentItem = null;


        public CustomViewData()
        {

            RaiseEvent += RaiseCustomEvent;
        }

        private void CustomViewData_RaiseCEvent()
        {
            throw new NotImplementedException();
        }

        public void RaiseCustomEvent(object? sender, CustomEventArgs e)
        {
            if (currentItem == EventType.NewFile)
                RaiseEvent.Invoke( null, new CustomEventArgs { Path = NewFile, Type = currentItem.Value });
            else if (currentItem == EventType.EditFile)
                RaiseEvent.Invoke(null, new CustomEventArgs { Path = EditFile, Type = currentItem.Value });

            currentItem = null;
       }

        
         
 
         public void PublishNewFile(ContentPath path)
        {
            if (path == null) return; 
             
            currentItem=EventType.NewFile;
            NewFile = path;
            RaiseCustomEvent(null, new CustomEventArgs { Type=EventType.NewFile,  Path=path  }   );
        }
        public void PublishLoadFile(ContentPath path)
        {

            if (path == null) return;

            currentItem = EventType.EditFile; ;
            NewFile = path;
            RaiseCustomEvent(null, new CustomEventArgs { Type = EventType.EditFile, Path = path });
        }






        public void subscrile(EventHandler<CustomEventArgs> handler)
        {
            RaiseEvent += handler;
        }

  
        
    }
}