using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    Vector3 moveV;
    [SerializeField] float speed = 3f;
    PlayerAnimator anim;

    // Start is called before the first frame update
    void Awake()
    {
        moveV = new Vector3();
        anim = GetComponent<PlayerAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveV.x = Input.GetAxisRaw("Horizontal");
        moveV.y = Input.GetAxisRaw("Vertical");

        anim.horizontal = moveV.x;
        anim.vertical = moveV.y;

        transform.position += new Vector3(moveV.x * speed * Time.deltaTime, moveV.y * speed * Time.deltaTime, 0);
               
    }
} 
