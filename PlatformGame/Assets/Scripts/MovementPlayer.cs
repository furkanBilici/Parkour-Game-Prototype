using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class MovementPlayer : MonoBehaviour
{
    public Rigidbody2D rb;
    Vector3 velocity;
    public float speed = 6f;
    float jump = 3f;
    public Animator animator;
    public int _sayac;
    public int sayac
    {
        get
        {
            return _sayac;
        }
        set
        {

            if (value > 1)
            { 
                _sayac = 1;   
            }
            else
            {
                _sayac = value;
            }
        }
    }
    public bool IsGrounded;
    public bool CanMove;
    public bool IsDead;
    public int key=0;
    public float WallWaitTime = 5f;
    float WallElapsedTime = 0f;
    public float DashGroundWaitTime = 0.5f;
    float DashElapsedTime = 0f;
    float DashElapsedTime2 = 0f;
    public float DashTime = 1f;
    public SaveManager savemanager;


    public float bok;


    public BoxCollider2D col;

    bool WallFall;

    public GameObject MovingP;
 


    [SerializeField]
    public GameObject player;
    int jumpCount;

    void Start()
    {
        if (savemanager.CheckpointLoad() != new Vector3(0, 0, 0))
        {
            transform.position = savemanager.CheckpointLoad();
        }
        WallFall =false;
        CanMove = true;
        sayac = 1;
        rb = GetComponent<Rigidbody2D>();
        jumpCount = 2;
        IsDead = false;
        bok = col.offset.y;
        savemanager.a++;
        Debug.Log(savemanager.a);
        
    }

    void Update()
    {
        Move();
        CharacterJump();
        Dash();
    }
    private void Move()
    {
        
        if (CanMove)
        {
            velocity = new Vector3(Input.GetAxis("Horizontal"), 0f);
            transform.position += velocity * speed * Time.deltaTime;
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
            {
                animator.SetBool("IsRun", true);
            }
            if (Mathf.Abs(Input.GetAxis("Horizontal")) == 0)
            {
                animator.SetBool("IsRun", false);
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            if (animator.GetBool("IsWall") && !WallFall)
            {
                float moveY = Input.GetAxis("Vertical");
                Vector3 moveDirection = new Vector3(0, moveY, 0);
                transform.Translate(moveDirection * speed * Time.deltaTime);
            }
            if(Input.GetKeyDown(KeyCode.LeftControl))
            {
                animator.SetBool("IsCrouch", true);
                col.size = new Vector3(col.size.y, col.size.x);
                
                col.offset = new Vector3(col.offset.x, -1); 
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                animator.SetBool("IsCrouch", false);
                col.size = new Vector3(col.size.y, col.size.x);
                col.offset = new Vector3(col.offset.x, bok); ;
            }
        }
    }

    private void CharacterJump()
    {
        if (CanMove)
        {
            if (Input.GetButtonDown("Jump") && jumpCount != 0)
            {
                if (animator.GetBool("IsWall"))
                {
                    Vector3 move = Vector3.zero;
                    if (Input.GetAxis("Horizontal") == 1)
                    {
                        move += Vector3.up;
                        move += Vector3.right;
                        transform.Translate(move * 5 * jump * Time.deltaTime);
                        jumpCount--;
                    }
                    if (Input.GetAxis("Horizontal") == -1)
                    {
                        move += Vector3.up;
                        move += Vector3.left;
                        transform.Translate(move * jump * Time.deltaTime);
                        jumpCount--;
                    }
                }
                if (jumpCount >= 2)
                {
                    rb.AddForce(Vector3.up * jump, ForceMode2D.Impulse);
                    animator.SetBool("IsJump", true);
                    jumpCount--;
                }
                if (jumpCount == 1)
                {
                    rb.AddForce(Vector3.up * (jump * 0.95f), ForceMode2D.Impulse);
                    animator.SetBool("IsJump", true);
                    jumpCount--;
                }

            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            animator.SetBool("IsJump", false);
            WallFall = false;
            WallElapsedTime = 0f;
            sayac = 1;
        }
        if (collision.gameObject.tag == "wall")
        {

            rb.velocity = Vector3.zero;
            jumpCount = 2;
            animator.SetBool("IsWall", true);

            animator.SetBool("IsJump", false);

        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            jumpCount = 2;
            animator.SetBool("IsJump", false);
            IsGrounded = true;
            
        }
        if (collision.gameObject.tag == "MoveingP")
        {
            jumpCount = 2;
            animator.SetBool("IsJump", false);
            IsGrounded = true;

        }
        if (collision.gameObject.tag == "wall")
        {
            rb.gravityScale = 0f;
            animator.SetBool("IsWall", true);
            animator.SetBool("IsJump", false);
            DuvarBekleme();

        }
        if (collision.gameObject.tag == "wall" && WallFall)
        {
            rb.gravityScale = 1f;
            animator.SetBool("IsWall", true);
            animator.SetBool("IsJump", false);
        }
        if(collision.gameObject.tag =="platform" && !animator.GetBool("IsDash"))
        {
            DashBekleme();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            if (!animator.GetBool("IsDash"))
            {
                jumpCount--;
                animator.SetBool("IsJump", true);

            }
            IsGrounded = false;
        }
        if (collision.gameObject.tag == "MoveingP")
        {
            animator.SetBool("IsJump", true);
            IsGrounded = false;
        }
        if (collision.gameObject.tag == "wall")
        {
            animator.SetBool("IsWall", false);
            rb.gravityScale = 1;
            jumpCount = 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "mantar")
        {
            rb.AddForce(Vector3.up * speed * 3f, ForceMode2D.Impulse);
        }
        if(collision.gameObject.tag == "key")
        {
            key += 1;
        }

    }
    public void Dash()
    {
        if(sayac == 1) 
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
            {
                sayac--;
                CanMove = false;
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                if (Input.GetKey(KeyCode.W))
                {
                    rb.AddForce(Vector3.up * speed * 1.5f, ForceMode2D.Impulse);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    rb.AddForce(Vector3.down * speed * 1.5f, ForceMode2D.Impulse);
                }
                rb.AddForce(Vector3.right * speed * 1.5f, ForceMode2D.Impulse);
                animator.SetBool("IsDash", true);
                Invoke("DashBitir", 3);
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
            {
                sayac--;
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                CanMove = false;
                if (Input.GetKey(KeyCode.W))
                {
                    rb.AddForce(Vector3.up * speed * 1.5f, ForceMode2D.Impulse);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    rb.AddForce(Vector3.down * speed * 1.5f, ForceMode2D.Impulse);
                }
                rb.AddForce(Vector3.left * speed * 1.5f, ForceMode2D.Impulse);
                animator.SetBool("IsDash", true);
                Invoke("DashBitir", 3);
            }
            else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.W))
            {
                sayac--;
                CanMove = false;
                rb.AddForce(Vector3.up * speed * 1.5f, ForceMode2D.Impulse);
                animator.SetBool("IsDash", true);
                Invoke("DashBitir", 3);
            }
            else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.S))
            {
                sayac--;
                CanMove = false;
                rb.AddForce(Vector3.down * speed * 1.5f, ForceMode2D.Impulse);
                animator.SetBool("IsDash", true);
                Invoke("DashBitir",3);
            }
        }
    }
    public void DashBekleme()
    {
        DashElapsedTime += Time.deltaTime;
        if (DashElapsedTime > DashGroundWaitTime)
        {
            sayac = 1;
            DashElapsedTime = 0;
        }
    }
    public void DashBitir()
    {
        animator.SetBool("IsDash", false);
        CanMove = true;
    }
    void DuvarBekleme()
    {
        WallElapsedTime += Time.deltaTime;
        if (WallElapsedTime >= WallWaitTime)
        {
            WallFall = true;
        }
    }

}

