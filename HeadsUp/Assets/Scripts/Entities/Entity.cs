using System;
using UnityEngine;

namespace Entities
{
	public class Entity : MonoBehaviour
	{

		public Vector2 LocalGravity;

		private Rigidbody2D _rigidbody;
		
	
		// Use this for initialization
		void Start ()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
		}
	
		// Update is called once per frame
		void Update () {
			
		}

		private void FixedUpdate()
		{
			transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.down, LocalGravity));
			_rigidbody.AddForce(LocalGravity, ForceMode2D.Force);
			var movement = new Vector2(-LocalGravity.normalized.y, LocalGravity.normalized.x);
			var targetForce = Input.GetAxis("Horizontal") * 5.0f;
			var currentHorizontal = new Vector2(_rigidbody.velocity.x * movement.x * movement.x, _rigidbody.velocity.y * movement.y * movement.y);
			var targetHorizontal = movement * targetForce;
			var delta = (targetHorizontal - currentHorizontal).normalized;

			if (Input.GetKeyDown(KeyCode.W))
			{
				_rigidbody.AddForce(LocalGravity * -30.0f, ForceMode2D.Force);
			}
			//Debug.DrawLine(transform.position, transform.position + new Vector3((movement * delta).x, (movement * delta).y, 0), Color.green);
			Debug.DrawLine(transform.position, transform.position + new Vector3(currentHorizontal.x, currentHorizontal.y, 0), Color.green);
			Debug.DrawLine(transform.position, transform.position + new Vector3(targetHorizontal.x, targetHorizontal.y, 0), Color.red);
			Debug.DrawLine(transform.position, transform.position + new Vector3(movement.x, movement.y, 0), Color.yellow);
			_rigidbody.AddForce(delta * 15.0f, ForceMode2D.Force);
		}
	}
}
