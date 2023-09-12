﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.ItemBoxRandom
//removed for regex reasons Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

using System.Collections.Generic;

namespace DuckGame
{
    [EditorGroup("Spawns")]
    [BaggedProperty("isInDemo", false)]
    [BaggedProperty("previewPriority", false)]
    public class ItemBoxRandom : ItemBox
    {
        public ItemBoxRandom(float xpos, float ypos)
          : base(xpos, ypos)
        {
            editorTooltip = "Spawns a random object each time it's used. Recharges after a short duration.";
            editorCycleType = typeof(ItemBox);
        }

        public override void Draw()
        {
            _sprite.frame += 2;
            base.Draw();
            _sprite.frame -= 2;
        }

        public static PhysicsObject GetRandomItem()
        {
            List<System.Type> physicsObjects = GetPhysicsObjects(Editor.Placeables);
            physicsObjects.RemoveAll(t => t == typeof(LavaBarrel) || t == typeof(Grapple) || t == typeof(Slag) || t == typeof(Holster));
            System.Type t1;
            if (Rando.Int(10000) == 0)
            {
                t1 = typeof(PositronShooter);
                Options.Data.specialTimes = 100;
            }
            else
            {
                if (Options.Data.specialTimes > 0)
                {
                    physicsObjects.Add(typeof(PositronShooter));
                    physicsObjects.Add(typeof(PositronShooter));
                    --Options.Data.specialTimes;
                }
                t1 = physicsObjects[Rando.Int(physicsObjects.Count - 1)];
            }
            if (Editor.clientonlycontent)
            {
                if (Rando.Int(1000) == 1)
                {
                    int rng = Rando.Int(9);
                    switch (rng)
                    {
                        default:
                        case 0:
                            t1 = typeof(DanGun);
                            break;
                        case 1:
                            t1 = typeof(HyeveGun);
                            break;
                        case 2:
                            t1 = typeof(CollinGun);
                            break;
                        case 3:
                            t1 = typeof(NiK0Gun);
                            break;
                    }
                    if (Rando.Int(50) == 0) t1 = typeof(SohRock);
                }
                else if (Rando.Int(300) == 1 && Steam.user != null)
                {
                    ulong u = Steam.user.id;
                    foreach (DGRebuiltDeveloper dgr in DGRDevs.AllWithGuns)
                    {
                        if (u == dgr.SteamID && dgr.DevItem != typeof(PositronShooter))
                        {
                            t1 = dgr.DevItem;
                            break;
                        }
                    }
                }
            }
            PhysicsObject thing = Editor.CreateThing(t1) as PhysicsObject;
            if (Rando.Int(1000) == 1 && thing is Gun && (thing as Gun).CanSpawnInfinite())
            {
                (thing as Gun).infiniteAmmoVal = true;
                (thing as Gun).infinite.value = true;
            }
            if (thing is Rock && Rando.Int(1000000) == 0)
                thing = Editor.CreateThing(typeof(SpawnedGoldRock)) as PhysicsObject;
            return thing;
        }

        public override PhysicsObject GetSpawnItem()
        {
            PhysicsObject randomItem = GetRandomItem();
            contains = randomItem.GetType();
            return randomItem;
        }

        public override void DrawHoverInfo()
        {
        }
    }
}
