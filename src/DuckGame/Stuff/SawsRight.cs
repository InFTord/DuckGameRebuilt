﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.SawsRight
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

namespace DuckGame
{
    [EditorGroup("Stuff|Spikes")]
    public class SawsRight : Saws
    {
        private SpriteMap _sprite;
        public new bool up = true;

        public SawsRight(float xpos, float ypos)
          : base(xpos, ypos)
        {
            this._sprite = new SpriteMap("movingSpikes", 16, 21)
            {
                speed = 0.3f
            };
            this.graphic = _sprite;
            this.center = new Vec2(8f, 14f);
            this.collisionOffset = new Vec2(-2f, -6f);
            this.collisionSize = new Vec2(4f, 12f);
            this._editorName = "Saws Right";
            this.editorTooltip = "Deadly hazards, able to cut through even the strongest of boots";
            this.physicsMaterial = PhysicsMaterial.Metal;
            this.editorCycleType = typeof(SawsDown);
            this.angle = 1.570796f;
            this.editorOffset = new Vec2(-6f, 0.0f);
            this.hugWalls = WallHug.Left;
            this._editorImageCenter = true;
            this.impactThreshold = 0.01f;
        }

        public override void Touch(MaterialThing with)
        {
            if (with.destroyed)
                return;
            with.Destroy(new DTImpale(this));
            with.hSpeed = 3f;
        }

        public override void Update() => base.Update();

        public override void Draw() => base.Draw();
    }
}
