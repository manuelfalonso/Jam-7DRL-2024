using NaughtyAttributes;
using System;
using UnityEngine;

namespace JAM.Systems.Abilities
{
    public abstract class Ability : ScriptableObject
    {
        [Header("Properties")]
        [Obsolete("Not implemented")]
        [SerializeField] protected int _level = 1;
        [SerializeField] protected string _name;
        [TextArea(5, 10)]
        [SerializeField] protected string _abilityDescription;
        [Tooltip("The lower the value the highest the priority")]
        [Obsolete("Not implemented")]
        [SerializeField] protected int _priority = 1;
        [SerializeField] protected bool _isOneShot;

        [Header("UI")]
        [SerializeField] protected bool _hasIcon;
        [ShowIf(nameof(_hasIcon))]
        [SerializeField] protected Sprite _icon;
        [ShowIf(nameof(_hasIcon))]
        [SerializeField] protected Sprite _iconBackground;

        [Header("Use time")]
        [SerializeField] protected bool _hasUseTime;
        [ShowIf(nameof(_hasUseTime))]
        [HideIf(nameof(_useRandomUseTime))]
        [SerializeField] protected float _useTime;
        [SerializeField] protected bool _useRandomUseTime;
        [ShowIf(nameof(_useRandomUseTime))]
        [SerializeField] protected float _minUseTime;
        [ShowIf(nameof(_useRandomUseTime))]
        [SerializeField] protected float _maxUseTime;

        [Header("Cooldown")]
        [SerializeField] private bool _hasCooldown;
        [ShowIf(nameof(_hasCooldown))]
        [HideIf(nameof(_useRandomCooldown))]
        [SerializeField] private float _cooldown;
        [SerializeField] private bool _useRandomCooldown;
        [ShowIf(nameof(_useRandomCooldown))]
        [SerializeField] private float _minCooldown;
        [ShowIf(nameof(_useRandomCooldown))]
        [SerializeField] private float _maxCooldown;

        [Header("Debug")]
        [SerializeField] private bool _showLogs = false;

        public Action<Ability, GameObject> AbilityUseTimeStarted;
        public Action<Ability, GameObject> AbilityUseTimeFinished;
        public Action<Ability, GameObject> AbilityCooldownStarted;
        public Action<Ability, GameObject> AbilityCooldownFinished;
        public Action<Ability, GameObject> AbilityExecuted;

        // Getters
        // Properties
        public int Level { get => _level; private set => _level = value; }
        public string Name { get => _name; private set => _name = value; }
        public string AbilityDescription { get => _abilityDescription; private set => _abilityDescription = value; }
        public int Priority { get => _priority; private set => _priority = value; }
        public bool IsOneShot { get => _isOneShot; private set => _isOneShot = value; }

        // UI
        public bool HasIcon { get => _hasIcon; private set => _hasIcon = value; }
        public Sprite Icon { get => _icon; private set => _icon = value; }
        public Sprite IconBackground { get => _iconBackground; private set => _iconBackground = value; }

        // Use Time
        public bool HasUseTime { get => _hasUseTime; private set => _hasUseTime = value; }
        public float UseTime { get => _useTime; private set => _useTime = value; }
        public bool UseRandomUseTime { get => _useRandomUseTime; private set => _useRandomUseTime = value; }
        public float MinUseTime { get => _minUseTime; private set => _minUseTime = value; }
        public float MaxUseTime { get => _maxUseTime; private set => _maxUseTime = value; }

        // Cooldown
        public bool HasCooldown { get => _hasCooldown; private set => _hasCooldown = value; }
        public float Cooldown { get => _cooldown; private set => _cooldown = value; }
        public bool UseRandomCooldown { get => _useRandomCooldown; private set => _useRandomCooldown = value; }
        public float MinCooldown { get => _minCooldown; private set => _minCooldown = value; }
        public float MaxCooldown { get => _maxCooldown; private set => _maxCooldown = value; }


        #region Unity Callbacks
        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }
        #endregion


        #region Public Methods
        public virtual bool TryExecuteAbility(GameObject owner, GameObject target)
        {
            if (CanExecuteAbility(owner, target))
            {
                ExecuteAbility(owner, target);
                return true;
            }
            return false;
        }
        #endregion


        #region Protected Methods
        protected virtual bool CanExecuteAbility(GameObject owner, GameObject target)
        {
            return AbilityManager.IsReadyToExecute(owner, this);
        }

        protected virtual void ExecuteAbility(GameObject owner, GameObject target)
        {
            if (_showLogs) { Debug.Log($"Ability {_name} Started"); }

            AbilityManager.HandleAbility(owner, this);
        }
        #endregion


        #region Listeners
        protected virtual void UseTimeStarted(Ability ability, GameObject owner)
        {
            if (_showLogs) { Debug.Log($"Ability {_name} Use Time Started"); }
        }

        protected virtual void UseTimeFinished(Ability ability, GameObject owner)
        {
            if (_showLogs) { Debug.Log($"Ability {_name} Use Time Finished"); }
        }

        protected virtual void CooldownStarted(Ability ability, GameObject owner)
        {
            if (_showLogs) { Debug.Log($"Ability {_name} Cooldown Started"); }
        }

        protected virtual void CooldownFinished(Ability ability, GameObject owner)
        {
            if (_showLogs) { Debug.Log($"Ability {_name} Cooldown Finished"); }
        }

        protected virtual void Executed(Ability ability, GameObject owner)
        {
            if (_showLogs) { Debug.Log($"Ability {_name} Executed"); }
        }
        #endregion


        #region Private Methods
        private void AddListeners()
        {
            if (_hasUseTime)
            {
                AbilityUseTimeStarted -= UseTimeStarted;
                AbilityUseTimeStarted += UseTimeStarted;
                AbilityUseTimeFinished -= UseTimeFinished;
                AbilityUseTimeFinished += UseTimeFinished;
            }

            AbilityExecuted -= Executed;
            AbilityExecuted += Executed;

            if (_hasCooldown)
            {
                AbilityCooldownStarted -= CooldownStarted;
                AbilityCooldownStarted += CooldownStarted;
                AbilityCooldownFinished -= CooldownFinished;
                AbilityCooldownFinished += CooldownFinished;
            }
        }

        private void RemoveListeners()
        {
            if (_hasUseTime)
            {
                AbilityUseTimeStarted -= UseTimeStarted;
                AbilityUseTimeFinished -= UseTimeFinished;
            }

            AbilityExecuted -= Executed;

            if (_hasCooldown)
            {
                AbilityCooldownStarted -= CooldownStarted;
                AbilityCooldownFinished -= CooldownFinished;
            }
        }
        #endregion
    }
}
