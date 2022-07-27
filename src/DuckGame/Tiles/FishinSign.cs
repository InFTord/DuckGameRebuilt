﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.FishinSign
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

namespace DuckGame
{
    [EditorGroup("Details|Signs")]
    public class FishinSign : Thing
    {
        private SpriteMap _sprite;

        public FishinSign(float xpos, float ypos)
          : base(xpos, ypos)
        {
            this._sprite = new SpriteMap("goneFishin", 32, 32);
            this.graphic = _sprite;
            this.center = new Vec2(16f, 16f);
            this._collisionSize = new Vec2(16f, 16f);
            this._collisionOffset = new Vec2(-8f, -9f);
            this.depth = - 0.5f;
            this._editorName = "Fishin Sign";
            this.editorTooltip = "It really explains itself, doesn't it?";
            this.hugWalls = WallHug.Floor;
        }

        public override void Draw()
        {
            this._sprite.frame = this.offDir > 0 ? 1 : 0;
            base.Draw();
        }
    }
}
