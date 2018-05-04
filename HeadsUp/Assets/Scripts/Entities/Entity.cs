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
			_rigidbody.AddForce(LocalGravity, ForceMode2D.Force);
			var movement = new Vector2(-LocalGravity.y, LocalGravity.x);
			_rigidbody.AddForce(movement * Input.GetAxis("Horizontal") * 20.0f, ForceMode2D.Force);
		}
	}
}
