using System.Linq;

using _HESA_T2IN1_REBORN_ANNIE.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;

namespace _HESA_T2IN1_REBORN_ANNIE.Features
{
    internal class Activator
    {
        public static void Run()
        {
            /* Auto Ignite */
            if (Menus.ActivatorMenu.Get<MenuCheckbox>("AutoIgnite").Checked)
            {
                SpellSlot _Slot = Globals.MyHero.GetSpellSlotFromName(SummonerSpells.Ignite);
                if (_Slot != SpellSlot.Unknown)
                {
                    Spell _Spell = new Spell(_Slot, 600); _Spell.SetTargetted(0, int.MaxValue);
                    AIHeroClient _Enemy = ObjectManager.Heroes.Enemies.FirstOrDefault(e => Globals.MyHero.GetSummonerSpellDamage(e, Damage.SummonerSpell.Ignite) >= e.Health + 28);
                    if (_Enemy.IsValidTarget(600))
                    {
                        Globals.DelayAction(() => _Spell.Cast(_Enemy));
                    }
                }
            }

            /* Auto Health Pot */ /* TODO: DAMAGE PREDICTION */
            if (Menus.ActivatorMenu.Get<MenuCheckbox>("AutoUsePots").Checked)
            {
                bool _IsCondiction = Globals.MyHero.HasBuff("regenerationpotion")
                                    || Globals.MyHero.HasBuff("itemminiregenpotion")
                                    || Globals.MyHero.HasBuff("itemcrystalflask")
                                    || Globals.MyHero.HasBuff("itemdarkcrystalflask")
                                    || Globals.MyHero.HasBuff("itemcrystalflaskjungle")
                                    || Globals.MyHero.InFountain();

                if (!_IsCondiction)
                {
                    if (Item.CanUseItem(ItemId.Health_Potion))
                    {
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
    }
}
