using UnityEngine;

public class CharacterM : MonoBehaviour, IVel
{
    public static class Helpers 
    {
        private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        public static Vector3 ToIso(Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
    }
    private Camera cameraObject;
    [SerializeField] private LayerMask mouseColliderLayerMask;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject volleyPrefab;
    [SerializeField] private LayerMask contactLayers;
    [SerializeField] private bool IsometricMovement;
    public Rigidbody RigidBody { get; private set; }
    public CharacterStats Stats { get; private set; }
    public LifeController Health { get; private set; }

    private float _lastMoveMagnitude;

    public Vector3 GetMouseWorldPosition()
    {
        var ray = cameraObject.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out var raycastHit, 100f, mouseColliderLayerMask) ? raycastHit.point : Vector3.zero;
    }
    public void LookAtMouse()
    {
        var lookAt = GetMouseWorldPosition();
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);
    }
    private void Awake()
    {
        cameraObject = Camera.main;
        //if (cameraObject != null) cameraObject.gameObject.GetComponent<CameraController>().AssignTarget(this.transform);
        RigidBody = GetComponent<Rigidbody>();  
        Stats = GetComponent<CharacterStats>();
        Health = GetComponent<LifeController>();
    }

    private void Start()
    {
        Health.AssignLife(Stats.MaxHealth);
    }

    public void Move(Vector3 dir)
    {
        var value = Vector3.ClampMagnitude(dir, 1);
        if (IsometricMovement)
        {
            RigidBody.MovePosition(transform.position + Helpers.ToIso(value) * Stats.TotalMoveSpeed * Time.deltaTime);
        }
        else
        {
            RigidBody.velocity = new Vector3(value.x * Stats.TotalMoveSpeed, RigidBody.velocity.y,
                value.z * Stats.TotalMoveSpeed);
        }

    }

    public void Shoot()
    {
        CheckSpread();
        VolleySpawn();
    }

    private void CheckSpread()
    {
        var mod = 0f;
        if (Stats.TotalSpread % 2 == 0)
        {
            mod += 2;
            for (var i = 0; i < Stats.TotalSpread; i++)
            {
                switch (i % 2)
                {
                    case 0:
                        ProjectileSpawn(mod);
                        continue;
                    case 1:
                        ProjectileSpawn(-mod);
                        break;
                }

                mod += 2;
            }
        }
        else
        {
            for (var i = 0; i < Stats.TotalSpread; i++)
            {
                switch (i % 2)
                {
                    case 1:
                        ProjectileSpawn(-mod);
                        continue;
                    case 0:
                        ProjectileSpawn(mod);
                        break;
                }

                mod += 2;
            }
        }
    }

    private void ProjectileSpawn(float angle)
    {
        var go = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        var projectile = go.GetComponent<Projectile>();
        projectile.StatSetup(Stats.TotalDamage, Stats.TotalPierce, Stats.TotalKnockBack,contactLayers,Stats.TotalDisruption,
            Stats.DisruptionDuration);
        projectile.transform.forward = Quaternion.Euler(0f, angle, 0f) * transform.forward;
    }

    public void CharacterInteraction(Interactable interactable)
    {
        interactable.Interaction();
    }

    private void VolleySpawn()
    {
        var go = Instantiate(volleyPrefab, GetMouseWorldPosition(), Quaternion.identity);
        var aoe = go.GetComponent<AreaOfEffect>();
        aoe.Init(Stats.TotalVolleyArea, Stats.TotalDamage, Stats.VolleyDuration);
    }

    public float Vel {    
        get => _lastMoveMagnitude;
        private set => _lastMoveMagnitude = value;
    }

}