using ConsoleRogue.Customizables;
using RLNET;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleRogue.Components.Drivers
{
    class Messenger
    {
        private static readonly int maxLines = 9;
        private readonly Queue<string> lines;
        public readonly Messages messages;

        public Messenger()
        {
            lines = new Queue<string>();
            messages = new Messages();
        }

        public void Add(string message)
        {
            lines.Enqueue(message);
            if (lines.Count > maxLines)
            {
                lines.Dequeue();
            }
        }

        public void Draw(RLConsole console)
        {
            console.Clear();
            string[] newLines = lines.ToArray();
            for(int i = 0; i < newLines.Length; ++i)
            {
                console.Print(1, i+1, newLines[i], ObjectColoring.textColor);
            }
        }
    }
}
