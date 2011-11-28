using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentLib
{
    class Figure : Content
    {
        public Figure(string id, string text, string imagePath) : base(id, text)
        {
            this.imagePath = imagePath;
        }

        public string imagePath;
    }
}
