using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName = "Sword"; // Nombre del ítem

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.CollectItem(itemName);
                Destroy(gameObject); // Destruir el objeto del ítem
            }
        }
    }
}
