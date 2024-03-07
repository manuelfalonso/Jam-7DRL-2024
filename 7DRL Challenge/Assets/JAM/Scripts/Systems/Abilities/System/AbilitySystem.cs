using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JAM.Systems.Abilities
{
    public class AbilitySystem : MonoBehaviour
    {
        [SerializeField] private Ability[] _abilities = new Ability[3];

        private Dictionary<Ability, AbilityData> _abilityData = new Dictionary<Ability, AbilityData>();

        public Ability[] Abilities { get => _abilities; set => _abilities = value; }

        [Header("Debug")]
        [SerializeField] private bool _showLogs = false;


        private void Awake()
        {
            InitializeAbilities(_abilities);
        }


        internal bool IsReadyToExecute(Ability ability)
        {
            if (!_abilityData.ContainsKey(ability))
            {
                if (_showLogs) { Debug.Log($"Ability {ability.name} not found in Ability System"); }
                return false;
            }
            if (_abilityData[ability].IsOnUseTime)
            {
                if (_showLogs) { Debug.Log($"Ability {ability.name} is on use time"); }
                return false;
            }
            if (_abilityData[ability].IsOnCooldown)
            {
                if (_showLogs) { Debug.Log($"Ability {ability.name} is on cooldown"); }
                return false;
            }
            if (!_abilityData[ability].CanBeExecuted)
            {
                if (_showLogs) { Debug.Log($"Ability {ability.name} cannot be executed"); }
                return false;
            }
            if (ability.IsOneShot && _abilityData[ability].Executed)
            {
                if (_showLogs) { Debug.Log($"Ability {ability.name} is one shot and has already been executed"); }
                return false;
            }

            return true;
        }

        internal void HandleAbility(Ability ability)
        {
            StartCoroutine(HandleAbilityTimers(ability));
        }


        private void InitializeAbilities(Ability[] abilities)
        {
            foreach (var ability in abilities)
            {
                if (ability == null) { continue; }

                var data = new AbilityData
                {
                    Executed = false,
                    IsOnUseTime = false,
                    IsOnCooldown = false,
                };

                _abilityData.Add(ability, data);
            }
        }


        private IEnumerator HandleAbilityTimers(Ability ability)
        {
            if (!_abilityData.ContainsKey(ability))
            {
                if (_showLogs) { Debug.Log($"Ability {ability.name} not found in Ability System"); }
                yield break;
            }

            if (ability.HasUseTime)
            {
                ability.AbilityUseTimeStarted?.Invoke(ability, gameObject);
                _abilityData[ability].IsOnUseTime = true;
                var useTime = ability.UseRandomUseTime ? Random.Range(ability.MinUseTime, ability.MaxUseTime) : ability.UseTime;
                yield return new WaitForSeconds(useTime);
                _abilityData[ability].IsOnUseTime = false;
                ability.AbilityUseTimeFinished?.Invoke(ability, gameObject);
            }

            ability.AbilityExecuted?.Invoke(ability, gameObject);
            _abilityData[ability].Executed = true;

            if (ability.HasCooldown)
            {
                ability.AbilityCooldownStarted?.Invoke(ability, gameObject);
                _abilityData[ability].IsOnCooldown = true;
                var cooldown = ability.UseRandomCooldown ? Random.Range(ability.MinCooldown, ability.MaxCooldown) : ability.Cooldown;
                yield return new WaitForSeconds(cooldown);
                _abilityData[ability].IsOnCooldown = false;
                ability.AbilityCooldownFinished?.Invoke(ability, gameObject);
            }
        }
    }
}
