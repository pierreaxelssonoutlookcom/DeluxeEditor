using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace DeluxeEdit.DefaultPlugins.ViewModel
{
    public enum EventType { NewFile, EditFile }

    public class CustomViewData
    {

        public delegate void ChangedHandler(EventType type);

        public event ChangedHandler Changed;


        public static ContentPath NewFile { get; set; }
        public static ContentPath EditFile { get; set; }
        private  static ContentPath OldNewFile { get; set; }
        private static ContentPath OldEditFile { get; set; }

        private bool IsNewFile { get; set; }

        protected  void RaiseEvent(EventArgs e)
        {

            if (! NewFile.Equals(OldNewFile))                
                Changed.Invoke(EventType.NewFile);

            if (!EditFile.Equals(OldEditFile))
                Changed.Invoke(EventType.EditFile);

        }
        public void PublishNewFile(ContentPath path)
        { 
            OldNewFile=NewFile;

            NewFile = path;
        }
        public void PublishLoadFile(ContentPath path)
        {
            OldEditFile = EditFile;
            EditFile = path;
        }






        public void subscrile(ChangedHandler handler)
        {
            Changed += handler;
        }

  
        
    }
}