using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    public GameObject impactEffect;

    [SerializeField]
    public float speed = 70f;
    [SerializeField]
    public float explosionRadius = 0f;

    public Enemy Enemy { get; private set; }

    private Transform target;

    private float damage;

    public void Seek(Transform _target, float damage)
    {
        target = _target;
        Enemy = target.GetComponent<Enemy>();
        this.damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 bulletPosition = target.position - transform.position;
        float distanceEnemy = speed * Time.deltaTime;

        if (bulletPosition.magnitude <= distanceEnemy)
        {
            HitTarget();
            return;
        }

        transform.position += (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
        transform.transform.LookAt(target.transform);

    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);

        if(explosionRadius > 0)
        {
            Explode();
        }
        else
        {
            Enemy.ApplyDamage(damage);
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].TryGetComponent(out Enemy enemy))
            {
                enemy.ApplyDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
