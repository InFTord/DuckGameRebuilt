﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.SpikesLeft
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

namespace DuckGame
{
    [EditorGroup("Stuff|Spikes")]
    [BaggedProperty("previewPriority", false)]
    public class SpikesLeft : Spikes
    {
        private SpriteMap _sprite;

        public SpikesLeft(float xpos, float ypos)
          : base(xpos, ypos)
        {
            this._sprite = new SpriteMap("spikes", 16, 19)
            {
                speed = 0.1f
            };
            this.graphic = _sprite;
            this.center = new Vec2(8f, 14f);
            this.collisionOffset = new Vec2(-3f, -7f);
            this.collisionSize = new Vec2(5f, 13f);
            this._editorName = "Spikes Left";
            this.editorTooltip = "Pointy and dangerous.";
            this.physicsMaterial = PhysicsMaterial.Metal;
            this.editorCycleType = typeof(Spikes);
            this.angle = -1.570796f;
            this.up = false;
            this.editorOffset = new Vec2(6f, 0.0f);
            this.hugWalls = WallHug.Right;
            this._killImpact = ImpactedFrom.Left;
        }

        public override void Update() => base.Update();

        public override void Draw() => base.Draw();
    }
}
