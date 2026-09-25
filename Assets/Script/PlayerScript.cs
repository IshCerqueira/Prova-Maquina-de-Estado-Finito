using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerScript : MonoBehaviour
{
    // Inicialização das variaveis
    private float moveSpeed = 1f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDirection;
    private Vector2 ZeroSpeed;
    private Animator animator;
    private bool canWalk = true;
    private bool crouching = false;
    private bool alive = true;


    // Puxa o rigidbody do objeto jogador dentro da unity
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // altera o vetor de movimento em relação a direção apertada e a velocidade pre determinada
    void Update()
    {
         if(canWalk && alive){
            rb.linearVelocity = moveInput * moveSpeed;
         }
        
    }

    // define a direção do movimento a partir do botão apertado pelo jogador puxado do input system
    public void Move(InputAction.CallbackContext context)
    { 
       
        animator.SetBool("IsWalking", true);

        // averigua se o jogador parou de andar e na sequencia salva seu ultimo input apertado e passa para a rotação da mira
        if (context.canceled && alive)
        {
            animator.SetBool("IsWalking", false);
            animator.SetFloat("LastX", moveInput.x);
            animator.SetFloat("LastY", moveInput.y);

          
            lastMoveDirection = moveInput;
            Vector3 vector3 = Vector3.left * lastMoveDirection.x + Vector3.down * lastMoveDirection.y;
        }
        
        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("X", moveInput.x);
        animator.SetFloat("Y", moveInput.y);
    

    }

    public void Attack1(InputAction.CallbackContext context){
        if(context.performed && moveInput.x == 0 && moveInput.y == 0 && !crouching){
             animator.SetTrigger("Attack1");
        }

        else if(context.performed && (moveInput.x != 0 || moveInput.y != 0) && !crouching){
            animator.SetTrigger("AttackRun");
        }
    }

    public void Attack2(InputAction.CallbackContext context){
       if(context.performed && moveInput.x == 0 && moveInput.y == 0 && !crouching){
             animator.SetTrigger("Attack2");
        }

        else if(context.performed && (moveInput.x != 0 || moveInput.y != 0) && !crouching){
            animator.SetTrigger("AttackRun");
        }
    }

     public void Attack3(InputAction.CallbackContext context){
       if(context.performed && moveInput.x == 0 && moveInput.y == 0 && !crouching){
             animator.SetTrigger("Attack3");
        }

        else if(context.performed && (moveInput.x != 0 || moveInput.y != 0) && !crouching){
            animator.SetTrigger("AttackRun");
        }
    }


    public void Block(InputAction.CallbackContext context){
       if(context.performed && moveInput.x == 0 && moveInput.y == 0){
             animator.SetBool("IsBlocking", true);
             canWalk = false;
        }
        else if(context.canceled){
            animator.SetBool("IsBlocking", false);
            canWalk = true;
        }
    }

    public void CastSpell(InputAction.CallbackContext context){
       if(context.performed && moveInput.x == 0 && moveInput.y == 0 && !crouching){
             animator.SetTrigger("CastSpell");
              
        }
        else if(context.performed && (moveInput.x != 0 || moveInput.y != 0) && !crouching){
            animator.SetFloat("LastX", moveInput.x);
            animator.SetFloat("LastY", moveInput.y);
            animator.SetTrigger("CastSpell");
        }
    }

     public void Crouch(InputAction.CallbackContext context){
       if(context.performed && moveInput.x == 0 && moveInput.y == 0){
             
             crouching = !crouching;
             animator.SetBool("IsCrouch", crouching);
              
        }
        else if(context.performed && (moveInput.x != 0 || moveInput.y != 0)){
            animator.SetFloat("LastX", moveInput.x);
            animator.SetFloat("LastY", moveInput.y);

            crouching = !crouching;
             animator.SetBool("IsCrouch", crouching);
        }
    }

  
    void OnTriggerEnter2D(Collider2D other)
    {
       
        animator.SetFloat("LastX", moveInput.x);
        animator.SetFloat("LastY", moveInput.y);
        rb.linearVelocity = ZeroSpeed;
       alive = false;
       animator.SetBool("IsDead", true); 
    }



}