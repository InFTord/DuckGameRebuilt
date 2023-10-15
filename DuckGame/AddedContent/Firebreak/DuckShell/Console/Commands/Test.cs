﻿using AddedContent.Firebreak;

namespace DuckGame.ConsoleEngine
{
    public static partial class Commands
    {
        [Marker.DSHCommand(Hidden = true)]
        public static string Test(huh whuh)
        {
            return $"{(int) whuh}: {whuh}";
        }

        public enum huh
        {
            Example1,
            Sample2,
            Placeholder3,
            Exemplar4,
            Subatomic5
        }
    }
}