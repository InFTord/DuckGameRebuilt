﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.IonCannon
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

using System.Collections.Generic;

namespace DuckGame
{
    public class IonCannon : Thing
    {
        public float _blast = 1f;
        private Vec2 _target;
        public bool serverVersion;

        public IonCannon(Vec2 pos, Vec2 target)
          : base(pos.x, pos.y)
        {
            this._target = target;
        }

        public override void Initialize()
        {
            Vec2 vec2_1 = this.position - this._target;
            vec2_1 = vec2_1.Rotate(Maths.DegToRad(-90f), Vec2.Zero);
            Vec2 normalized1 = vec2_1.normalized;
            Vec2 vec2_2 = this.position - this._target;
            vec2_2 = vec2_2.Rotate(Maths.DegToRad(90f), Vec2.Zero);
            Vec2 normalized2 = vec2_2.normalized;
            Level.Add((Thing)new LaserLine(this.position, this._target - this.position, normalized1, 4f, Color.White, 1f, 0.03f));
            Level.Add((Thing)new LaserLine(this.position, this._target - this.position, normalized2, 4f, Color.White, 1f, 0.03f));
            Level.Add((Thing)new LaserLine(this.position, this._target - this.position, normalized1, 2.5f, Color.White, 2f, 0.03f));
            Level.Add((Thing)new LaserLine(this.position, this._target - this.position, normalized2, 2.5f, Color.White, 2f, 0.03f));
            if (!this.serverVersion)
                return;
            float num1 = 64f;
            float num2 = 12f;
            float num3 = num1 / (num2 - 1f);
            Vec2 vec2_3 = this.position + normalized1 * num1 / 2f;
            HashSet<ushort> varBlocks = new HashSet<ushort>();
            List<BlockGroup> blockGroupList = new List<BlockGroup>();
            for (int index = 0; (double)index < (double)num2; ++index)
            {
                Vec2 vec2_4 = vec2_3 + normalized2 * num3 * (float)index;
                foreach (PhysicsObject physicsObject in Level.CheckLineAll<PhysicsObject>(vec2_4, vec2_4 + (this._target - this.position)))
                {
                    physicsObject.Destroy((DestroyType)new DTIncinerate((Thing)this));
                    physicsObject._sleeping = false;
                    physicsObject.vSpeed = -2f;
                }
                foreach (BlockGroup blockGroup1 in Level.CheckLineAll<BlockGroup>(vec2_4, vec2_4 + (this._target - this.position)))
                {
                    if (blockGroup1 != null)
                    {
                        BlockGroup blockGroup2 = blockGroup1;
                        List<Block> blockList = new List<Block>();
                        foreach (Block block in blockGroup2.blocks)
                        {
                            if (Collision.Line(vec2_4, vec2_4 + (this._target - this.position), block.rectangle))
                            {
                                block.shouldWreck = true;
                                if (block is AutoBlock)
                                    varBlocks.Add((block as AutoBlock).blockIndex);
                            }
                        }
                        blockGroupList.Add(blockGroup1);
                    }
                }
                foreach (Block block in Level.CheckLineAll<Block>(vec2_4, vec2_4 + (this._target - this.position)))
                {
                    switch (block)
                    {
                        case AutoBlock _:
                            block.skipWreck = true;
                            block.shouldWreck = true;
                            varBlocks.Add((block as AutoBlock).blockIndex);
                            continue;
                        case Door _:
                        case VerticalDoor _:
                            Level.Remove((Thing)block);
                            block.Destroy((DestroyType)new DTRocketExplosion((Thing)null));
                            continue;
                        default:
                            continue;
                    }
                }
            }
            foreach (BlockGroup blockGroup in blockGroupList)
                blockGroup.Wreck();
            if (Network.isActive && varBlocks.Count > 0)
                Send.Message((NetMessage)new NMDestroyBlocks(varBlocks));
            if (Recorder.currentRecording == null)
                return;
            Recorder.currentRecording.LogBonus();
        }

        public override void Update()
        {
            this._blast = Maths.CountDown(this._blast, 0.05f);
            if ((double)this._blast >= 0.0)
                return;
            Level.Remove((Thing)this);
        }

        public override void Draw()
        {
            double num1 = (double)Maths.NormalizeSection(this._blast, 0.0f, 0.2f);
            double num2 = (double)Maths.NormalizeSection(this._blast, 0.6f, 1f);
            double blast = (double)this._blast;
            Vec2 vec2_1 = this.position - this._target;
            vec2_1 = vec2_1.Rotate(Maths.DegToRad(-90f), Vec2.Zero);
            Vec2 normalized1 = vec2_1.normalized;
            Vec2 vec2_2 = this.position - this._target;
            vec2_2 = vec2_2.Rotate(Maths.DegToRad(90f), Vec2.Zero);
            Vec2 normalized2 = vec2_2.normalized;
            float num3 = 64f;
            float num4 = 7f;
            float num5 = num3 / (num4 - 1f);
            Vec2 vec2_3 = this.position + normalized1 * num3 / 2f;
            for (int index = 0; (double)index < (double)num4; ++index)
            {
                Vec2 p1 = vec2_3 + normalized2 * num5 * (float)index;
                Graphics.DrawLine(p1, p1 + (this._target - this.position), Color.SkyBlue * (this._blast * 0.9f), 2f, (Depth)0.9f);
            }
        }
    }
}