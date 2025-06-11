using System;
using System.Collections.Generic;
using UnityEngine;

namespace TDS
{
    public class WeaponVisualController : MonoBehaviour
    {
        [SerializeField] List<GameObject> weaponList;
        [SerializeField] Transform leftHandIKTarget;

        GameObject currentWeapon = null;
        Animator animator;

        void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            ActivateWeapon(0);
        }

        void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
            {
                ActivateWeapon(0);
                ActivateWeaponLayer(1);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
            {
                ActivateWeapon(1);
                ActivateWeaponLayer(1);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha3))
            {
                ActivateWeapon(2);
                ActivateWeaponLayer(1);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha4))
            {
                ActivateWeapon(3);
                ActivateWeaponLayer(2);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha5))
            {
                ActivateWeapon(4);
                ActivateWeaponLayer(3);
            }
        }

        void ActivateWeapon(int index)
        {
            for (int i = 0; i < weaponList.Count; i++)
            {
                weaponList[i].SetActive(i == index);

                if (i == index)
                {
                    currentWeapon = weaponList[i];
                    SetupLeftHandIK();
                }
            }
        }

        private void SetupLeftHandIK()
        {
            Transform weaponLeftHandTarget = currentWeapon.GetComponentInChildren<LeftHandTarget>().transform;
            leftHandIKTarget.SetLocalPositionAndRotation(weaponLeftHandTarget.localPosition, weaponLeftHandTarget.localRotation);
        }

        void ActivateWeaponLayer(int layer)
        {
            for (int i = 1; i < animator.layerCount; i++)
            {
                animator.SetLayerWeight(i, 0);
            }
            animator.SetLayerWeight(layer, 1);
        }
    }
}
