using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Inputs;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        public HeroAnimator Animator;
        public CharacterController CharacterController;
        private IInputService _input;

        private const string LayerHittable = "Hittable";

        private static int _layerMask;

        private Collider[] _hits = new Collider[3];
        private Stats _stats;

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();

            _layerMask = 1 << LayerMask.NameToLayer(LayerHittable);
        }

        private void Update()
        {
            if (_input.IsAttackButton() && Animator.IsAttacking)
                Animator.PlayAttack();
        }

        private void OnAttack()
        {
        }

        public void LoadProgress(PlayerProgress progress) => 
            _stats = progress.Stats;

        private int Hit() =>
            Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.DamageRadius, _hits, _layerMask);

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, CharacterController.center.y / 2, transform.position.z);
    }
}