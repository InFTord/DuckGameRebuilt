﻿namespace DuckGame
{
    public class DistanceMarker : Thing
    {
        private BitmapFont _font;
        private Sprite _distanceSign;
        private int _distance;

        public DistanceMarker(float xpos, float ypos, int dist)
          : base(xpos, ypos)
        {
            _distanceSign = new Sprite("distanceSign");
            _distanceSign.CenterOrigin();
            _distance = dist;
            _font = new BitmapFont("biosFont", 8);
            _distanceSign.CenterOrigin();
            collisionOffset = new Vec2(-8f, -6f);
            collisionSize = new Vec2(16f, 13f);
            center = new Vec2(_distanceSign.w / 2, _distanceSign.h / 2);
        }

        public override void Draw()
        {
            _distanceSign.depth = depth;
            Graphics.Draw(ref _distanceSign, x, y);
            string text = Change.ToString(_distance);
            _font.Draw(text, x - _font.GetWidth(text) / 2f, (float)(y - _font.height / 2f + 1f), Color.DarkSlateGray, depth + 1);
        }
    }
}
