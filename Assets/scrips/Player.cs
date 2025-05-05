using Unity.VisualScripting;
using UnityEngine;

public class Player: MonoBehaviour
{
    public static Player singleton;
    public State state = State.Idle;
    public Rigidbody2D body;
    public float jumpForce;
    Vector2 direction;
    public Animation anim;
    public enum State
    {
        Idle,
        Walking,
        Jumping

    }
    void Start()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(this.gameObject); 
        }
        
    }

  private void Update()
    {
        Debug.Log($"State:{state}");
        switch (state)
        {
           
            case State.Idle:
                OnIdle();
                    break;
            case State.Walking:
                OnWalking();
                break;
            case State.Jumping:
                OnJumping();
               
                break;

        }
        
       
    /* if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * Time.deltaTime); 
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * Time.deltaTime);
         
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //transform.Translate(Vector2.up * Time.deltaTime);
            Debug.Log("lo");
            body.AddForceY(jumpForce);
        }*/

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "plataforma")
        {
            Debug.Log("Walking");
            state = State.Walking;
        }
    }
    void OnWalking()
    {
        if (!HorizontalMove())
        {
            
            state = State.Idle;
        }
        transform.Translate(direction * Time.deltaTime);
    } 
void OnIdle()
    {
        if (HorizontalMove())
        {
            Debug.Log("Walking");
            state = State.Walking;
        }
        if (jump())
        {
            Debug.Log("Jumping");
            state = State.Jumping;
        }
    }
    void OnJumping()
    {
        if (HorizontalMove())
        {
            transform.Translate(direction * Time.deltaTime);
        }
    }
    bool HorizontalMove()
    {
        if (Input.GetKey(KeyCode.A))
        {
            direction = Vector2.left;
            return true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction = Vector2.right;
            return true;
        }
        return false;

    }
    bool jump()
    {
        if (Input.GetKey(KeyCode.W))
        {
            body.AddForceY(jumpForce);
            return true;
        }
        return false;
        
    }
}
