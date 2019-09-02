using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BokLib.Tools;

namespace BokLib.Log
{
    public class LogTools
    {
        public static void Initialize(BokModInfo info)
        {
            Console.WriteLine($"{Timestamp()} BokLib - Loading Mod: \"{info.Name}\" Version: \"{info.Version}\"");
        }

        public static void Debug(BokModInfo info, string message)
        {
#if DEBUG
            Console.WriteLine($"{Timestamp()} BokLib - DEBUG ({info.Name}): {message}");
#endif
        }

        public static void Log(BokModInfo info, string message)
        {
            Console.WriteLine($"{Timestamp()} BokLib - LOG ({info.Name}): {message}");
        }
 
        private static string Timestamp() => System.DateTime.UtcNow.ToString("[HH:mm:ss.fff]");
    }
}