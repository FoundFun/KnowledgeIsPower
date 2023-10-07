using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        public HeroAnimator Animator;
        
        private State _state;

        public event Action HealthChanged;
        
        public float Current
        {
            get => _state.CurrentHealth;
            private set
            {
                if (_state.CurrentHealth != value)
                {
                    _state.CurrentHealth = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public float Max
        {
            get => _state.MaxHealth;
            private set => _state.MaxHealth = value;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.HeroState;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentHealth = Current;
            progress.HeroState.MaxHealth = Max;
        }

        public void TakeDamage(float damage)
        {
            if(Current <= 0)
                return;

            Current -= damage;
            Animator.PlayHit();
        }
    }
}