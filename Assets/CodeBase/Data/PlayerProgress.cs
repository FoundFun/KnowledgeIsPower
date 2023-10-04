using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        private readonly string _initialLevel;
        public WorldData WorldData;

        public PlayerProgress(string initialLevel)
        {
            _initialLevel = initialLevel;
        }
    }
}