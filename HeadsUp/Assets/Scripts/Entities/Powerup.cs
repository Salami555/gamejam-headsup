using System;
using UnityEngine;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

namespace Entities
{
    public class Powerup : Entity
    {
        private CircleCollider2D _circ_col;
                
        protected override void Start()
        {
            base.Start();
            _circ_col = GetComponent<CircleCollider2D>();
        }

        private void Update()
        {
        }

        protected override void OnCollisionEnter2D(Collision2D other) {
            base.OnCollisionEnter2D(other);
            if (other.gameObject.CompareTag ("Player")) {
                other.gameObject.GetComponent<Player> ().Collect (this);
                Debug.Log("Powerup got");
                Destroy(gameObject);
                Destroy(this);
            }
        }
    }
}