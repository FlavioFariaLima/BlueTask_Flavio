using UnityEngine;

public class Behaviour_NPC : MonoBehaviour
{
    public GameObject interactionPanel;
    public string playerTag = "Player"; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            interactionPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            interactionPanel.SetActive(false);
        }
    }
}
