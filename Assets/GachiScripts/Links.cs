using UnityEngine;
using UnityEngine.SceneManagement;

namespace GachiScripts
{
    public class Links : MonoBehaviour
    {
        public void NextScene()
        {
            SceneManager.LoadScene(1);
        }

        public void Donate()
        {
            Application.OpenURL("https://qiwi.me/animetiddies");
        }
    }
}
