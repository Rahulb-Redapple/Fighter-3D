using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhotonMultiplayer
{
    public static class ExTransforms
    {
        public static void DestroyChildren(Transform parent, bool ImmediateDestroy = false)
        {
            foreach (Transform child in parent)
            {
                if(!ImmediateDestroy)
                    MonoBehaviour.Destroy(child.gameObject);
                else
                    MonoBehaviour.DestroyImmediate(child.gameObject);

            }           
        }
    }
}
