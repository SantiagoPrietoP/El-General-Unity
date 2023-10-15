
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloque : MonoBehaviour
{
    private float tiempo;
    
  private void OnCollisionEnter2D(Collision2D other)
   {
    
    if(other.collider.CompareTag("Player"))
     {
        Debug.Log("Jugador Entro");
     }
   }


      private void OnCollisionStay2D(Collision2D other)
   {
        if(other.collider.CompareTag("Player"))
     {
        tiempo += Time.deltaTime;
        Debug.Log("Jugador en bloque ");
   }
 }

   private void OnCollisionExit2D(Collision2D other)
   {
        if(other.collider.CompareTag("Player"))
     {
        Debug.Log("Jugador Salio : " + tiempo);
        tiempo = 0;
   }
 }
}
