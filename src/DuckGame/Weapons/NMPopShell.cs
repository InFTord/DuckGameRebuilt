﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.NMPopShell
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

namespace DuckGame
{
    public class NMPopShell : NMEvent
    {
        public Gun gun;

        public NMPopShell()
        {
        }

        public NMPopShell(Gun pGun) => this.gun = pGun;

        public override void Activate()
        {
            if (this.gun != null)
                this.gun.PopShell(true);
            base.Activate();
        }
    }
}