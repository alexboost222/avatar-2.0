using UnityEngine;

namespace Magic
{
    public class MagicShield : MonoBehaviour
    {
        public MagicType Type => type;

        [SerializeField]
        private MagicType type;
        
        public void Touch(MagicProjectile from)
        {
            if (from.Type != type)
            {
                from.Dissolve();
            }
            
        }
        
        
    }
}