﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.Color
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

using System;
using System.Collections.Generic;

namespace DuckGame
{
    /// <summary>An RGBA color.</summary>
    public struct Color : IEquatable<Color>
    {
        public byte r;
        public byte g;
        public byte b;
        public byte a;
        public static Color AliceBlue = new Color(240, 248, (int)byte.MaxValue, (int)byte.MaxValue);
        public static Color AntiqueWhite = new Color(250, 235, 215, (int)byte.MaxValue);
        public static Color Aqua = new Color(0, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
        public static Color Aquamarine = new Color((int)sbyte.MaxValue, (int)byte.MaxValue, 212, (int)byte.MaxValue);
        public static Color Azure = new Color(240, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
        public static Color Beige = new Color(245, 245, 220, (int)byte.MaxValue);
        public static Color Bisque = new Color((int)byte.MaxValue, 228, 196, (int)byte.MaxValue);
        public static Color Black = new Color(0, 0, 0, (int)byte.MaxValue);
        public static Color BlanchedAlmond = new Color((int)byte.MaxValue, 235, 205, (int)byte.MaxValue);
        public static Color Blue = new Color(0, 0, (int)byte.MaxValue, (int)byte.MaxValue);
        public static Color BlueViolet = new Color(138, 43, 226, (int)byte.MaxValue);
        public static Color Brown = new Color(165, 42, 42, (int)byte.MaxValue);
        public static Color BurlyWood = new Color(222, 184, 135, (int)byte.MaxValue);
        public static Color CadetBlue = new Color(95, 158, 160, (int)byte.MaxValue);
        public static Color Chartreuse = new Color((int)sbyte.MaxValue, (int)byte.MaxValue, 0, (int)byte.MaxValue);
        public static Color Chocolate = new Color(210, 105, 30, (int)byte.MaxValue);
        public static Color Coral = new Color((int)byte.MaxValue, (int)sbyte.MaxValue, 80, (int)byte.MaxValue);
        public static Color CornflowerBlue = new Color(100, 149, 237, (int)byte.MaxValue);
        public static Color Cornsilk = new Color((int)byte.MaxValue, 248, 220, (int)byte.MaxValue);
        public static Color Crimson = new Color(220, 20, 60, (int)byte.MaxValue);
        public static Color Cyan = new Color(0, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
        public static Color DarkBlue = new Color(0, 0, 139, (int)byte.MaxValue);
        public static Color DarkCyan = new Color(0, 139, 139, (int)byte.MaxValue);
        public static Color DarkGoldenrod = new Color(184, 134, 11, (int)byte.MaxValue);
        public static Color DarkGray = new Color(169, 169, 169, (int)byte.MaxValue);
        public static Color DarkGreen = new Color(0, 100, 0, (int)byte.MaxValue);
        public static Color DarkKhaki = new Color(189, 183, 107, (int)byte.MaxValue);
        public static Color DarkMagenta = new Color(139, 0, 139, (int)byte.MaxValue);
        public static Color DarkOliveGreen = new Color(85, 107, 47, (int)byte.MaxValue);
        public static Color DarkOrange = new Color((int)byte.MaxValue, 140, 0, (int)byte.MaxValue);
        public static Color DarkOrchid = new Color(153, 50, 204, (int)byte.MaxValue);
        public static Color DarkRed = new Color(139, 0, 0, (int)byte.MaxValue);
        public static Color DarkSalmon = new Color(233, 150, 122, (int)byte.MaxValue);
        public static Color DarkSeaGreen = new Color(143, 188, 139, (int)byte.MaxValue);
        public static Color DarkSlateBlue = new Color(72, 61, 139, (int)byte.MaxValue);
        public static Color DarkSlateGray = new Color(47, 79, 79, (int)byte.MaxValue);
        public static Color DarkTurquoise = new Color(0, 206, 209, (int)byte.MaxValue);
        public static Color DarkViolet = new Color(148, 0, 211, (int)byte.MaxValue);
        public static Color DeepPink = new Color((int)byte.MaxValue, 20, 147, (int)byte.MaxValue);
        public static Color DeepSkyBlue = new Color(0, 191, (int)byte.MaxValue, (int)byte.MaxValue);
        public static Color DimGray = new Color(105, 105, 105, (int)byte.MaxValue);
        public static Color DodgerBlue = new Color(30, 144, (int)byte.MaxValue, (int)byte.MaxValue);
        public static Color Firebrick = new Color(178, 34, 34, (int)byte.MaxValue);
        public static Color FloralWhite = new Color((int)byte.MaxValue, 250, 240, (int)byte.MaxValue);
        public static Color ForestGreen = new Color(34, 139, 34, (int)byte.MaxValue);
        public static Color Fuchsia = new Color((int)byte.MaxValue, 0, (int)byte.MaxValue, (int)byte.MaxValue);
        public static Color Gainsboro = new Color(220, 220, 220, (int)byte.MaxValue);
        public static Color GhostWhite = new Color(248, 248, (int)byte.MaxValue, (int)byte.MaxValue);
        public static Color Gold = new Color((int)byte.MaxValue, 215, 0, (int)byte.MaxValue);
        public static Color Goldenrod = new Color(218, 165, 32, (int)byte.MaxValue);
        public static Color Gray = new Color(128, 128, 128, (int)byte.MaxValue);
        public static Color Green = new Color(0, 128, 0, (int)byte.MaxValue);
        public static Color GreenYellow = new Color(173, (int)byte.MaxValue, 47, (int)byte.MaxValue);
        public static Color Honeydew = new Color(240, (int)byte.MaxValue, 240, (int)byte.MaxValue);
        public static Color HotPink = new Color((int)byte.MaxValue, 105, 180, (int)byte.MaxValue);
        public static Color IndianRed = new Color(205, 92, 92, (int)byte.MaxValue);
        public static Color Indigo = new Color(75, 0, 130, (int)byte.MaxValue);
        public static Color Ivory = new Color((int)byte.MaxValue, (int)byte.MaxValue, 240, (int)byte.MaxValue);
        public static Color Khaki = new Color(240, 230, 140, (int)byte.MaxValue);
        public static Color Lavender = new Color(230, 230, 250, (int)byte.MaxValue);
        public static Color LavenderBlush = new Color((int)byte.MaxValue, 240, 245, (int)byte.MaxValue);
        public static Color LawnGreen = new Color(124, 252, 0, (int)byte.MaxValue);
        public static Color LemonChiffon = new Color((int)byte.MaxValue, 250, 205, (int)byte.MaxValue);
        public static Color LightBlue = new Color(173, 216, 230, (int)byte.MaxValue);
        public static Color LightCoral = new Color(240, 128, 128, (int)byte.MaxValue);
        public static Color LightCyan = new Color(224, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
        public static Color LightGoldenrodYellow = new Color(250, 250, 210, (int)byte.MaxValue);
        public static Color LightGray = new Color(211, 211, 211, (int)byte.MaxValue);
        public static Color LightGreen = new Color(144, 238, 144, (int)byte.MaxValue);
        public static Color LightPink = new Color((int)byte.MaxValue, 182, 193, (int)byte.MaxValue);
        public static Color LightSalmon = new Color((int)byte.MaxValue, 160, 122, (int)byte.MaxValue);
        public static Color LightSeaGreen = new Color(32, 178, 170, (int)byte.MaxValue);
        public static Color LightSkyBlue = new Color(135, 206, 250, (int)byte.MaxValue);
        public static Color LightSlateGray = new Color(119, 136, 153, (int)byte.MaxValue);
        public static Color LightSteelBlue = new Color(176, 196, 222, (int)byte.MaxValue);
        public static Color LightYellow = new Color((int)byte.MaxValue, (int)byte.MaxValue, 224, (int)byte.MaxValue);
        public static Color Lime = new Color(0, (int)byte.MaxValue, 0, (int)byte.MaxValue);
        public static Color LimeGreen = new Color(50, 205, 50, (int)byte.MaxValue);
        public static Color Linen = new Color(250, 240, 230, (int)byte.MaxValue);
        public static Color Magenta = new Color((int)byte.MaxValue, 0, (int)byte.MaxValue, (int)byte.MaxValue);
        public static Color Maroon = new Color(128, 0, 0, (int)byte.MaxValue);
        public static Color MediumAquamarine = new Color(102, 205, 170, (int)byte.MaxValue);
        public static Color MediumBlue = new Color(0, 0, 205, (int)byte.MaxValue);
        public static Color MediumOrchid = new Color(186, 85, 211, (int)byte.MaxValue);
        public static Color MediumPurple = new Color(147, 112, 219, (int)byte.MaxValue);
        public static Color MediumSeaGreen = new Color(60, 179, 113, (int)byte.MaxValue);
        public static Color MediumSlateBlue = new Color(123, 104, 238, (int)byte.MaxValue);
        public static Color MediumSpringGreen = new Color(0, 250, 154, (int)byte.MaxValue);
        public static Color MediumTurquoise = new Color(72, 209, 204, (int)byte.MaxValue);
        public static Color MediumVioletRed = new Color(199, 21, 133, (int)byte.MaxValue);
        public static Color MidnightBlue = new Color(25, 25, 112, (int)byte.MaxValue);
        public static Color MintCream = new Color(245, (int)byte.MaxValue, 250, (int)byte.MaxValue);
        public static Color MistyRose = new Color((int)byte.MaxValue, 228, 225, (int)byte.MaxValue);
        public static Color Moccasin = new Color((int)byte.MaxValue, 228, 181, (int)byte.MaxValue);
        public static Color NavajoWhite = new Color((int)byte.MaxValue, 222, 173, (int)byte.MaxValue);
        public static Color OldLace = new Color(253, 245, 230, (int)byte.MaxValue);
        public static Color Olive = new Color(128, 128, 0, (int)byte.MaxValue);
        public static Color OliveDrab = new Color(107, 142, 35, (int)byte.MaxValue);
        public static Color Orange = new Color((int)byte.MaxValue, 165, 0, (int)byte.MaxValue);
        public static Color OrangeRed = new Color((int)byte.MaxValue, 69, 0, (int)byte.MaxValue);
        public static Color Orchid = new Color(218, 112, 214, (int)byte.MaxValue);
        public static Color PaleGoldenrod = new Color(238, 232, 170, (int)byte.MaxValue);
        public static Color PaleGreen = new Color(152, 251, 152, (int)byte.MaxValue);
        public static Color PaleTurquoise = new Color(175, 238, 238, (int)byte.MaxValue);
        public static Color PaleVioletRed = new Color(219, 112, 147, (int)byte.MaxValue);
        public static Color PapayaWhip = new Color((int)byte.MaxValue, 239, 213, (int)byte.MaxValue);
        public static Color PeachPuff = new Color((int)byte.MaxValue, 218, 185, (int)byte.MaxValue);
        public static Color Peru = new Color(205, 133, 63, (int)byte.MaxValue);
        public static Color Pink = new Color((int)byte.MaxValue, 192, 203, (int)byte.MaxValue);
        public static Color Plum = new Color(221, 160, 221, (int)byte.MaxValue);
        public static Color PowderBlue = new Color(176, 224, 230, (int)byte.MaxValue);
        public static Color Purple = new Color(128, 0, 128, (int)byte.MaxValue);
        public static Color Red = new Color((int)byte.MaxValue, 0, 0, (int)byte.MaxValue);
        public static Color RosyBrown = new Color(188, 143, 143, (int)byte.MaxValue);
        public static Color RoyalBlue = new Color(65, 105, 225, (int)byte.MaxValue);
        public static Color SaddleBrown = new Color(139, 69, 19, (int)byte.MaxValue);
        public static Color Salmon = new Color(250, 128, 114, (int)byte.MaxValue);
        public static Color SandyBrown = new Color(244, 164, 96, (int)byte.MaxValue);
        public static Color SeaGreen = new Color(46, 139, 87, (int)byte.MaxValue);
        public static Color SeaShell = new Color((int)byte.MaxValue, 245, 238, (int)byte.MaxValue);
        public static Color Sienna = new Color(160, 82, 45, (int)byte.MaxValue);
        public static Color Silver = new Color(192, 192, 192, (int)byte.MaxValue);
        public static Color SkyBlue = new Color(135, 206, 235, (int)byte.MaxValue);
        public static Color SlateBlue = new Color(106, 90, 205, (int)byte.MaxValue);
        public static Color SlateGray = new Color(112, 128, 144, (int)byte.MaxValue);
        public static Color Snow = new Color((int)byte.MaxValue, 250, 250, (int)byte.MaxValue);
        public static Color SpringGreen = new Color(0, (int)byte.MaxValue, (int)sbyte.MaxValue, (int)byte.MaxValue);
        public static Color SteelBlue = new Color(70, 130, 180, (int)byte.MaxValue);
        public static Color Tan = new Color(210, 180, 140, (int)byte.MaxValue);
        public static Color Teal = new Color(0, 128, 128, (int)byte.MaxValue);
        public static Color Thistle = new Color(216, 191, 216, (int)byte.MaxValue);
        public static Color Tomato = new Color((int)byte.MaxValue, 99, 71, (int)byte.MaxValue);
        public static Color Transparent = new Color(0, 0, 0, 0);
        public static Color Turquoise = new Color(64, 224, 208, (int)byte.MaxValue);
        public static Color Violet = new Color(238, 130, 238, (int)byte.MaxValue);
        public static Color Wheat = new Color(245, 222, 179, (int)byte.MaxValue);
        public static Color White = new Color((int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
        public static Color WhiteSmoke = new Color(245, 245, 245, (int)byte.MaxValue);
        public static Color Yellow = new Color((int)byte.MaxValue, (int)byte.MaxValue, 0, (int)byte.MaxValue);
        public static Color YellowGreen = new Color(154, 205, 50, (int)byte.MaxValue);
        public static readonly List<Color> RainbowColors = new List<Color>()
    {
      new Color(163, 206, 39),
      new Color(247, 224, 90),
      new Color(235, 137, 49),
      new Color(192, 32, 45),
      new Color(237, 94, 238),
      new Color(138, 38, 190),
      new Color(49, 162, 242)
    };

        public Color(byte r, byte g, byte b, byte a)
          : this()
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public Color(byte r, byte g, byte b)
          : this(r, g, b, byte.MaxValue)
        {
        }

        public Color(int r, int g, int b, int a)
          : this((byte)MathHelper.Clamp(r, 0, (int)byte.MaxValue), (byte)MathHelper.Clamp(g, 0, (int)byte.MaxValue), (byte)MathHelper.Clamp(b, 0, (int)byte.MaxValue), (byte)MathHelper.Clamp(a, 0, (int)byte.MaxValue))
        {
        }

        public Color(int r, int g, int b)
          : this(r, g, b, (int)byte.MaxValue)
        {
        }

        public Color(float r, float g, float b, float a)
          : this((byte)((double)MathHelper.Clamp(r, 0.0f, 1f) * (double)byte.MaxValue), (byte)((double)MathHelper.Clamp(g, 0.0f, 1f) * (double)byte.MaxValue), (byte)((double)MathHelper.Clamp(b, 0.0f, 1f) * (double)byte.MaxValue), (byte)((double)MathHelper.Clamp(a, 0.0f, 1f) * (double)byte.MaxValue))
        {
        }

        public Color(float r, float g, float b)
          : this(r, g, b, 1f)
        {
        }

        public Color(uint hex)
          : this((byte)(hex & (uint)byte.MaxValue), (byte)((int)hex << 8 & (int)byte.MaxValue), (byte)((int)hex << 16 & (int)byte.MaxValue), (byte)((int)hex << 24 & (int)byte.MaxValue))
        {
        }

        public Color(uint hex, bool newHexSystem)
          : this()
        {
            if (hex > 16777215U)
            {
                this.r = (byte)(hex >> 24 & (uint)byte.MaxValue);
                this.g = (byte)(hex >> 16 & (uint)byte.MaxValue);
                this.b = (byte)(hex >> 8 & (uint)byte.MaxValue);
                this.a = (byte)(hex & (uint)byte.MaxValue);
            }
            else
            {
                this.r = (byte)(hex >> 16 & (uint)byte.MaxValue);
                this.g = (byte)(hex >> 8 & (uint)byte.MaxValue);
                this.b = (byte)(hex & (uint)byte.MaxValue);
                this.a = byte.MaxValue;
            }
        }

        public static Color FromHexString(string pString)
        {
            try
            {
                if (pString.StartsWith("#"))
                    pString = pString.Substring(1, pString.Length - 1);
                else if (pString.StartsWith("0x"))
                    pString = pString.Substring(2, pString.Length - 2);
                return new Color(Convert.ToUInt32("0x" + pString, 16), true);
            }
            catch (Exception ex)
            {
                return Color.White;
            }
        }

        public static explicit operator int(Color color) => (int)color.r | (int)color.g >> 8 | (int)color.b >> 16 | (int)color.a >> 24;

        public static explicit operator Color(uint hex) => new Color(hex);

        public static Color operator *(Color c, float r) => new Color((byte)MathHelper.Clamp((float)c.r * r, 0.0f, (float)byte.MaxValue), (byte)MathHelper.Clamp((float)c.g * r, 0.0f, (float)byte.MaxValue), (byte)MathHelper.Clamp((float)c.b * r, 0.0f, (float)byte.MaxValue), (byte)MathHelper.Clamp((float)c.a * r, 0.0f, (float)byte.MaxValue));

        public static Color operator /(Color c, float r) => new Color((byte)MathHelper.Clamp((float)c.r / r, 0.0f, (float)byte.MaxValue), (byte)MathHelper.Clamp((float)c.g / r, 0.0f, (float)byte.MaxValue), (byte)MathHelper.Clamp((float)c.b / r, 0.0f, (float)byte.MaxValue), (byte)MathHelper.Clamp((float)c.a / r, 0.0f, (float)byte.MaxValue));

        public static bool operator ==(Color l, Color r) => l.Equals(r);

        public static bool operator !=(Color l, Color r) => !l.Equals(r);

        public bool Equals(Color other) => (int)this.r == (int)other.r && (int)this.g == (int)other.g && (int)this.b == (int)other.b && (int)this.a == (int)other.a;

        public override bool Equals(object obj) => obj is Color other ? this.Equals(other) : base.Equals(obj);

        public override int GetHashCode() => (int)this;

        public override string ToString() => string.Format("{0} {1} {2} {3}", (object)this.r, (object)this.g, (object)this.b, (object)this.a);

        public string ToDGColorString() => string.Format("|{0},{1},{2}|", (object)this.r, (object)this.g, (object)this.b);

        public static Color Lerp(Color a, Color b, float v) => DuckGame.Lerp.ColorSmooth(a, b, v);

        public Color(Vec4 vec)
          : this(vec.x, vec.y, vec.z, vec.w)
        {
        }

        public Color(Vec3 vec)
          : this(vec.x, vec.y, vec.z)
        {
        }

        public Vec4 ToVector4() => new Vec4((float)this.r / (float)byte.MaxValue, (float)this.g / (float)byte.MaxValue, (float)this.b / (float)byte.MaxValue, (float)this.a / (float)byte.MaxValue);

        public Vec3 ToVector3() => new Vec3((float)this.r / (float)byte.MaxValue, (float)this.g / (float)byte.MaxValue, (float)this.b / (float)byte.MaxValue);

        public static implicit operator Microsoft.Xna.Framework.Color(Color c) => new Microsoft.Xna.Framework.Color((int)c.r, (int)c.g, (int)c.b, (int)c.a);

        public static implicit operator Color(Microsoft.Xna.Framework.Color c) => new Color(c.R, c.G, c.B, c.A);
    }
}