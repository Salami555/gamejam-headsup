﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Entities
{
    public class Player : Entity
    {
        public enum ThrustState
        {
            FULL,
            HALF,
            ZERO
        }

        public ThrustState Thrust
        {
            get
            {
                if (input.Vertical > 0.5f)
                {
                    return ThrustState.FULL;
                } else if (input.Vertical > -0.5f)
                {
                    return ThrustState.HALF;
                } else

                {
                    return ThrustState.ZERO;
                }
            }
        }
        
        public float VerticalThrust;
        public float HalfThrust;
        public float HorizontalThrust;
        
        public float RotateTimeout;

        public GameObject hit_explosion;

        public string playerName;
        public Text winText;
        
        private InputController input;

        private CircleCollider2D _circ_col;
        public int health;
        private float hit_knockback = 10;
        private float hit_threshhold = 18;//im Bereich bis ca 14 würde man beim gegen die Wand springen schaden nehmen.

        private bool jump;
        private float _rotateTimeout = 0;
        public bool _grounded = false;
        
        protected override void Start()
        {
            base.Start();
            _circ_col = GetComponent<CircleCollider2D>();
            input = GetComponent<InputController>();

        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            Debug.DrawLine(transform.position, transform.position + new Vector3(input.Horizontal, input.Vertical, 0));

            switch (Thrust)
            {
                case ThrustState.FULL:
                    _rigidbody.AddForce(LocalGravity.normalized * -VerticalThrust);
                    break;
                case ThrustState.HALF:
                    _rigidbody.AddForce(LocalGravity.normalized * -HalfThrust, ForceMode2D.Force);
                    break;
            }
            
            var movement = HorizontalVector;
            var targetHorizontal = movement * input.Horizontal;
            _rigidbody.AddForce(targetHorizontal * HorizontalThrust, ForceMode2D.Force);

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
            

            //jump |= input.Jump;
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
            if (other.otherCollider == _circ_col)
            {
                if (other.relativeVelocity.magnitude > hit_threshhold || other.gameObject.CompareTag("Player"))
                {
                    ContactPoint2D[] contacts = new ContactPoint2D[1];
                    other.GetContacts(contacts);
                    Vector2 collision_pos =contacts[0].point;
                    Camera.main.GetComponent<ShockWaveRenderer>().MakeWave(collision_pos, 0.6f);
                    GameObject this_explosion = Instantiate(hit_explosion, collision_pos, transform.rotation);
                    health--;
                    Debug.Log(health);
                    _rigidbody.AddForce(-transform.up.normalized * hit_knockback, ForceMode2D.Impulse);//Knockback nach "unten", nicht sicher, ob das so gut ist. Eine Explosion-Force wäre vielleicht passender.
                    if (health == 0 && other.gameObject.GetComponent<Player>() != null)
                    {
                        StartCoroutine(PlayerWon(other.gameObject.GetComponent<Player>().playerName));
                    }
                }
            }
        }

        public void Collect(Powerup item)
        {
            Debug.Log("Player Powerup got");
            health = Math.Min(health + 1, 3);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            _grounded = true;
        }

        private IEnumerator PlayerWon(string winnerName)
        {
            winText.text = "Player " + winnerName + " won!";
            winText.enabled = true;
            InputController.Reset();
            yield return new WaitForSeconds(3.0f);
            SceneManager.LoadScene(0);
        }
    }
}