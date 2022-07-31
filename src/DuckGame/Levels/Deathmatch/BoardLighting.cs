﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.BoardLighting
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

namespace DuckGame
{
    public class BoardLighting : Thing
    {
        private Sprite _lightRay;

        public BoardLighting(float xpos, float ypos)
          : base(xpos, ypos)
        {
            this._lightRay = new Sprite("rockThrow/lightRays");
            this.center = new Vec2(305f, 0f);
            this.graphic = this._lightRay;
        }

        public override void Draw()
        {
            if (RockWeather.lightOpacity < 0.01f || Layer.blurry)
                return;
            base.Draw();
        }
    }
}
