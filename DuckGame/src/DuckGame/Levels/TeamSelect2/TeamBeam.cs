﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DuckGame
{
    public class TeamBeam : MaterialThing
    {
        private Sprite _selectBeam;
        private float _spawnWait;
        private SinWave _wave;
        private SinWave _wave2;
        private List<BeamDuck> _ducks = new List<BeamDuck>();
        private List<Thing> _guns = new List<Thing>();
        private float _beamHeight = 180f;
        private float _flash;
        private int waitFrames;
        private List<Duck> _networkDucks = new List<Duck>();

        public TeamBeam(float xpos, float ypos)
          : base(xpos, ypos)
        {
            _wave = new SinWave(this, 0.016f);
            _wave2 = new SinWave(this, 0.02f);
            _selectBeam = new Sprite("selectBeam")
            {
                alpha = 0.9f,
                depth = -0.8f
            };
            _selectBeam.center = new Vec2(_selectBeam.w / 2, 0f);
            depth = (Depth)0f;
            _collisionOffset = new Vec2((float)-(_selectBeam.w / 2 * 0.8f), 0f);
            _collisionSize = new Vec2(_selectBeam.w * 0.8f, 180f);
            center = new Vec2(_selectBeam.w / 2);
            thickness = 10f;
        }

        public override void Initialize() => base.Initialize();

        public void TakeDuck(Duck d)
        {
            if (_ducks.Any(t => t.duck == d))
                return;
            float num = d.y >= 100f ? (d.y >= 150f ? 220f : 130f) : 40f;
            if (DG.FiftyPlayerMode)
            {
                if (d.y >= 100f)
                {
                    if (d.y >= 150f)
                    {
                        num = (90 * ((float)Math.Ceiling((d.y - 40) / 90))) + 36;
                    }
                    else
                    {
                        num = 130f;
                    }
                }
                else
                {
                    num = 40f;
                }
            }
            SFX.Play("stepInBeam");
            d.beammode = true;
            d.immobilized = true;
            d.crouch = false;
            d.sliding = false;
            if (d.holdObject != null)
                _guns.Add(d.holdObject);
            d.ThrowItem();
            d.solid = false;
            d.grounded = false;
            d.offDir = 1;
            _ducks.Add(new BeamDuck()
            {
                duck = d,
                entryHeight = num,
                leaving = false,
                entryDir = d.x < x ? -1 : 1,
                sin = new SinWave(this, 0.1f),
                sin2 = new SinWave(this, 0.05f)
            });
            if (_ducks.Count <= 0)
                return;
            int currentIndex = NetworkDebugger.currentIndex;
        }

        public void ClearBeam()
        {
            foreach (BeamDuck duck in _ducks)
                duck.leaving = true;
        }

        public void RemoveDuck(Duck duck)
        {
            foreach (BeamDuck duck1 in _ducks)
            {
                if (duck1.duck == duck)
                    duck1.leaving = true;
            }
        }

        public override void Update()
        {

            if (TeamSelect2.zoomedOut)
            {
                
                if (DG.FiftyPlayerMode)
                {
                    int maxslots = (int)Math.Ceiling(Math.Sqrt(DG.MaxPlayers + 1));
                    _beamHeight = 90f * maxslots;
                    _collisionSize = new Vec2(_selectBeam.w * 0.8f, 90f * maxslots);
                }
                else
                {
                    _beamHeight = 270f;
                    _collisionSize = new Vec2(_selectBeam.w * 0.8f, 270f);
                }
            }
            else
            {
                _beamHeight = 180f;
                _collisionSize = new Vec2(_selectBeam.w * 0.8f, 180f);
            }
            _selectBeam.color = new Color(0.3f, (float)(0.3f + _wave2.normalized * 0.2f), (float)(0.5 + _wave.normalized * 0.3f)) * (1f + _flash);
            _flash = Maths.CountDown(_flash, 0.1f);
            _spawnWait -= 0.1f * DGRSettings.ActualParticleMultiplier;
            if (_spawnWait < 0f)
            {
                Level.Add(new BeamParticle(x, y + 290f, -0.8f - _wave.normalized, false, Color.Cyan * 0.8f));
                Level.Add(new BeamParticle(x, y + 290f, -0.8f - _wave2.normalized, true, Color.LightBlue * 0.8f));
                _spawnWait = 1f;
            }
            ++waitFrames;
            if (waitFrames > 5)
            {
                foreach (Duck d in Level.CheckRectAll<Duck>(topLeft, bottomRight))
                {
                    if (d.isServerForObject)
                        TakeDuck(d);
                }
            }
            foreach (Holdable holdable in Level.CheckRectAll<Holdable>(topLeft, bottomRight))
            {
                if (holdable.isServerForObject)
                {
                    if (holdable is RagdollPart)
                    {
                        Duck captureDuck = (holdable as RagdollPart).doll.captureDuck;
                        if (captureDuck != null)
                        {
                            (holdable as RagdollPart).doll.Unragdoll();
                            TakeDuck(captureDuck);
                        }
                    }
                    else if (holdable.owner == null && !_guns.Contains(holdable))
                        _guns.Add(holdable);
                }
            }
            int val2 = _ducks.Count;
            if (Network.isActive)
            {
                foreach (BeamDuck duck in _ducks)
                    duck.floatOrder = 0;
                if (Level.current is TeamSelect2 current)
                {
                    int val1 = 0;
                    foreach (ProfileBox2 profile in current._profiles)
                    {
                        if (profile.ready && profile.duck != null && Math.Abs(profile.duck.x - x) < 16)
                        {
                            if (profile.duck != null && profile.duck.profile != null)
                            {
                                foreach (BeamDuck duck in _ducks)
                                {
                                    if (profile.duck.profile.networkIndex < duck.duck.profile.networkIndex)
                                        ++duck.floatOrder;
                                }
                            }
                            ++val1;
                        }
                    }
                    val2 = Math.Max(val1, val2);
                }
            }
            int num1 = 0;
            float num2 = (float)(_beamHeight / val2 / 2 + 20 * (val2 > 1 ? 1 : 0));
            float num3 = (float)((_beamHeight - num2 * 2) / (val2 > 1 ? val2 - 1 : 1));
            for (int index = 0; index < _ducks.Count; ++index)
            {
                BeamDuck duck = _ducks[index];
                if (duck.duck == null || duck.duck.removeFromLevel || !duck.duck.beammode)
                {
                    _ducks.RemoveAt(index);
                    --index;
                }
                else
                {
                    if (duck.leaving)
                    {
                        duck.duck.solid = true;
                        duck.duck.y = MathHelper.Lerp(duck.duck.position.y, duck.entryHeight, 0.35f);
                        duck.duck.vSpeed = 0f;
                        if (Math.Abs(duck.duck.position.y - duck.entryHeight) < 4f)
                        {
                            duck.duck.position.y = duck.entryHeight;
                            duck.duck.hSpeed = duck.entryDir * 3f;
                            duck.duck.vSpeed = 0f;
                        }
                        if (Math.Abs(duck.duck.position.x - x) > 24f)
                        {
                            duck.duck.gravMultiplier = 1f;
                            duck.duck.immobilized = false;
                            duck.duck.beammode = false;
                            _ducks.RemoveAt(index);
                            --index;
                            continue;
                        }
                    }
                    else
                    {
                        if (Math.Abs(duck.duck.position.x - x) <= 24f)
                            duck.duck.beammode = true;
                        int num4 = index;
                        if (Network.isActive && duck.duck.profile != null)
                            num4 = duck.floatOrder;
                        duck.duck.position.x = Lerp.FloatSmooth(duck.duck.position.x, position.x + (float)duck.sin2 * 1f, 0.4f);
                        duck.duck.position.y = Lerp.FloatSmooth(duck.duck.position.y, (float)(num2 + num3 * num4 + (float)duck.sin * 2f), 0.1f);
                        duck.duck.vSpeed = 0f;
                        duck.duck.hSpeed = 0f;
                        duck.duck.gravMultiplier = 0f;
                    }
                    if (duck.duck.inputProfile != null && duck.duck.inputProfile.Pressed(Triggers.Cancel) && Math.Abs(duck.duck.position.x - x) < 2f)
                        duck.leaving = true;
                    if (duck.duck.profile == null)
                        duck.leaving = true;
                    if (Network.isActive && duck.duck.profile != null && (duck.duck.profile.connection == null || duck.duck.profile.connection.status == ConnectionStatus.Disconnected))
                        duck.leaving = true;
                    ++num1;
                }
            }
            for (int index = 0; index < _guns.Count; ++index)
            {
                Thing gun = _guns[index];
                gun.vSpeed = 0f;
                gun.hSpeed = 0f;
                if (Math.Abs(position.x - gun.position.x) < 6f)
                {
                    gun.position = Vec2.Lerp(gun.position, new Vec2(position.x, gun.position.y - 3f), 0.1f);
                    gun.alpha = Maths.LerpTowards(gun.alpha, 0f, 0.1f);
                    if (gun.alpha <= 0f)
                    {
                        gun.y = -200f;
                        _guns.RemoveAt(index);
                        --index;
                    }
                }
                else
                    gun.position = Vec2.Lerp(gun.position, new Vec2(position.x, gun.position.y), 0.2f);
            }
            base.Update();
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            for (int index = 0; index < DGRSettings.ActualParticleMultiplier * 6; ++index)
                Level.Add(new GlassParticle(hitPos.x, hitPos.y, new Vec2(Rando.Float(-1f, 1f), Rando.Float(-1f, 1f))));
            _flash = 1f;
            if (bullet != null)
                bullet.hitArmor = true;
            return true;
        }

        public override void Draw()
        {
            base.Draw();
            if (DG.FiftyPlayerMode)
            {
                int maxslots = (int)Math.Ceiling(Math.Sqrt(DG.MaxPlayers + 1));
                for (int index = 0; index < (maxslots * 3) + 1; ++index)
                    Graphics.Draw(_selectBeam, x, y - 32f + index * 32);
            }
            else
            {
                for (int index = 0; index < 10; ++index)
                    Graphics.Draw(_selectBeam, x, y - 32f + index * 32);
            }
        }
    }
}
