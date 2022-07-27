﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.LaserRebound
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

namespace DuckGame
{
    public class LaserRebound : Thing
    {
        private Tex2D _rebound = Content.Load<Tex2D>("laserRebound");

        public LaserRebound(float xpos, float ypos)
          : base(xpos, ypos)
        {
            this.graphic = new Sprite(this._rebound);
            this.depth = (Depth)0.9f;
            this.center = new Vec2(4f, 4f);
        }

        public override void Update()
        {
            this.alpha -= 0.07f;
            if ((double)this.alpha > 0.0)
                return;
            Level.Remove((Thing)this);
        }
    }
}