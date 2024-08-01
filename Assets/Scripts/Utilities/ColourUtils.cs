using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utilities
{
    public class ColourUtils : MonoBehaviour
    {
        private void Start()
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            Material material = new Material(meshRenderer.material);

            float r = Random.Range(0f, 1f);
            float g = Random.Range(0f, 1f);
            float b = Random.Range(0f, 1f);

            material.color = new Color(r, g, b);

            meshRenderer.material = material;
        }
    }
}