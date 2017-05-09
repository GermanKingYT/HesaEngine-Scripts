using System.Linq;

using _HESA_T2IN1_REBORN_ANNIE.Managers;
using _HESA_T2IN1_REBORN_ANNIE.Visuals;

using HesaEngine.SDK;
using HesaEngine.SDK.Enums;

namespace _HESA_T2IN1_REBORN_ANNIE.Modes
{
    internal static class PermActive
    {
        public static void Initialize()
        {
            /* Tibbers Auto Control */
            Globals.IsTibbersSpawned = Globals.MyHero.GetSpell(SpellSlot.R).SpellData.Name.Equals("InfernalGuardianGuide");
            if (Globals.IsTibbersSpawned)
            {
                if (Menus.ComboMenu.Get<MenuCheckbox>("AutoControl").Checked)
                {
                    Modes.Tibbers.TibbersMethod();
                }
            }

            /* Auto Health Pot */ /* TODO: DAMAGE PREDICTION */
            if (Menus.ActivatorMenu.Get<MenuCheckbox>("AutoUsePots").Checked)
            {
                var _IsCondiction = Globals.MyHero.HasBuff("regenerationpotion")
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
                            if (Globals.MyHero.HasBuffOfType(BuffType.Damage) || Globals.MyHero.HasBuffOfType(BuffType.Poison))
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

            /* Auto Stack Passive */
            if (Menus.MiscMenu.Get<MenuCheckbox>("AutoStackPassive").Checked && !Globals.MyHero.IsRecalling())
            {
                if (!Globals.IsStunReady())
                {
                    var _MyMana = Globals.MyHero.ManaPercent;
                    if (Globals.MyHero.InFountain() && _MyMana > Menus.MiscMenu.Get<MenuSlider>("StackPassiveManaSpawn").CurrentValue)
                    {
                        if (Globals.CanUseSpell(SpellSlot.E))
                        {
                            Globals.DelayAction(() => SpellsManager.E.Cast());
                        }

                        if (Globals.CanUseSpell(SpellSlot.W))
                        {
                            Globals.DelayAction(() => SpellsManager.W.Cast());
                        }
                    }
                    else
                    {
                        if (_MyMana > Menus.MiscMenu.Get<MenuSlider>("StackPassiveMana").CurrentValue)
                        {
                            if (Globals.CanUseSpell(SpellSlot.E))
                            {
                                Globals.DelayAction(() => SpellsManager.E.Cast());
                            }
                        }
                    }
                }
            }

            /* Auto Ignite */
            if (Menus.MiscMenu.Get<MenuCheckbox>("AutoIgnite").Checked)
            {
                var _Slot = Globals.MyHero.GetSpellSlotFromName(SummonerSpells.Ignite);
                if (_Slot != SpellSlot.Unknown)
                {
                    var _Spell = new Spell(_Slot, 600); _Spell.SetTargetted(0, int.MaxValue);
                    var _Enemy = ObjectManager.Heroes.Enemies.FirstOrDefault(e => Globals.MyHero.GetSummonerSpellDamage(e, Damage.SummonerSpell.Ignite) >= e.Health + 28);
                    if (Globals.IsTargetValidWithRange(_Enemy, 600))
                    {
                        Globals.DelayAction(() => _Spell.Cast(_Enemy));
                    }
                }
            }

            /* Killsteal */
            if (Menus.MiscMenu.Get<MenuCheckbox>("KillSteal").Checked && Globals.Orb.ActiveMode != Orbwalker.OrbwalkingMode.Combo)
            {
                if (Globals.CanUseSpell(SpellSlot.Q) && Globals.CanUseSpell(SpellSlot.W))
                {
                    var _Slots = new[] { SpellSlot.Q, SpellSlot.W };
                    var _Target = ObjectManager.Heroes.Enemies.FirstOrDefault(e => e.IsValidTarget(625) && Globals.MyHero.GetComboDamage(e, _Slots) >= e.Health);
                    if (Globals.IsTargetValidWithRange(_Target, 625))
                    {
                        Globals.DelayAction(() => SpellsManager.Q.Cast(_Target));
                        Globals.DelayAction(() => SpellsManager.W.CastOnUnit(_Target));
                    }
                }
                else if (Globals.CanUseSpell(SpellSlot.Q))
                {
                    var _Target = ObjectManager.Heroes.Enemies.FirstOrDefault(e => e.IsValidTarget(SpellsManager.Q.Range) && Globals.MyHero.GetSpellDamage(e, SpellSlot.Q) >= e.Health);
                    if (Globals.IsTargetValidWithRange(_Target, SpellsManager.Q.Range))
                    {
                        Globals.DelayAction(() => SpellsManager.Q.Cast(_Target));
                    }
                }
                else if (Globals.CanUseSpell(SpellSlot.W))
                {
                    var _Target = ObjectManager.Heroes.Enemies.FirstOrDefault(e => e.IsValidTarget(SpellsManager.W.Range) && Globals.MyHero.GetSpellDamage(e, SpellSlot.W) >= e.Health);
                    if (Globals.IsTargetValidWithRange(_Target, SpellsManager.W.Range))
                    {
                        Globals.DelayAction(() => SpellsManager.W.CastOnUnit(_Target));
                    }
                }
            } 
        }
    }
}
