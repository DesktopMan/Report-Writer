using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentLib
{
    class Content
    {
        public Content(string id, string text)
        {
            this.id = id;
            this.text = text;
        }

        public string id;
        public string text;
    }
}
