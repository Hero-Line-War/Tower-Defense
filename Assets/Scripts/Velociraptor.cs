using UnityEngine;

class Velociraptor : Enemy
{

    Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

}
