using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        public HeroHealth Health;
        
        public HeroMove Move;
        public HeroAnimator Animator;
        
        public GameObject DeathFX;
        private bool _isDead;

        private void Start() => 
            Health.HealthChanged += OnHealthChange;

        private void OnDestroy() => 
            Health.HealthChanged -= OnHealthChange;

        private void OnHealthChange()
        {
            if (!_isDead && Health.Current <= 0)
                Die();
        }

        private void Die()
        {
            _isDead = true;
            DisableMove();
            Animator.PlayDeath();

            Instantiate(DeathFX, transform.position, Quaternion.identity);
        }

        private void DisableMove() => 
            Move.enabled = false;
    }
}