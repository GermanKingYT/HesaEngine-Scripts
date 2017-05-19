using System;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;

namespace _HESA_T2IN1_REBORN_ANNIE.Managers
{
    internal class SpellsManager
    {
        public static Spell Q, W, E, R;

        public static void Initialize()
        {
            Q = new Spell(SpellSlot.Q, 625, TargetSelector.DamageType.Magical);  
            W = new Spell(SpellSlot.W, 625, TargetSelector.DamageType.Magical);
            E = new Spell(SpellSlot.E, 0);
            R = new Spell(SpellSlot.R, 600, TargetSelector.DamageType.Magical);

            Q.SetTargetted(0.25f, 870f);
            W.SetSkillshot(0.6f, 1f, float.MaxValue, false, SkillshotType.SkillshotLine);
            R.SetSkillshot(0.5f, 200f, float.MaxValue, false, SkillshotType.SkillshotCircle);

            AIHeroClient.OnLevelUp += Features.AutoLeveler.OnLevelUp;

            Logger.Log(">> Executed", ConsoleColor.Green);
        }
    }
}
