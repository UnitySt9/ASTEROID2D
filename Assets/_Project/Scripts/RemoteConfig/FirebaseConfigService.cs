using System;
using System.Threading.Tasks;
using Firebase.RemoteConfig;
using UnityEngine;

namespace _Project.Scripts
{
    public class FirebaseConfigService : IConfigService
    {
        private const string CONFIG_KEY = "game_config";
        
        public event Action OnConfigUpdated;
        public GameConfig Config { get; private set; }

        public FirebaseConfigService()
        {
            SetDefaultConfig();
            FetchConfigs();
        }

        private void SetDefaultConfig()
        {
            Config = new GameConfig
            {
                ship = new GameConfig.ShipConfig
                {
                    maxSpeed = 10f,
                    acceleration = 5f,
                    rotationSpeed = 200f
                },
                weapons = new GameConfig.WeaponsConfig
                {
                    laserCooldown = 5f,
                    maxLaserShots = 3
                },
                spaceObjects = new GameConfig.SpaceObjectsConfig
                {
                    ufoSpeed = 2f,
                    asteroidSpeed = 3f
                }
            };
        }

        public void FetchConfigs(Action<bool> callback = null)
        {
            _ = FetchConfigAsync(callback);
        }

        private async Task FetchConfigAsync(Action<bool> callback)
        {
            try
            {
                var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
                await remoteConfig.FetchAndActivateAsync();
                ParseConfig(remoteConfig);
                callback?.Invoke(true);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to fetch remote config: {e}");
                callback?.Invoke(false);
            }
        }

        private void ParseConfig(FirebaseRemoteConfig remoteConfig)
        {
            try
            {
                var json = remoteConfig.GetValue(CONFIG_KEY).StringValue;
                if (!string.IsNullOrEmpty(json))
                {
                    Config = JsonUtility.FromJson<GameConfig>(json);
                    OnConfigUpdated?.Invoke();
                    Debug.Log("Config updated from remote");
                }
                else
                {
                    Debug.LogWarning("Empty config received, using default");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to parse config: {e}");
            }
        }
    }
}
