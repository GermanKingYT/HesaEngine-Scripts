using System.Linq;

using _HESA_T2IN1_REBORN_ANNIE.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;

namespace _HESA_T2IN1_REBORN_ANNIE.Features
{
    internal static class AutoLevel
    {
        public static void OnLevelUp(AIHeroClient sender, int level)
        {
            if (Menus.MiscMenu.Get<MenuCheckbox>("AutoLevel").Checked && sender.IsMe)
            {
                var _Delay = Menus.MiscMenu.Get<MenuSlider>("AutoLevelDelaySlider").CurrentValue;
                Core.DelayAction(_LevelLogic, _Delay);
            }
        }


        private static SpellSlot _LastLearned;
        private static bool _CanSpellBeUpgraded(SpellSlot _Slot)
        {
            int _SpellLevel = Globals.MyHero.GetSpell(_Slot).Level;
            if (Globals.MyHeroSpellTrainingsPoints > 0)
            {
                SpellSlot[] _Upgradeable;
                switch (Globals.MyHeroLevel)
                {
                    case 1:
                        _Upgradeable = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E };
                        return _Upgradeable.Contains(_Slot);
                    case 2:
                        _Upgradeable = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E };
                        return _Upgradeable.Contains(_Slot);
                    case 3:
                        _Upgradeable = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E };
                        return _Upgradeable.Contains(_Slot);
                    case 4:
                        _Upgradeable = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E };
                        return _Upgradeable.Contains(_Slot);
                    case 5:
                        _Upgradeable = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E };
                        return _Upgradeable.Contains(_Slot);
                    case 6:
                        _Upgradeable = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.R };
                        return _Upgradeable.Contains(_Slot);
                    case 7:
                        _Upgradeable = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E };
                        return _Upgradeable.Contains(_Slot);
                    case 8:
                        _Upgradeable = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E };
                        return _Upgradeable.Contains(_Slot);
                    case 9:
                        _Upgradeable = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E };
                        return _Upgradeable.Contains(_Slot);
                    case 10:
                        _Upgradeable = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E };
                        return _Upgradeable.Contains(_Slot);
                    case 11:
                        _Upgradeable = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.R };
                        return _Upgradeable.Contains(_Slot);
                    case 12:
                        _Upgradeable = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E };
                        return _Upgradeable.Contains(_Slot);
                    case 13:
                        _Upgradeable = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E };
                        return _Upgradeable.Contains(_Slot);
                    case 14:
                        _Upgradeable = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E };
                        return _Upgradeable.Contains(_Slot);
                    case 15:
                        _Upgradeable = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E };
                        return _Upgradeable.Contains(_Slot);
                    case 16:
                        _Upgradeable = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.R };
                        return _Upgradeable.Contains(_Slot);
                    case 17:
                        _Upgradeable = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E };
                        return _Upgradeable.Contains(_Slot);
                    case 18:
                        _Upgradeable = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E };
                        return _Upgradeable.Contains(_Slot);
                }
            }
            return false;
        }

        private static void _LevelLogic()
        {
            if (_CanSpellBeUpgraded(SpellSlot.R))
            {
                Globals.MyHero.Spellbook.LevelSpell(SpellSlot.R);
            } 

            var _FirstFocusSlot = _ConvertSlot(Menus.MiscMenu.Get<MenuSlider>("AutoLevelFirstFocus").CurrentValue);
            var _SecondFocusSlot = _ConvertSlot(Menus.MiscMenu.Get<MenuSlider>("AutoLevelSecondFocus").CurrentValue);
            var _ThirdFocusSlot = _ConvertSlot(Menus.MiscMenu.Get<MenuSlider>("AutoLevelThirdFocus").CurrentValue);

            var _SecondSpell = Globals.MyHero.GetSpell(_SecondFocusSlot);
            var _ThirdSpell = Globals.MyHero.GetSpell(_ThirdFocusSlot);

            if (_CanSpellBeUpgraded(_FirstFocusSlot))
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

            if (_CanSpellBeUpgraded(_SecondFocusSlot))
            {
                if (_ThirdSpell.State == SpellState.NotLearned)
                {
                    Globals.MyHero.Spellbook.LevelSpell(_ThirdFocusSlot);
                }

                Globals.MyHero.Spellbook.LevelSpell(_FirstFocusSlot);
                Globals.MyHero.Spellbook.LevelSpell(_SecondFocusSlot);
            }

            if (_CanSpellBeUpgraded(_ThirdFocusSlot))
            {
                Globals.MyHero.Spellbook.LevelSpell(_ThirdFocusSlot);
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
    }
}
