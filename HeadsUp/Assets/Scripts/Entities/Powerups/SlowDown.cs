using System;
using UnityEngine;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

namespace Entities
{
    public class SlowDown : Powerup
    {
        public float SlowTime;
        
        public override void ApplyEffect(Player player)
        {
            player.SlowDown(SlowTime);
        }
    }
}