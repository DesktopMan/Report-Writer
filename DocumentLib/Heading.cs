using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentLib
{
    public class Heading : Content
    {
        public Heading(string id, int position, string text, int level, bool showInToc) : base(id, position, text)
        {
            this.level = level;
            this.showInToc = showInToc;
        }

        public override string ToString()
        {
            return text.PadLeft((level - 1) * 4 + text.Length);
        }

        public int level;
        public bool showInToc;
    }
}
