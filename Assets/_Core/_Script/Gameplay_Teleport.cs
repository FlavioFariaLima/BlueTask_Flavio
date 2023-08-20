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

            Gameplay_SoundLibrary.instance.PlaySound("Teleport");

            collision.transform.position = destination.position;


            justTeleported = true;


            // Evita teletransporte contínuo ao entrar no ponto após ser teletransportado
            destination.GetComponentInChildren<Gameplay_Teleport>().justTeleported = true;
            destination.GetComponentInChildren<Gameplay_Teleport>().Invoke("ResetTeleportFlag", 1.5f);
            Invoke("ResetTeleportFlag", 1.5f);
        }
    }

    private void ResetTeleportFlag()
    {
        justTeleported = false;
    }


}
