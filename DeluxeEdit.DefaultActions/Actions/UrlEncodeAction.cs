using DeluxeEdit.Model.Interface;
using DeluxeEdit.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace DeluxeEdit.DefaultActions.Actions
{
    public class UrlEncodeAction :  INamedAction
    {
        public string Name { get; set; } = "UrlEncode";
        public string Titel { get; set; } = "Url Declode";
        public string Result { get; set; } = "";

        public ActionParameter Parameter { get; set; }

        public PresentationOptions PresentationOptions { get; set; }

        public UrlEncodeAction()
        {
            PresentationOptions = new PresentationOptions();
            Parameter=new ActionParameter();
        }

        public string Perform(ActionParameter parameter)
        {
            Result = WebUtility.UrlEncode(parameter.Parameter);
            return Result;
        }
    }


}
