using System;
using UnityEngine;

namespace TDS
{
    public class EnemyRangeWeaponVisual : MonoBehaviour
    {
        [SerializeField] Transform leftHandIKTarget;
        [SerializeField] WeaponModel[] weaponModels;
        Animator animator;

        public WeaponModel CurrentWeaponModel { get; private set; }

        private void Awake()
        {
            weaponModels = GetComponentsInChildren<WeaponModel>(true);
            animator = GetComponent<Animator>();
        }

        private void InitializeRandomWeaponModel(WeaponType weaponType)
        {
            int randomIndex = UnityEngine.Random.Range(0, weaponModels.Length);

            for (int i = 0; i < weaponModels.Length; i++)
            {
                weaponModels[i].gameObject.SetActive(i == randomIndex);
            }
        }

        public void InitializeWeaponModel(WeaponType weaponType)
        {
            if (weaponModels.Length == 0)
                weaponModels = GetComponentsInChildren<WeaponModel>(true);

            for (int i = 0; i < weaponModels.Length; i++)
            {
                WeaponModel weaponModel = weaponModels[i];

                weaponModel.gameObject.SetActive(weaponModel.weaponType == weaponType);

                if (weaponModel.weaponType == weaponType)
                {
                    //-1 because we're doing the same thing for player as well. Player has a different animator setup. Check layer for both player and enemy.
                    //e.g: Shotgun Layer for Player: 3, Shotgun Layer for Enemy: 2
                    CurrentWeaponModel = weaponModel;
                    ActivateWeaponLayer((int)weaponModel.holdType - 1);
                    SetupLeftHandIK(weaponModel.holdPoint);
                }
            }
        }

        void ActivateWeaponLayer(int layer)
        {
            for (int i = 1; i < animator.layerCount; i++)
            {
                animator.SetLayerWeight(i, 0);
            }
            animator.SetLayerWeight(layer, 1);
        }

        private void SetupLeftHandIK(Transform holdPoint)
        {
            Transform weaponLeftHandTarget = holdPoint;
            leftHandIKTarget.SetLocalPositionAndRotation(weaponLeftHandTarget.localPosition, weaponLeftHandTarget.localRotation);
        }
    }
}
