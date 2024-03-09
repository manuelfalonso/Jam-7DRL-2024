using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace JAM
{
    [IncludeInSettings(true)]
    public class SensorController : MonoBehaviour
    {
        [SerializeField] private EntitySensor _chaseSensor;
        [SerializeField] private EntitySensor _attackSensor;

        public bool CanAttack { get; protected set; }

        private void OnEnable()
        {
            _chaseSensor.Initialize();
            _attackSensor.Initialize();
        }

        private void OnDisable()
        {
            _chaseSensor.Clear();
            _attackSensor.Clear();
        }

        public bool ChaseEntity()
        {
            return _chaseSensor.EntityList != null && _chaseSensor.EntityList.Count > 0;
        }

        public bool AttackEntity()
        {
            return _attackSensor.EntityList != null && _attackSensor.EntityList.Count > 0;
        }

        public Vector3 GetChaseTargetEntityPosition()
        {
            return _chaseSensor.EntityList == null ? Vector3.zero : _chaseSensor.EntityList[0].transform.position;
        }

        public Vector3 GetAttackTargetEntityPosition()
        {
            return _attackSensor.EntityList == null ? Vector3.zero : _attackSensor.EntityList[0].transform.position;
        }

        public Transform GetChaseTargetEntityTransform()
        {
            return _chaseSensor.EntityList == null ? null : _chaseSensor.EntityList[0].transform;
        }

        public Transform GetAttackTargetEntityTransform()
        {
            return _attackSensor.EntityList == null ? null : _attackSensor.EntityList[0].transform;
        }

        public void SetCanAttack(bool canAttack)
        {
            CanAttack = canAttack;
        }
    }
}
//EOF.