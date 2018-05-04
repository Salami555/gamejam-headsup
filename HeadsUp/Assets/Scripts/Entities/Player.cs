using UnityEngine;

namespace Entities
{
    public class Player : Entity
    {
        private InputController input;

        private CircleCollider2D _circ_col;
        private int health = 5;
        private float hit_knockback = 10;

        protected override void Start()
        {
            base.Start();
            _circ_col = GetComponent<CircleCollider2D>();
            input = GetComponent<InputController>();

        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            var movement = new Vector2(-LocalGravity.normalized.y, LocalGravity.normalized.x);
            var targetForce = input.Horizontal * 5.0f;
            var currentHorizontal = new Vector2(_rigidbody.velocity.x * movement.x * movement.x, _rigidbody.velocity.y * movement.y * movement.y);
            var targetHorizontal = movement * targetForce;
            var delta = (targetHorizontal - currentHorizontal).normalized;

            if (input.Jump)
            {
                var currentVertical = new Vector2(_rigidbody.velocity.x * LocalGravity.normalized.x * LocalGravity.normalized.x, _rigidbody.velocity.y * LocalGravity.normalized.y * LocalGravity.normalized.y);
                _rigidbody.AddForce(-currentVertical, ForceMode2D.Impulse);
                _rigidbody.AddForce(LocalGravity.normalized * -16.0f, ForceMode2D.Impulse);
            }
            //Debug.DrawLine(transform.position, transform.position + new Vector3((movement * delta).x, (movement * delta).y, 0), Color.green);
            Debug.DrawLine(transform.position, transform.position + new Vector3(currentHorizontal.x, currentHorizontal.y, 0), Color.green);
            Debug.DrawLine(transform.position, transform.position + new Vector3(targetHorizontal.x, targetHorizontal.y, 0), Color.red);
            Debug.DrawLine(transform.position, transform.position + new Vector3(movement.x, movement.y, 0), Color.yellow);
            _rigidbody.AddForce(delta * 150.0f, ForceMode2D.Force);
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
            //Braucht noch nen CircleCollider für den Kopf
            if (other.otherCollider == _circ_col)
            {
                health--;
                _rigidbody.AddForce(-transform.up.normalized * hit_knockback, ForceMode2D.Impulse);
                if (health <= 0)
                {
                    //Die
                }
            }
        }
    }
}