using System;

namespace CodeBase.UI
{
    public interface IHealth
    {
        event Action HealthChanged;
        float Current { get; }
        float Max { get; }
    }
}