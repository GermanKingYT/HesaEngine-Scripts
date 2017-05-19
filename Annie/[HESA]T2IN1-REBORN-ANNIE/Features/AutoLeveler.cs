using System.Linq;

using _HESA_T2IN1_REBORN_ANNIE.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;

namespace _HESA_T2IN1_REBORN_ANNIE.Features
{
    internal static class AutoLeveler
    {
        public static void OnLevelUp(AIHeroClient sender, int level)
        {
            if (Menus.MiscMenu.Get<MenuCheckbox>("AutoLevel").Checked && sender.IsMe)
            {
                var _Delay = Menus.MiscMenu.Get<MenuSlider>("AutoLevelDelaySlider").CurrentValue;
                Core.DelayAction(() => _LevelLogic(level), _Delay);
            }
        }

        private static SpellSlot _ConvertSlot(int value)
        {
            switch (value)
            {
                case 0:
                    return SpellSlot.Q;
                case 1:
                    return SpellSlot.W;
                case 2:
                    return SpellSlot.E;
            }
            Chat.Print("<font color='#e74c3c'>[T2IN1-REBORN]</font> Failed getting Slot");
            return SpellSlot.Unknown;
        }

        private static bool _CanLevel(this SpellSlot slot, int level)
        {
            if (Globals.MyHero.SpellTrainingPoints.Equals(0)) return false;

            var _SpellLevel = Globals.MyHero.GetSpell(slot).Level;
            var _LevelQ = new[] { 1, 3, 5, 7, 9};
            var _LevelW = new[] { 1, 3, 5, 7, 9 };
            var _LevelE = new[] { 1, 3, 5, 7, 9 };
            var _LevelR = new[] { 6, 11, 16 };

            switch (slot)
            {
                case SpellSlot.Q:
                {
                    return _SpellLevel < _LevelQ.Count(x => level >= x);
                }
                case SpellSlot.W:
                {
                    return _SpellLevel < _LevelW.Count(x => level >= x);
                }
                case SpellSlot.E:
                {
                    return _SpellLevel < _LevelE.Count(x => level >= x);
                }
                case SpellSlot.R:
                {
                    return _SpellLevel < _LevelR.Count(x => level >= x);
                }
                default:
                {
                    return false;
                }
            }
        }

        private static void _LevelLogic(int level)
        {
            if (SpellSlot.R._CanLevel(level))
            {
                Globals.MyHero.Spellbook.LevelSpell(SpellSlot.R);
            }

            var _FirstFocusSlot = _ConvertSlot(Menus.MiscMenu.Get<MenuSlider>("AutoLevelFirstFocus").CurrentValue);
            var _SecondFocusSlot = _ConvertSlot(Menus.MiscMenu.Get<MenuSlider>("AutoLevelSecondFocus").CurrentValue);
            var _ThirdFocusSlot = _ConvertSlot(Menus.MiscMenu.Get<MenuSlider>("AutoLevelThirdFocus").CurrentValue);

            var _SecondSpell = Globals.MyHero.GetSpell(_SecondFocusSlot);
            var _ThirdSpell = Globals.MyHero.GetSpell(_ThirdFocusSlot);

            if (_FirstFocusSlot._CanLevel(level))
            {
                Globals.MyHero.Spellbook.LevelSpell(_FirstFocusSlot);

                if (_SecondSpell.State == SpellState.NotLearned)
                {
                    Globals.MyHero.Spellbook.LevelSpell(_SecondFocusSlot);
                }

                if (_ThirdSpell.State == SpellState.NotLearned)
                {
                    Globals.MyHero.Spellbook.LevelSpell(_ThirdFocusSlot);
                }
            }

            if (_SecondFocusSlot._CanLevel(level))
            {
                if (_ThirdSpell.State == SpellState.NotLearned)
                {
                    Globals.MyHero.Spellbook.LevelSpell(_ThirdFocusSlot);
                }

                Globals.MyHero.Spellbook.LevelSpell(_FirstFocusSlot);
                Globals.MyHero.Spellbook.LevelSpell(_SecondFocusSlot);
            }

            if (_ThirdFocusSlot._CanLevel(level))
            {
                Globals.MyHero.Spellbook.LevelSpell(_ThirdFocusSlot);
            }
        }
    }
}
