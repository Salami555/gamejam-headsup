using System;
using UnityEngine;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

namespace Entities
{
        
    public class Shield : Powerup
    {
        public float ShieldTime;

        public override void ApplyEffect(Player player)
        {
            player.ActivateShield(ShieldTime);
        }
    }
}