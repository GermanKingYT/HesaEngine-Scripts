using System;
using System.Linq;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using SharpDX;

namespace _HESA_T2IN1_REBORN.Champions.Annie.Visuals
{
    internal static class DamageIndicator
    {
        public static void Initialize()
        {
            Drawing.OnEndScene += _Drawing_OnEndScene;
            Logger.Log(">> Executed", ConsoleColor.Green);
        }

        private static double _TotalDamage(Obj_AI_Base target)
        {
            var _Slots = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.R };
            var _Damage = Globals.MyHero.Spellbook.Spells.Where(s => s.Slot.IsReady() && _Slots.Contains(s.Slot)).Sum(s => Globals.MyHero.GetSpellDamage(target, s.Slot));
            var _AutoAttackDamage = Orbwalker.CanAttack() ? Globals.MyHero.GetAutoAttackDamage(target) : 0f;
            return _Damage + _AutoAttackDamage;
        }

        private static void _Drawing_OnEndScene(EventArgs args)
        {
            if (Menus.VisualsMenu.Get<MenuCheckbox>("DrawDamage").Checked && !Globals.MyHero.IsDead)
            {
                foreach (var _Enemy in ObjectManager.Heroes.Enemies.Where(e => e.IsEnemy && !e.IsDead && Globals.IsTargetValid(e) && e.IsVisible))
                {
                    var _Damage = _TotalDamage(_Enemy);

                    /* TODO: Waiting for HPBarPosition 
                    var _DamagePercent = (_Enemy.TotalShieldHealth - _Damage > 0 ? _Enemy.TotalShieldHealth - _Damage : 0) / _Enemy.AllShield + _Enemy.Health;
                    var _CurrentHPPercent = _Enemy.TotalShieldHealth / _Enemy.AllShield + _Enemy.Health;
                    var _InitPoint = new Vector2((int)(_Enemy.HPBarPosition.X + _OffsetX + _DamagePercent * _Width), (int)_Enemy.HPBarPosition.Y + _OffsetY);
                    var _EndPoint = new Vector2((int)(_Enemy.HPBarPosition.X + _OffsetX + _CurrentHPPercent * _Width) + 1, (int)_Enemy.HPBarPosition.Y + _OffsetY);

                    Drawing.DrawLine(_InitPoint, _EndPoint, new ColorBGRA(255, 15, 255, 255), _Thickness);
                    */

                    /* TODO: bugs sometimes, needs fixing */
                    var _ScreenPosition = Drawing.WorldToScreen(_Enemy.Position);
                    var _TextPosition = new Vector2(_ScreenPosition.X, _ScreenPosition.Y);
                    var _EstimatedDamagePercent = string.Concat(Math.Ceiling((int)_Damage / _Enemy.Health * 100), "%");
                    var _Text = "Estimated Damage: NaN";
                    if (_Damage > 0) { _Text = "Estimated Damage: " + _EstimatedDamagePercent; }
                    Drawing.DrawText(_TextPosition, new ColorBGRA(152, 219, 52, 255), _Text);
                }
            }
        }
    }
}
