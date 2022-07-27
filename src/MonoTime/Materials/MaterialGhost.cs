﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.MaterialGhost
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

using Microsoft.Xna.Framework.Graphics;

namespace DuckGame
{
    public class MaterialGhost : Material
    {
        public MaterialGhost() => this._effect = Content.Load<MTEffect>("Shaders/ghost");

        public override void Apply()
        {
            foreach (EffectPass pass in this._effect.effect.CurrentTechnique.Passes)
                pass.Apply();
        }
    }
}