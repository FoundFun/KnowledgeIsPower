using UnityEngine;

namespace CodeBase.UI
{
    public class ActorUI : MonoBehaviour
    {
        public HealthBar HealthBar;

        private IHealth _heroHealth;

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();
            
            if(health != null)
                Construct(health);
        }

        private void OnDestroy() =>
            _heroHealth.HealthChanged -= UpdateHealthBar;

        public void Construct(IHealth heroHealth)
        {
            _heroHealth = heroHealth;

            _heroHealth.HealthChanged += UpdateHealthBar;
        }

        private void UpdateHealthBar()
        {
            HealthBar.SetValue(_heroHealth.Current, _heroHealth.Max);
        }
    }
}