using TMPro;
using UnityEngine;
using DG.Tweening;

namespace _Project.Scripts
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private TextMeshProUGUI _endScore;
        [SerializeField] private TextMeshProUGUI _highScore;
        [SerializeField] private GameObject _newRecordLabel;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = _gameOverPanel.GetComponent<CanvasGroup>();
            if (_canvasGroup == null)
            {
                _canvasGroup = _gameOverPanel.AddComponent<CanvasGroup>();
            }
            _canvasGroup.alpha = 0f;
            _gameOverPanel.SetActive(false);
        }

        public void ShowPanel(int score, int highScore, bool isNewRecord)
        {
            _gameOverPanel.SetActive(true);
            _endScore.text = $"GAME OVER. SCORE: {score}";
            _highScore.text = $"BEST SCORE: {highScore}";
            _newRecordLabel.SetActive(isNewRecord);
            
            _canvasGroup.DOFade(1f, 2f).SetEase(Ease.OutQuad);
            _gameOverPanel.transform.localScale = Vector3.one * 7f;
            _gameOverPanel.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 1f).SetEase(Ease.OutBack);
        }
        
        public void HidePanel()
        {
            _canvasGroup.DOFade(0f, 0.3f).OnComplete(() => _gameOverPanel.SetActive(false));
        }
    }
}
