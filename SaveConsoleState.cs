using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleBuddy
{
   class SaveConsoleState : IDisposable
   {
      public readonly int Top;
      public readonly int Left;
      public readonly ConsoleColor colorF;
      public readonly ConsoleColor colorB;
      public SaveConsoleState()
      {
         Left = System.Console.CursorLeft;
         Top = System.Console.CursorTop;
         colorF = System.Console.ForegroundColor;
         colorB = System.Console.BackgroundColor;
      }

      public void Dispose()
      {
         System.Console.ForegroundColor = colorF;
         System.Console.BackgroundColor = colorB;
         System.Console.SetCursorPosition(Left, Top);
      }
   }
}
