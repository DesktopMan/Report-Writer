using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentLib
{
    public class Parser
    {
        public void SetText(string text)
        {
            this.text = text;
        }

        public bool Parse()
        {
            if (text == "")
                return true;

            return true;
        }

        string text;
    }
}
