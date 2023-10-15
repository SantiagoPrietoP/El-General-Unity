using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator animator;
    public string[] itemsToGive; // Array de nombres de los ítems que el cofre contiene
    public int maxItemsToGive = 3; // Cantidad máxima de ítems que se pueden recibir del cofre

    private bool isOpen = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact(PlayerInventory playerInventory)
    {
        if (!isOpen)
        {
            animator.SetTrigger("open");
            isOpen = true;
            Debug.Log("Opening chest...");

            // Aquí puedes implementar la lógica para abrir el cofre y dar los ítems al jugador
            for (int i = 0; i < maxItemsToGive && i < itemsToGive.Length; i++)
            {
                string item = itemsToGive[i];
                playerInventory.AddItem(item);
                Debug.Log("Obtained item: " + item);
            }
        }
        else
        {
            Debug.Log("The chest is already open.");
        }
    }
}
