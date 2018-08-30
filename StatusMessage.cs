using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleBuddy
{
   public class StatusMessage : StatusItem
   {
      private string _Message;

      public string Message
      {
         get { return _Message; }
         set
         {
            //if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(_Message) && _Message.Length > value.Length)
            //   NeedsClear = _Message.Length - value.Length;
            _Message = value;
            Update();
         }
      }
      private int _MaxBufferWidth;

      public int MaxBufferWidth
      {
         get { return _MaxBufferWidth; }
         set
         {
            _MaxBufferWidth = value;
            Update();
         }
      }
      //int NeedsClear;


      public StatusMessage(string Message, int MaxBufferWidth, int x = 0, int y = 0, ConsoleColor color = ConsoleColor.White) : base(x, y, color)
      {
         this._MaxBufferWidth = MaxBufferWidth;
         this.Message = Message;
      }
      public override void Update()
      {
         using (var old = new SaveConsoleState())
         {
            System.Console.SetCursorPosition(X, Y);
            System.Console.ForegroundColor = color;
            if (Message.Length > MaxBufferWidth)
            {
               System.Console.Write(Message.Substring(0, MaxBufferWidth - 3) + "...");
            }
            else
            {
               System.Console.Write(Message.PadRight(MaxBufferWidth));
            }
         }
      }
   }
}
