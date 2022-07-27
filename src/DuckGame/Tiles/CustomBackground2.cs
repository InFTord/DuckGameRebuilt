﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.CustomBackground2
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

namespace DuckGame
{
    [EditorGroup("Background|custom", EditorItemType.Custom)]
    [BaggedProperty("isInDemo", false)]
    public class CustomBackground2 : CustomBackground
    {
        private static CustomType _customType = CustomType.Background;

        public static string customBackground02
        {
            get => Custom.data[CustomBackground2._customType][1];
            set
            {
                Custom.data[CustomBackground2._customType][1] = value;
                Custom.Clear(CustomType.Background, value);
            }
        }

        public CustomBackground2(float x, float y)
          : base(x, y)
        {
            this.customIndex = 1;
            this.graphic = new SpriteMap("arcadeBackground", 16, 16, true);
            this._opacityFromGraphic = true;
            this.center = new Vec2(8f, 8f);
            this.collisionSize = new Vec2(16f, 16f);
            this.collisionOffset = new Vec2(-8f, -8f);
            this._editorName = "02";
            this.UpdateCurrentTileset();
        }
    }
}
