using UnityEngine;

namespace Magic
{
    public class MagicSource : MonoBehaviour
    {
        public MagicType type;

        public bool IsRegenerated { get; private set; } = true;

    }
}