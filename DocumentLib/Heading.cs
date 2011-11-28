using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentLib
{
    class Heading : Content
    {
        public Heading(string id, string text, int level, bool showInToc) : base(id, text)
        {
            this.level = level;
            this.showInToc = showInToc;
        }

        public int level;
        public bool showInToc;
    }
}
