using System.Linq;

using T2IN1_REBORN_WUKONG.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;

namespace T2IN1_REBORN_WUKONG.Features
{
    internal class Activator
    {
        public static void Run()
        {
            /* Auto Ignite */
            if (Menus.ActivatorMenu.Get<MenuCheckbox>("AutoIgnite").Checked)
            {
                SpellSlot slot = Globals.MyHero.GetSpellSlotFromName(SummonerSpells.Ignite);

                if (slot != SpellSlot.Unknown)
                {
                    Spell spell = new Spell(slot, 600); spell.SetTargetted(0, int.MaxValue);

                    AIHeroClient enemy = ObjectManager.Heroes.Enemies.FirstOrDefault(e => Globals.MyHero.GetSummonerSpellDamage(e, Damage.SummonerSpell.Ignite) >= e.Health + 28);

                    if (enemy.IsValidTarget(600))
                    {
                        spell.Cast(enemy);
                    }
                }
            }

            /* Auto Health Pot */ /* TODO: DAMAGE PREDICTION */
            if (Menus.ActivatorMenu.Get<MenuCheckbox>("AutoUsePots").Checked)
            {
                bool isCondition = Globals.MyHero.HasBuff("regenerationpotion")
                                    || Globals.MyHero.HasBuff("itemminiregenpotion")
                                    || Globals.MyHero.HasBuff("itemcrystalflask")
                                    || Globals.MyHero.HasBuff("itemdarkcrystalflask")
                                    || Globals.MyHero.HasBuff("itemcrystalflaskjungle")
                                    || Globals.MyHero.InFountain();

                if (isCondition) return;

                if (!Item.CanUseItem(ItemId.Health_Potion)) return;

                if (Menus.ActivatorMenu.Get<MenuCheckbox>("AutoUsePotsOnBuff").Checked)
                {
                    if (Globals.MyHero.HasBuffOfType(BuffType.Damage) || Globals.MyHero.HasBuffOfType(BuffType.Poison) || Globals.MyHero.HealthPercent <= Menus.ActivatorMenu.Get<MenuSlider>("AutoUsePotsHealth").CurrentValue)
                    {
                        Item.UseItem(ItemId.Health_Potion);
                    }
                }
                else if (Globals.MyHero.HealthPercent <= Menus.ActivatorMenu.Get<MenuSlider>("AutoUsePotsHealth").CurrentValue)
                {
                    Item.UseItem(ItemId.Health_Potion);
                }
            }
        }
    }
}
