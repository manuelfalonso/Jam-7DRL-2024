using System;
using UnityEngine;

namespace BossRushJam2024.Systems.Abilities
{
    internal static class AbilityManager
    {
        internal static bool IsReadyToExecute(GameObject owner, Ability ability)
        {
            if (!owner.TryGetComponent(out AbilitySystem abilitySystem)) { return false; }

            return abilitySystem.IsReadyToExecute(ability);
        }

        internal static void HandleAbility(GameObject owner, Ability ability)
        {
            if (!owner.TryGetComponent(out AbilitySystem abilitySystem)) { return; }

            abilitySystem.HandleAbility(ability);
        }
    }
}
