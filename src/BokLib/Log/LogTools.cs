﻿using System;
using BokLib.Utils;

namespace BokLib.Log
{
    public static class LogTools
    {
        public static void Initialize(BokModInfo info)
        {
            Console.WriteLine($"{Timestamp()} BokLib - Loading Mod: \"{info.Name}\" Version: \"{info.Version}\"");
            // Just print a notice that DEBUG is active if it is, it shouldn't be active in the wild
            Debug(info, "DEBUG enabled!");
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