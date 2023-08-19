using UnityEngine;

public class Gameplay_Teleport : MonoBehaviour
{
    public Transform destination; 
    internal bool justTeleported = false; 

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Detected");

        if (justTeleported)
        {
            return;
        }

        if (collision.CompareTag("Player"))
        {

            collision.transform.position = destination.position;


            justTeleported = true;


            destination.GetComponentInChildren<Gameplay_Teleport>().justTeleported = true; // Evita teletransporte contínuo ao entrar no ponto após ser teletransportado
            Invoke("ResetTeleportFlag", 0.5f);
        }
    }

    private void ResetTeleportFlag()
    {
        justTeleported = false;
    }
}
