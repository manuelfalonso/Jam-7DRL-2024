using BossRushJam2024.Entities;
using JAM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossRushJam2024.Entities.Enemy
{
    public class Enemy : Entity
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private Attack _enemyAttack;
        Astar soyUnAstar;

        protected override void Awake()
        {
            base.Awake();
            _enemyAttack.gameObject.SetActive(false);
        }

        private void Start()
        {
            TurnSystem.Instance.AddEnemyToList(this);
        }

        private void Move() 
        {
            
        }

        private void Attack() 
        {
            
        }

        public void Execute() 
        {
            Move();
            Attack();
        }

        protected override void DeathOfEntity(float toCall)
        {
            base.DeathOfEntity(toCall);
            this.gameObject.SetActive(false);
        }
    }
}
