using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleBuddy
{
   public static class Console
   {
      public static int HeaderHeight = 2;

      static Console()
      {

         System.Console.SetCursorPosition(0, HeaderHeight + 1);
         System.Console.WindowHeight = 65;
         System.Console.WindowWidth = 120;
         System.Console.BufferHeight = System.Console.WindowHeight;
         System.Console.BufferWidth = 120;
         System.Console.ForegroundColor = ConsoleColor.Cyan;
         System.Console.WriteLine(new String('-', System.Console.BufferWidth));
         System.Console.ForegroundColor = ConsoleColor.White;
      }

      public static void Write(bool trim, int maxBufferWidth, params object[] objects)
      {

         var last = System.Console.ForegroundColor;
         int length = 0;
         int MaxLength = System.Console.BufferWidth;
         StringBuilder stringBuilder = new StringBuilder();
         foreach (object o in objects)
         {
            if (o.GetType() == typeof(System.ConsoleColor))
            {
               System.Console.ForegroundColor = (System.ConsoleColor)o;
            }
            else
            {
               string s = o.ToString();
               if (trim)
               {

                  length += s.Length;
                  if (length > MaxLength)
                  {
                     System.Console.Write(s.Substring(0, length - MaxLength - 3) + "...");
                  }
                  else
                  {
                     System.Console.Write(s.PadRight(maxBufferWidth));
                  }
               }
               else
               {
                  System.Console.Write(s);
               }
            }
         }
         if (last != System.Console.ForegroundColor)
            System.Console.ForegroundColor = last;
      }

      public static void WriteLine(params object[] objects)
      {
         if (System.Console.CursorTop == System.Console.BufferHeight - 1)
         {
            System.Console.MoveBufferArea(0, HeaderHeight + 3,
               System.Console.BufferWidth, System.Console.BufferHeight - (HeaderHeight + 3),
               0, HeaderHeight + 2);
            System.Console.SetCursorPosition(0, System.Console.CursorTop - 1);
         }
         Write(true, System.Console.BufferWidth-1, objects);
         Write(false, 1,"\n");
         //Interlocked.Increment(ref HeaderHeight);
      }
      public static void WriteSpecificLine(int column, int line,int BufferWidth, params object[] objects)
      {
         int current = System.Console.CursorTop;
         System.Console.SetCursorPosition(column, line);
         Write(true,BufferWidth, objects);
         System.Console.SetCursorPosition(0, current);

      }
      public static void ClearLine(int line)
      {
         int current = System.Console.CursorTop;
         System.Console.SetCursorPosition(0, line);
         System.Console.WriteLine();
         System.Console.SetCursorPosition(0, current);
      }
   }
}

