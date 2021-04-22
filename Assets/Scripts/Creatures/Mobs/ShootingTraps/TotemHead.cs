using Platformer.Components.GoBased;
using Platformer.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Creatures.Mobs.ShootingTraps

{
    [System.Serializable]
    public class TotemHead : MonoBehaviour
    {
        [SerializeField] private SpawnComponent _rangeAttack;

        private static readonly int Range = Animator.StringToHash("range");

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        [ContextMenu("Attack")]
        public void RangeAttack()
        {
            _animator.SetTrigger(Range);
        }

        
        public void OnRangeAttack()
        {
            _rangeAttack.Spawn();
        }

    }
}