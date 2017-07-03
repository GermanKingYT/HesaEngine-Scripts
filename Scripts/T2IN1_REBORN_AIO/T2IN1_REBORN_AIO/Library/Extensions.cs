using System;
using System.Collections.Generic;

using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using SharpDX;

namespace T2IN1_REBORN_AIO.Library
{
    interface IPlugin
    {
        string Name { get; }

        bool Initialized { get; set; }

        Menu Menu { get; set; }

        void Initialize();

        void Unload();
    }

    internal static class Extensions
    {
        /* Plugins */
        public static List<IPlugin> PluginList { get; set; }

        /* Chat */
        public static void PrintMessage(string text, string hexColor) => Chat.Print("<font color='" + hexColor + "'>" + text + "</font>");
        public static void PrintMessage(string tag, string text, string hexColor) => Chat.Print("<font color='" + hexColor + "'>" + tag + "</font>" + text);

        /* Menu */
        public static void AddMenuCheckbox(this Menu menu, string name, string text, bool active) => menu.Add(new MenuCheckbox(name, text, active));
        public static void AddMenuSlider(this Menu menu, string name, string text, Slider slider) => menu.Add(new MenuSlider(name, text, slider));
        public static void AddMenuCombobox(this Menu menu, string name, string text, string[] list, int defaultValue) => menu.Add(new MenuCombo(name, text, list, defaultValue));
        public static bool GetCheckbox(this Menu menu, string name) => menu.Get<MenuCheckbox>(name).Checked;
        public static int GetSlider(this Menu menu, string name) => menu.Get<MenuSlider>(name).CurrentValue;
        public static int GetCombobox(this Menu menu, string name) => menu.Get<MenuCombo>(name).CurrentValue;
        public static void RemoveMenu(this Menu menu) => Menu.Remove(menu);
        //public static void RemoveMenuEntry(this Menu menu, string entry) => Menu.Remove(menu.Item(entry) as Menu);

        /* Checks */
        public static bool IsValidEntity(this Obj_AI_Base entity) => entity != null && entity.IsValid();
        public static bool IsValidEntity(this Obj_AI_Base entity, float range) => entity != null && entity.IsValidTarget(range);

        /* Drawings */
        public static void DrawLine(Vector3 pos1, Vector3 pos2, int bold, Color color)
        {
            Vector2 worldToScreen1 = Drawing.WorldToScreen(pos1);
            Vector2 worldToScreen2 = Drawing.WorldToScreen(pos2);
            Drawing.DrawLine(worldToScreen1, worldToScreen2, color, bold);
        }

        public static void DrawText(string msg, Vector3 Hero, Color color, int weight = 0)
        {
            Vector2 worldToScreen = Drawing.WorldToScreen(Hero);
            Drawing.DrawText(worldToScreen[0] - msg.Length * 5, worldToScreen[1] + weight, color, msg);
        }

        public static void DrawLineRectangle(Vector2 start, Vector2 end, int radius, int width, ColorBGRA color)
        {
            Vector2 dir = (end - start).Normalized();
            Vector2 pDir = dir.Perpendicular();

            Vector2 rightStartPos = start + pDir * radius;
            Vector2 leftStartPos = start - pDir * radius;
            Vector2 rightEndPos = end + pDir * radius;
            Vector2 leftEndPos = end - pDir * radius;

            Vector3 rStartPos = rightStartPos.To3D();
            Vector3 lStartPos = leftStartPos.To3D();
            Vector3 rEndPos = rightEndPos.To3D();
            Vector3 lEndPos = leftEndPos.To3D();

            Drawing.DrawLine(rStartPos, rEndPos, width, color);
            Drawing.DrawLine(lStartPos, lEndPos, width, color);
            Drawing.DrawLine(rStartPos, lStartPos, width, color);
            Drawing.DrawLine(lEndPos, rEndPos, width, color);
        }

        /* Prediction */
        public static void CastPrediction(this Spell spell, AIHeroClient target, int menuValue) => CastPrediction(spell, target, menuValue, 1);
        public static void CastPrediction(this Spell spell, AIHeroClient target, int menuValue, int hitCount)
        {
            if (!spell.IsReady() || !target.IsValidEntity(spell.Range)) return;

            /* TODO: MEH */
            if (!target.CanMove) 
            {
                spell.Cast(target.ServerPosition);
                return;
            }

            HitChance hitchance = HitChance.High;
            switch (menuValue)
            {
                case 0:
                    hitchance = HitChance.VeryHigh;
                    break;
                case 1:
                    hitchance = HitChance.High;
                    break;
                case 2:
                    hitchance = HitChance.Medium;
                    break;
                default:
                    throw new Exception("damm you fucked it up");
            }

            SkillshotType skillshotType = SkillshotType.SkillshotLine;
            bool isAoe = false;

            if (spell.Type == SkillshotType.SkillshotCircle) 
            {
                skillshotType = SkillshotType.SkillshotCircle;
                isAoe = true;
            }

            if (spell.Width > 80 && !spell.Collision) isAoe = true;

            PredictionInput predictionInput = new PredictionInput
            {
                Aoe = isAoe,
                Collision = spell.Collision,
                Speed = spell.Speed,
                Delay = spell.Delay,
                Range = spell.Range,
                From = ObjectManager.Me.ServerPosition,
                Radius = spell.Width,
                Unit = target,
                Type = skillshotType
            };

            PredictionOutput getPrediction = new Prediction().GetPrediction(predictionInput, true, true);
            if (isAoe && getPrediction.Hitchance >= hitchance && getPrediction.AoeTargetsHitCount >= hitCount) 
            {
                spell.Cast(getPrediction.CastPosition);
            }
            else if (getPrediction.Hitchance >= hitchance) 
            {
                spell.Cast(getPrediction.CastPosition);
            }
        }
    }
}
