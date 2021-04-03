using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Components
{
    public class MoveBetweenPointsComponent : MonoBehaviour
    {

        [SerializeField] private Transform[] _destTransformArray;
        private int _destTransformArrayIndex=0;

        private Transform _destTransform;
        [SerializeField] private float _movingSpeed;
        
        // Start is called before the first frame update
        void Start()
        {
            _destTransform = _destTransformArray[_destTransformArrayIndex];
            
        }

        // Update is called once per frame
        void Update()
        {

            MoveTowardsTheTarget();
            SetDestination();


        }

        private void MoveTowardsTheTarget()
		{
            transform.position = Vector3.MoveTowards(transform.position, _destTransform.position, _movingSpeed*Time.deltaTime);
        }

        private void SetDestination()
		{
            if (transform.position == _destTransform.position)
            {
                _destTransformArrayIndex = (_destTransformArrayIndex + 1) == (_destTransformArray.Length)? 0: _destTransformArrayIndex + 1;
            }
            _destTransform = _destTransformArray[_destTransformArrayIndex];
        }

        
    }

}
