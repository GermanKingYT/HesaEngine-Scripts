using System.Linq;

using T2IN1_REBORN_ANNIE.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.Data;

namespace T2IN1_REBORN_ANNIE.Features
{
    internal static class AutoLeveler
    {
        public static void OnLevelUp(int level)
        {
            if (!Menus.MiscMenu.Get<MenuCheckbox>("AutoLevel").Checked) return;

            int delay = Menus.MiscMenu.Get<MenuSlider>("AutoLevelDelaySlider").CurrentValue;
            Core.DelayAction(() => LevelLogic(level), delay);
        }

        private static SpellSlot ConvertSlot(int value)
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

        private static bool CanLevel(this SpellSlot slot, int level)
        {
            if (Globals.MyHero.SpellTrainingPoints.Equals(0)) return false;

            int spellLevel = Globals.MyHero.GetSpell(slot).Level;
            int[] levelQ = { 1, 3, 5, 7, 9};
            int[] levelW = { 1, 3, 5, 7, 9 };
            int[] levelE = { 1, 3, 5, 7, 9 };
            int[] levelR = { 6, 11, 16 };

            switch (slot)
            {
                case SpellSlot.Q:
                    return spellLevel < levelQ.Count(x => level >= x);
                case SpellSlot.W:
                    return spellLevel < levelW.Count(x => level >= x);
                case SpellSlot.E:
                    return spellLevel < levelE.Count(x => level >= x);
                case SpellSlot.R:
                    return spellLevel < levelR.Count(x => level >= x);
                default:
                    return false;
            }
        }

        private static void LevelLogic(int level)
        {
            if (SpellSlot.R.CanLevel(level))
            {
                Globals.MyHero.Spellbook.LevelSpell(SpellSlot.R);
            }

            SpellSlot firstFocusSlot = ConvertSlot(Menus.MiscMenu.Get<MenuCombo>("AutoLevelFirstFocus").CurrentValue);
            SpellSlot secondFocusSlot = ConvertSlot(Menus.MiscMenu.Get<MenuCombo>("AutoLevelSecondFocus").CurrentValue);
            SpellSlot thirdFocusSlot = ConvertSlot(Menus.MiscMenu.Get<MenuCombo>("AutoLevelThirdFocus").CurrentValue);

            SpellDataInst secondSpell = Globals.MyHero.GetSpell(secondFocusSlot);
            SpellDataInst thirdSpell = Globals.MyHero.GetSpell(thirdFocusSlot);

            if (firstFocusSlot.CanLevel(level))
            {
                Globals.MyHero.Spellbook.LevelSpell(firstFocusSlot);

                if (secondSpell.State == SpellState.NotLearned)
                {
                    Globals.MyHero.Spellbook.LevelSpell(secondFocusSlot);
                }

                if (thirdSpell.State == SpellState.NotLearned)
                {
                    Globals.MyHero.Spellbook.LevelSpell(thirdFocusSlot);
                }
            }

            if (secondFocusSlot.CanLevel(level))
            {
                if (thirdSpell.State == SpellState.NotLearned)
                {
                    Globals.MyHero.Spellbook.LevelSpell(thirdFocusSlot);
                }

                Globals.MyHero.Spellbook.LevelSpell(firstFocusSlot);
                Globals.MyHero.Spellbook.LevelSpell(secondFocusSlot);
            }

            if (thirdFocusSlot.CanLevel(level))
            {
                Globals.MyHero.Spellbook.LevelSpell(thirdFocusSlot);
            }
        }
    }
}
