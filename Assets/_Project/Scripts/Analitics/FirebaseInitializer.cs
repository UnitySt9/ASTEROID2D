using Firebase;
using Firebase.Analytics;
using UnityEngine;

namespace _Project.Scripts
{
    public class FirebaseInitializer : MonoBehaviour
    {
        private DependencyStatus _dependencyStatus = DependencyStatus.UnavailableOther;

        private void Start()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                _dependencyStatus = task.Result;
                if (_dependencyStatus == DependencyStatus.Available)
                {
                    FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                    Debug.Log("Firebase Analytics initialized");
                }
                else
                {
                    Debug.LogError($"Could not resolve all Firebase dependencies: {_dependencyStatus}");
                }
            });
        }
    }
}
