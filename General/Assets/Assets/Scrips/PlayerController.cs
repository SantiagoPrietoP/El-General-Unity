using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    //Movimiento
    public float moveSpeed;
    
    //Salto
    public float jumpForce;

    // Componentes
    public Rigidbody2D theRB;

    //Animator
    private Animator anim;
    private SpriteRenderer theSR;

    // Deteccion de piso
    public Transform groundCheckpoint;
    public LayerMask whatIsGround;
    private bool isGrounded;

    //Knock Back
    
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.5f;
    private float knockbackTimer = 0f;
    private bool isKnockback = false;
    private Vector3 enemyKnockbackOrigin; // Variable para guardar la posición del enemigo que causa el knockback


    //Attack
    public float attackCooldown = 0.5f;
    public float attackRange = 1.5f;
    public int attackDamage = 10;

    private AudioSource audioSource;
    private Transform attackPoint;
    private float attackTimer = 0f;
    private bool isAttacking = false;

    //Inventory

    public PlayerInventory playerInventory;
    private bool isInventoryOpen = false;

    //Chest

    private bool isInteracting = false;
    private Chest currentChest;
  
  
    


    void Start()
    {
       anim = GetComponent<Animator>();    
       theSR = GetComponent<SpriteRenderer>();

        theRB = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        attackPoint = transform.Find("AttackPoint");
        playerInventory = new PlayerInventory();
      
    }


    void Update()

    {
        

        theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y );

        isGrounded = Physics2D.OverlapCircle(groundCheckpoint.position, .2f, whatIsGround);


        if(Input.GetButtonDown("Jump"))
        {
            if(isGrounded)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);   
            }
            
        }
        

        
        


        if(theRB.velocity.x < 0)
        {
            theSR.flipX = true;
        } 
        else if (theRB.velocity.x > 0)
        {
            theSR.flipX = false;
        }

            

        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);


    
         if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        if (Input.GetKeyDown(KeyCode.E) && !isInventoryOpen && !isInteracting)
        {
            Interact();
        }

        if (isKnockback)
        {
            KnockbackMovement();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q) && !isAttacking)
            {
                Attack();
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && !isAttacking)
        {
            InteractWithDoor();
        }
        
    }


void FixedUpdate() {

        if (Input.GetKeyDown(KeyCode.W) && theRB.velocity.y == 0f) {
            theRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("jump");
        }
    }





    void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;

        if (isInventoryOpen)
        {
            OpenInventory();
        }
        else
        {
            CloseInventory();
        }
    }

    void OpenInventory()
    {
        Debug.Log("Opening Inventory");
        Debug.Log(playerInventory.GetInventoryContents());
        
        // Add code to display the inventory UI
    }

    void CloseInventory()
    {
        Debug.Log("Closing Inventory");
        
        // Add code to close the inventory UI
    }
        void Interact()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1.5f);
        if (hit.collider != null)
        {
            Chest chest = hit.collider.GetComponent<Chest>();
            if (chest != null)
            {
                currentChest = chest;
                chest.Interact(playerInventory);
                isInteracting = true;
            }
        }
    }

   public  void CollectItem(string itemName)
    {
        playerInventory.AddItem(itemName);
        Debug.Log("Collected item: " + itemName);
    }



        void Attack()
    {
        isAttacking = true;
        anim.SetTrigger("attack");
      //  audioSource.PlayOneShot(audioSource.clip);

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                EnemyController enemy = hitCollider.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    enemy.TakeDamage(attackDamage);
                }
            }
        }
      anim.SetTrigger("attack");
        Invoke("EndAttack", attackCooldown);
        
    }

    void EndAttack()
    {
         anim.SetTrigger("attack");
        isAttacking = false;
    }

 void KnockbackMovement()
    {
        if (knockbackTimer > 0)
        {
            knockbackTimer -= Time.deltaTime;
            float knockbackDirection = transform.position.x < enemyKnockbackOrigin.x ? 1 : -1;
            theRB.velocity = new Vector2(knockbackDirection * knockbackForce, theRB.velocity.y);
        }
        else
        {
            isKnockback = false;
        }
    }

    public void Knockback(Vector3 origin)
    {
        enemyKnockbackOrigin = origin;
        knockbackTimer = knockbackDuration;
        isKnockback = true;
    }

     void InteractWithDoor()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1.5f);
        if (hit.collider != null)
        {
            Door door = hit.collider.GetComponent<Door>();
            if (door != null)
            {
                StartCoroutine(door.TeleportPlayer(transform));
            }
        }
    }
}
    

