using System;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;

namespace T2IN1_REBORN_WUKONG.Managers
{
    internal class SpellsManager
    {
        public static Spell Q, W, E, R;
        public static Item Hydra, Tiamat;

        public static void Initialize()
        {
            Q = new Spell(SpellSlot.Q, 300, TargetSelector.DamageType.Physical);  
            W = new Spell(SpellSlot.W, 300);
            E = new Spell(SpellSlot.E, 625, TargetSelector.DamageType.Physical);
            R = new Spell(SpellSlot.R, 315, TargetSelector.DamageType.Physical);

            Tiamat = new Item(ItemId.Tiamat_Melee_Only, 400);
            Hydra = new Item(ItemId.Ravenous_Hydra_Melee_Only, 400);

            Logger.Log(">> Executed", ConsoleColor.Green);
        }
    }
}
