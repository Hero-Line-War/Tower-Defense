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
        if(Health <= 0f)
        {
            animator.SetTrigger("Fly Forward");
        }

        transform.position = new Vector3(transform.position.x, 1, transform.position.z);

    }
}
