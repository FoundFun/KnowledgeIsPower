using System;
using CodeBase.Logic;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        public EnemyAnimator Animator;

        [SerializeField]
        private float _current;
        [SerializeField] 
        private float _max;

        public event Action HealthChanged;
        public float Current { get; private set; }
        public float Max { get; private set; }

        public void TakeDamage(float damage)
        {
            Current -= damage;
            
            Animator.PlayHit();
            
            HealthChanged?.Invoke();
        }
    }
}