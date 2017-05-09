using _HESA_T2IN1_REBORN.Champions.Annie.Managers;
using _HESA_T2IN1_REBORN.Champions.Annie.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;

namespace _HESA_T2IN1_REBORN.Champions.Annie.Features
{
    internal class Interrupt
    {
        public static void _OnEnemyGapCloser(ActiveGapcloser gapcloser)
        {
            Logger.Log(gapcloser.Sender.IsMe ? "It's me?" : "It's him?");

            if (Menus.MiscMenu.Get<MenuCheckbox>("InterruptOnGapCloser").Checked && Globals.IsStunReady())
            {
                if (Globals.IsObjectValidWithRange(gapcloser.Target, 625))
                {
                    if (Globals.CanUseSpell(SpellSlot.W))
                    {
                        Globals.DelayAction(() => SpellsManager.W.CastOnUnit(gapcloser.Target));
                    }
                    else if (Globals.CanUseSpell(SpellSlot.Q))
                    {
                        Globals.DelayAction(() => SpellsManager.Q.Cast(gapcloser.Target));
                    }
                }
            }
        }

        public static void Initialize()
        {
            AntiGapcloser.OnEnemyGapcloser += _OnEnemyGapCloser;
        }
    }
}
