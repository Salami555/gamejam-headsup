using System;
using UnityEngine;

namespace Entities
{
	public class Entity : MonoBehaviour
	{

		public Vector2 LocalGravity;
        public GameObject ground_hit_effect;

        
		protected Rigidbody2D _rigidbody;
        
	
		// Use this for initialization
		protected virtual void Start ()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
		}

		protected virtual void FixedUpdate()
		{
			transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.down, LocalGravity));
			_rigidbody.AddForce(LocalGravity, ForceMode2D.Force);
		}

		protected virtual void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.CompareTag("Wall"))
			{
				var contactNormal = other.contacts[0].normal;
				float angle = Vector2.Angle(contactNormal, new Vector2(0, 1));
				if (angle < 5)
				{
					OnGroundTouch();
				}
			}
		}

		private void OnGroundTouch()
		{
			//TODO ground touch particles
			Vector2 touchPosition = transform.position - new Vector3(0, 1, 0);
			Camera.main.GetComponent<ShockWaveRenderer>().MakeWave(touchPosition, 0.3f);
            Instantiate(ground_hit_effect, touchPosition, ground_hit_effect.transform.rotation);
		}
	}
}
