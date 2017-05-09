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

        private static void _LevelLogic()
        {
            /* TODO: CanSpellBeUpgraded
            if (Functions.MyHero.Spellbook.CanSpellBeUpgraded(SpellSlot.R))
            {
                Functions.MyHero.Spellbook.LevelSpell(SpellSlot.R);
            } 

            var _FirstFocusSlot = _ConvertSlot(Menus.Misc.Get<MenuSlider>("AutoLevelFirstFocus").CurrentValue);
            var _SecondFocusSlot = _ConvertSlot(Menus.Misc.Get<MenuSlider>("AutoLevelSecondFocus").CurrentValue);
            var _ThirdFocusSlot = _ConvertSlot(Menus.Misc.Get<MenuSlider>("AutoLevelThirdFocus").CurrentValue);

            var _SecondSpell = Functions.MyHero.GetSpell(_SecondFocusSlot);
            var _ThirdSpell = Functions.MyHero.GetSpell(_ThirdFocusSlot);

            if (Functions.MyHero.Spellbook.CanSpellBeUpgraded(_FirstFocusSlot))
            {
                if (_SecondSpell.State == SpellState.NotLearned)
                {
                    Functions.MyHero.Spellbook.LevelSpell(_SecondFocusSlot);
                }

                if (_ThirdSpell.State == SpellState.NotLearned)
                {
                    Functions.MyHero.Spellbook.LevelSpell(_ThirdFocusSlot);
                }
                    
                Functions.MyHero.Spellbook.LevelSpell(_FirstFocusSlot);
            }

            if (Functions.MyHero.Spellbook.CanSpellBeUpgraded(_SecondFocusSlot))
            {
                if (_ThirdSpell.State == SpellState.NotLearned)
                {
                    Functions.MyHero.Spellbook.LevelSpell(_ThirdFocusSlot);
                }
                    
                Functions.MyHero.Spellbook.LevelSpell(_FirstFocusSlot);
                Functions.MyHero.Spellbook.LevelSpell(_SecondFocusSlot);
            }

            if (Functions.MyHero.Spellbook.CanSpellBeUpgraded(_ThirdFocusSlot))
            {
                Functions.MyHero.Spellbook.LevelSpell(_ThirdFocusSlot);
            }
            */
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
