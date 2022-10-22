﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.Saxaphone
//removed for regex reasons Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

using System;
using System.Collections.Generic;

namespace DuckGame
{
    [EditorGroup("Guns|Misc")]
    [BaggedProperty("isFatal", false)]
    [BaggedProperty("previewPriority", true)]
    public class Saxaphone : Gun
    {
        public StateBinding _notePitchBinding = new StateBinding(nameof(notePitch));
        public StateBinding _handPitchBinding = new StateBinding(nameof(handPitch));
        public float notePitch;
        public float handPitch;
        private float prevNotePitch;
        private float hitPitch;
        private Sound noteSound;
        private List<InstrumentNote> _notes = new List<InstrumentNote>();

        public Saxaphone(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 4;
            _ammoType = new ATLaser();
            _ammoType.range = 170f;
            _ammoType.accuracy = 0.8f;
            _type = "gun";
            graphic = new Sprite("saxaphone");
            center = new Vec2(20f, 18f);
            collisionOffset = new Vec2(-4f, -7f);
            collisionSize = new Vec2(8f, 16f);
            _barrelOffsetTL = new Vec2(24f, 16f);
            _fireSound = "smg";
            _fullAuto = true;
            _fireWait = 1f;
            _kickForce = 3f;
            _holdOffset = new Vec2(6f, 2f);
            holsterAngle = 90f;
            _notePitchBinding.skipLerp = true;
            _editorName = "Saxophone";
            editorTooltip = "Crave the dulcet tones of smooth jazz? This is the item for you.";
            isFatal = false;
        }

        public override void Initialize() => base.Initialize();

        public override void Update()
        {
            if (this.owner is Duck owner)
            {
                if (isServerForObject && owner.inputProfile != null)
                {
                    handPitch = owner.inputProfile.leftTrigger;
                    if (owner.inputProfile.hasMotionAxis)
                        handPitch += owner.inputProfile.motionAxis;
                    int num = Keyboard.CurrentNote(owner.inputProfile, this);
                    if (num >= 0)
                    {
                        notePitch = (float)(num / 12.0 + 0.01f);
                        handPitch = notePitch;
                        if (notePitch != prevNotePitch)
                        {
                            prevNotePitch = 0f;
                            if (noteSound != null)
                            {
                                noteSound.Stop();
                                noteSound = null;
                            }
                        }
                    }
                    else
                        notePitch = !owner.inputProfile.Down("SHOOT") ? 0f : handPitch + 0.01f;
                }
                if (notePitch != prevNotePitch)
                {
                    if (notePitch != 0.0)
                    {
                        int num = (int)Math.Round(notePitch * 12.0);
                        if (num < 0)
                            num = 0;
                        if (num > 12)
                            num = 12;
                        if (noteSound == null)
                        {
                            hitPitch = notePitch;
                            noteSound = SFX.Play("sax" + Change.ToString(num));
                            Level.Add(new MusicNote(barrelPosition.x, barrelPosition.y, barrelVector));
                        }
                        else
                            noteSound.Pitch = Maths.Clamp((float)((notePitch - hitPitch) * 0.1f), -1f, 1f);
                    }
                    else if (noteSound != null)
                    {
                        noteSound.Stop();
                        noteSound = null;
                    }
                }
                if (_raised)
                {
                    handAngle = 0f;
                    handOffset = new Vec2(0f, 0f);
                    _holdOffset = new Vec2(0f, 2f);
                    collisionOffset = new Vec2(-4f, -7f);
                    collisionSize = new Vec2(8f, 16f);
                    OnReleaseAction();
                }
                else
                {
                    handOffset = new Vec2((float)(5.0 + (1.0 - handPitch) * 2.0), (float)((1.0 - handPitch) * 4.0 - 2.0));
                    handAngle = (float)((1.0 - handPitch) * 0.04f) * offDir;
                    _holdOffset = new Vec2((float)(4.0 + handPitch * 2.0), handPitch * 2f);
                    collisionOffset = new Vec2(-1f, -7f);
                    collisionSize = new Vec2(2f, 16f);
                }
            }
            else
            {
                collisionOffset = new Vec2(-4f, -7f);
                collisionSize = new Vec2(8f, 16f);
            }
            prevNotePitch = notePitch;
            base.Update();
        }

        public override void OnPressAction()
        {
        }

        public override void OnReleaseAction()
        {
        }

        public override void Fire()
        {
        }
    }
}