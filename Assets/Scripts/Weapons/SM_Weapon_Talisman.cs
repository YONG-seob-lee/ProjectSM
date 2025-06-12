using System;
using System.Collections;
using Characters;
using UnityEngine;

namespace Weapon
{
    public class SM_Weapon_Talisman : SM_WeaponBase
    {
        public GameObject talismanPrefab;
        public float fireDelay = 0.2f;
        public float reloadTime = 2f;
        public int maxAmmo = 6;
        private int currentAmmo;
        private bool isReloading;

        private void Start()
        {
            currentAmmo = maxAmmo;
        }

        public override void Fire()
        {
            if (isReloading || currentAmmo <= 0)
            {
                return;
            }

            StartCoroutine(FireSequence());
        }

        private IEnumerator FireSequence()
        {
            int shotsToFire = Mathf.Min(3, currentAmmo);

            for (int i = 0; i < shotsToFire; ++i)
            {
                SpawnTalisman();
                currentAmmo--;

                if (currentAmmo <= 0)
                {
                    StartCoroutine(Reload());
                    yield break;
                }

                yield return new WaitForSeconds(fireDelay);
            }
        }

        private void SpawnTalisman()
        {
            GameObject go = Instantiate(talismanPrefab, transform.position, Quaternion.identity);
            var proj = go.GetComponent<SM_TalismanProjectile>();
            proj?.Init(_owner);
        }

        private IEnumerator Reload()
        {
            isReloading = true;
            yield return new WaitForSeconds(reloadTime);
            currentAmmo = maxAmmo;
            isReloading = false;
        }
    }

    public class SM_TalismanProjectile : MonoBehaviour
    {
        public float speed = 10f;
        public float rotateSpeed = 720f;
        public float lifetime = 5f;
        private Transform target;

        public void Init(SM_UnitBase owner)
        {
            GameObject[] enemies = null;
            float minDist = float.MaxValue;

            foreach (var enemy in enemies)
            {
                float dist = Vector2.Distance(transform.position, enemy.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    target = enemy.transform;
                }
            }
            
            Destroy(gameObject, lifetime);
        }

        private void Update()
        {
            if (target == null)
            {
                return;
            }

            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
            transform.position += transform.right * (speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
        }
    }
}