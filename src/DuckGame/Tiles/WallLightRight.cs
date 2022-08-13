﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.WallLightRight
//removed for regex reasons Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

using System.Collections.Generic;

namespace DuckGame
{
    [EditorGroup("Details|Arcade", EditorItemType.Lighting)]
    [BaggedProperty("isInDemo", true)]
    public class WallLightRight : Thing
    {
        private PointLight _light;
        private SpriteThing _shade;
        private List<LightOccluder> _occluders = new List<LightOccluder>();

        public WallLightRight(float xpos, float ypos)
          : base(xpos, ypos)
        {
            graphic = new Sprite("wallLight");
            center = new Vec2(8f, 8f);
            _collisionSize = new Vec2(5f, 16f);
            _collisionOffset = new Vec2(2f, -8f);
            depth = (Depth)0.9f;
            hugWalls = WallHug.Right;
            layer = Layer.Game;
        }

        public override void Initialize()
        {
            if (Level.current is Editor)
                return;
            _occluders.Add(new LightOccluder(topLeft, topRight + new Vec2(2f, 0f), new Color(1f, 0.8f, 0.8f)));
            _occluders.Add(new LightOccluder(bottomLeft, bottomRight + new Vec2(2f, 0f), new Color(1f, 0.8f, 0.8f)));
            _light = new PointLight(x + 5f, y, new Color((int)byte.MaxValue, (int)byte.MaxValue, 190), 100f, _occluders);
            Level.Add(_light);
            _shade = new SpriteThing(x, y, new Sprite("wallLight"))
            {
                center = center,
                layer = Layer.Foreground,
                flipHorizontal = true
            };
            Level.Add(_shade);
        }

        public override void Update()
        {
            _light.visible = visible;
            _shade.visible = visible;
            base.Update();
        }

        public override void Draw()
        {
            graphic.flipH = true;
            base.Draw();
        }
    }
}
