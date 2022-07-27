﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.ArcadeFrame
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

using Microsoft.Xna.Framework.Graphics;
using System;

namespace DuckGame
{
    [EditorGroup("Details|Arcade", EditorItemType.Arcade)]
    [BaggedProperty("isOnlineCapable", false)]
    public class ArcadeFrame : Thing
    {
        private SpriteMap _frame;
        public EditorProperty<int> style = new EditorProperty<int>(0, max: 5f, increment: 1f);
        public EditorProperty<float> respect = new EditorProperty<float>(0.0f, increment: 0.05f);
        public EditorProperty<string> requirement = new EditorProperty<string>("");
        private Sprite _image;
        private ChallengeSaveData _saveData;
        public Sprite _screen;
        public string _identifier;

        public ChallengeSaveData saveData
        {
            get => this._saveData;
            set
            {
                if (this._saveData != null && this._saveData != value)
                {
                    this._saveData.frameID = "";
                    this._saveData.frameImage = "";
                }
                this._saveData = value;
                if (this._saveData == null)
                    return;
                Texture2D texture = Editor.StringToTexture(this._saveData.frameImage);
                if (texture != null)
                    this._image = new Sprite((Tex2D)texture);
                this._saveData.frameID = this._identifier;
            }
        }

        public ArcadeFrame(float xpos, float ypos)
          : base(xpos, ypos)
        {
            this._frame = new SpriteMap("arcadeFrame01", 48, 48);
            this._frame.imageIndex = 0;
            this.graphic = (Sprite)this._frame;
            this.center = new Vec2((float)(this.graphic.width / 2), (float)(this.graphic.height / 2));
            this._collisionSize = new Vec2(16f, 16f);
            this._collisionOffset = new Vec2(-8f, -8f);
            this._screen = new Sprite("shot01");
            this.depth = - 0.9f;
        }

        public Vec2 GetRenderTargetSize()
        {
            if ((int)this.style == 0)
                return new Vec2(38f, 28f);
            if ((int)this.style == 1)
                return new Vec2(28f, 38f);
            if ((int)this.style == 2)
                return new Vec2(28f, 20f);
            if ((int)this.style == 3)
                return new Vec2(20f, 28f);
            if ((int)this.style == 4)
                return new Vec2(18f, 12f);
            return (int)this.style == 5 ? new Vec2(12f, 16f) : new Vec2(32f, 32f);
        }

        public float GetRenderTargetZoom() => 1f;

        public override void Initialize()
        {
            if (this._identifier == null)
                this._identifier = Guid.NewGuid().ToString();
            base.Initialize();
        }

        public override BinaryClassChunk Serialize()
        {
            BinaryClassChunk binaryClassChunk = base.Serialize();
            if (Editor.copying)
                binaryClassChunk.AddProperty("FrameID", (object)Guid.NewGuid().ToString());
            else
                binaryClassChunk.AddProperty("FrameID", (object)this._identifier);
            return binaryClassChunk;
        }

        public override bool Deserialize(BinaryClassChunk node)
        {
            this._identifier = node.GetProperty<string>("FrameID");
            return base.Deserialize(node);
        }

        public override DXMLNode LegacySerialize()
        {
            DXMLNode dxmlNode = base.LegacySerialize();
            if (Editor.copying)
                dxmlNode.Add(new DXMLNode("FrameID", (object)Guid.NewGuid().ToString()));
            else
                dxmlNode.Add(new DXMLNode("FrameID", (object)this._identifier));
            return dxmlNode;
        }

        public override bool LegacyDeserialize(DXMLNode node)
        {
            DXMLNode dxmlNode = node.Element("FrameID");
            if (dxmlNode != null)
                this._identifier = dxmlNode.Value;
            return base.LegacyDeserialize(node);
        }

        public override void Update()
        {
            this.visible = this.saveData != null && this._image != null;
            int num = this.visible ? 1 : 0;
            base.Update();
        }

        public override void Draw()
        {
            this._frame.frame = this.style.value;
            if (this._image != null)
            {
                Vec2 renderTargetSize = this.GetRenderTargetSize();
                this._image.depth = this.depth + 10;
                this._image.scale = new Vec2(0.1666667f);
                DuckGame.Graphics.doSnap = false;
                DuckGame.Graphics.Draw(this._image, this.x - renderTargetSize.x / 2f, this.y - renderTargetSize.y / 2f);
                DuckGame.Graphics.doSnap = true;
            }
            base.Draw();
        }
    }
}