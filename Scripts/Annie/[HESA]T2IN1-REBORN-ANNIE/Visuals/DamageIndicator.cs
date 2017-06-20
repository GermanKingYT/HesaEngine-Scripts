using System;
using System.Linq;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using SharpDX;

namespace _HESA_T2IN1_REBORN_ANNIE.Visuals
{
    internal class DamageIndicator
    {
        public static void Initialize()
        {
            Drawing.OnEndScene += Drawing_OnEndScene;
            Logger.Log(">> Executed", ConsoleColor.Green);
        }

        private static double TotalDamage(Obj_AI_Base target)
        {
            SpellSlot[] _Slots = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.R };
            double _Damage = Globals.MyHero.Spellbook.Spells.Where(s => s.Slot.IsReady() && _Slots.Contains(s.Slot)).Sum(s => Globals.MyHero.GetSpellDamage(target, s.Slot));
            double _AutoAttackDamage = Orbwalker.CanAttack() ? Globals.MyHero.GetAutoAttackDamage(target) : 0f;
            return _Damage + _AutoAttackDamage;
        }

        private static void Drawing_OnEndScene(EventArgs args)
        {
            if (Menus.VisualsMenu.Get<MenuCheckbox>("DrawDamage").Checked && !Globals.MyHero.IsDead)
            {
                foreach (AIHeroClient _Enemy in ObjectManager.Heroes.Enemies.Where(e => e.IsEnemy && !e.IsDead && e.IsValidTarget() && e.IsVisibleOnScreen)) /* TODO: Maybe change back to "IsVisible" if there is a drawing delay */
                {
                    double _Damage = TotalDamage(_Enemy);

                    /* TODO: Waiting for HPBarPosition 
                    var _DamagePercent = (_Enemy.TotalShieldHealth - _Damage > 0 ? _Enemy.TotalShieldHealth - _Damage : 0) / _Enemy.AllShield + _Enemy.Health;
                    var _CurrentHPPercent = _Enemy.TotalShieldHealth / _Enemy.AllShield + _Enemy.Health;
                    var _InitPoint = new Vector2((int)(_Enemy.HPBarPosition.X + _OffsetX + _DamagePercent * _Width), (int)_Enemy.HPBarPosition.Y + _OffsetY);
                    var _EndPoint = new Vector2((int)(_Enemy.HPBarPosition.X + _OffsetX + _CurrentHPPercent * _Width) + 1, (int)_Enemy.HPBarPosition.Y + _OffsetY);

                    Drawing.DrawLine(_InitPoint, _EndPoint, new ColorBGRA(255, 15, 255, 255), _Thickness);
                    */

                    /* TODO: bugs sometimes, needs fixing */
                    Vector2 _ScreenPosition = Drawing.WorldToScreen(_Enemy.Position);
                    Vector2 _TextPosition = new Vector2(_ScreenPosition.X, _ScreenPosition.Y);
                    double _EstimatedDamage = Math.Ceiling((int) _Damage / _Enemy.Health * 100);
                    string _Text = "Estimated Damage: NaN";
                    ColorBGRA _Color = new ColorBGRA(152, 219, 52, 255);

                    if (_Damage > 0)
                    {
                        if (_EstimatedDamage > 100)
                        {
                            _Text = "Is Killable";
                            _Color = Color.Red;
                        }
                        else
                        {
                            _Text = "Estimated Damage: " + string.Concat(_EstimatedDamage, "%");
                        }
                    }

                    Drawing.DrawText(_TextPosition, _Color, _Text);
                }
            }
        }
    }
}
