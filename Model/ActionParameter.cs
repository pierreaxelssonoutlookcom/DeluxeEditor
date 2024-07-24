﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
 {
    public class ActionParameter
    {
        public string Parameter { get; set; } = "";
        public List<string> InData { get; set; }
        public ActionParameter()
        {
            Parameter = "";
            InData = new List<string>();
        }
        public ActionParameter(string parameter, string indata)
        {
            Parameter = parameter;
            InData = indata.Split(Environment.NewLine).ToList();
        }
        public ActionParameter(string parameter, List<string> indata)
        {
            Parameter = parameter;
            InData = indata;
        }
        public ActionParameter(string? parameter)
        {

            if (parameter!=null) Parameter = parameter;       }
    }
}
