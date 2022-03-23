using UnityEngine;

public class TargetPoint : MonoBehaviour
{

    public Enemy Enemy { get; private set; }

    public Vector3 Position => transform.position;

    void Awake()
    {
        Enemy = transform.root.GetComponent<Enemy>();
        Debug.Assert(gameObject.layer == 9, "Point cible sur le mauvais calque!", this);
        Debug.Assert(
            GetComponent<SphereCollider>() != null,
            "Target point without sphere collider!", this
        );

    }


}