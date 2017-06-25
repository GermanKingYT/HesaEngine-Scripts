using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;

namespace T2IN1_REBORN_ANNIE.Managers
{
    internal class EventManager
    {
        public static void Initialize()
        {
            Obj_AI_Base.OnBuffGained += (sender, args) =>
            {
                if (!sender.IsMe) return;

                if (args.Buff.Name.ToLower().Equals("pyromania_particle")) 
                {
                    Globals.IsPassiveReady = true;
                }
            };

            Obj_AI_Base.OnBuffLost += (sender, args) =>
            {
                if (!sender.IsMe) return;

                if (args.Buff.Name.ToLower().Equals("pyromania_particle")) 
                {
                    Globals.IsPassiveReady = false;
                }
            };

            Logger.Log(">> Executed", ConsoleColor.Green);
        }
    }
}
