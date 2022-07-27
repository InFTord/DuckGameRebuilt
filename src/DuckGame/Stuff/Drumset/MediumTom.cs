﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.MediumTom
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

namespace DuckGame
{
    public class MediumTom : Drum
    {
        private Sprite _stand;

        public MediumTom(float xpos, float ypos)
          : base(xpos, ypos)
        {
            this.graphic = new Sprite("drumset/mediumTom");
            this.center = new Vec2(this.graphic.w / 2, this.graphic.h / 2);
            this._stand = new Sprite("drumset/highTomStand");
            this._stand.center = new Vec2(this._stand.w / 2, 0.0f);
            this._sound = "medTom";
        }

        public override void Draw()
        {
            base.Draw();
            this._stand.depth = this.depth - 1;
            Graphics.Draw(this._stand, this.x + 7f, this.y);
        }
    }
}
