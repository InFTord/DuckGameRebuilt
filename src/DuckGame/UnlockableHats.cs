﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.UnlockableHats
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

using System;
using System.Collections.Generic;
using System.Linq;

namespace DuckGame
{
    public class UnlockableHats : Unlockable
    {
        private List<Team> _teams;
        private DuckPersona[] _persona = new DuckPersona[4];

        public UnlockableHats(
          string identifier,
          List<Team> t,
          Func<bool> condition,
          string nam,
          string desc,
          string achieve = "")
          : this(true, identifier, t, condition, nam, desc, achieve)
        {
        }

        public UnlockableHats(
          bool canHint,
          string identifier,
          List<Team> t,
          Func<bool> condition,
          string nam,
          string desc,
          string achieve = "")
          : base(identifier, condition, nam, desc, achieve)
        {
            this.allowHints = canHint;
            this._teams = t;
            this._showScreen = true;
            this._persona[0] = Persona.all.ElementAt<DuckPersona>(Rando.Int(3));
            this._persona[1] = Persona.all.ElementAt<DuckPersona>(Rando.Int(3));
            this._persona[2] = Persona.all.ElementAt<DuckPersona>(Rando.Int(3));
            this._persona[3] = Persona.all.ElementAt<DuckPersona>(Rando.Int(3));
        }

        public override void Initialize()
        {
            foreach (Team team in this._teams)
            {
                if (team != null)
                    team.locked = true;
            }
        }

        protected override void Unlock()
        {
            foreach (Team team in this._teams)
            {
                if (team != null)
                    team.locked = false;
            }
        }

        protected override void Lock()
        {
            foreach (Team team in this._teams)
            {
                if (team != null)
                    team.locked = true;
            }
        }

        public override void Draw(float x, float y, Depth depth)
        {
            y -= 9f;
            float num1 = 9f;
            if (this._teams.Count == 3)
                num1 = 18f;
            int index = 0;
            foreach (Team team in this._teams)
            {
                if (team != null && index < 8)
                {
                    float num2 = x;
                    float y1 = y + 12f;
                    this._persona[index].sprite.depth = depth;
                    this._persona[index].sprite.color = Color.White;
                    Graphics.Draw(this._persona[index].sprite, 0, num2 - num1 + (float)(index * 18), y1);
                    this._persona[index].armSprite.frame = this._persona[index].sprite.imageIndex;
                    this._persona[index].armSprite.scale = new Vec2(1f, 1f);
                    this._persona[index].armSprite.depth = depth + 4;
                    Graphics.Draw((Sprite)this._persona[index].armSprite, (float)((double)num2 - (double)num1 + (double)(index * 18) - 3.0), y1 + 6f);
                    Vec2 hatPoint = DuckRig.GetHatPoint(this._persona[index].sprite.imageIndex);
                    team.hat.depth = depth + 2;
                    team.hat.center = new Vec2(16f, 16f) + team.hatOffset;
                    Graphics.Draw(team.hat, team.hat.frame, num2 - num1 + (float)(index * 18) + hatPoint.x, y1 + hatPoint.y);
                }
                ++index;
            }
        }
    }
}