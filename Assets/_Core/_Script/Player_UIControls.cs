using UnityEngine;

public class Player_UIControls : MonoBehaviour
{
    public GameObject inventoryUI; // Arraste o GameObject do seu inventário para esta variável no Unity Editor
    public GameObject menuUI; // Arraste o GameObject do seu menu para esta variável no Unity Editor

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

            // Se o menu estiver aberto quando o inventário for aberto, feche o menu
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

            // Se o inventário estiver aberto quando o menu for aberto, feche o inventário
            if (isMenuOpen && isInventoryOpen)
            {
                isInventoryOpen = false;
                inventoryUI.SetActive(false);
            }
        }
    }
}
