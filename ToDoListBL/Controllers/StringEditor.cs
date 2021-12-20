using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ToDoListBL.Controllers
{
    class StringEditor
    {
        private string Str { get; set; }

        /// <summary>
        /// Class for text edition.
        /// </summary>
        /// <param name="str">Task name.</param>
        public StringEditor(string str)
        {
            Str = str;
            Str = KeyRead();
        }

        /// <summary>
        /// Get edited text.
        /// </summary>
        /// <returns></returns>
        public string Str_()
        {
            return Str;
        }

        /// <summary>
        /// Text edition function.
        /// </summary>
        /// <returns></returns>
        private string KeyRead()
        {
            string oldStr = Str;
            int caret = Str.Length;
            Console.WriteLine("Text:");
            Console.Write(Str);
            do
            {
                var k = Console.ReadKey(true);
                {
                    if (k.Key == ConsoleKey.LeftArrow && caret > 0)
                    {
                        Console.CursorLeft--;
                        caret--;
                        continue;
                    }
                    if (k.Key == ConsoleKey.RightArrow & caret < Str.Length)
                    {
                        Console.CursorLeft++;
                        caret++;
                        continue;
                    }
                    if ((k.Modifiers & ConsoleModifiers.Alt) == ConsoleModifiers.Alt ||
                        (k.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control || //Ignore if Alt or Ctrl is pressed.
                        k.KeyChar == '\u0000' || //Ignore if KeyChar value is \u0000.
                        k.Key == ConsoleKey.Tab) // Ignore tab key.
                        continue;
                    if (k.Key == ConsoleKey.Backspace) // Backspace key processing.
                    {
                        try
                        {
                            Str = Str.Substring(0, caret - 1) + Str.Substring(caret);
                            caret = Console.CursorLeft - 1;
                            int StrOldL = Str.Length;
                            StrUpdate(Str, caret);
                        }
                        catch (ArgumentOutOfRangeException) // Catching caret == 0 case.
                        {

                        }
                        continue;
                    }
                    if (k.Key == ConsoleKey.Escape) return oldStr; // Esc key processing. Exit without save.
                    if (k.Key == ConsoleKey.Enter) return Str; // Enter key processing. Exit with save.
                    if (Str.Length >= 110) // Max string length 110;
                        continue;
                    Str = Str.Insert(caret, k.KeyChar.ToString()); // Add symbol after all checkings.
                    caret++;
                    StrUpdate(Str, caret);
                    continue;
                }
            }
            while (true);
        } //TODO: Info head.

        /// <summary>
        /// Console update text.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        private void StrUpdate(string str, int c)
        {
            Console.Clear();
            Console.WriteLine("Text:");
            Console.Write(str);
            Console.CursorLeft = c;
        }

    }
}