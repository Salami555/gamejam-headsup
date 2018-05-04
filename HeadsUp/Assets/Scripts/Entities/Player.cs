using System;
using UnityEngine;

namespace Entities
{
    public class Player : Entity
    {
        public float MovementSpeed;
        public float JumpHeight;

        public float AerialSpeedFactor;
        public float AerialAcceleration;
        public float RotateTimeout;
        
        private InputController input;

        private CircleCollider2D _circ_col;
        private int health = 3;
        private float hit_knockback = 10;
        private float hit_threshhold = 18;//im Bereich bis ca 14 würde man beim gegen die Wand springen schaden nehmen.

        private bool jump;
        private float _rotateTimeout = 0;
        private bool _grounded = false;
        
        protected override void Start()
        {
            base.Start();
            _circ_col = GetComponent<CircleCollider2D>();
            input = GetComponent<InputController>();

        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            var movement = HorizontalVector;
            var targetVelocity = input.Horizontal * MovementSpeed;
            var targetHorizontal = movement * targetVelocity;
            var delta = targetHorizontal - HorizontalMovement;

            if (jump)
            {
                var currentVertical = new Vector2(_rigidbody.velocity.x * LocalGravity.normalized.x * LocalGravity.normalized.x, _rigidbody.velocity.y * LocalGravity.normalized.y * LocalGravity.normalized.y);
                _rigidbody.AddForce(-currentVertical, ForceMode2D.Impulse);
                var jumpForce = Mathf.Sqrt(2 * JumpHeight * LocalGravity.magnitude);
                _rigidbody.AddForce(LocalGravity.normalized * -jumpForce, ForceMode2D.Impulse);
                jump = false;
            }

            if (_grounded)
            {
                _rigidbody.AddForce(delta, ForceMode2D.Impulse);   
            }
            else if (HorizontalMovement.magnitude < MovementSpeed * AerialSpeedFactor)
            {
                _rigidbody.AddForce(targetHorizontal * AerialAcceleration, ForceMode2D.Impulse);
            }

            _grounded = false;

        }

        private void Update()
        {
            _rotateTimeout -= Time.deltaTime;
            if (_rotateTimeout < 0)
            {
                if (input.GravityTurnLeft)
                {
                    LocalGravity = new Vector2(-LocalGravity.y, LocalGravity.x);
                    _rotateTimeout = RotateTimeout;
                }
                if (input.GravityTurnRight)
                {
                    LocalGravity = new Vector2(LocalGravity.y, -LocalGravity.x);
                    _rotateTimeout = RotateTimeout;
                }
            }
            

            jump |= input.Jump;
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
            if (other.otherCollider == _circ_col)
            {
                if (other.relativeVelocity.magnitude > hit_threshhold || other.gameObject.CompareTag("Player"))
                {
                    Camera.main.GetComponent<ShockWaveRenderer>().MakeWave(new Vector2(transform.position.x, transform.position.y) + _circ_col.offset, 0.6f);
                    health--;
                    Debug.Log(health);
                    _rigidbody.AddForce(-transform.up.normalized * hit_knockback, ForceMode2D.Impulse);//Knockback nach "unten", nicht sicher, ob das so gut ist. Eine Explosion-Force wäre vielleicht passender.
                    if (health <= 0)
                    {
                        //Die
                    }
                }
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            _grounded = true;
        }
    }
}