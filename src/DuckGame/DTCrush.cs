﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.DTCrush
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

namespace DuckGame
{
    public class DTCrush : DestroyType
    {
        private PhysicsObject _thing;

        public PhysicsObject thing => this._thing;

        public DTCrush(PhysicsObject t)
          : base(t)
        {
            this._thing = t;
        }
    }
}
