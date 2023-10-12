using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        public HealthBar HealthBar;

        private IHealth _health;

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();

            if (health != null)
                Construct(health);
        }

        private void OnDestroy()
        {
            if (_health != null)
                _health.HealthChanged -= UpdateHealthBar;
        }

        public void Construct(IHealth health)
        {
            _health = health;

            _health.HealthChanged += UpdateHealthBar;
        }

        private void UpdateHealthBar()
        {
            HealthBar.SetValue(_health.Current, _health.Max);
        }
    }
}