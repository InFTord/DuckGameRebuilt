﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.Factory`1
//removed for regex reasons Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

namespace DuckGame
{
    internal static class Factory<T> where T : new()
    {
        private static int kMaxObjects = 1024;
        private static T[] _objects = new T[Factory<T>.kMaxObjects];
        private static int _lastActiveObject = 0;

        static Factory()
        {
            for (int index = 0; index < Factory<T>.kMaxObjects; ++index)
                Factory<T>._objects[index] = new T();
        }

        public static T New()
        {
            T obj = Factory<T>._objects[Factory<T>._lastActiveObject];
            Factory<T>._lastActiveObject = (Factory<T>._lastActiveObject + 1) % Factory<T>.kMaxObjects;
            return obj;
        }
    }
}
