using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.Singleton;

namespace JAM
{
    public class WinLoseCondition : MonoBehaviourSingleton<WinLoseCondition>
    {
        public Action onWin;
        public Action onLose;

        private void Awake()
        {
            onWin += OnVictory;
            onLose += OnGameOver;
        }

        private void OnVictory() 
        {
            if (TurnSystem.Instance.GetNumberOfEnemies() == 0)
                SceneManager.LoadScene("Main Gameplay");
        }

        private void OnGameOver() 
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
