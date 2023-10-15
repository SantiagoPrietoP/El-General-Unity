using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float attackRange = 1.5f;
    public int attackDamage = 10;
    public float attackCooldown = 2f;
    public int maxHealth = 100;
    public int currentHealth;

    // Componentes
    public Rigidbody2D rb;

    //Animator
    private Animator anim;
    private SpriteRenderer theSR;

    private Transform player;
    private bool isAttacking = false;

    void Start()
    {
        anim = GetComponent<Animator>();    
        theSR = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!isAttacking)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        if (player == null)
            return;

        Vector3 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            anim.SetBool("isAttacking", true);
            StartCoroutine(AttackPlayer());
        }

        if (rb.velocity.x < 0)
        {
            theSR.flipX = true;
        } 
        else if (rb.velocity.x > 0)
        {
            theSR.flipX = false;
        }

        anim.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));
    }

    IEnumerator AttackPlayer()
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackCooldown);

        if (player != null)
        {
            PlayerCombat playerCombat = player.GetComponent<PlayerCombat>();
            if (playerCombat != null)
            {
                playerCombat.TomarDaño(attackDamage, transform.position); // Pasar la posición del enemigo
            }
        }

        isAttacking = false;
        anim.SetBool("isAttacking", false);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            anim.SetTrigger("takeDamage");
        }
    }

    void Die()
    {
        // Aquí puedes añadir la lógica para que el enemigo muera.
        // Por ejemplo, reproducir una animación de muerte y luego destruir el objeto.
        Destroy(gameObject);
    }
}