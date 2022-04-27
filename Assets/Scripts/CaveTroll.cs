using UnityEngine;

class CaveTroll : Enemy
{

    Animator animator;


    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

    }

    void Update()
    {
        if (Health <= 0f)
        {
            animator.SetTrigger("walk");
        }

    }
}
