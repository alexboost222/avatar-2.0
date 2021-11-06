using Controls;
using UnityEngine;

namespace Magic.Projectile
{
    public class WaterProjectile : MagicProjectile
    {
        public float knockback;
        public float toAir;
        
        public override MagicType Type => MagicType.Water;
        
        protected override void ApplyEffect(PlayerController pc)
        {
            pc.Rb.AddForce(transform.forward * knockback + pc.transform.up * toAir);
        }
    }
}