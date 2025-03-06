using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class GameEntryPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _shipPrefab;
        [SerializeField] private Transform _shipSpawnPoint;
        [SerializeField] private GameStateManager _gameStateManager;
        [SerializeField] private BulletFactory _bulletFactory;
        [SerializeField] private LazerFactory _lazerFactory;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private TextMeshProUGUI _endScore;
        [SerializeField] private UIShowing _uiShowing;

        private Score _score;
        private InputHandler _inputHandler;

        private void Start()
        {
            Debug.Log("Initializing GameEntryPoint");
            _score = new Score();
            GameObject shipInstance = Instantiate(_shipPrefab, _shipSpawnPoint.position, _shipSpawnPoint.rotation);
            
            var spaceShipController = shipInstance.GetComponent<SpaceShipController>();
            var gameOverUI = shipInstance.GetComponent<GameOverUI>();
            var spaceShipShooting = shipInstance.GetComponent<SpaceShipShooting>();
            _inputHandler = new InputHandler(_gameStateManager);
            spaceShipController.Initialize(_inputHandler);
            gameOverUI.Initialize(_gameOverPanel, _endScore, _score, _gameStateManager);
            _uiShowing.Initialize(spaceShipShooting, _score);
            _bulletFactory.OnBulletCreated += bullet => bullet.Initialize(_score);
            _lazerFactory.OnLazerCreated += lazer => lazer.Initialize(_score);
        }

        private void Update()
        {
            _inputHandler?.Update();
        }

        private void OnDestroy()
        {
            // Отписка от событий
            _bulletFactory.OnBulletCreated -= bullet => bullet.Initialize(_score);
            _lazerFactory.OnLazerCreated -= lazer => lazer.Initialize(_score);
        }
    }
}
