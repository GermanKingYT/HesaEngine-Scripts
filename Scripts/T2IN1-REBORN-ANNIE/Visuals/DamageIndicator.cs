using System;
using System.Linq;

using T2IN1_REBORN_LIB.Helpers;

using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using SharpDX;

namespace T2IN1_REBORN_ANNIE.Visuals
{
    internal class DamageIndicator
    {
        public static void Initialize()
        {
            Drawing.OnEndScene += Drawing_OnEndScene;

            Logger.Log(">> Executed", ConsoleColor.Green);
        }

        private static void Drawing_OnEndScene(EventArgs args)
        {
            if (!Menus.VisualsMenu.Get<MenuCheckbox>("DrawDamage").Checked || Globals.MyHero.IsDead) return;

            if (Globals.CachedEnemies == null || !Globals.CachedEnemies.Any()) return;

            Globals.CachedEnemies.Where(e => e.IsEnemy && !e.IsDead && e.IsValidTarget() && e.IsVisibleOnScreen).ToList().ForEach(x =>
            {
                double _Damage = Spells.TotalSpellDamage(x);

                /* TODO: Waiting for HPBarPosition 
                    var _DamagePercent = (_Enemy.TotalShieldHealth - _Damage > 0 ? _Enemy.TotalShieldHealth - _Damage : 0) / _Enemy.AllShield + _Enemy.Health;
                    var _CurrentHPPercent = _Enemy.TotalShieldHealth / _Enemy.AllShield + _Enemy.Health;
                    var _InitPoint = new Vector2((int)(_Enemy.HPBarPosition.X + _OffsetX + _DamagePercent * _Width), (int)_Enemy.HPBarPosition.Y + _OffsetY);
                    var _EndPoint = new Vector2((int)(_Enemy.HPBarPosition.X + _OffsetX + _CurrentHPPercent * _Width) + 1, (int)_Enemy.HPBarPosition.Y + _OffsetY);

                    Drawing.DrawLine(_InitPoint, _EndPoint, new ColorBGRA(255, 15, 255, 255), _Thickness);
                    */

                /* TODO: bugs sometimes, needs fixing */
                Vector2 screenPosition = Drawing.WorldToScreen(x.Position);

                Vector2 textPosition = new Vector2(screenPosition.X, screenPosition.Y);

                double estimatedDamage = Math.Ceiling((int)_Damage / x.Health * 100);

                string text = "Estimated Damage: NaN";

                ColorBGRA color = new ColorBGRA(152, 219, 52, 255);

                if (_Damage > 0) 
                {
                    if (estimatedDamage > 100) 
                    {
                        text = "Is Killable";
                        color = Color.Red;
                    }
                    else 
                    {
                        text = "Estimated Damage: " + string.Concat(estimatedDamage, "%");
                    }
                }

                Drawing.DrawText(textPosition, color, text);
            });
        }
    }
}
