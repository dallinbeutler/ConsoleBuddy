using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleBuddy
{
   public abstract class StatusItem
   {
      protected int X = 0;
      protected int Y = 0;
      protected ConsoleColor color;

      public StatusItem(int x, int y, ConsoleColor color)
      {
         this.X = x;
         this.Y = y;
         this.color = color;
      }

      public abstract void Update();

   }
}
