using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer
{
    public class Hero : MonoBehaviour
    {

        [SerializeField] private float _speed;
        [SerializeField] private float _jumpSpeed;
        

        [SerializeField] private LayerCheck _groundCheck;
        




        private Rigidbody2D _rigidbody;
        private Vector2 _direction;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector2(_direction.x * _speed, _rigidbody.velocity.y);

            var isJumping = _direction.y > 0;
            if(isJumping)
            {
                if(IsGrounded())
                {
                    _rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
                }
                
            }
            else if(_rigidbody.velocity.y>0)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y *0.5f);
            }
            

        }

        private bool IsGrounded()
        {
            return _groundCheck.IsTouchingLayer;

        }

        private void OnDrawGizmos() //Checking ray for jumping
        {
            Gizmos.color=IsGrounded() ? Color.green : Color.red;
            Gizmos.DrawSphere(transform.position, 0.3f);
        }


        public void SaySomehting()
        {
            Debug.Log("Somehing");
        }





    }

}
