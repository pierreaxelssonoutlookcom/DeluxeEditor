using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace DeluxeEdit.DefaultPlugins.ViewModel
{
    public enum EventType { NewFileType, LoadFile }

    public class CustomViewData
    {

        public delegate void ChangedHandler(EventType type);

        public event ChangedHandler Changed;


        public static ContentPath OldLastNewFile { get; set; }
        public static ContentPath LastNewFile { get; set; }
        public static ContentPath LastLoadFile { get; set; }

        private  bool IsNewFile { get; set; }

        protected  void RaiseEvent(EventArgs e)
        {
         
            
//            Changed.Invoke

        }
        public void PublishNewFile(ContentPath path)
        {
            LastNewFile = path;
        }
        public void PublishLoadFile(ContentPath path)
        {
            LastLoadFile= path;
        }





        public void subscrile(ChangedHandler handler)
        {
            Changed += handler;
        }

  
        
    }
}