using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singleton;

namespace JAM
{
    public class TurnSystem : MonoBehaviourSingleton<TurnSystem>
    {
        private bool playerTurn;
        private bool hasMoved;
        private bool hasAttacked;
        // list<Enemigos> enemigos;

        public Action onMove;
        public Action onAttack;
        public Action onTurnEnd;
        public Action onTurnStart;

        private void Start()
        {
            playerTurn = false;
            hasMoved = false;
            hasAttacked = false;
            onMove += OnPlayerMove;
            onAttack += OnPlayerAttack;
            onTurnEnd += OnPlayerTurnEnd;
            onTurnEnd += OnEnemiesTurn;
            onTurnStart += OnPlayerTurnStart;
            // enemigos = new list<Enemigos>();
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
            Debug.Log("OnPlayerTurnEnd");
            playerTurn = false;
        }

        private void OnPlayerTurnStart() 
        {
            Debug.Log("OnPlayerTurnStart");
            playerTurn = true;
            hasMoved = false;
            hasAttacked = false;
        }

        private void OnEnemiesTurn() 
        {
            Debug.Log("OnEnemiesTurn");
            /* for (int i = 0; i < enemigos.Length; i++)
             * {
             *      enemigos[i].Execute();
             * }
             */
            onTurnStart?.Invoke();
        }

        public void AddEnemyToList() 
        {
            // enemigos.Add(enemigo);
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
    }
}
