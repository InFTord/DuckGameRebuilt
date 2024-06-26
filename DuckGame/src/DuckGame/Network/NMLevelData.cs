﻿namespace DuckGame
{
    public class NMLevelData : NMEvent
    {
        public new byte levelIndex;
        private Level _level;

        public NMLevelData() => manager = BelongsToManager.EventManager;

        public NMLevelData(Level pLevel)
        {
            manager = BelongsToManager.EventManager;
            levelIndex = pLevel.networkIndex = DuckNetwork.levelIndex;
            _level = pLevel;
        }

        public override void Activate()
        {
            if (DuckNetwork.levelIndex != levelIndex)
                return;
            DevConsole.Log(DCSection.DuckNet, "|DGGREEN|Received Level Information (" + levelIndex.ToString() + ").");
            Level.current.TransferComplete(connection);
            Send.Message(new NMLevelReady(levelIndex), NetMessagePriority.ReliableOrdered);
            connection.levelIndex = levelIndex;
        }
    }
}
