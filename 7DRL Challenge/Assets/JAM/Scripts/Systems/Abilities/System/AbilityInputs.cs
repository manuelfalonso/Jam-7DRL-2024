#if FROM_OTHER_JAM
using BossRushJam2024.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BossRushJam2024.Systems.Abilities
{
    public class AbilityInputs : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private AbilitySystem _abilitySystem;


        private void Start()
        {
            // _player.PlayerInputs.Player.AbilityOne.performed -= OnAbilityOne;
            // _player.PlayerInputs.Player.AbilityTwo.performed -= OnAbilityTwo;
            // _player.PlayerInputs.Player.AbilityThree.performed -= OnAbilityThree;
            //
            // _player.PlayerInputs.Player.AbilityOne.performed += OnAbilityOne;
            // _player.PlayerInputs.Player.AbilityTwo.performed += OnAbilityTwo;
            // _player.PlayerInputs.Player.AbilityThree.performed += OnAbilityThree;
        }


        #region Inputs
        public void OnAbilityOne(InputAction.CallbackContext context)
        {
            if (_abilitySystem.Abilities[0] == null)
            {
                Debug.LogError("Ability not set on Slot One");
                return;
            }
            _abilitySystem.Abilities[0].TryExecuteAbility(gameObject, null);
        }

        public void OnAbilityTwo(InputAction.CallbackContext context)
        {
            if (_abilitySystem.Abilities[1] == null)
            {
                Debug.LogError("Ability not set on Slot Two");
                return;
            }
            _abilitySystem.Abilities[1].TryExecuteAbility(gameObject, null);
        }

        public void OnAbilityThree(InputAction.CallbackContext context)
        {
            if (_abilitySystem.Abilities[2] == null)
            {
                Debug.LogError("Ability not set on Slot Three");
                return;
            }
            _abilitySystem.Abilities[2].TryExecuteAbility(gameObject, null);
        }
        #endregion
    }
}
#endif