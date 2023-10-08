using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMoveToHero : Follow
    {
        public NavMeshAgent Agent;

        private Transform _heroTransform;
        private IGameFactory _gameFactory;
        
        private void Update() => 
            SetDestinationForAgent();

        public void Construct(Transform heroTransform) => 
            _heroTransform = heroTransform;

        private void SetDestinationForAgent()
        {
            if (_heroTransform)
                Agent.destination = _heroTransform.position;
        }
    }
}