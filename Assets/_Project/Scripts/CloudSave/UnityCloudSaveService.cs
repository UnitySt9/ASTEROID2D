using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine;

namespace _Project.Scripts
{
    public class UnityCloudSaveService : ICloudSaveService
    {
        private const string SAVE_KEY = "GameSaveData";
        private bool _isInitialized = false;
        private ISaveService _localSaveService;

        public UnityCloudSaveService(ISaveService localSaveService)
        {
            _localSaveService = localSaveService;
        }

        public async Task InitializeAsync()
        {
            if (_isInitialized) 
                return;
            try
            {
                await UnityServices.InitializeAsync();
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                _isInitialized = true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Cloud save initialization failed: {e.Message}");
                _isInitialized = false;
            }
        }

        public async Task SaveAsync(GameData data)
        {
            if (!_isInitialized) 
                return;
            data.SaveDateTime = DateTime.UtcNow;
            try
            {
                var dataDict = new Dictionary<string, object>
                {
                    { SAVE_KEY, JsonUtility.ToJson(data) }
                };
                await CloudSaveService.Instance.Data.ForceSaveAsync(dataDict);
                Debug.Log("Game data successfully saved to cloud");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save game data to cloud: {e.Message}");
            }
        }

        public async Task<GameData> LoadAsync()
        {
            if (!_isInitialized) 
                return null;
            try
            {
                var data = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { SAVE_KEY });
                if (data.TryGetValue(SAVE_KEY, out var savedData))
                {
                    var gameData = JsonUtility.FromJson<GameData>(savedData);
                    Debug.Log("Game data successfully loaded from cloud");
                    return gameData;
                }
                return null;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load game data from cloud: {e.Message}");
                return null;
            }
        }

        public async Task<bool> SynchronizeAsync()
        {
            if (!_isInitialized) 
                return false;
            try
            {
                var localData = _localSaveService.Load();
                var cloudData = await LoadAsync();
                if (cloudData == null)
                {
                    await SaveAsync(localData);
                    return true;
                }
                if (localData.SaveDateTime > cloudData.SaveDateTime)
                {
                    await SaveAsync(localData);
                }
                else if (localData.SaveDateTime < cloudData.SaveDateTime)
                {
                    _localSaveService.Save(cloudData);
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to synchronize game data: {e.Message}");
                return false;
            }
        }
    }
}
