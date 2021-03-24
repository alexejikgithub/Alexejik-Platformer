using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Components
{
    public class ChangeHealthComponent : MonoBehaviour
    {
        [SerializeField] private int _healthPointsToChange;

        [SerializeField] bool _dealDamage;
        
        


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