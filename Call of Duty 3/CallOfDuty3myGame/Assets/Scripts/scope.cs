using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scope : MonoBehaviour
{
    public Animator animator;

    private bool isScoped = false;

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            isScoped = !isScoped;
            animator.SetBool("Scope", isScoped);
        }
    }
}
