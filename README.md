# ConsoleBuddy
A simple Console wrapper for storing status and progress at the top of the window, as well as nice color handling

## Example:
```C#
   using ConsoleBuddy;
   using System;
   using System.Threading;
   
   namespace ConsoleTesting
   {
      class Program
      {
         static void Main(string[] args)
         {
            StatusBar statusBar = new StatusBar(3, 0, 0, 100, ConsoleColor.Red,40);
            StatusMessage message = new StatusMessage("wow!",10,20,1,ConsoleColor.Green);
   
            for (int i = 0; i < 10000; i++)
            {
               ConsoleBuddy.Console.WriteLine(i + " testing many lines ", ConsoleColor.Green, " and Colors");
               Thread.Sleep(90);
               message.Message = ((i % 2) == 1) ? "Odd!" : "Even!!!!";
               statusBar.Current++;
            }
         }
      }
   }
```
