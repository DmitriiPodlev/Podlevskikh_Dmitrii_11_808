using System;
using System.Collections.Generic;
using System.Text;

namespace Autotests
{
    public class Node
    {
        public Node(string text)
        {
            Text = text;
        }

        public Node()
        {
            Text = "New text";
        }

        public string Text { get; set; }
    }
}
