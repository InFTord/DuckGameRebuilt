﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.Main
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

using System;
using System.Globalization;

namespace DuckGame
{
    public class Main : MonoMain
    {
        public static bool isDemo = false;
        public static DuckGameEditor editor;
        public static string lastLevel = "";
        public static string SpecialCode = "";
        public static string SpecialCode2 = "";
        public static int codeNumber = 0;
        private BitmapFont _font;
        public static ulong connectID = 0;
        public static bool foundPurchaseInfo = false;
        public static float price = 10f;
        public static string currencyType = "USD";
        public static bool stopForever = false;
        public static bool _gotHook = false;
        private bool didHash;
        public bool joinedLobby;

        public static string GetPriceString() => "|GREEN|" + Main.price.ToString("0.00", (IFormatProvider)CultureInfo.InvariantCulture) + " " + Main.currencyType + "|WHITE|";

        public static void SetPurchaseDetails(float p, string ct)
        {
            Main.price = p;
            Main.currencyType = ct;
            Main.foundPurchaseInfo = true;
        }

        public static void ResetMatchStuff()
        {
            DevConsole.Log(DCSection.General, "ResetMatchStuff()");
            DuckFile.BeginDataCommit();
            PurpleBlock.Reset();
            Highlights.ClearHighlights();
            Crowd.GoHome();
            GameMode.lastWinners.Clear();
            Deathmatch.levelsSinceRandom = 0;
            Deathmatch.levelsSinceCustom = 0;
            GameMode.numMatchesPlayed = 0;
            GameMode.showdown = false;
            RockWeather.Reset();
            Music.Reset();
            if (!Program.crashed)
            {
                foreach (Team team in Teams.all)
                {
                    team.prevScoreboardScore = team.score = 0;
                    if (team.activeProfiles.Count > 0)
                    {
                        foreach (Profile activeProfile in team.activeProfiles)
                        {
                            activeProfile.stats.lastPlayed = activeProfile.stats.lastPlayed = DateTime.Now;
                            activeProfile.RecordPreviousStats();
                            Profiles.Save(activeProfile);
                        }
                    }
                }
                if (Profiles.experienceProfile != null)
                    Profiles.Save(Profiles.experienceProfile);
                if (Profiles.all != null)
                {
                    foreach (Profile profile in Profiles.all)
                        profile?.RecordPreviousStats();
                }
                Global.Save();
                Options.Save();
            }
            Crowd.InitializeCrowd();
            DuckFile.EndDataCommit();
            DuckFile.FlagForBackup();
        }

        public static void ResetGameStuff()
        {
            if (Profiles.all == null)
                return;
            foreach (Profile profile in Profiles.all)
            {
                if (profile != null)
                    profile.wins = 0;
            }
        }

        protected override void OnStart()
        {
            Options.Initialize();
            Teams.PostInitialize();
            Unlocks.Initialize();
            ConnectionStatusUI.Initialize();
            Unlocks.CalculateTreeValues();
            Profiles.Initialize();
            Challenges.InitializeChallengeData();
            ProfilesCore.TryAutomerge();
            Dialogue.Initialize();
            DuckTitle.Initialize();
            News.Initialize();
            Script.Initialize();
            DuckNews.Initialize();
            VirtualBackground.InitializeBack();
            AmmoType.InitializeTypes();
            DestroyType.InitializeTypes();
            VirtualTransition.Initialize();
            Unlockables.Initialize();
            UIInviteMenu.Initialize();
            LevelGenerator.Initialize();
            DuckFile.InitializeMojis();
            Main.ResetMatchStuff();
            DuckFile._flaggedForBackup = false;
            foreach (Profile profile in Profiles.active)
                profile.RecordPreviousStats();
            Main.editor = new DuckGameEditor();
            Input.devicesChanged = false;
            TeamSelect2.ControllerLayoutsChanged();
            Main.SetPurchaseDetails(9.99f, "USD");
            if (Main.connectID != 0UL)
            {
                Main.SpecialCode = "Joining lobby on startup (" + Main.connectID.ToString() + ")";
                NCSteam.PrepareProfilesForJoin();
                NCSteam.inviteLobbyID = Main.connectID;
                Level.current = (Level)new JoinServer(Main.connectID, MonoMain.lobbyPassword);
            }
            else if (Level.current == null)
            {
                if (MonoMain.networkDebugger)
                {
                    Level.core.currentLevel = (Level)new NetworkDebugger(startLayer: Layer.core);
                    Layer.core = new LayerCore();
                    Layer.core.InitializeLayers();
                    Level.core.nextLevel = (Level)null;
                    Level.current.DoInitialize();
                    Level.core.currentLevel.lowestPoint = 100000f;
                }
                else
                    Level.current = !MonoMain.startInEditor ? (!MonoMain.noIntro ? (Level)new BIOSScreen() : (Level)new TitleScreen()) : (Level)Main.editor;
            }
            this._font = new BitmapFont("biosFont", 8);
            ModLoader.Start();
        }

        protected override void OnUpdate()
        {
            if (DevConsole.startupCommands.Count > 0)
            {
                DevConsole.RunCommand(DevConsole.startupCommands[0]);
                DevConsole.startupCommands.RemoveAt(0);
            }
            Main.isDemo = false;
            RockWeather.TickWeather();
            RandomLevelDownloader.Update();
            if (!NetworkDebugger.enabled)
                FireManager.Update();
            DamageManager.Update();
            if (!Network.isActive)
                NetRand.generator = Rando.generator;
            if (this.joinedLobby || !Program.testServer || Network.isActive || !Steam.lobbySearchComplete)
                return;
            if (Steam.lobbySearchResult != null)
            {
                Network.JoinServer("", 0, Steam.lobbySearchResult.id.ToString());
                this.joinedLobby = true;
            }
            else
            {
                User who = Steam.friends.Find((Predicate<User>)(x => x.name == "superjoebob"));
                if (who == null)
                    return;
                Steam.SearchForLobby(who);
            }
        }

        protected override void OnDraw()
        {
        }
    }
}