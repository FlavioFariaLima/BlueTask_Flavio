using UnityEngine;

public class Player_UIControls : MonoBehaviour
{
    public GameObject inventoryUI; // Arraste o GameObject do seu invent�rio para esta vari�vel no Unity Editor
    public GameObject menuUI; // Arraste o GameObject do seu menu para esta vari�vel no Unity Editor

    private bool isInventoryOpen = false;
    private bool isMenuOpen = false;

    void Update()
    {
        HandleInventoryInput();
        HandleMenuInput();
    }

    private void HandleInventoryInput()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isInventoryOpen = !isInventoryOpen;
            inventoryUI.SetActive(isInventoryOpen);

            // Se o menu estiver aberto quando o invent�rio for aberto, feche o menu
            if (isMenuOpen && isInventoryOpen)
            {
                isMenuOpen = false;
                menuUI.SetActive(false);
            }
        }
    }

    private void HandleMenuInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMenuOpen = !isMenuOpen;
            menuUI.SetActive(isMenuOpen);

            // Se o invent�rio estiver aberto quando o menu for aberto, feche o invent�rio
            if (isMenuOpen && isInventoryOpen)
            {
                isInventoryOpen = false;
                inventoryUI.SetActive(false);
            }
        }
    }
}
