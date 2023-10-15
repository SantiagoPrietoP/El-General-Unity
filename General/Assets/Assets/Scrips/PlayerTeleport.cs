using System.Collections;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private bool isTeleporting = false;

    void Update()
    {
        if (!isTeleporting && Input.GetKeyDown(KeyCode.F))
        {
             Debug.Log("TPTPTPTPTPTPT");
            StartCoroutine(TeleportRoutine());
        }
    }

    IEnumerator TeleportRoutine()
    {
        isTeleporting = true;

        // Buscar el componente de puerta (Door) cercano
        Collider2D nearbyDoor = Physics2D.OverlapCircle(transform.position, 1.5f, LayerMask.GetMask("Door"));
        if (nearbyDoor != null)
        {
            Door door = nearbyDoor.GetComponent<Door>();
            if (door != null)
            {
                yield return StartCoroutine(door.TeleportPlayer(transform));

                // Pequeño retardo para evitar teletransporte múltiple
                yield return new WaitForSeconds(0.2f);
            }
        }

        isTeleporting = false;
    }
}
