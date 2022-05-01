using UnityEngine;

class EvilDragon : Enemy
{
    Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
    
    }
}
