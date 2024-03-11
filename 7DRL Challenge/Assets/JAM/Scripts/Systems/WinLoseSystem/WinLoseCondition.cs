using System;
using JAM.TileMap;
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
            TileMapManager.Instance.GenerateRandomTileMap();
        }

        private void CheckWinCondition()
        {
            if (TurnSystem.Instance.GetNumberOfEnemies() == 0)
                onWin?.Invoke();
        }

        private void OnGameOver() 
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
