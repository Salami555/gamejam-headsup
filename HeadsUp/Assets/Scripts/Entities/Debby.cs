using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

namespace Entities
{
    public class Debby : MonoBehaviour
    {
        public Vector2 InitialVelocityRange;
        public Vector2 InitialVelocity;
        public bool Invincibility;

        private void Start()
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Water"), LayerMask.NameToLayer("Water"));
            if (InitialVelocity.magnitude < 0.01f)
            {
                InitialVelocity = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * Random.RandomRange(InitialVelocityRange.x, InitialVelocityRange.y);
            }
            GetComponent<Rigidbody2D>().AddForce(InitialVelocity, ForceMode2D.Impulse);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            Invincibility = false;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!Invincibility)
            {
                ContactPoint2D[] data = new ContactPoint2D[1];
                other.GetContacts(data);
                var contact = data[0];
                Debug.DrawLine(new Vector3(contact.point.x, contact.point.y),
                    new Vector3(contact.point.x, contact.point.y) + new Vector3(contact.normal.x, contact.normal.y));
                var outVec = Vector2.Reflect(GetComponent<Rigidbody2D>().velocity.normalized, contact.normal);
                outVec *= GetComponent<Rigidbody2D>().velocity.magnitude;
                for (int i = 0; i < 3; i++)
                {
                    var next = Instantiate(gameObject);
                    var angle = Random.Range(-0.1f, 0.1f);
                    var s = Mathf.Sin(angle);
                    var c = Mathf.Cos(angle);
                    next.GetComponent<Debby>().InitialVelocity = new Vector2(
                        outVec.x * c - outVec.y * s,
                        outVec.x * s + outVec.y * c
                    );
                    next.GetComponent<Debby>().Invincibility = true;
                }
                Destroy(gameObject);
            }
        }
    }
}