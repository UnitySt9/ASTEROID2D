using System;

namespace _Project.Scripts
{
    public interface IConfigService
    {
        GameConfig Config { get; }
        event Action OnConfigUpdated;
    }
}
