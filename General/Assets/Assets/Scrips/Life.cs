using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag ("Player"))
        {
            other.GetComponent<PlayerCombat>().Curar(20);
            Destroy(gameObject);
        }
    }
}
