using UnityEngine;

namespace _Project.Scripts
{
    public class LazerFactory : MonoBehaviour
    {
        [SerializeField] private Lazer _lazerPrefab;

        public void CreateLazer(Transform firePoint)
        {
            Instantiate(_lazerPrefab, firePoint.position, firePoint.rotation);
        }
    }
}
