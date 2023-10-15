using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] int vida;
    [SerializeField] int maximoVida;
    private Animator anim;

    public Slider healthSlider; // Referencia al componente Slider de la barra de vida
    private PlayerController playerController; // Referencia al componente PlayerController

    private void Start()
    {
        anim = GetComponent<Animator>();
        vida = maximoVida;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maximoVida; // Establece el valor máximo de la barra de vida
            healthSlider.value = vida; // Establece el valor actual de la barra de vida
        }

        // Obtener la referencia al componente PlayerController
        playerController = GetComponent<PlayerController>();
    }

    public void TomarDaño(int daño, Vector3 enemyPosition)
    {
        vida -= daño;
        if (vida <= 0)
        {
            anim.SetTrigger("muerto");
            Destroy(gameObject);
        }
        else
        {
            // Llamar al método Knockback desde la referencia correcta de PlayerController
            playerController.Knockback(enemyPosition);
        }

        if (healthSlider != null)
        {
            anim.SetTrigger("takeDamage");
            healthSlider.value = vida; // Actualiza el valor de la barra de vida después de recibir daño
        }
    }

    public void Curar(int curacion)
    {
        if ((vida + curacion) > maximoVida)
        {
            vida = maximoVida;
        }
        else
        {
            vida += curacion;
        }

        if (healthSlider != null)
        {
            healthSlider.value = vida; // Actualiza el valor de la barra de vida después de curarse
        }
    }
}
