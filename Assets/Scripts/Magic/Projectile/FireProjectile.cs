using Controls;
using UnityEngine;

namespace Magic.Projectile
{
    public class FireProjectile : MagicProjectile
    {
        public int damagePoints;
        
        public override MagicType Type => MagicType.Fire;
        
        protected override void ApplyEffect(PlayerController pc)
        {
            pc.Stats.ApplyDamage(damagePoints);
        }
    }
}