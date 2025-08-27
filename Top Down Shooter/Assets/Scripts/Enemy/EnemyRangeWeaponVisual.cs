using System;
using System.Collections.Generic;
using UnityEngine;

namespace TDS
{
    public class EnemyRangeWeaponVisual : MonoBehaviour
    {
        [SerializeField] Transform leftHandIKTarget;
        [SerializeField] Transform weaponHandler;
        [SerializeField] Transform leftHandWeaponHandler;
        [SerializeField] List<WeaponModel> weaponModels;
        Animator animator;

        public WeaponModel CurrentWeaponModel { get; private set; }
        WeaponModel leftHandWeaponModel;

        private void Awake()
        {
            //weaponModels = GetComponentsInChildren<WeaponModel>(true);
            animator = GetComponent<Animator>();
        }

        private void InitializeRandomWeaponModel(WeaponType weaponType)
        {
            int randomIndex = UnityEngine.Random.Range(0, weaponModels.Count);

            for (int i = 0; i < weaponModels.Count; i++)
            {
                weaponModels[i].gameObject.SetActive(i == randomIndex);
            }
        }

        public void InitializeWeaponModel(WeaponType weaponType)
        {
            if (weaponModels.Count == 0)
            {
                //weaponModels = GetComponentsInChildren<WeaponModel>(true);
                for (int i = 0; i < weaponHandler.childCount; i++)
                {
                    Transform child = weaponHandler.GetChild(i);
                    if (child.TryGetComponent(out WeaponModel weaponModel))
                    {
                        weaponModels.Add(weaponModel);
                    }
                }
            }

            for (int i = 0; i < weaponModels.Count; i++)
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
                    SetupLeftHandWeaponModel(weaponType);
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

        void SetupLeftHandWeaponModel(WeaponType weaponType)
        {
            for (int i = 0; i < leftHandWeaponHandler.childCount; i++)
            {
                Transform child = leftHandWeaponHandler.GetChild(i);
                if (child.TryGetComponent(out WeaponModel weaponModel))
                {
                    //child.gameObject.SetActive(weaponModel.weaponType == weaponType);

                    if (weaponModel.weaponType == weaponType)
                        leftHandWeaponModel = weaponModel;
                }
            }
        }

        public void EnableMainWeaponModel() => CurrentWeaponModel.gameObject.SetActive(true);
        public void DisableMainWeaponModel() => CurrentWeaponModel.gameObject.SetActive(false);

        public void DisableLeftHandWeaponModel() => leftHandWeaponModel.gameObject.SetActive(false);
        public void EnableLeftHandWeaponModel() => leftHandWeaponModel.gameObject.SetActive(true);
    }
}
