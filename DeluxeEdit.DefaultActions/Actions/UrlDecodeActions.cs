﻿using DeluxeEdit.Model;
using DeluxeEdit.Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace DeluxeEdit.DefaultActions.Actions
{
    public class UrlDecodeAction : INamedAction
    {
        public string Name { get; set; } ="UrlDeclode";
        public string Titel { get; set; } =  "Url Declode";
        public PresentationOptions PresentationOptions { get; set; }
        public ActionParameter Parameter { get; set; }

        public string Result { get; set; } = "";

        public UrlDecodeAction()
        {
            Parameter = new ActionParameter();
            PresentationOptions= new PresentationOptions();
        }
        public string Perform(ActionParameter parameter)
        {   
         
            Result = WebUtility.UrlDecode(parameter.Parameter);
            return Result;
        }

    }


}
