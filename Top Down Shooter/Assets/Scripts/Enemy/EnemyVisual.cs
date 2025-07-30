using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TDS
{
    public class EnemyVisual : MonoBehaviour
    {
        [SerializeField] SkinnedMeshRenderer meshRenderer;
        [SerializeField] Texture[] textureArray;
        [SerializeField] EnemyCorruptionCrystals[] corruptionCrystalsArray;
        [SerializeField] int maxCorruption;


        void Awake()
        {
            SetupRandomTexture();
            SetupCorruptionCrystals();
        }


        void SetupRandomTexture()
        {
            Texture randomTexture = textureArray[UnityEngine.Random.Range(0, textureArray.Length)];
            meshRenderer.material.mainTexture = randomTexture;
        }

        private void SetupCorruptionCrystals()
        {
            corruptionCrystalsArray = GetComponentsInChildren<EnemyCorruptionCrystals>();

            if (corruptionCrystalsArray == null || corruptionCrystalsArray.Length == 0)
                return;

            // 1. Create a list of all indices and shuffle them randomly.
            List<int> indices = Enumerable.Range(0, corruptionCrystalsArray.Length)
                                          .OrderBy(i => Random.value)
                                          .ToList();

            // 2. Deactivate all crystals first.
            for (int i = 0; i < corruptionCrystalsArray.Length; i++)
                corruptionCrystalsArray[i].gameObject.SetActive(false);

            // 3. Activate only a random set of crystals up to maxCorruption.
            for (int i = 0; i < Mathf.Min(maxCorruption, corruptionCrystalsArray.Length); i++)
                corruptionCrystalsArray[indices[i]].gameObject.SetActive(true);
        }

    }
}
