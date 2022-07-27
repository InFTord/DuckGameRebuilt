﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.MaterialSunshine
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

using Microsoft.Xna.Framework.Graphics;

namespace DuckGame
{
    public class MaterialSunshine : Material
    {
        private RenderTarget2D _colorMap;

        public MaterialSunshine(RenderTarget2D col)
        {
            this._effect = Content.Load<MTEffect>("Shaders/sunshine");
            this._colorMap = col;
        }

        public override void Apply()
        {
            DuckGame.Graphics.device.Textures[1] = (Texture)(Texture2D)(Tex2D)this._colorMap;
            DuckGame.Graphics.device.SamplerStates[1] = SamplerState.PointClamp;
            DuckGame.Graphics.device.SamplerStates[0] = SamplerState.PointClamp;
            foreach (EffectPass pass in this._effect.effect.CurrentTechnique.Passes)
                pass.Apply();
        }
    }
}