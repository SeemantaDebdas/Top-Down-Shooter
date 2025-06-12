using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace TDS
{
    public enum WeaponGrabType { BackGrab, SideGrab }
    public class PlayerWeaponVisuals : MonoBehaviour
    {
        [SerializeField] List<GameObject> weaponList;
        [SerializeField] Transform leftHandIKTarget;
        [SerializeField] float rigWeightIncreaseRate = 0.15f;

        GameObject currentWeapon = null;
        Animator animator;
        Rig rig;
        bool rigShouldBeIncreased = false;
        bool isGrabbingWeapon = false;

        void Awake()
        {
            animator = GetComponent<Animator>();
            rig = GetComponentInChildren<Rig>();
            ActivateWeapon(0);
        }

        void Update()
        {
            HandleWeaponSwitch();

            if (UnityEngine.Input.GetKeyDown(KeyCode.R) && !isGrabbingWeapon)
                HandleReload();

            if (rigShouldBeIncreased)
            {
                rig.weight += rigWeightIncreaseRate * Time.deltaTime;
                if (Mathf.Approximately(rig.weight, 1))
                    rigShouldBeIncreased = false;
            }
        }

        public void ReturnRigWeightToOne() => rigShouldBeIncreased = true;

        public void FinishedGrabbingWeapon()
        {
            isGrabbingWeapon = false;
            animator.SetBool("isGrabbingWeapon", isGrabbingWeapon);
        }

        private void HandleWeaponSwitch()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
            {
                ActivateWeapon(0);
                ActivateWeaponLayer(1);
                PlayGrabAnimation(WeaponGrabType.SideGrab);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
            {
                ActivateWeapon(1);
                ActivateWeaponLayer(1);
                PlayGrabAnimation(WeaponGrabType.SideGrab);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha3))
            {
                ActivateWeapon(2);
                ActivateWeaponLayer(1);
                PlayGrabAnimation(WeaponGrabType.BackGrab);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha4))
            {
                ActivateWeapon(3);
                ActivateWeaponLayer(2);
                PlayGrabAnimation(WeaponGrabType.BackGrab);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha5))
            {
                ActivateWeapon(4);
                ActivateWeaponLayer(3);
                PlayGrabAnimation(WeaponGrabType.BackGrab);
            }
        }

        void HandleReload()
        {
            rig.weight = 0.15f;
            animator.SetTrigger("reload");
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

        void PlayGrabAnimation(WeaponGrabType grabType)
        {
            isGrabbingWeapon = true;
            animator.SetBool("isGrabbingWeapon", isGrabbingWeapon);

            rig.weight = 0;
            animator.SetFloat("weaponType", (float)grabType);
            animator.SetTrigger("grabWeapon");
        }
    }
}
