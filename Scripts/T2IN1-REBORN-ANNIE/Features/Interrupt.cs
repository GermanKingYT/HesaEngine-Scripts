using T2IN1_REBORN_ANNIE.Managers;
using T2IN1_REBORN_ANNIE.Visuals;

using HesaEngine.SDK;

namespace T2IN1_REBORN_ANNIE.Features
{
    internal class Interrupt
    {
        public static void OnEnemyGapCloser(ActiveGapcloser gapcloser)
        {
            Logger.Log(gapcloser.Sender.IsMe ? "It's me?" : "It's him?");

            if (Menus.MiscMenu.Get<MenuCheckbox>("InterruptOnGapCloser").Checked && Globals.IsPassiveReady)
            {
                if (gapcloser.Target.IsValidTarget(625))
                {
                    if (SpellsManager.W.IsUsable())
                    {
                        SpellsManager.W.CastOnUnit(gapcloser.Target);
                    }
                    else if (SpellsManager.Q.IsUsable())
                    {
                        SpellsManager.Q.Cast(gapcloser.Target);
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
