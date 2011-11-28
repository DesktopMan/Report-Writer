using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentLib
{
    public class Content
    {
        public Content(string id, int line, string text)
        {
            this.id = id;
            this.line = line;
            this.text = text;
        }

        public string id;
        public string text;
        public int line;
    }
}
