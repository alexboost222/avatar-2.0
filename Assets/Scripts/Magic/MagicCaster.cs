using System;
using UnityEngine;

namespace Magic
{
    public class MagicCaster : MonoBehaviour
    {
        public MagicShield ActiveShield => activeShield;
        
        public Transform spawnPoint;

        public MagicProjectile fireProjectile;
        public MagicProjectile waterProjectile;

        public MagicShield fireShield;
        public MagicShield waterShield;

        private MagicShield activeShield;
        
        public void CastProjectile(MagicType type)
        {
            MagicProjectile p = Instantiate(GetProjectile(type), spawnPoint.position, spawnPoint.rotation);
        }

        public void ShieldUp(MagicType type)
        {
            if (activeShield == null)
                activeShield = Instantiate(GetShield(type), spawnPoint);
            else
                Destroy(activeShield.gameObject);
        }

        private MagicProjectile GetProjectile(MagicType type) =>
            type switch
            {
                MagicType.Water => waterProjectile,
                MagicType.Fire => fireProjectile,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        
        private MagicShield GetShield(MagicType type) =>
            type switch
            {
                MagicType.Water => waterShield,
                MagicType.Fire => fireShield,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
    }
}