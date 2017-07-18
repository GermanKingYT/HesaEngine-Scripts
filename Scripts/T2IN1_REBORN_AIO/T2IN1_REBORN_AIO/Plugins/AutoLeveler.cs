using System.Linq;

using T2IN1_REBORN_AIO.Library;

using HesaEngine.SDK;
using HesaEngine.SDK.Data;
using HesaEngine.SDK.Enums;

namespace T2IN1_REBORN_AIO.Plugins
{
    internal class AutoLeveler : IPlugin
    {
        public string Name => "Auto Leveler";
        public bool Initialized { get; set; }
        public Menu Menu { get; set; }

        public void Initialize()
        {
            if (Initialized) return;

            Menu = Globals.PluginMenu.AddSubMenu(Name);
            Menu.AddMenuCombobox("autoLevelFirstFocus", "1 Spell to Focus", new[] { "Q", "W", "E" }, 0);
            Menu.AddMenuCombobox("autoLevelSecondFocus", "2 Spell to Focus", new[] { "Q", "W", "E" }, 1);
            Menu.AddMenuCombobox("autoLevelThirdFocus", "3 Spell to Focus", new[] { "Q", "W", "E" }, 2);
            Menu.AddMenuSlider("autoLevelDelaySlider", "Delay Slider", new Slider(200, 150, 500));

            Game.OnUpdate += Game_OnUpdate;

            Library.Extensions.PrintMessage("[T2IN1-REBORN-AIO]", " Auto Leveler Plugin Loaded", "#27ae60");

            Initialized = true;
        }

        public void Unload()
        {
            if (!Initialized) return;

            Menu.RemoveMenu();

            Game.OnUpdate -= Game_OnUpdate;

            Library.Extensions.PrintMessage("[T2IN1-REBORN-AIO]", " Auto Leveler Plugin Unloaded", "#e74c3c");

            Initialized = false;
        }

        private int currentLevel = 0;
        private void Game_OnUpdate()
        {
            if (currentLevel == ObjectManager.Me.Level) return;

            currentLevel = ObjectManager.Me.Level;

            Core.DelayAction(() => LevelSpells(currentLevel), Menu.GetSlider("autoLevelDelaySlider"));
        }

        private SpellSlot ConvertSlot(int value)
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

            Library.Extensions.PrintMessage("[T2IN1-REBORN-AIO]", " Failed getting Slot", "#e74c3c");

            return SpellSlot.Unknown;
        }

        private bool CanLevel(SpellSlot slot, int level)
        {
            if (ObjectManager.Me.SpellTrainingPoints.Equals(0)) return false;

            int spellLevel = ObjectManager.Me.GetSpell(slot).Level;
            int[] levelQ = { 1, 3, 5, 7, 9 };
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

        private void LevelSpells(int level)
        {
            if (CanLevel(SpellSlot.R, level)) ObjectManager.Me.Spellbook.LevelSpell(SpellSlot.R);
            
            SpellSlot firstFocusSlot = ConvertSlot(Menu.GetCombobox("autoLevelFirstFocus"));
            SpellSlot secondFocusSlot = ConvertSlot(Menu.GetCombobox("autoLevelSecondFocus"));
            SpellSlot thirdFocusSlot = ConvertSlot(Menu.GetCombobox("autoLevelThirdFocus"));

            SpellDataInst secondSpell = ObjectManager.Me.GetSpell(secondFocusSlot);
            SpellDataInst thirdSpell = ObjectManager.Me.GetSpell(thirdFocusSlot);

            if (CanLevel(firstFocusSlot, level)) 
            {
                ObjectManager.Me.Spellbook.LevelSpell(firstFocusSlot);

                if (secondSpell.State == SpellState.NotLearned) ObjectManager.Me.Spellbook.LevelSpell(secondFocusSlot);
                if (thirdSpell.State == SpellState.NotLearned) ObjectManager.Me.Spellbook.LevelSpell(thirdFocusSlot);
            }

            if (CanLevel(secondFocusSlot, level)) 
            {
                if (thirdSpell.State == SpellState.NotLearned) ObjectManager.Me.Spellbook.LevelSpell(thirdFocusSlot);

                ObjectManager.Me.Spellbook.LevelSpell(firstFocusSlot);
                ObjectManager.Me.Spellbook.LevelSpell(secondFocusSlot);
            }

            if (CanLevel(thirdFocusSlot, level)) ObjectManager.Me.Spellbook.LevelSpell(thirdFocusSlot);
        }
    }
}
