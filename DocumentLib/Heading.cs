using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentLib
{
    public class Heading : Content
    {
        public Heading(string id, int line, string text, int level, bool showInToc) : base(id, line, text)
        {
            this.level = level;
            this.showInToc = showInToc;
        }

        public int level;
        public bool showInToc;
    }
}
