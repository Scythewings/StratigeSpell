using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerInput))]
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
    public PlayerInput input;
    public int action;
    public Animator anim;
    [SerializeField] private CamController CameraScript;

    public bool walk;
    

    // Start is called before the first frame update
    void Start()
    {
        _movePoint.parent = null;
        basec = GetComponent<BaseChar>();
        input = GetComponent<PlayerInput>();      
        action = basec.ActionPoint;
        anim = GetComponentInChildren<Animator>();
        walk = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (action > -1)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, _movePoint.position, _speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _movePoint.position) == 0f)
            {
                if (input.moveRight)
                {
                    AnimPlay(CharacterAnim.Walk);
                    MoveDirection(2f, 0);
                }
                else if (input.moveLeft)
                {
                    AnimPlay(CharacterAnim.Walk);
                    MoveDirection(-2f, 0); 
                }
                else if (input.moveUp)
                {
                    AnimPlay(CharacterAnim.Walk);
                    MoveDirection( 0, 2f);
                }
                else if (input.moveDown)
                {
                    AnimPlay(CharacterAnim.Walk);
                    MoveDirection( 0, -2f);
                }
              
            }
        }
        
    }

    public void MoveDirection(float xDirection, float yDirection) 
    {
        if (!Physics2D.OverlapCircle(_movePoint.position + new Vector3(xDirection, yDirection, 0f), .2f, _border))
        {
            _movePoint.position += new Vector3(xDirection, yDirection, 0f);
            action--;
            if (xDirection > 0 && !facingRight)
            {
                if (action != -1)
                Flip();
            }
            else if (xDirection < 0 && facingRight)
            {
                if (action != -1)
                Flip();
            }
        }
        
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 charScale = transform.localScale;
        charScale.x *= -1;
        transform.localScale = charScale;
    }

    //Trigger play once then stop.
    public void AnimPlay(CharacterAnim _animType = CharacterAnim.Idle) // Idle is default animation
    {
        switch (_animType)
            //break is for ending case
        {
            case CharacterAnim.Idle:
                break;
            case CharacterAnim.Walk:
                anim.SetTrigger("Walk");
                break;
            case CharacterAnim.Atk:
                break;
        }

    }
}

public enum CharacterAnim
{
    Idle,
    Walk,
    Atk,
}
