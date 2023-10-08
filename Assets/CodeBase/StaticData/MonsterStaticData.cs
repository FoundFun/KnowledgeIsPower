using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData",menuName = "StaticData/Monster", order = 51)]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeId MonsterTypeId;
        
        [Range(1, 100)]
        public int Health;
        
        [Range(1f, 30f)]
        public float Damage;

        [Range(1f, 10f)]
        public float MoveSpeed;

        [Range(0.5f, 1F)]
        public float EffectiveDistance;
        
        [Range(0.5f, 1f)]
        public float Cleavage;

        public GameObject Prefab;
    }
}