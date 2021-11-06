using UnityEngine;
using UnityEngine.SceneManagement;

namespace Helpers
{
    [DisallowMultipleComponent]
    public class LoadNextSceneOnStart : MonoBehaviour
    {
        private void Start() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}