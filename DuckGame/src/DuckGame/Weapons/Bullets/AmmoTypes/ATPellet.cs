﻿namespace DuckGame
{
    public class ATPellet : AmmoType
    {
        public ATPellet()
        {
            accuracy = 1f;
            range = 4000f;
            penetration = 0.4f;
            bulletSpeed = 18f;
            gravityMultiplier = 2f;
            affectedByGravity = true;
            speedVariation = 0f;
            rebound = true;
            softRebound = true;
            weight = 5f;
            bulletThickness = 1f;
            bulletColor = Color.White;
            sprite = new Sprite("pellet")
            {
                center = new Vec2(1f, 1f)
            };
            bulletType = typeof(PelletBullet);
            flawlessPipeTravel = true;
        }
    }
}
