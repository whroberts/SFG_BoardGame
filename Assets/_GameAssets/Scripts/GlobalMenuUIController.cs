using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace menus
{
    public abstract class GlobalMenuUIController : MonoBehaviour
    {
        public void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void ExitToDesktop()
        {
            Application.Quit();
        }
    }
}
