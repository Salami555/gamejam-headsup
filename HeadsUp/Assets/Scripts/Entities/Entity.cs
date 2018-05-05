using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Entities
{
	public class Entity : MonoBehaviour
	{

		public Vector2 LocalGravity;
        public GameObject ground_hit_effect;

        
		protected Rigidbody2D _rigidbody;

		protected Vector2 HorizontalVector
		{
			get { return new Vector2(-LocalGravity.normalized.y, LocalGravity.normalized.x); }
		}
		
		protected Vector2 HorizontalMovement
		{
			get
			{
				var horizontal = HorizontalVector;
				return new Vector2(horizontal.x * horizontal.x * _rigidbody.velocity.x, horizontal.y * horizontal.y * _rigidbody.velocity.y);
			}
		}
		
		protected Vector2 VerticalMovement
		{
			get
			{
				var gravity = LocalGravity.normalized;
				return new Vector2(gravity.x * gravity.x * _rigidbody.velocity.x, gravity.y * gravity.y * _rigidbody.velocity.y);
			}
		}
        
	
		// Use this for initialization
		protected virtual void Start ()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
		}

		protected virtual void FixedUpdate()
		{
            if (CompareTag("Player"))
            {
                transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.down, LocalGravity));
            }
			_rigidbody.AddForce(LocalGravity, ForceMode2D.Force);
		}

		protected virtual void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.CompareTag("Wall"))
			{
                if (other.contacts.Length > 0)
                {
                    var contactNormal = other.contacts[0].normal;

		            Camera.main.GetComponent<ScreenShake>().MakeDirectedShake(0.2f, 0.2f, _rigidbody.velocity.normalized);
	                
                    float angle = Vector2.Angle(contactNormal, LocalGravity * -1);
                    if (angle < 5)
                    {
                        OnGroundTouch();
                    }
                }
			}
		}

		private void OnGroundTouch()
		{
			//TODO ground touch particles
			Vector3 offset = -LocalGravity.normalized;
			Vector2 touchPosition = transform.position - offset;
			Camera.main.GetComponent<ShockWaveRenderer>().MakeWave(touchPosition, 0.3f);
            Instantiate(ground_hit_effect, touchPosition, transform.rotation);
		}
	}
}
