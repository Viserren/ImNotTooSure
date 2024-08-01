using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class ListUtils
    {
        public static void Shuffle(List<Transform> list) // Shuffles any Transform array
        {
            for (var t = 0; t < list.Count; t++)
            {
                var tmp = list[t];
                var r = Random.Range(t, list.Count);
                list[t] = list[r];
                list[r] = tmp;
            }
        }
        
        public static void Shuffle(List<GameObject> list) // Shuffles any GameObject array
        {
            for (var t = 0; t < list.Count; t++)
            {
                var tmp = list[t];
                var r = Random.Range(t, list.Count);
                list[t] = list[r];
                list[r] = tmp;
            }
        }
    }
}
