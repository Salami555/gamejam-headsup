using System;
using UnityEngine;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

namespace Entities
{
    public class Heart : Powerup
    {
        public override void ApplyEffect(Player player)
        {
            player.IncreaseHealth();
        }
    }
}