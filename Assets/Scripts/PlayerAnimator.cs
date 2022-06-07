using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator anim;
    public float horizontal;
    public float vertical;
    public bool isRun = false;
    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);
        anim.SetBool("isMove",isRun);
    }


}
