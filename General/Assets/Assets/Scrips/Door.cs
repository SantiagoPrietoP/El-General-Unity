using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform destinationDoor;

    public IEnumerator TeleportPlayer(Transform player)
    {
        // Desactivar al jugador para evitar interacciones mientras se teletransporta
        player.gameObject.SetActive(false);

        // Teletransportar al jugador al destino de la puerta
        player.position = destinationDoor.position;

        // Pequeño retardo para evitar interacciones inmediatas después del teletransporte
        yield return new WaitForSeconds(0.2f);

        // Reactivar al jugador después del teletransporte
        player.gameObject.SetActive(true);
    }
}
