using JAM.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace JAM
{
    public enum Team {Player, Ally, Enemy}

    public class EntitySensor : MonoBehaviour
    {
        [SerializeField] private ProximityTrigger _proximityTrigger;
        [SerializeField] private Team _sensorTeam;

        private bool _alreadyInitialized = false;
        private List<Entity> _myEntityList;

        public List<Entity> EntityList => _myEntityList;
        public bool AlreadyInitialized => _alreadyInitialized;
        public Entity TargetEntity { get => _myEntityList == null ? null : _myEntityList[0]; }

        public System.Action OnListChange;

        public virtual void Initialize()
        {
            if (_alreadyInitialized) { return; }

            _proximityTrigger.SetProximityTriggers(OnEnter, OnExit);
            _alreadyInitialized = true;
            _myEntityList = new List<Entity>();
        }

        public virtual void Clear()
        {
            if (!_alreadyInitialized) { return; }

            _proximityTrigger.ClearTriggers();
            _myEntityList.Clear();
            _alreadyInitialized = false;

            if (OnListChange != null) { OnListChange(); }
        }

        public void OnEnter(Collider targetCollider)
        {
            //if (!Effect.EffectSystem.IsTargetable(sensorTeam, targetCollider.tag)) { return; }
            if (!targetCollider.TryGetComponent<Entity>(out var entity)) { return; }
            //if (entity.IsDead) { return; }

            if (!_myEntityList.Contains(entity)) { _myEntityList.Add(entity); }

            //if (entity != PlayerEntity.Player_Entity && _myEntityList.Remove(PlayerEntity.Player_Entity))
            //{
            //    // HACK for now to make the player the lastTarget.
            //    _myEntityList.Add(PlayerEntity.Player_Entity);
            //}

            if (OnListChange != null) { OnListChange(); }
        }

        public void OnExit(Collider targetCollider)
        {
            //if (!Effect.EffectSystem.IsTargetable(sensorTeam, targetCollider.tag)) { return; }
            if (!targetCollider.TryGetComponent<Entity>(out var entity)) { return; }

            RemoveEntity(entity);
        }

        public void RemoveEntity(Entity entityToRemove)
        {
            if (_myEntityList.Contains(entityToRemove))
            {
                _myEntityList.Remove(entityToRemove);
            }

            if (OnListChange != null) { OnListChange(); }
        }

        public void SendToFirst(Entity entityToMove)
        {
            if (_myEntityList.Remove(entityToMove))
            {
                _myEntityList.Insert(0, entityToMove);
            }

            if (OnListChange != null) { OnListChange(); }
        }

        public void SendToLast(Entity entityToMove)
        {
            if (_myEntityList.Remove(entityToMove))
            {
                _myEntityList.Add(entityToMove);
            }

            if (OnListChange != null) { OnListChange(); }
        }

        public void SortByDistance()
        {
            if (_myEntityList == null) { return; }

            _myEntityList.Sort(CompareDistanceToMe);

            //if (_myEntityList.Remove(PlayerEntity.Player_Entity))
            //{
            //    // HACK for now to make the player the lastTarget.
            //    _myEntityList.Add(PlayerEntity.Player_Entity);
            //}

            if (OnListChange != null) { OnListChange(); }
        }


        private int CompareDistanceToMe(Entity a, Entity b)
        {
            float squaredRangeA = (a.transform.position - transform.position).sqrMagnitude;
            float squaredRangeB = (b.transform.position - transform.position).sqrMagnitude;
            return squaredRangeA.CompareTo(squaredRangeB);
        }

        public bool TryGetRandomEntity(out Entity randomEntity)
        {
            randomEntity = null;
            if (_myEntityList.Count <= 0) { return false; }

            //Removed the last one in order to avoid getting the player
            randomEntity = _myEntityList[UnityEngine.Random.Range(0, (_myEntityList.Count - 1))];
            return true;
        }

        
    }
}
//EOF.