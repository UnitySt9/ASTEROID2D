using System.Collections;
using UnityEngine;

namespace _Project.Scripts
{
    public class CoroutineHelper
    {
        private MonoBehaviour _coroutineRunner;

        public CoroutineHelper(MonoBehaviour runner)
        {
            _coroutineRunner = runner;
        }

        public void StartCoroutine(IEnumerator coroutine)
        {
            if (_coroutineRunner != null)
            {
                _coroutineRunner.StartCoroutine(coroutine);
            }
        }
    }
}
