using System.Numerics;
using System.Security.Cryptography;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

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
    
    public LayerMask ContactLayer { get; private set; }

    public void StatSetup(int damage, int pierce, float knockBack,LayerMask contactLayer, float disruption = 0, float disruptionDuration = 0,
        float speed = 0,
        float life = 0)
    {
        Damage = damage;
        Pierce = pierce;
        KnockBack = knockBack;
        Disruption = disruption;
        DisruptionDuration = disruptionDuration;
        ContactLayer = contactLayer;
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
        OnHit(other.gameObject,other.ClosestPoint(other.transform.position));
        // var charStats = other.gameObject.GetComponent<CharacterStats>();
        // if (charStats != null)
        //     charStats.ChangeModifier(StatNames.MoveSpeedF, false, default, Disruption, true, DisruptionDuration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnHit(collision.gameObject,collision.contacts[0].point);
        if (Pierce <= 0) Destroy(gameObject);
    }

    private void OnHit(GameObject col, Vector3 contact)
    {
        if (!LayerCompare.IsGoInLayerMask(col, ContactLayer)) 
        {
            Destroy(gameObject); return;
        }
        var projectileDir = CalculateKnockBackDir(contact, col.transform);
            projectileDir *= KnockBack * 10000;
            
            var targetRb = col.gameObject.GetComponent<Rigidbody>();
            col.GetComponent<LifeController>().TakeDamage(Damage);
            if (targetRb != null)
            {
                targetRb.AddForce(projectileDir, ForceMode.Force);
            }
            Pierce--;
            if (Pierce <= 0) Destroy(gameObject);
    }

    private Vector3 CalculateKnockBackDir(Vector3  point, Transform target)
    {
        var contactPos = point;
        var projectileDir = (target.position - contactPos).normalized;
        return projectileDir;
    }
    private void OnTriggerStay(Collider other)
    {
        if (LayerCompare.IsGoInLayerMask(other.gameObject,ContactLayer))
            sphereSolid.enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (LayerCompare.IsGoInLayerMask(other.gameObject,ContactLayer))
            sphereSolid.enabled = true;
    }
}