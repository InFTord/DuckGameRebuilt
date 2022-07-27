﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.Layer
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DuckGame
{
    public class Layer : DrawList
    {
        public bool enableCulling;
        public static bool lightingTwoPointOh = false;
        private static bool _lighting = false;
        private static LayerCore _core = new LayerCore();
        private static Layer _preDrawLayer = new Layer("PREDRAW");
        protected MTSpriteBatch _batch;
        private string _name;
        private int _depth;
        private Vec2 _depthSpan;
        private Effect _effect;
        private bool _visible = true;
        private bool _blurEffect;
        private bool _perspective;
        private BlendState _blend = BlendState.AlphaBlend;
        private BlendState _targetBlend = BlendState.AlphaBlend;
        private Color _targetClearColor = new Color(0, 0, 0, 0);
        private DepthStencilState _targetDepthStencil = DepthStencilState.Default;
        private RenderTarget2D _slaveTarget;
        private float _targetFade = 1f;
        protected Rectangle _scissor;
        protected float _fade = 1f;
        protected float _fadeAdd;
        protected Vec3 _colorAdd = Vec3.Zero;
        protected Vec3 _colorMul = Vec3.One;
        protected float _darken;
        public Camera _tallCamera;
        protected Camera _camera;
        protected RasterizerState _state;
        private Sprite _dropShadow = new Sprite("dropShadow");
        public RenderTarget2D _target;
        private Layer _shareDrawObjects;
        private bool _targetOnly;
        public bool aspectReliesOnGameLayer;
        private bool _allowTallAspect;
        public float flashAddInfluence;
        public float flashAddClearInfluence;
        private Viewport _oldViewport;
        private RenderTarget2D _oldRenderTarget;
        private Camera _targetCamera = new Camera();
        public static Vec3 kGameLayerFade;
        public static Vec3 kGameLayerAdd;
        public static bool blurry = false;
        public static bool ignoreTransparent = false;
        public static bool skipDrawing = false;
        public float currentSpanOffset;

        public static bool lighting
        {
            get => Options.Data.lighting && Layer._lighting && !(Level.current is Editor);
            set => Layer._lighting = value;
        }

        public static LayerCore core
        {
            get => Layer._core;
            set => Layer._core = value;
        }

        public static Layer PreDrawLayer => Layer._preDrawLayer;

        public static Layer Parallax => Layer._core._parallax;

        public static Layer Virtual => Layer._core._virtual;

        public static Layer Background => Layer._core._background;

        public static Layer Game => Layer._core._game;

        public static Layer Blocks => Layer._core._blocks;

        public static Layer Glow => Layer._core._glow;

        public static Layer Lighting => Layer._core._lighting;

        public static Layer Foreground => Layer._core._foreground;

        public static Layer HUD
        {
            get => Layer._core._hud;
            set => Layer._core._hud = value;
        }

        public static Layer Console => Layer._core._console;

        public static bool doVirtualEffect
        {
            get => Layer._core.doVirtualEffect;
            set => Layer._core.doVirtualEffect = value;
        }

        public static MTEffect basicWireframeEffect => Layer._core.basicWireframeEffect;

        public static bool basicWireframeTex
        {
            get => Layer._core.basicWireframeTex;
            set => Layer._core.basicWireframeTex = value;
        }

        public static MTEffect itemSpawnEffect => Layer._core._itemSpawnEffect;

        public static bool allVisible
        {
            set => Layer._core.allVisible = value;
        }

        public static MTEffect basicLayerEffect => Layer._core._basicEffectFadeAdd;

        public static bool IsBasicLayerEffect(MTEffect e) => Layer._core.IsBasicLayerEffect(e);

        public static void InitializeLayers() => Layer._core.InitializeLayers();

        public static void ClearLayers() => Layer._core.ClearLayers();

        public static void DrawLayers() => Layer._core.DrawLayers();

        public static void DrawTargetLayers() => Layer._core.DrawTargetLayers();

        public static void UpdateLayers() => Layer._core.UpdateLayers();

        public static void ResetLayers() => Layer._core.ResetLayers();

        public static Layer Get(string layer) => Layer._core.Get(layer);

        public static void Add(Layer l) => Layer._core.Add(l);

        public static void Remove(Layer l) => Layer._core.Remove(l);

        public static bool Contains(Layer l) => Layer._core.Contains(l);

        public Matrix fullMatrix => this._batch.fullMatrix;

        public string name => this._name;

        public int depth
        {
            get => this._depth;
            set => this._depth = value;
        }

        public Vec2 depthSpan
        {
            get => this._depthSpan;
            set => this._depthSpan = value;
        }

        public Effect effect
        {
            get => this._effect;
            set => this._effect = value;
        }

        public bool visible
        {
            get => this._visible;
            set => this._visible = value;
        }

        public bool blurEffect
        {
            get => this._blurEffect;
            set => this._blurEffect = value;
        }

        public float barSize => (float)(((double)this.camera.width * (double)DuckGame.Graphics.aspect - (double)this.camera.width * (9.0 / 16.0)) / 2.0);

        public Matrix projection { get; set; }

        public Matrix view { get; set; }

        public bool perspective
        {
            get => this._perspective;
            set => this._perspective = value;
        }

        public BlendState blend
        {
            get => this._blend;
            set => this._blend = value;
        }

        public BlendState targetBlend
        {
            get => this._targetBlend;
            set => this._targetBlend = value;
        }

        public Color targetClearColor
        {
            get => this._targetClearColor;
            set => this._targetClearColor = value;
        }

        public DepthStencilState targetDepthStencil
        {
            get => this._targetDepthStencil;
            set => this._targetDepthStencil = value;
        }

        public RenderTarget2D slaveTarget
        {
            get => this._slaveTarget;
            set => this._slaveTarget = value;
        }

        public float targetFade
        {
            get => this._targetFade;
            set => this._targetFade = value;
        }

        public Rectangle scissor
        {
            get => this._scissor;
            set
            {
                if ((double)this._scissor.width == 0.0 && (double)value.width != 0.0)
                {
                    this._state = new RasterizerState();
                    this._state.CullMode = CullMode.None;
                    this._state.ScissorTestEnable = true;
                }
                this._scissor = value;
            }
        }

        public void ClearScissor()
        {
            if ((double)this._scissor.width == 0.0)
                return;
            this._scissor = new Rectangle(0.0f, 0.0f, 0.0f, 0.0f);
            this._state = new RasterizerState();
            this._state.CullMode = CullMode.None;
        }

        public float fade
        {
            get => this._fade;
            set => this._fade = value;
        }

        public float fadeAdd
        {
            get => this._fadeAdd;
            set => this._fadeAdd = value;
        }

        public Vec3 colorAdd
        {
            get => this._colorAdd;
            set => this._colorAdd = value;
        }

        public Vec3 colorMul
        {
            get => this._colorMul;
            set => this._colorMul = value;
        }

        public float darken
        {
            get => this._darken;
            set => this._darken = value;
        }

        public Camera camera
        {
            get => this._camera == null && Level.activeLevel != null ? Level.activeLevel.camera : this._camera;
            set => this._camera = value;
        }

        public float width => this.camera.width;

        public float height => this.camera.height;

        public RenderTarget2D target => this._slaveTarget != null ? this._slaveTarget : this._target;

        public Layer shareDrawObjects
        {
            get => this._shareDrawObjects;
            set => this._shareDrawObjects = value;
        }

        public bool targetOnly
        {
            get => this._targetOnly;
            set => this._targetOnly = value;
        }

        public bool allowTallAspect
        {
            get => this._allowTallAspect && !this.camera.sixteenNine;
            set => this._allowTallAspect = value;
        }

        public bool isTargetLayer => this.target != null;

        public Layer(string nameval, int depthval = 0, Camera cam = null, bool targetLayer = false, Vec2 targetSize = default(Vec2))
        {
            this._name = nameval;
            this._depth = depthval;
            this._batch = new MTSpriteBatch(DuckGame.Graphics.device);
            this._state = new RasterizerState();
            this._state.CullMode = CullMode.None;
            this._camera = cam;
            this._dropShadow.CenterOrigin();
            this._dropShadow.alpha = 0.5f;
            if (!targetLayer)
                return;
            if (targetSize == new Vec2())
                this._target = new RenderTarget2D(DuckGame.Graphics.width, DuckGame.Graphics.height);
            else
                this._target = new RenderTarget2D((int)targetSize.x, (int)targetSize.y);
        }

        public virtual void Update()
        {
            foreach (Thing thing in this._transparentRemove)
                this._transparent.Remove(thing);
            foreach (Thing thing in this._opaqueRemove)
                this._opaque.Remove(thing);
            this._transparentRemove.Clear();
            this._opaqueRemove.Clear();
        }

        public virtual void Begin(bool transparent, bool isTargetDraw = false)
        {
            int num1 = this.name == "LIGHTING" ? 1 : 0;
            if (this.aspectReliesOnGameLayer && this.camera != Layer.Game.camera)
            {
                this.camera.width = 320f;
                this.camera.height = 320f / Layer.Game.camera.aspect;
            }
            if (this.allowTallAspect)
                DuckGame.Graphics.SetFullViewport();
            try
            {
                if (isTargetDraw & transparent && this._target != null)
                {
                    this._oldRenderTarget = DuckGame.Graphics.GetRenderTarget();
                    this._oldViewport = DuckGame.Graphics.viewport;
                    DuckGame.Graphics.SetRenderTarget(this._target);
                    if ((double)this.flashAddClearInfluence > 0.0)
                        DuckGame.Graphics.Clear(new Color((byte)Math.Min((float)this._targetClearColor.r + (float)((double)this.flashAddClearInfluence * (double)DuckGame.Graphics.flashAddRenderValue * (double)byte.MaxValue), (float)byte.MaxValue), (byte)Math.Min((float)this._targetClearColor.g + (float)((double)this.flashAddClearInfluence * (double)DuckGame.Graphics.flashAddRenderValue * (double)byte.MaxValue), (float)byte.MaxValue), (byte)Math.Min((float)this._targetClearColor.b + (float)((double)this.flashAddClearInfluence * (double)DuckGame.Graphics.flashAddRenderValue * (double)byte.MaxValue), (float)byte.MaxValue), this._targetClearColor.a));
                    else
                        DuckGame.Graphics.Clear(this._targetClearColor);
                }
                if (!isTargetDraw)
                {
                    if (DuckGame.Graphics.currentRenderTarget != null)
                    {
                        if (!DuckGame.Graphics.currentRenderTarget.depth)
                            goto label_14;
                    }
                    DuckGame.Graphics.device.Clear(ClearOptions.DepthBuffer, (Microsoft.Xna.Framework.Color)Color.Black, 1f, 0);
                }
            }
            catch (Exception ex)
            {
                DevConsole.Log("|DGRED|Layer.Begin exception: " + ex.Message);
            }
        label_14:
            DuckGame.Graphics.ResetSpanAdjust();
            Effect effect = (Effect)Layer._core._basicEffect;
            Vec3 vec3_1 = new Vec3((float)((double)DuckGame.Graphics.fade * (double)this._fade * (1.0 - (double)this._darken))) * this.colorMul;
            Vec3 vec3_2 = this._colorAdd + new Vec3(this._fadeAdd) + new Vec3(DuckGame.Graphics.flashAddRenderValue) * this.flashAddInfluence + new Vec3(DuckGame.Graphics.fadeAddRenderValue) - new Vec3(this.darken);
            vec3_2 = new Vec3(Maths.Clamp(vec3_2.x, -1f, 1f), Maths.Clamp(vec3_2.y, -1f, 1f), Maths.Clamp(vec3_2.z, -1f, 1f));
            vec3_2 *= vec3_1;
            if (this == Layer.Game)
            {
                Layer.kGameLayerFade = vec3_1;
                Layer.kGameLayerAdd = vec3_2;
            }
            if ((double)this._darken > 0.0)
                this._darken -= 0.15f;
            else if ((double)this._darken < 0.0)
                this._darken += 0.15f;
            if ((double)Math.Abs(this._darken) < 0.159999996423721)
                this._darken = 0.0f;
            if (this._effect != null)
            {
                effect = this._effect;
                effect.Parameters["fade"]?.SetValue((Vector3)vec3_1);
                effect.Parameters["add"]?.SetValue((Vector3)vec3_2);
            }
            else
            {
                float num2 = vec3_2.LengthSquared();
                if (vec3_1 != Vec3.One && (double)num2 > 1.0 / 1000.0)
                {
                    effect = (Effect)Layer._core._basicEffectFadeAdd;
                    effect.Parameters["fade"].SetValue((Vector3)vec3_1);
                    effect.Parameters["add"].SetValue((Vector3)vec3_2);
                }
                else if (vec3_1 != Vec3.One)
                {
                    effect = (Effect)Layer._core._basicEffectFade;
                    effect.Parameters["fade"].SetValue((Vector3)vec3_1);
                }
                else if ((double)num2 > 1.0 / 1000.0)
                {
                    effect = (Effect)Layer._core._basicEffectAdd;
                    effect.Parameters["add"].SetValue((Vector3)vec3_2);
                }
                if (Layer.doVirtualEffect && (Layer.Game == this || Layer.Foreground == this || Layer.Blocks == this || Layer.Background == this))
                    effect = !Layer.basicWireframeTex ? (Effect)Layer._core._basicWireframeEffect : (Effect)Layer._core._basicWireframeEffectTex;
            }
            if (this._state.ScissorTestEnable)
                DuckGame.Graphics.SetScissorRectangle(this._scissor);
            DuckGame.Graphics.screen = this._batch;
            Camera camera = this.camera;
            if (this.target != null & isTargetDraw && !this.targetOnly)
            {
                this._targetCamera.x = (float)Math.Round((double)this.camera.x - 1.0);
                this._targetCamera.y = (float)Math.Round((double)this.camera.y - 1.0);
                this._targetCamera.width = Math.Max(this.camera.width, (float)DuckGame.Graphics.width);
                this._targetCamera.height = Math.Max(this.camera.height, (float)DuckGame.Graphics.height);
                camera = this._targetCamera;
            }
            BlendState blendState = this._blend;
            if (isTargetDraw)
                blendState = this._targetBlend;
            if (this.target != null & isTargetDraw)
            {
                Vec2 position1 = camera.position;
                position1.x = (float)Math.Floor((double)position1.x);
                position1.y = (float)Math.Floor((double)position1.y);
                Vec2 size1 = camera.size;
                size1.x = (float)Math.Floor((double)size1.x);
                size1.y = (float)Math.Floor((double)size1.y);
                Vec2 position2 = camera.position;
                Vec2 size2 = camera.size;
                this._batch.Begin(SpriteSortMode.BackToFront, blendState, SamplerState.PointClamp, this._targetDepthStencil, this._state, (MTEffect)effect, camera.getMatrix());
                camera.position = position2;
                camera.size = size2;
            }
            else if (Layer.blurry || this._blurEffect)
            {
                if (!transparent)
                    this._batch.Begin(SpriteSortMode.FrontToBack, blendState, SamplerState.LinearClamp, DepthStencilState.Default, this._state, (MTEffect)effect, camera.getMatrix());
                else
                    this._batch.Begin(SpriteSortMode.BackToFront, blendState, SamplerState.LinearClamp, DepthStencilState.DepthRead, this._state, (MTEffect)effect, camera.getMatrix());
            }
            else if (!transparent)
                this._batch.Begin(SpriteSortMode.FrontToBack, blendState, SamplerState.PointClamp, DepthStencilState.Default, this._state, (MTEffect)effect, camera.getMatrix());
            else
                this._batch.Begin(SpriteSortMode.BackToFront, blendState, SamplerState.PointClamp, DepthStencilState.DepthRead, this._state, (MTEffect)effect, camera.getMatrix());
        }

        public void End(bool transparent, bool isTargetDraw = false)
        {
            this._batch.End();
            DuckGame.Graphics.screen = (MTSpriteBatch)null;
            DuckGame.Graphics.currentLayer = (Layer)null;
            if (isTargetDraw & transparent && this._target != null)
            {
                DuckGame.Graphics.SetRenderTarget(this._oldRenderTarget);
                DuckGame.Graphics.viewport = this._oldViewport;
            }
            if (!this.allowTallAspect)
                return;
            DuckGame.Graphics.RestoreOldViewport();
        }

        public virtual void Draw(bool transparent, bool isTargetDraw = false)
        {
            if ((double)this.currentSpanOffset > 10000.0)
                this.currentSpanOffset = 0.0f;
            if (!transparent && Layer.ignoreTransparent || isTargetDraw && this.slaveTarget != null || this.target != null && !isTargetDraw && this.targetOnly)
                return;
            if (Network.isActive && this == Layer.Game)
                DuckGame.Graphics.currentFrameCalls = new List<DrawCall>();
            Level.activeLevel.InitializeDraw(this);
            DuckGame.Graphics.currentLayer = this;
            this.Begin(transparent, isTargetDraw);
            if (this.target != null && !isTargetDraw)
            {
                Vec2 position = Level.activeLevel.camera.position - new Vec2(1f, 1f);
                position.x = (float)Math.Round((double)position.x);
                position.y = (float)Math.Round((double)position.y);
                Color color = new Color(1f * this._targetFade, 1f * this._targetFade, 1f * this._targetFade, 1f);
                Vec2 vec2 = new Vec2(Math.Max(this.camera.width, (float)DuckGame.Graphics.width), Math.Max(this.camera.height, (float)DuckGame.Graphics.height));
                DuckGame.Graphics.skipReplayRender = true;
                DuckGame.Graphics.Draw((Tex2D)this.target, position, new Rectangle?(), color, 0.0f, Vec2.Zero, new Vec2(vec2.x / (float)this.target.width, vec2.y / (float)this.target.height), SpriteEffects.None, (Depth)1f);
                if (this.name == "LIGHTING")
                {
                    if (VirtualTransition.core._scanStage == 1)
                        this.targetClearColor = Lerp.ColorSmooth(new Color(120, 120, 120, (int)byte.MaxValue), Color.White, VirtualTransition.core._stick);
                    else if (VirtualTransition.core._scanStage == 3)
                        this.targetClearColor = new Color(120, 120, 120, (int)byte.MaxValue);
                }
                DuckGame.Graphics.skipReplayRender = false;
            }
            else
            {
                if (transparent)
                    Level.activeLevel.PreDrawLayer(this);
                HashSet<Thing> transparent1 = this._transparent;
                HashSet<Thing> opaque = this._opaque;
                if (this._shareDrawObjects != null)
                {
                    transparent1 = this._shareDrawObjects._transparent;
                    opaque = this._shareDrawObjects._opaque;
                }
                if (!Layer.skipDrawing)
                {
                    if (transparent)
                    {
                        if (Network.isActive)
                        {
                            foreach (Thing thing in transparent1)
                            {
                                if (thing.visible && (thing.ghostObject == null || thing.ghostObject.IsInitialized()))
                                {
                                    if (this._perspective)
                                    {
                                        Vec2 position = thing.position;
                                        Vec3 source = new Vec3(position.x, thing.z, thing.bottom);
                                        Viewport viewport = new Viewport(0, 0, 320, 180);
                                        source = (Vec3)viewport.Project((Vector3)source, (Microsoft.Xna.Framework.Matrix)this.projection, (Microsoft.Xna.Framework.Matrix)this.view, (Microsoft.Xna.Framework.Matrix)Matrix.Identity);
                                        thing.position = new Vec2(source.x, source.y - thing.centery);
                                        thing.DoDraw();
                                        DuckGame.Graphics.material = (Material)null;
                                        thing.position = position;
                                        if (thing is PhysicsObject)
                                        {
                                            float num = Maths.NormalizeSection(-thing.y, 8f, 64f);
                                            this._dropShadow.alpha = (float)(0.5 - 0.5 * (double)num);
                                            this._dropShadow.scale = new Vec2(1f - num, 1f - num);
                                            this._dropShadow.depth = thing.depth - 10;
                                            source = new Vec3(position.x, thing.z, 0.0f);
                                            source = (Vec3)viewport.Project((Vector3)source, (Microsoft.Xna.Framework.Matrix)this.projection, (Microsoft.Xna.Framework.Matrix)this.view, (Microsoft.Xna.Framework.Matrix)Matrix.Identity);
                                            DuckGame.Graphics.Draw(this._dropShadow, source.x - 1f, source.y - 1f);
                                        }
                                    }
                                    else
                                        thing.DoDraw();
                                    DuckGame.Graphics.material = (Material)null;
                                }
                            }
                        }
                        else if (this == Layer.Lighting)
                        {
                            foreach (Thing thing in transparent1)
                            {
                                if (thing.visible)
                                {
                                    thing.DoDraw();
                                    DuckGame.Graphics.material = (Material)null;
                                }
                            }
                        }
                        else
                        {
                            foreach (Thing thing in transparent1)
                            {
                                if (thing.visible)
                                {
                                    if (this._perspective)
                                    {
                                        Vec2 position = thing.position;
                                        Vec3 source = new Vec3(position.x, thing.z, thing.bottom);
                                        Viewport viewport = new Viewport(0, 0, 320, 180);
                                        source = (Vec3)viewport.Project((Vector3)source, (Microsoft.Xna.Framework.Matrix)this.projection, (Microsoft.Xna.Framework.Matrix)this.view, (Microsoft.Xna.Framework.Matrix)Matrix.Identity);
                                        thing.position = new Vec2(source.x, source.y - thing.centery);
                                        thing.DoDraw();
                                        DuckGame.Graphics.material = (Material)null;
                                        thing.position = position;
                                        if (thing is PhysicsObject)
                                        {
                                            float num = Maths.NormalizeSection(-thing.y, 8f, 64f);
                                            this._dropShadow.alpha = (float)(0.5 - 0.5 * (double)num);
                                            this._dropShadow.scale = new Vec2(1f - num, 1f - num);
                                            this._dropShadow.depth = thing.depth - 10;
                                            source = new Vec3(position.x, thing.z, 0.0f);
                                            source = (Vec3)viewport.Project((Vector3)source, (Microsoft.Xna.Framework.Matrix)this.projection, (Microsoft.Xna.Framework.Matrix)this.view, (Microsoft.Xna.Framework.Matrix)Matrix.Identity);
                                            DuckGame.Graphics.Draw(this._dropShadow, source.x - 1f, source.y - 1f);
                                        }
                                    }
                                    else
                                        thing.DoDraw();
                                    DuckGame.Graphics.material = (Material)null;
                                }
                            }
                            if (DevConsole.showCollision)
                            {
                                foreach (Thing thing in transparent1)
                                {
                                    if (thing.visible)
                                        thing.DrawCollision();
                                }
                            }
                        }
                        if (Layer.ignoreTransparent)
                        {
                            foreach (Thing thing in opaque)
                            {
                                if (thing.visible)
                                    thing.DoDraw();
                                DuckGame.Graphics.material = (Material)null;
                            }
                            StaticRenderer.RenderLayer(this);
                        }
                    }
                    else
                    {
                        foreach (Thing thing in opaque)
                        {
                            if (thing.visible)
                                thing.DoDraw();
                        }
                        StaticRenderer.RenderLayer(this);
                    }
                }
                if (transparent)
                    Level.activeLevel.PostDrawLayer(this);
            }
            if (Network.isActive && Network.inputDelayFrames > 0 && this == Layer.Game)
            {
                DuckGame.Graphics.drawCalls.Enqueue(DuckGame.Graphics.currentFrameCalls);
                if (DuckGame.Graphics.drawCalls.Count > 0)
                {
                    List<DrawCall> drawCallList = DuckGame.Graphics.drawCalls.Peek();
                    if (DuckGame.Graphics.drawCalls.Count > Network.inputDelayFrames)
                        DuckGame.Graphics.drawCalls.Dequeue();
                    foreach (DrawCall drawCall in drawCallList)
                    {
                        if (drawCall.material != null)
                            DuckGame.Graphics.screen.DrawWithMaterial(drawCall.texture, drawCall.position, drawCall.sourceRect, drawCall.color, drawCall.rotation, drawCall.origin, drawCall.scale, drawCall.effects, drawCall.depth, drawCall.material);
                        else
                            DuckGame.Graphics.screen.Draw(drawCall.texture, drawCall.position, drawCall.sourceRect, drawCall.color, drawCall.rotation, drawCall.origin, drawCall.scale, drawCall.effects, drawCall.depth);
                    }
                }
            }
            this.End(transparent, isTargetDraw);
        }
    }
}