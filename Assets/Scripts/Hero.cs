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
        private Animator _animator;
        private SpriteRenderer _sprite;

        private static readonly int _isRunning = Animator.StringToHash("isRunning");
        private static readonly int _isGrounded = Animator.StringToHash("isGrounded");
        private static readonly int _verticalVelocity = Animator.StringToHash("verticalVelocity");
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _sprite = GetComponent<SpriteRenderer>();
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector2(_direction.x * _speed, _rigidbody.velocity.y);

            var isJumping = _direction.y > 0;
            var isGrounded = IsGrounded();
            if (isJumping)
            {
                if (IsGrounded() && _rigidbody.velocity.y <= 0)
                {
                    _rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
                }

            }
            else if (_rigidbody.velocity.y > 0)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
            }


            _animator.SetBool(_isRunning, _direction.x != 0);
            _animator.SetBool(_isGrounded, isGrounded);
            _animator.SetFloat(_verticalVelocity, _rigidbody.velocity.y);

            UpdateSpriteDirection();


        }
        private void UpdateSpriteDirection()
        {
            if (_direction.x > 0)
            {
                _sprite.flipX = false;

            }
            else if (_direction.x < 0)
            {
                _sprite.flipX = true;

            }

        }

        private bool IsGrounded()
        {
            return _groundCheck.IsTouchingLayer;

        }

        private void OnDrawGizmos() //Checking ray for jumping
        {
            Gizmos.color = IsGrounded() ? Color.green : Color.red;
            Gizmos.DrawSphere(transform.position, 0.3f);
        }


        public void SaySomehting()
        {
            Debug.Log("Somehing");
        }


    }

}
