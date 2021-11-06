using Magic;
using UnityEngine;

namespace Controls
{
    public class MagicController : MonoBehaviour
    {
        public MagicType? CurrentMagic { get; private set; }

        public LayerMask magicSourceMask;
        public float magicExtractionDistance;

        public MagicCaster caster;

        [SerializeField]
        private Transform head;
        [SerializeField]
        private PlayerController playerController;
        
        private void OnEnable()
        {
            playerController.PrimaryAction += Fire;
            playerController.TakeSource += ShieldUp;
        }

        
        private void OnDisable()
        {
            playerController.PrimaryAction -= Fire;
            playerController.TakeSource -= ShieldUp;
        }
        
        private void ShieldUp()
        {
            if (!CurrentMagic.HasValue)
                return;
            
            if (caster.ActiveShield == null)
            {
                caster.ShieldUp(CurrentMagic.Value);
            }
            else if (caster.ActiveShield != null)
            {
                caster.ShieldUp(CurrentMagic.Value);
            }
        }

        private void Fire()
        {
            if (caster.ActiveShield != null)
                return;
            
            if (!CurrentMagic.HasValue)
            {
                HandleExtractSource();
            }
            else
            {
                caster.CastProjectile(CurrentMagic.Value);
                CurrentMagic = null;
            }
        }

        private void HandleExtractSource()
        {
            if(!Physics.Raycast(head.position, head.forward,  out var hit, magicExtractionDistance, magicSourceMask))
                return;

            var source = hit.collider.GetComponentInParent<MagicSource>();

            if (source.IsRegenerated && !CurrentMagic.HasValue)
            {
                CurrentMagic = source.type;
            }
                
        }
    }
}