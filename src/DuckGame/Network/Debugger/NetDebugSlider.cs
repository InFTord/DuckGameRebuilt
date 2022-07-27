﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.NetDebugSlider
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

using System;

namespace DuckGame
{
    public class NetDebugSlider : NetDebugElement
    {
        private Func<float> _getValue;
        private Action<float> _setValue;
        private Func<float, string> _formatter;

        public NetDebugSlider(
          NetDebugInterface pInterface,
          string pName,
          Func<float> pGet,
          Action<float> pSet,
          Func<float, string> pDisplayFormatter)
          : base(pInterface)
        {
            this._name = pName;
            this._getValue = pGet;
            this._setValue = pSet;
            this._formatter = pDisplayFormatter;
        }

        public void Update()
        {
        }

        protected override bool Draw(Vec2 position, bool allowInput)
        {
            bool flag = !allowInput;
            position.x += this.indent;
            Graphics.DrawString(this._name, position, Color.White, this.depth + 10);
            float num1 = this._getValue();
            int num2 = 20;
            int num3 = (int)Math.Round((double)num1 * (double)num2);
            Rectangle rectangle = new Rectangle(position.x + 100f, position.y, (float)(num2 * 5), 8f);
            float num4 = -1f;
            if (rectangle.Contains(Mouse.positionConsole) & allowInput)
            {
                num4 = (float)(int)(((double)Mouse.positionConsole.x - (double)rectangle.Left) / (double)rectangle.width * (double)num2);
                if (Mouse.left == InputState.Down)
                {
                    this._setValue(num4 / (float)num2);
                    flag = true;
                }
            }
            Vec2 tl = rectangle.tl;
            for (int index = 0; index < num2; ++index)
            {
                Color col = Color.Gray;
                if ((double)num4 >= (double)index && (double)num4 != -1.0)
                    col = Color.White;
                else if (num3 >= index)
                    col = new Color(200, 200, 200);
                Graphics.DrawRect(tl, tl + new Vec2(4f, 8f), col, this.depth + 5);
                tl.x += 5f;
            }
            tl.x += 2f;
            Graphics.DrawString("(" + this._formatter(num1) + ")", tl, Color.White, this.depth + 5);
            return flag;
        }
    }
}