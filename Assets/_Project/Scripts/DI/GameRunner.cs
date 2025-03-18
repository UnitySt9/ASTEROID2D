using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class GameRunner : MonoBehaviour
    {
        private EntryPoint _entryPoint;
        private InputHandler _inputHandler;

        [Inject]
        private void Construct(EntryPoint entryPoint, InputHandler inputHandler)
        {
            _entryPoint = entryPoint;
            _inputHandler = inputHandler;
        }

        private void Start()
        {
            _entryPoint.StartGame();
        }

        private void Update()
        {
            _inputHandler.InputState();
        }

        private void OnDestroy()
        {
            _entryPoint.Dispose();
        }
    }
}
