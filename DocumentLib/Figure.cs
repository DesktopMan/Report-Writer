using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentLib
{
    class Figure : Content
    {
        public Figure(string id, int line, string text, string imagePath) : base(id, line, text)
        {
            this.imagePath = imagePath;
        }

        public string imagePath;
    }
}
