using System;
using UnityEngine;

namespace TDS
{
    public class PickupWeapon : Interactable
    {
        [SerializeField] WeaponData weaponData;
        Weapon weapon;

        void Awake()
        {
            weapon = new Weapon(weaponData);
            UpdateWeapon();
        }

        public void Setup(Weapon weapon, Vector3 position)
        {
            this.weapon = weapon;
            weaponData = weapon.WeaponData;

            transform.position = position;

            UpdateWeapon();
        }


        [ContextMenu("Update Active Model")]
        void UpdateWeapon()
        {
            gameObject.name = "Pickup Weapon - " + weaponData.weaponType.ToString();
            UpdateWeaponModel();
        }

        void UpdateWeaponModel()
        {
            foreach (BackupWeaponModel backupWeaponModel in GetComponentsInChildren<BackupWeaponModel>(true))
            {
                bool matchingWeaponType = backupWeaponModel.weaponType == weaponData.weaponType;

                backupWeaponModel.gameObject.SetActive(matchingWeaponType);

                if (matchingWeaponType)
                {
                    meshRenderer = backupWeaponModel.GetComponent<MeshRenderer>();
                    defaultMaterial = meshRenderer.material;
                }
            }
        }

        public override void Interact(PlayerInteraction playerInteraction)
        {
            base.Interact(playerInteraction);

            if (playerInteraction.TryGetComponent(out PlayerWeaponController controller))
            {
                controller.PickupWeapon(weapon);

                if (!ObjectPool.Instance.TryReturnObjectToPool(gameObject))
                    Destroy(gameObject);
            }
        }
    }
}
