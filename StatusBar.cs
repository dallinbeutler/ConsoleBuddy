using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBuddy
{
   

   public class StatusBar : StatusItem
   {
      private int barWidth;
      private bool showNums;

      private int _Current;

      public int Current
      {
         get { return _Current; }
         set
         {
            _Current = value;
            Update();
         }
      }

      private int _Max;

      public int Max
      {
         get { return _Max; }
         set
         {
            _Max = value;
            Update();
         }
      }


      public StatusBar(int x, int y, int current, int max, ConsoleColor color, int barWidth = 10, bool showNums = true) : base(x, y, color)
      {
         this.X = x;
         this.Y = y;
         this._Current = current;
         this._Max = max;
         this.color = color;
         this.barWidth = barWidth;
         this.showNums = showNums;
         Update();
      }

      public override void Update()
      {
         using (var old = new SaveConsoleState())
         {
            System.Console.SetCursorPosition(X, Y);
            //Console.ForegroundColor = ConsoleColor.White;
            //Console.Write("[");
            int percent = (int)((Current / (float)(Max)) * barWidth);
            System.Console.BackgroundColor = color;
            System.Console.Write(new String(' ', Util.Clamp(percent, 0, barWidth)));
            System.Console.BackgroundColor = ConsoleColor.DarkGray;
            System.Console.Write(new String(' ', Util.Clamp(barWidth - percent, 0, barWidth)));
            System.Console.BackgroundColor = old.colorB;
            System.Console.Write(" " + Current + "/" + Max);

            //Console.Write("]");
         }
      }
   }




}
