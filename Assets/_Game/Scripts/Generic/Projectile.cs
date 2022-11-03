using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float lifespan = 2f;
    [SerializeField] private Collider sphereSolid;

    public int Damage { get; private set; }
    public int Pierce { get; private set; }
    public float KnockBack { get; private set; }
    public float Disruption { get; private set; }
    public float DisruptionDuration { get; private set; }

    public void StatSetup(int damage, int pierce, float knockBack, float disruption = 0, float disruptionDuration = 0,
        float speed = 0,
        float life = 0)
    {
        Damage = damage;
        Pierce = pierce;
        KnockBack = knockBack;
        Disruption = disruption;
        DisruptionDuration = disruptionDuration;
        if (speed != 0) moveSpeed = speed;
        if (life != 0) lifespan = life;
    }

    private void Start()
    {
        Destroy(gameObject, lifespan);
    }

    private void Update()
    {
        var t = transform;
        t.position += t.forward * (moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var hp = other.gameObject.GetComponent<Health>();
        var charStats = other.gameObject.GetComponent<CharacterStats>();
        if (charStats != null)
            charStats.ChangeModifier(StatNames.MoveSpeedF, false, default, Disruption, true, DisruptionDuration);
        if (hp == null) return;
        hp.TakeDamage(Damage);
        Pierce--;
        if (Pierce <= 0) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;
        var contact = collision.contacts[0];
        var contactPos = contact.point;
        var projectileDir = (collision.gameObject.transform.position - contactPos).normalized;
        projectileDir *= KnockBack * 10000;
        collision.gameObject.GetComponent<Rigidbody>().AddForce(projectileDir, ForceMode.Force);
        if (Pierce <= 0) Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("CanPierce"))
            sphereSolid.enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("CanPierce"))
            sphereSolid.enabled = true;
    }
}