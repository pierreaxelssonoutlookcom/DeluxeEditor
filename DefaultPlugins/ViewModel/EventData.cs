using Model;
using System;
using System.CodeDom;

namespace DeluxeEdit.DefaultPlugins.ViewModel
{
    public class EventData
    {
        public delegate void Cust(EventType type, ContentPath path);
        public event EventHandler<CustomEventArgs> Changed;
        private MyEditFile editFile;
        private EventType currentType;
        private bool TypeChanged;
        private bool EditFilehanged;
        public MyEditFile EditFile
        {
            get
            {
                return editFile;
            }
            set
            {
                if (value != editFile)
                {
                    editFile = value;
                    EditFilehanged = true;


                    CheckToRaiseEvent();

                }
            }
        }



        public EventType CurrentType
        {
            get
            {
                return currentType;
            }
            set
            {
                if (value != currentType)
                {
                    currentType = value;
                    TypeChanged = true;

                    CheckToRaiseEvent();

                }
            }
        }





        public void CheckToRaiseEvent()
        {
            if (EditFilehanged && TypeChanged)
            {

                Changed.Invoke(this, new CustomEventArgs { Type = CurrentType, Data = EditFile });

            }

        }

        public EventData()
        {


            Changed += EventData_Changed;
        }

        private void EventData_Changed(object? sender, CustomEventArgs e)
        {
            CheckToRaiseEvent();
        }



        public void PublishNewFile(MyEditFile data)
        {
            if (data == null) return;

            CurrentType = EventType.NewFile;
            EditFile = data;




            CheckToRaiseEvent();

        }
        public void PublishEditFile(MyEditFile data)
        {

            if (data == null) return;

            CurrentType = EventType.EditFile; ;
            EditFile = data;

            CheckToRaiseEvent();

        }




        public void subscrile(EventHandler<CustomEventArgs> handler)
        {
            Changed += handler;
        }

    }
}