using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovementScript : MonoBehaviour
{
    // Inicialização das variaveis
    private float moveSpeed = 1f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDirection;
    private Animator animator;




    // Puxa o rigidbody do objeto jogador dentro da unity
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // altera o vetor de movimento em relação a direção apertada e a velocidade pre determinada
    void Update()
    {
         
        rb.linearVelocity = moveInput * moveSpeed;
        
    }

    // define a direção do movimento a partir do botão apertado pelo jogador puxado do input system
    public void Move(InputAction.CallbackContext context)
    { 
       
        animator.SetBool("IsWalking", true);

        // averigua se o jogador parou de andar e na sequencia salva seu ultimo input apertado e passa para a rotação da mira
        if (context.canceled)
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
}