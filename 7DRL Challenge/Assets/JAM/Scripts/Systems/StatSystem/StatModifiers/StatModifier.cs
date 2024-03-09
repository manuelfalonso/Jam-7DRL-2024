using UnityEngine;

namespace JAM.Stats
{
    [System.Serializable]
    public abstract class StatModifier<T>
    {
        [SerializeField] public object _source;
        [SerializeField] protected T _value;

        public T Value => _value;

        public StatModifier(T value, object source)
        {
            _value = value;
            _source = source;
        }

        public StatModifier(T value) : this(value, null) { }
    }

    public class IntStatModifier : StatModifier<int>
    {
        [SerializeField] private ModifierOperationType _modType = ModifierOperationType.Additive;

        public ModifierOperationType ModType => _modType;

        public IntStatModifier(int value, ModifierOperationType modifierOperationType, object source) : base(value, source)
        {
            _modType = modifierOperationType;
        }

        public IntStatModifier(int value, ModifierOperationType modifierOperationType) : this(value, modifierOperationType, null) { }
    }

    public class FloatStatModifier : StatModifier<float>
    {
        [SerializeField] private ModifierOperationType _modType = ModifierOperationType.Additive;

        public ModifierOperationType ModType => _modType;

        public FloatStatModifier(float value, ModifierOperationType modifierOperationType, object source) : base(value, source)
        {
            _value = value;
            _modType = modifierOperationType;
        }

        public FloatStatModifier(float value, ModifierOperationType modifierOperationType) : this(value, modifierOperationType, null) { }
    }

    public class BoolStatModifier : StatModifier<bool>
    {
        public BoolStatModifier(bool value, object source) : base(value, source) { }
    }

    public enum ModifierOperationType
    {
        Additive = 100,
        Multiplicative = 200
    }
}
//EOF.