using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Components.Health
{
    public class ChangeHealthComponent : MonoBehaviour
    {
        [Range(0,100)]
        [SerializeField] private int _healthPointsToChange;
        [SerializeField] private ChangingHealsStates _healOrDamage;


        public bool _dealHealing =>_healOrDamage == ChangingHealsStates.Heal;
        public bool _dealDamage => _healOrDamage == ChangingHealsStates.Damage;
        
        private enum ChangingHealsStates
		{
            Heal,
            Damage
		}
        


        public void ChangeHealthAmount(GameObject target)
		{
            var healthComponent = target.GetComponent<HealthComponent>();
            if(healthComponent!=null)
			{
                if(_dealDamage)
				{
                    healthComponent.ApplyDamage(_healthPointsToChange);
                }
                else 
                {
                    healthComponent.ApplyHealing(_healthPointsToChange);
                }


            }
		}
    }
}