using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class DeathOfShip : MonoBehaviour
    {
        [SerializeField] GameObject _gameOverPanel;
        [SerializeField] TextMeshProUGUI _endScore;
        [SerializeField] Score _score;
        
        private void OnCollisionEnter2D()
        {
            _gameOverPanel.SetActive(true);
            _endScore.text = "GAME OVER. SCORE: " + _score.Count;
            Time.timeScale = 0;
        }
    }
}
