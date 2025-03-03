using UnityEngine;

namespace _Project.Scripts
{
    public class LazerFactory : MonoBehaviour
    {
        [SerializeField] Lazer _lazerPrefab;
        [SerializeField] Score _score;

        public void CreateLazer(Transform firePoint)
        {
            Lazer lazer = Instantiate(_lazerPrefab, firePoint.position, firePoint.rotation);
            lazer.Initialize(_score);
            _score.SubscribeToLazer(lazer);
        }
    }
}
