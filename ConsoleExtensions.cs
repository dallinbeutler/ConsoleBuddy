using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleBuddy
{
   public static class ConsoleExtensions
   {
      public static void WriteAtPosition(this System.ConsoleColor color, int x = 0, int y = 0, params object[] objects)
      {
         using (var old = new SaveConsoleState())
         {
            System.Console.SetCursorPosition(x, y);
            foreach (object o in objects)
            {
               if (o.GetType() == typeof(System.ConsoleColor))
               {
                  System.Console.ForegroundColor = (System.ConsoleColor)o;
               }
               else
               {
                  System.Console.Write(o);
               }
            }
         }
      }

   }
}
