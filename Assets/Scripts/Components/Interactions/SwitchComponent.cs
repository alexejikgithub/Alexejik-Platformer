using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Components.Interactions
{
    public class SwitchComponent : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private bool _state;
        [SerializeField] private string _anomationKey;

        public void Swich()
		{
            _state = !_state;

            _animator.SetBool(_anomationKey, _state);

		}

        [ContextMenu("Swich")]
        public void SwichIt()
		{
            Swich();

        }

    }
}
