using System;

namespace _Project.Scripts
{
    public interface IConfigService
    {
        GameConfig Config { get; }
        event Action OnConfigUpdated;
        void FetchConfigs(Action<bool> callback = null);
    }
}
