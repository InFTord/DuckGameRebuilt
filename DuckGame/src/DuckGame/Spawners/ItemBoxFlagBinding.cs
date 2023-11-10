﻿namespace DuckGame
{
    public class ItemBoxFlagBinding : StateFlagBase
    {
        public override ushort ushortValue
        {
            get
            {
                _value = 0;
                if ((_thing as ItemBox)._hit)
                    _value = 1;
                return _value;
            }
            set
            {
                _value = value;
                (_thing as ItemBox)._hit = (_value & 1U) > 0U;
            }
        }

        public ItemBoxFlagBinding(GhostPriority p = GhostPriority.Normal)
          : base(p, 1)
        {
        }
    }
}
