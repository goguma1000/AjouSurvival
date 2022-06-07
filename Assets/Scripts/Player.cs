using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    Vector3 moveV;
    [SerializeField] float speed = 3f;
    [SerializeField]
    SpriteRenderer playerRenderer;
    [SerializeField]
    Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        moveV = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        moveV.x = Input.GetAxisRaw("Horizontal");
        moveV.y = Input.GetAxisRaw("Vertical");

        if (moveV.x > 0)
        {
            playerRenderer.flipX = false;
            anim.SetBool("isMove", true);
        }
        else if (moveV.x == 0) anim.SetBool("isMove", false);
        else
        {
            playerRenderer.flipX = true;
            anim.SetBool("isMove", true);
        }
        anim.SetFloat("Vertical", moveV.y);
        transform.position += new Vector3(moveV.x * speed * Time.deltaTime, moveV.y * speed * Time.deltaTime, 0);
               
    }
} 
