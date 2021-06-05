using PromotionEngine.Interfaces;
using System;

namespace PromotionEngine.Classes
{
   public class ConsoleLogger : ILogger
    {
        public void LogInfo(string info)
        {
            Console.WriteLine(info);
        }
    }
}
