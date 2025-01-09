using Model;
using System;

namespace ViewModel
{
    public class DoWhenTextChange
    {
        public void Load()
        {
            string? headerString;

            if (MyEditFiles.Current != null)
            {
                var tab = MyEditFiles.Current.Tab;
                if (tab != null)
                {
                    var header = tab.Header;
                    MyEditFiles.Current.TextHasChanged = true;
                    if (header != null)
                    {
                        headerString = header.ToString();

                        if (headerString != null && headerString.EndsWith("*") == false)
                        {
                            headerString = String.Concat(headerString, "*");
                            tab.Header = headerString;
                        }
                    }
                }
            }
        }
    }
}
