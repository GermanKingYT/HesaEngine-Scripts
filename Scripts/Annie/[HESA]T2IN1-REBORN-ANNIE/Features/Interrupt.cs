using _HESA_T2IN1_REBORN_ANNIE.Managers;
using _HESA_T2IN1_REBORN_ANNIE.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;

namespace _HESA_T2IN1_REBORN_ANNIE.Features
{
    internal class Interrupt
    {
        public static void OnEnemyGapCloser(ActiveGapcloser gapcloser)
        {
            Logger.Log(gapcloser.Sender.IsMe ? "It's me?" : "It's him?");

            if (Menus.MiscMenu.Get<MenuCheckbox>("InterruptOnGapCloser").Checked && Globals.IsStunReady)
            {
                if (gapcloser.Target.IsValidTarget(625))
                {
                    if (SpellSlot.W.CanUseSpell())
                    {
                        Globals.DelayAction(() => SpellsManager.W.CastOnUnit(gapcloser.Target));
                    }
                    else if (SpellSlot.Q.CanUseSpell())
                    {
                        Globals.DelayAction(() => SpellsManager.Q.Cast(gapcloser.Target));
                    }
                }
            }
        }

        public static void Initialize()
        {
            AntiGapcloser.OnEnemyGapcloser += OnEnemyGapCloser;
        }
    }
}
