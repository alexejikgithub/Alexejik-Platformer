using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer
{
    public class Hero : MonoBehaviour
    {

        [SerializeField] private float _speed;

<<<<<<< HEAD
        [SerializeField] private LayerCheck _groundCheck;
        
        
=======
        private Vector3 _direction;
>>>>>>> parent of 1c8d75b (Jumping + movable box)

        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }

        private void Update()
        {
            if (_direction != Vector3.zero)
            {
                var deltaX = _direction.x * _speed * Time.deltaTime;
                var newXposition = transform.position.x + deltaX;
                var deltaY = _direction.y * _speed * Time.deltaTime;
                var newYposition = transform.position.y + deltaY;

                transform.position = new Vector3(newXposition, newYposition, transform.position.z);
            }
        }
        public void SaySomehting()
        {
            Debug.Log("Somehing");
        }





    }

}
