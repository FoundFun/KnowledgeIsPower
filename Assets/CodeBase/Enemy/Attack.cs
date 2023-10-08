using System.Linq;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        private const string LayerPlayer = "Player";
        public EnemyAnimator Animator;

        public float AttackCooldown = 3f;
        public float Cleavage = 0.5f;
        public float EffectiveDistance = 0.5f;
        public float Damage = 10f;

        private static int _layerMask;

        private Collider[] _hits = new Collider[1];
        private Transform _heroTransform;
        private float _currentAttackCooldown;
        private bool _isAttacking;
        private bool _attackIsActive;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer(LayerPlayer);
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();
        }

        public void Construct(Transform heroTransform) => 
            _heroTransform = heroTransform;

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebug(StartPoint(), Cleavage, 1);
                hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
            }
        }

        private void OnAttackEnded()
        {
            _currentAttackCooldown = AttackCooldown;
            _isAttacking = false;
        }

        public void DisableAttack() =>
            _attackIsActive = false;

        public void EnableAttack() =>
            _attackIsActive = true;

        private bool Hit(out Collider hit)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);

            hit = _hits.FirstOrDefault();

            return hitCount > 0;
        }

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
            transform.forward * EffectiveDistance;

        private bool CanAttack() =>
            _attackIsActive && !_isAttacking && CooldownIsUp();

        private bool CooldownIsUp() =>
            _currentAttackCooldown <= 0;
        
        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _currentAttackCooldown -= Time.deltaTime;
        }

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            Animator.PlayAttack();

            _isAttacking = true;
        }
    }
}