using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class SceneLoader : ISceneLoader
    {
        private const int MENU_INDEX = 0;
        
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LoadMenu()
        {
            SceneManager.LoadScene(MENU_INDEX);
        }
    }
}
