using System;
using System.Collections.Generic;
using System.Linq;

using HesaEngine.SDK;

namespace T2IN1_REBORN_LIB
{
    public static class Misc
    {
        public static bool IsEmpty<T>(this IEnumerable<T> source) => !source.Any();

        public static Random Randomizer = new Random();
        public static void DelayAction(Action action) { Core.DelayAction(action, Randomizer.Next(300, 500)); }
    }
}
