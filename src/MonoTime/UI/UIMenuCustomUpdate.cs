﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.src.MonoTime.UI.UIMenuCustomUpdate
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

using System;

namespace DuckGame.src.MonoTime.UI
{
    public class UIMenuCustomUpdate : UIMenu
    {
        private Action _customUpdate;

        public UIMenuCustomUpdate(
          Action customUpdate,
          string title,
          float xpos,
          float ypos,
          float wide = -1f,
          float high = -1f,
          string conString = "",
          InputProfile conProfile = null,
          bool tiny = false)
          : base(title, xpos, ypos, wide, high, conString, conProfile, tiny)
        {
            this._customUpdate = customUpdate;
        }

        public override void Update()
        {
            if (this._customUpdate != null)
                this._customUpdate();
            base.Update();
        }
    }
}