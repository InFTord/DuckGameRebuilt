﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.ProfileStats
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

using System;
using System.Collections.Generic;

namespace DuckGame
{
    public class ProfileStats : DataClass
    {
        private List<string> _hotnessStrings = new List<string>()
    {
      "Absolute Zero",
      "Icy Moon",
      "Antarctica",
      "Ice Cube",
      "Ice Cream",
      "Coffee",
      "Fire",
      "A Volcanic Eruption",
      "The Sun"
    };
        private Dictionary<string, int> _timesKilledBy = new Dictionary<string, int>();

        public float GetStatCalculation(StatInfo info)
        {
            switch (info)
            {
                case StatInfo.KillDeathRatio:
                    return this.kills / this.timesKilled;
                case StatInfo.Coolness:
                    return coolness;
                case StatInfo.ProfileScore:
                    return this.GetProfileScore();
                default:
                    return 0.0f;
            }
        }

        public int GetProfileScore() => (int)Math.Round((double)Maths.Clamp((float)((double)this.CalculateProfileScore() * 0.300000011920929 * 250.0), -50f, 200f));

        public string GetCoolnessString() => this._hotnessStrings[(int)Math.Floor((Maths.Clamp(this.GetProfileScore(), -50, 200) + 50) / 250.0 * 8.98999977111816)];

        public string currentTitle { get; set; }

        public int kills { get; set; }

        public int suicides { get; set; }

        public int timesKilled { get; set; }

        public int matchesWon { get; set; }

        public int trophiesSinceLastWinCounter { get; set; }

        public int trophiesSinceLastWin { get; set; }

        public int timesSpawned { get; set; }

        public int trophiesWon { get; set; }

        public int gamesPlayed { get; set; }

        public int fallDeaths { get; set; }

        public int timesSwore { get; set; }

        public int bulletsFired { get; set; }

        public int bulletsThatHit { get; set; }

        public int trickShots { get; set; }

        public void LogKill(Profile p)
        {
            string key = "";
            if (p != null)
                key = p.name;
            if (!this._timesKilledBy.ContainsKey(key))
                this._timesKilledBy[key] = 0;
            ++this._timesKilledBy[key];
        }

        public ProfileStats()
        {
            this.lastPlayed = DateTime.Now;
            this.lastWon = DateTime.MinValue;
            this.currentTitle = "";
            this._nodeName = "Stats";
        }

        public DateTime lastPlayed { get; set; }

        public DateTime lastWon { get; set; }

        public DateTime lastKillTime { get; set; }

        public int coolness { get; set; }

        public int unarmedDucksShot { get; set; }

        public int killsFromTheGrave { get; set; }

        public int timesNetted { get; set; }

        public float timeInNet { get; set; }

        public int GetFans()
        {
            if (this.loyalFans < 0)
                this.loyalFans = 0;
            if (this.unloyalFans < 0)
                this.unloyalFans = 0;
            return this.loyalFans + this.unloyalFans;
        }

        public bool TryFanTransfer(Profile to, float awesomeness, bool loyal)
        {
            if (this.unloyalFans > 0 && !loyal)
            {
                --this.unloyalFans;
                return true;
            }
            if (this.loyalFans > 0 && (double)Rando.Float(3f) < (double)awesomeness)
            {
                this.MakeFanUnloyal();
                if (loyal)
                    return true;
            }
            return false;
        }

        public void MakeFanLoyal()
        {
            --this.unloyalFans;
            ++this.loyalFans;
        }

        public void MakeFanUnloyal()
        {
            ++this.unloyalFans;
            --this.loyalFans;
        }

        public bool FanConsidersLeaving(float awfulness, bool loyal)
        {
            if (this.unloyalFans > 0 && !loyal)
            {
                --this.unloyalFans;
                return true;
            }
            if (this.loyalFans > 0 && (double)Rando.Float(3f) < (double)Math.Abs(awfulness))
            {
                this.MakeFanUnloyal();
                if (loyal)
                    return true;
            }
            return false;
        }

        public int loyalFans { get; set; }

        public int unloyalFans { get; set; }

        public float timeUnderMindControl { get; set; }

        public int timesMindControlled { get; set; }

        public float timeOnFire { get; set; }

        public int timesLitOnFire { get; set; }

        public float airTime { get; set; }

        public int timesJumped { get; set; }

        public int disarms { get; set; }

        public int timesDisarmed { get; set; }

        public int quacks { get; set; }

        public float timeWithMouthOpen { get; set; }

        public float timeSpentOnMines { get; set; }

        public int minesSteppedOn { get; set; }

        public float timeSpentReloadingOldTimeyWeapons { get; set; }

        public int presentsOpened { get; set; }

        public int respectGivenToDead { get; set; }

        public int funeralsPerformed { get; set; }

        public int funeralsRecieved { get; set; }

        public float timePreaching { get; set; }

        public int conversions { get; set; }

        public int timesConverted { get; set; }

        public float CalculateProfileScore(bool log = false)
        {
            List<StatContribution> statContributionList = new List<StatContribution>();
            float num1 = 0.0f;
            float num2 = 0.0f;
            float num3 = 0.0f;
            float num4 = 0.0f;
            if (this.timesSpawned > 0)
                num4 = (float)(matchesWon / (double)this.timesSpawned * 0.400000005960464);
            float num5 = num1 + num4;
            if ((double)num4 > 0.0)
                num3 += num4;
            else if ((double)num4 < 0.0)
                num2 += num4;
            statContributionList.Add(new StatContribution()
            {
                name = "MAT",
                amount = num4
            });
            if (this.gamesPlayed > 0)
                num4 = (float)(trophiesWon / (double)this.gamesPlayed * 0.400000005960464);
            float num6 = num5 + num4;
            if ((double)num4 > 0.0)
                num3 += num4;
            else if ((double)num4 < 0.0)
                num2 += num4;
            statContributionList.Add(new StatContribution()
            {
                name = "WON",
                amount = num4
            });
            int num7 = this.timesKilled;
            if (num7 < 1)
                num7 = 1;
            float num8 = (float)Math.Log(1.0 + kills / (double)num7) * 0.4f;
            float num9 = num6 + num8;
            if ((double)num8 > 0.0)
                num3 += num8;
            else if ((double)num8 < 0.0)
                num2 += num8;
            statContributionList.Add(new StatContribution()
            {
                name = "KDR",
                amount = num8
            });
            float num10 = (float)(Maths.Clamp((DateTime.Now - this.lastPlayed).Days, 0, 60) / 60.0 * 0.5);
            float num11 = num9 + num10;
            if ((double)num10 > 0.0)
                num3 += num10;
            else if ((double)num10 < 0.0)
                num2 += num10;
            statContributionList.Add(new StatContribution()
            {
                name = "LVE",
                amount = num10
            });
            float num12 = (float)Math.Log(1.0 + quacks * 9.99999974737875E-05) * 0.4f;
            float num13 = num11 + num12;
            if ((double)num12 > 0.0)
                num3 += num12;
            else if ((double)num12 < 0.0)
                num2 += num12;
            statContributionList.Add(new StatContribution()
            {
                name = "CHR",
                amount = num12
            });
            float num14 = (float)Math.Log(0.75 + coolness * 0.025000000372529);
            float num15 = num13 + num14;
            if ((double)num14 > 0.0)
                num3 += num14;
            else if ((double)num14 < 0.0)
                num2 += num14;
            statContributionList.Add(new StatContribution()
            {
                name = "COO",
                amount = num14
            });
            float num16 = (float)Math.Log(1.0 + bulletsFired * 9.99999974737875E-05);
            float num17 = num15 + num16;
            if ((double)num16 > 0.0)
                num3 += num16;
            else if ((double)num16 < 0.0)
                num2 += num16;
            statContributionList.Add(new StatContribution()
            {
                name = "SHT",
                amount = num16
            });
            if (this.bulletsFired > 0)
                num16 = (float)(bulletsThatHit / (double)this.bulletsFired * 0.200000002980232 - 0.100000001490116);
            float num18 = num17 + num16;
            if ((double)num16 > 0.0)
                num3 += num16;
            else if ((double)num16 < 0.0)
                num2 += num16;
            statContributionList.Add(new StatContribution()
            {
                name = "ACC",
                amount = num16
            });
            float num19 = (float)Math.Log(1.0 + disarms * 0.000500000023748726) * 0.5f;
            float num20 = num18 + num19;
            if ((double)num19 > 0.0)
                num3 += num19;
            else if ((double)num19 < 0.0)
                num2 += num19;
            statContributionList.Add(new StatContribution()
            {
                name = "DSM",
                amount = num19
            });
            float num21 = -(float)(Math.Log(1.0 + (this.timesLitOnFire + this.timesMindControlled + this.timesNetted + this.timesDisarmed + this.minesSteppedOn + this.fallDeaths) * 0.000500000023748726) * 0.5);
            float num22 = num20 + num21;
            if ((double)num21 > 0.0)
                num3 += num21;
            else if ((double)num21 < 0.0)
                num2 += num21;
            statContributionList.Add(new StatContribution()
            {
                name = "BAD",
                amount = num21
            });
            float num23 = (float)(-(Maths.Clamp((DateTime.Now - this.lastWon).Days, 0, 60) / 60.0) * 0.300000011920929);
            float num24 = num22 + num23;
            if ((double)num23 > 0.0)
                num3 += num23;
            else if ((double)num23 < 0.0)
                num2 += num23;
            statContributionList.Add(new StatContribution()
            {
                name = "LOS",
                amount = num23
            });
            float num25 = (float)Math.Log(1.0 + timesJumped * 9.99999974737875E-05) * 0.2f;
            float num26 = num24 + num25;
            if ((double)num25 > 0.0)
                num3 += num25;
            else if ((double)num25 < 0.0)
                num2 += num25;
            statContributionList.Add(new StatContribution()
            {
                name = "JMP",
                amount = num25
            });
            float num27 = (float)Math.Log(1.0 + (double)this.timeWithMouthOpen * (1.0 / 1000.0)) * 0.5f;
            float num28 = num26 + num27;
            if ((double)num27 > 0.0)
                num3 += num27;
            else if ((double)num27 < 0.0)
                num2 += num27;
            statContributionList.Add(new StatContribution()
            {
                name = "MTH",
                amount = num27
            });
            float num29 = (float)Math.Log(1.0 + timesSwore) * 0.5f;
            float profileScore = num28 + num29;
            if ((double)num29 > 0.0)
                num3 += num29;
            else if ((double)num29 < 0.0)
                num2 += num29;
            statContributionList.Add(new StatContribution()
            {
                name = "SWR",
                amount = num29
            });
            if (log && (double)profileScore != 0.0)
            {
                foreach (StatContribution statContribution in statContributionList)
                {
                    float num30 = 0.0f;
                    if (statContribution.amount != 0.0)
                        num30 = statContribution.amount <= 0.0 ? (float)((double)Math.Abs(statContribution.amount) / (double)Math.Abs(num2) * ((double)Math.Abs(num2) / ((double)num3 + (double)Math.Abs(num2)))) : (float)((double)Math.Abs(statContribution.amount) / (double)Math.Abs(num3) * ((double)num3 / ((double)num3 + (double)Math.Abs(num2))));
                    if (statContribution.amount < 0.0)
                        num30 = -num30;
                    float num31 = 0.5f;
                    float num32 = 0.5f;
                    float r;
                    float g;
                    if ((double)num30 < 0.0)
                    {
                        r = num31 + Math.Abs(num30) * 0.5f;
                        g = num32 - Math.Abs(num30) * 0.5f;
                    }
                    else
                    {
                        g = num32 + Math.Abs(num30) * 0.5f;
                        r = num31 - Math.Abs(num30) * 0.5f;
                    }
                    DevConsole.Log(statContribution.name + ": " + (num30 * 100f).ToString("0.000") + "%", new Color(r, g, 0.0f), 1f);
                }
            }
            return profileScore;
        }
    }
}
