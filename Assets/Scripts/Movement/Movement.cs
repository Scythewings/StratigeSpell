using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private Transform _movePoint;
    [SerializeField] private LayerMask _border;
    [SerializeField] private Rigidbody2D rb;
    public bool facingRight = true;
    public float movementSmoothing = 0.05f;
    Vector3 refVelocity = Vector3.zero;
    public BaseChar basec;
    public int action;
    public Animator animator;
   

    [SerializeField] private CamController CameraScript;
        
    // Start is called before the first frame update
    void Start()
    {
        _movePoint.parent = null;
        basec = GetComponent<BaseChar>();
        action = basec.ActionPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (action >= 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, _movePoint.position, _speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _movePoint.position) == 0f)
            {

                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(_movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * 2, 0f, 0f), .2f, _border))
                    {
                        _movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * 2, 0f, 0f);
                        action--;
                    }
                }
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(_movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * 2, 0f), .2f, _border))
                    {
                        _movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * 2, 0f);
                        action--;
                    }
                }
            }           
        }
       
    }

    public void Move(float input)
    {
        Vector3 targetVelocity = new Vector2(input, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, movementSmoothing);

        if (input < 0 && !facingRight)
        {
            Flip();
        }
        else if (input > 0 && facingRight)
        {
            Flip();
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 charScale = transform.localScale;
        charScale.x *= -1;
        transform.localScale = charScale;
    }
}
