using Firebase;
using Firebase.Analytics;
using Firebase.RemoteConfig;
using UnityEngine;
using System.Threading.Tasks;

namespace _Project.Scripts
{
    public class FirebaseInitializer : MonoBehaviour
    {
        private DependencyStatus _dependencyStatus = DependencyStatus.UnavailableOther;

        private async void Start()
        {
            await InitializeFirebase();
        }

        private async Task InitializeFirebase()
        {
            var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
            _dependencyStatus = dependencyStatus;

            if (_dependencyStatus == DependencyStatus.Available)
            {
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                await InitializeRemoteConfig();
                Debug.Log("Firebase successfully initialized");
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {_dependencyStatus}");
            }
        }

        private async Task InitializeRemoteConfig()
        {
            var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            var defaults = new System.Collections.Generic.Dictionary<string, object>();
            defaults.Add("game_config", "{}");
            await remoteConfig.SetDefaultsAsync(defaults);
            var configSettings = new ConfigSettings();
            #if UNITY_EDITOR || DEVELOPMENT_BUILD
            configSettings.MinimumFetchIntervalInMilliseconds = 0;
            #else
            configSettings.MinimumFetchIntervalInMilliseconds = 3600000; // 1 час для production
            #endif
            await remoteConfig.SetConfigSettingsAsync(configSettings);
        }
    }
}
