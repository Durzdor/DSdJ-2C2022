using UnityEngine;

namespace Traps
{
    public class PressurePlateTrap : MonoBehaviour
    {
        [Header("Trap Stats")] [Space(5)] [SerializeField]
        private float shootDelay = 1;

        [Header("Projectile Stats")] [Space(5)] [SerializeField]
        private int damage = 1;

        [SerializeField] private int pierce = 1;
        [SerializeField] private float knockBack = 1;
        [SerializeField] private float disruption;
        [SerializeField] private float disruptionDuration;
        [SerializeField] private LayerMask contactLayers;
        [SerializeField] [Tooltip("0 for Default")]
        private float moveSpeed;

        [SerializeField] [Tooltip("0 for Default")]
        private float duration;

        [Header("References")] [Space(5)] [SerializeField]
        private Transform shootingPoint;

        [SerializeField] private Projectile projectilePrefab;

        private float _currShootDelay;
        private bool _countdown;

        private void Start()
        {
            ResetTimer();
        }

        private void Update()
        {
            ShootTimer();
        }

        private void ShootTimer()
        {
            if (!_countdown) return;
            if (_currShootDelay > 0) _currShootDelay -= Time.deltaTime;
            if (_currShootDelay <= 0) ShootProjectile();
        }

        private void ShootProjectile()
        {
            var go = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity);
            go.StatSetup(damage, pierce, knockBack,contactLayers ,disruption, disruptionDuration, moveSpeed,
                duration);
            go.transform.forward = transform.forward;
            ResetTimer();
        }

        private void ResetTimer()
        {
            _countdown = false;
            _currShootDelay = shootDelay;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_countdown) _countdown = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!_countdown) _countdown = true;
        }
    }
}