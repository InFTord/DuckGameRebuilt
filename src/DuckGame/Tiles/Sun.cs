﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.Sun
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

namespace DuckGame
{
    [EditorGroup("Details|Lights", EditorItemType.Lighting)]
    [BaggedProperty("isInDemo", true)]
    public class Sun : Thing
    {
        public Sun(float xpos, float ypos)
          : base(xpos, ypos)
        {
            this.graphic = new Sprite("officeLight");
            this.center = new Vec2(16f, 3f);
            this._collisionSize = new Vec2(30f, 6f);
            this._collisionOffset = new Vec2(-15f, -3f);
            this.depth = (Depth)0.9f;
            this.hugWalls = WallHug.Ceiling;
            this.layer = Layer.Game;
        }

        public override void Initialize()
        {
            if (Level.current is Editor)
                return;
            Level.Add((Thing)new SunLight(this.x, this.y - 1f, new Color((int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue), 100f));
            Level.Remove((Thing)this);
        }
    }
}