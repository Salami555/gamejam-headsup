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
				}
				else if (input.Vertical > -0.5f && !grounded)
				{
					return ThrustState.HALF;
				}
				else
				{
					return ThrustState.ZERO;
				}
			}
		}

		public float VerticalThrust;
		public float HalfThrust;
		public float HorizontalThrust;

		private bool slowed = false;
		private float slowedTime;

		public float RotateTimeout;

		public GameObject hit_explosion;
		public GameObject turnEffect;

		public string playerName;
		public Text winText;

		private InputController input;

		private CircleCollider2D _circ_col;
		public BoxCollider2D AliveBoxCollider, DeadBoxCollider;
		public int health;
		public bool alive
		{
			get
			{
				return health > 0;
			}
		}

		private float shieldTime;

		private float hit_threshhold = 18; //im Bereich bis ca 14 würde man beim gegen die Wand springen schaden nehmen.

		private bool jump;
		private float _rotateTimeout = 0;
		public int _framesSinceGrounded = 0;

		public bool grounded
		{
			get
			{
				return _framesSinceGrounded < 3;
			}
		}

		protected override void Start()
		{
			base.Start();
			_circ_col = GetComponent<CircleCollider2D>();
			AliveBoxCollider.enabled = true;
			DeadBoxCollider.enabled = false;
			input = GetComponent<InputController>();
		}

		protected override void FixedUpdate()
		{
			base.FixedUpdate();
			ControlThrust();
		}

		private void ControlThrust()
		{
			switch (Thrust)
			{
				case ThrustState.FULL:
					_rigidbody.AddForce(LocalGravity.normalized * -VerticalThrust);
					break;
				case ThrustState.HALF:
					_rigidbody.AddForce(LocalGravity.normalized * -HalfThrust, ForceMode2D.Force);
					break;
			}

			_framesSinceGrounded++;
		}

		private void ControlRotation()
		{
			_rotateTimeout -= Time.deltaTime;

			if (_rotateTimeout < 0)
			{
				if (input.GravityTurnLeft)
				{
					GameObject this_effect = Instantiate(turnEffect, transform);
					this_effect.GetComponent<TurnEffect>().toRotate = Quaternion.FromToRotation(transform.up, -transform.right);
					LocalGravity = new Vector2(-LocalGravity.y, LocalGravity.x);
					_rotateTimeout = RotateTimeout;
				}
				if (input.GravityTurnRight)
				{
					GameObject this_effect = Instantiate(turnEffect, transform);
					this_effect.GetComponent<TurnEffect>().toRotate = Quaternion.FromToRotation(transform.up, transform.right);
					LocalGravity = new Vector2(LocalGravity.y, -LocalGravity.x);
					_rotateTimeout = RotateTimeout;
				}
			}
		}

		private void Update()
		{
			if (alive)
			{
				UpdatePowerUps();
				ControlRotation();
				slowedTime -= Time.deltaTime;
				if (slowed && slowedTime < 0)
				{
					HalfThrust *= 2;
					VerticalThrust *= 2;
					slowed = false;
				}
			}
		}

		private void UpdatePowerUps()
		{
			shieldTime -= Time.deltaTime;
			if (shieldTime < 0)
			{
				transform.Find("cage").gameObject.SetActive(false);
			}
		}

		protected override void OnCollisionEnter2D(Collision2D other)
		{
			base.OnCollisionEnter2D(other);

			if (other.otherCollider == _circ_col)
			{
				if (other.relativeVelocity.magnitude > hit_threshhold || other.gameObject.CompareTag("Player"))
				{
					// Code für wenn man nur durch andere Spieler schaden 
					/*
                    var otherPlayer = other.gameObject.GetComponent<Player>();
                    Vector2 maxVelocity = otherPlayer._rigidbody.velocity.sqrMagnitude > _rigidbody.velocity.sqrMagnitude
                        ? otherPlayer._rigidbody.velocity
                        : _rigidbody.velocity;
                    Camera.main.GetComponent<ScreenShake>().MakeDirectedShake(0.9f, 0.4f, maxVelocity.normalized);
                    */
					Camera.main.GetComponent<ScreenShake>().MakeUndirectedShake(0.9f, 0.4f);
					ContactPoint2D[] contacts = new ContactPoint2D[1];
					other.GetContacts(contacts);
					Vector2 collision_pos = contacts[0].point;
					Camera.main.GetComponent<ShockWaveRenderer>().MakeWave(collision_pos, 0.6f);
					Instantiate(hit_explosion, collision_pos, transform.rotation);
					DecreaseHealth();
				}
			}
		}

		public void DecreaseHealth()
		{
			health--;
			if (!alive)
			{
				Kill();
			}
			else
			{
				GetComponent<PlayerSounds>().playHitSound();
			}
		}

		public void Kill()
		{
			GetComponent<PlayerSounds>().playDieSound();
			_circ_col.enabled = false;
			AliveBoxCollider.enabled = false;
			DeadBoxCollider.enabled = true;

            // check win condition
			transform.parent.GetComponent<WinManager>().CheckLives();
		}

		public void Collect(Powerup item)
		{
			Debug.Log("Player Powerup got");
			GetComponent<PlayerSounds>().playPowerupSound();
			item.ApplyEffect(this);
		}

		public void IncreaseHealth()
		{
			health = Math.Min(health + 1, 3);
		}

		public void ActivateShield(float time)
		{
			shieldTime = time;
			transform.Find("cage").gameObject.SetActive(true);
		}

		private void OnCollisionStay2D(Collision2D other)
		{

			foreach (var foo in other.contacts)
			{
				var contactNormal = foo.normal;
				float angle = Vector2.Angle(contactNormal, LocalGravity * -1);
				if (angle < 5)
				{
					_framesSinceGrounded = 0;
				}
			}
		}

		public void SlowDown(float slowTime)
		{
			var players = GameObject.FindGameObjectsWithTag("Player");
			for (int playerNumber = 0; playerNumber < players.Length; playerNumber++)
			{
				var currentPlayer = players[playerNumber]; 
				if (currentPlayer != gameObject)
				{
					var component = currentPlayer.GetComponent<Player>();
					if (component != null) component.ReduceSpeed(slowTime);
				}
			}
		}

		public void ReduceSpeed(float slowTime)
		{
			slowedTime = slowTime;
			if (slowed)
			{
				return;
			}
			slowed = true;
			VerticalThrust /= 2;
			HalfThrust /= 2;
		}
	}
}