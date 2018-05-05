﻿using System;
using UnityEngine;
using System.Collections;
using Entities;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource output;
    public AudioSource outputLouder;
    public AudioClip thrustingIgnite;
    public AudioClip thrustingLooping;
    public AudioClip playerHit;
    public AudioClip playerDie;
    public AudioClip colectPowerup;

    private Player.ThrustState? currentThrust = null;
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public void playHitSound()
    {
        outputLouder.PlayOneShot(playerHit);
    }

    public void playDieSound()
    {
        outputLouder.PlayOneShot(playerDie);
    }

    public void playPowerupSound()
    {
        outputLouder.PlayOneShot(colectPowerup);
    }

    private void Update()
    {
        if (currentThrust != player.Thrust)
        {
            switch (player.Thrust)
            {
                case Player.ThrustState.FULL:
                    output.volume = 1.0f;
                    if (currentThrust == Player.ThrustState.ZERO)
                    {
                        StopCoroutine("Thrusting");
                        StartCoroutine("Thrusting"); 
                    }
                    break;
                case Player.ThrustState.HALF:
                    output.volume = 0.5f;
                    if (currentThrust == Player.ThrustState.ZERO)
                    {
                        StopCoroutine("Thrusting");
                        StartCoroutine("Thrusting");
                    }
                    break;
                case Player.ThrustState.ZERO:
                    StopCoroutine("Thrusting");
                    output.Pause();
                    break;
                default:
                    break;
            }
            currentThrust = player.Thrust;
        }

    }

    public IEnumerator Thrusting()
    {
        output.loop = false;
        output.clip = thrustingIgnite;
        output.Play();
        yield return new WaitForSeconds(thrustingIgnite.length);
        output.loop = true;
        output.clip = thrustingLooping;
        output.Play();
    }
}
