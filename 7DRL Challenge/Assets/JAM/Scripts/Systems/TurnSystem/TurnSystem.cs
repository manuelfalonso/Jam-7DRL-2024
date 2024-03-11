using JAM.Entities.Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singleton;

namespace JAM
{
    public class TurnSystem : MonoBehaviourSingleton<TurnSystem>
    {
        private int turnNumber;
        private bool playerTurn;
        private bool hasMoved;
        private bool hasAttacked;
        public List<Enemy> enemies =new();

        public Action onMove;
        public Action onAttack;
        public Action onTurnEnd;
        public Action onTurnStart;

        protected override void Awake()
        {
            base.Awake();
            
            turnNumber = 1;
            playerTurn = true;
            hasMoved = false;
            hasAttacked = false;
            onMove += OnPlayerMove;
            onAttack += OnPlayerAttack;
            onTurnEnd += OnPlayerTurnEnd;
            onTurnEnd += OnEnemiesTurn;
            onTurnStart += OnPlayerTurnStart;
            onTurnStart += SumTurn;
        }

        private void OnPlayerMove() 
        {
            hasMoved = true;
        }

        private void OnPlayerAttack() 
        {
            hasAttacked = true;
        }

        private void OnPlayerTurnEnd() 
        {
            playerTurn = false;
        }

        private void OnPlayerTurnStart() 
        {
            playerTurn = true;
            hasMoved = false;
            hasAttacked = false;
        }

        private void OnEnemiesTurn() 
        {
             for (int i = 0; i < enemies.Count; i++)
             {
                enemies[i].Execute();
             }

             onTurnStart?.Invoke();
        }

        private void SumTurn() 
        {
            turnNumber++;
        }

        private void ResetTurns() 
        {
            turnNumber = 1;
        }

        public void AddEnemyToList(Enemy enemy) 
        {
            enemies.Add(enemy);
        }

        public void RemoveEnemyFromList(Enemy enemy) 
        {
            enemies.Remove(enemy);
        }

        public int GetNumberOfEnemies() 
        {
            return enemies.Count;
        }

        public bool IsPlayerTurn() 
        {
            return playerTurn;
        }

        public bool HasPlayerMoved() 
        {
            return hasMoved;
        }

        public bool HasPlayerAttacked() 
        {
            return hasAttacked;
        }

        public int GetTurnNumber() 
        {
            return turnNumber;
        }
        
        public void AddEnemy(Enemy enemy) 
        {
            enemies.Add(enemy);
        }

        public List<Vector3Int> GetEnemiesPositions()
        {
            List<Vector3Int> enemiesPositions = new List<Vector3Int>();
            for (int i = 0; i < enemies.Count; i++)
            {
                enemiesPositions.Add(enemies[i].MyPosition);
            }
            return enemiesPositions;
        }
    }
}
