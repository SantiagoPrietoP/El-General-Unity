using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
 
 [SerializeField] private float tiempoEntreDaño;

 private float tiempoSiguienteDaño;

 
private void OnTriggerStay2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        Debug.Log("Recibido azul");
        tiempoSiguienteDaño -= Time.deltaTime;

        if (tiempoSiguienteDaño <= 0)
        {
            PlayerCombat playerCombat = other.GetComponent<PlayerCombat>();
            if (playerCombat != null)
            {
                playerCombat.TomarDaño(5, transform.position);
                tiempoSiguienteDaño = tiempoEntreDaño;
                Debug.Log("Recibido rojo");
            }
        }
    }
}
}