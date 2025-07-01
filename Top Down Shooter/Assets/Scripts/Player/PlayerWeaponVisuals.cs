using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace TDS
{
    public class PlayerWeaponVisuals : MonoBehaviour
    {
        [SerializeField] WeaponModel[] weaponModels;
        [SerializeField] BackupWeaponModel[] backupWeaponModels;
        [SerializeField] Transform leftHandIKTarget;
        [SerializeField] float rigWeightIncreaseRate = 0.15f;
        Animator animator;
        PlayerWeaponController weaponController;
        Rig rig;
        bool rigShouldBeIncreased = false;

        void Awake()
        {
            animator = GetComponent<Animator>();
            rig = GetComponentInChildren<Rig>();
            weaponController = GetComponent<PlayerWeaponController>();

            weaponModels = GetComponentsInChildren<WeaponModel>(true);
            backupWeaponModels = GetComponentsInChildren<BackupWeaponModel>(true);

        }

        void Start()
        {
            SwitchOnCurrentWeaponModel();
        }

        void Update()
        {
            if (rigShouldBeIncreased)
            {
                rig.weight += rigWeightIncreaseRate * Time.deltaTime;
                if (Mathf.Approximately(rig.weight, 1))
                    rigShouldBeIncreased = false;
            }
        }


        public void OnReloadOver()
        {
            ReturnRigWeightToOne();
        }

        public void ReturnRigWeightToOne()
        {
            rigShouldBeIncreased = true;
        }


        public void PlayReloadAnimation()
        {
            rig.weight = 0.15f;
            animator.SetFloat("reloadSpeed", weaponController.CurrentWeapon.reloadSpeed);
            animator.SetTrigger("reload");
        }

        public void SwitchOnCurrentWeaponModel()
        {
            int animationLayerIdx = (int)GetCurrentWeaponModel().holdType;

            SwitchOffWeaponModels();
            SwitchOffBackupWeaponModels();
            SwitchOnBackupWeaponModels();

            ActivateWeaponLayer(animationLayerIdx);
            GetCurrentWeaponModel().gameObject.SetActive(true);
            SetupLeftHandIK();
        }

        public void SwitchOffWeaponModels()
        {
            for (int i = 0; i < weaponModels.Length; i++)
            {
                weaponModels[i].gameObject.SetActive(false);
            }
        }

        public void SwitchOnBackupWeaponModels()
        {
            List<Weapon> backupWeapons = weaponController.GetBackupWeapons();

            if (backupWeapons == null || backupWeapons.Count == 0)
                return;

            foreach (Weapon backupWeapon in backupWeapons)
            {
                for (int i = 0; i < backupWeaponModels.Length; i++)
                {
                    if (backupWeaponModels[i].weaponType == backupWeapon.weaponType)
                        backupWeaponModels[i].gameObject.SetActive(true);
                }
            }
        }

        public void SwitchOffBackupWeaponModels()
        {
            for (int i = 0; i < backupWeaponModels.Length; i++)
            {
                backupWeaponModels[i].gameObject.SetActive(false);
            }
        }


        private void SetupLeftHandIK()
        {
            Transform weaponLeftHandTarget = GetCurrentWeaponModel().holdPoint;
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

        public void PlayWeaponEquipAnimation()
        {
            WeaponEquipType equipType = GetCurrentWeaponModel().equipType;
            rig.weight = 0;
            animator.SetFloat("weaponType", (float)equipType);

            animator.SetFloat("equipSpeed", weaponController.CurrentWeapon.equipSpeed);
            animator.SetTrigger("equipWeapon");
        }

        public void PlayShootAnimation()
        {
            animator.SetTrigger("fire");
        }

        public WeaponModel GetCurrentWeaponModel()
        {
            Weapon currentWeapon = weaponController.CurrentWeapon;

            for (int i = 0; i < weaponModels.Length; i++)
            {
                if (weaponModels[i].weaponType == currentWeapon.weaponType)
                    return weaponModels[i];
            }

            return null;
        }
    }
}
