﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.CustomBackground3
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

namespace DuckGame
{
    [EditorGroup("Background|custom", EditorItemType.Custom)]
    [BaggedProperty("isInDemo", false)]
    public class CustomBackground3 : CustomBackground
    {
        private static CustomType _customType = CustomType.Background;

        public static string customBackground03
        {
            get => Custom.data[CustomBackground3._customType][2];
            set
            {
                Custom.data[CustomBackground3._customType][2] = value;
                Custom.Clear(CustomType.Background, value);
            }
        }

        public CustomBackground3(float x, float y)
          : base(x, y)
        {
            this.customIndex = 2;
            this.graphic = (Sprite)new SpriteMap("arcadeBackground", 16, 16, true);
            this._opacityFromGraphic = true;
            this.center = new Vec2(8f, 8f);
            this.collisionSize = new Vec2(16f, 16f);
            this.collisionOffset = new Vec2(-8f, -8f);
            this._editorName = "03";
            this.UpdateCurrentTileset();
        }
    }
}