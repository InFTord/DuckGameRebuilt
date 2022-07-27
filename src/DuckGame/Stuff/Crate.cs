﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.Crate
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

using System;

namespace DuckGame
{
    [EditorGroup("Stuff|Props")]
    [BaggedProperty("isInDemo", true)]
    [BaggedProperty("previewPriority", true)]
    public class Crate : Holdable, IPlatform
    {
        public StateBinding _destroyedBinding = new StateBinding("_destroyed");
        public StateBinding _hitPointsBinding = new StateBinding("_hitPoints");
        public StateBinding _damageMultiplierBinding = new StateBinding(nameof(damageMultiplier));
        public float damageMultiplier = 1f;
        private SpriteMap _sprite;
        private float _burnt;

        public Crate(float xpos, float ypos)
          : base(xpos, ypos)
        {
            this._maxHealth = 15f;
            this._hitPoints = 15f;
            this._sprite = new SpriteMap("crate", 16, 16);
            this.graphic = (Sprite)this._sprite;
            this.center = new Vec2(8f, 8f);
            this.collisionOffset = new Vec2(-8f, -8f);
            this.collisionSize = new Vec2(16f, 16f);
            this.depth = - 0.5f;
            this._editorName = nameof(Crate);
            this.thickness = 2f;
            this.weight = 5f;
            this.buoyancy = 1f;
            this._holdOffset = new Vec2(2f, 0.0f);
            this.flammable = 0.3f;
            this.collideSounds.Add("crateHit");
            this.editorTooltip = "It's made of wood. That's...pretty much it.";
        }

        protected override bool OnDestroy(DestroyType type = null)
        {
            this._hitPoints = 0.0f;
            Level.Remove((Thing)this);
            SFX.Play("crateDestroy");
            Vec2 vec2 = Vec2.Zero;
            if (type is DTShot)
                vec2 = (type as DTShot).bullet.travelDirNormalized;
            for (int index = 0; index < 6; ++index)
            {
                WoodDebris woodDebris = WoodDebris.New(this.x - 8f + Rando.Float(16f), this.y - 8f + Rando.Float(16f));
                woodDebris.hSpeed = (float)(((double)Rando.Float(1f) > 0.5 ? 1.0 : -1.0) * (double)Rando.Float(3f) + (double)Math.Sign(vec2.x) * 0.5);
                woodDebris.vSpeed = -Rando.Float(1f);
                Level.Add((Thing)woodDebris);
            }
            for (int index = 0; index < 5; ++index)
            {
                SmallSmoke smallSmoke = SmallSmoke.New(this.x + Rando.Float(-6f, 6f), this.y + Rando.Float(-6f, 6f));
                smallSmoke.hSpeed += Rando.Float(-0.3f, 0.3f);
                smallSmoke.vSpeed -= Rando.Float(0.1f, 0.2f);
                Level.Add((Thing)smallSmoke);
            }
            return true;
        }

        private bool CheckForPhysicalBullet(MaterialThing with)
        {
            if (with is PhysicalBullet)
            {
                Bullet bullet = (with as PhysicalBullet).bullet;
                if (bullet != null && bullet.ammo is ATGrenade)
                    return true;
            }
            return false;
        }

        public override void SolidImpact(MaterialThing with, ImpactedFrom from)
        {
            if (this.CheckForPhysicalBullet(with))
                this.Destroy((DestroyType)new DTShot((with as PhysicalBullet).bullet));
            else
                base.SolidImpact(with, from);
        }

        public override void Impact(MaterialThing with, ImpactedFrom from, bool solidImpact)
        {
            if (this.CheckForPhysicalBullet(with))
                this.Destroy((DestroyType)new DTShot((with as PhysicalBullet).bullet));
            else
                base.Impact(with, from, solidImpact);
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if ((double)this._hitPoints <= 0.0)
                return base.Hit(bullet, hitPos);
            if (bullet.isLocal && this.owner == null)
                Thing.Fondle((Thing)this, DuckNetwork.localConnection);
            for (int index = 0; (double)index < 1.0 + (double)this.damageMultiplier / 2.0; ++index)
            {
                WoodDebris woodDebris = WoodDebris.New(hitPos.x, hitPos.y);
                woodDebris.hSpeed = (float)(-(double)bullet.travelDirNormalized.x * 2.0 * ((double)Rando.Float(1f) + 0.300000011920929));
                woodDebris.vSpeed = (float)(-(double)bullet.travelDirNormalized.y * 2.0 * ((double)Rando.Float(1f) + 0.300000011920929)) - Rando.Float(2f);
                Level.Add((Thing)woodDebris);
            }
            SFX.Play("woodHit");
            if (this.isServerForObject && TeamSelect2.Enabled("EXPLODEYCRATES"))
            {
                Thing.Fondle((Thing)this, DuckNetwork.localConnection);
                if (this.duck != null)
                    this.duck.ThrowItem();
                this.Destroy((DestroyType)new DTShot(bullet));
                Level.Add((Thing)new GrenadeExplosion(this.x, this.y));
            }
            this._hitPoints -= this.damageMultiplier;
            this.damageMultiplier += 2f;
            if ((double)this._hitPoints <= 0.0)
            {
                if (bullet.isLocal)
                    Thing.SuperFondle((Thing)this, DuckNetwork.localConnection);
                this.Destroy((DestroyType)new DTShot(bullet));
            }
            return base.Hit(bullet, hitPos);
        }

        public override void ExitHit(Bullet bullet, Vec2 exitPos)
        {
            for (int index = 0; (double)index < 1.0 + (double)this.damageMultiplier / 2.0; ++index)
            {
                WoodDebris woodDebris = WoodDebris.New(exitPos.x, exitPos.y);
                woodDebris.hSpeed = (float)((double)bullet.travelDirNormalized.x * 3.0 * ((double)Rando.Float(1f) + 0.300000011920929));
                woodDebris.vSpeed = (float)((double)bullet.travelDirNormalized.y * 3.0 * ((double)Rando.Float(1f) + 0.300000011920929) - ((double)Rando.Float(2f) - 1.0));
                Level.Add((Thing)woodDebris);
            }
        }

        public override void Update()
        {
            base.Update();
            if ((double)this.damageMultiplier > 1.0)
                this.damageMultiplier -= 0.2f;
            else
                this.damageMultiplier = 1f;
            this._sprite.frame = (int)Math.Floor((1.0 - (double)this._hitPoints / (double)this._maxHealth) * 4.0);
            if ((double)this._hitPoints <= 0.0 && !this._destroyed)
                this.Destroy((DestroyType)new DTImpact((Thing)this));
            if (!this._onFire || (double)this._burnt >= 0.899999976158142)
                return;
            float num = 1f - this.burnt;
            if ((double)this._hitPoints > (double)num * (double)this._maxHealth)
                this._hitPoints = num * this._maxHealth;
            this._sprite.color = new Color(num, num, num);
        }
    }
}