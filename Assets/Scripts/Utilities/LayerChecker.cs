using UnityEngine;

namespace Utilities
{
    public static class LayerChecker
    {
        public static bool IsIncludeLayer(int otherLayer, LayerMask mask) =>
            (mask & (1 << otherLayer)) != 0;
    }
}