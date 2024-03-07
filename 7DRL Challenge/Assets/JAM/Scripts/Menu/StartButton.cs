using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JAM
{
    public class StartButton : MonoBehaviour
    {
        [SerializeField] private string sceneName;

        public void StartLevel() 
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
