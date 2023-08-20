using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory_Slot : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Inventory_ItemBlueprint item;
    public Image icon;

    public bool isEquipSlot;
    public ItemType equipType;
    public Character_Equipment characterEquipment;

    [Space]
    public GameObject tooltipObject;
    public Text itemNameText;
    public Text itemValueText;
    public Image itemEnlargedIcon;

    private GameObject draggingIconObject; // Novo campo para o objeto que será arrastado
    private Image draggingIcon; // A imagem do objeto que será arrastado


    // Methods
    private void Awake()
    {
        if (tooltipObject)
            tooltipObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item)
        {
            ShowTooltip();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideTooltip();
    }

    private void ShowTooltip()
    {
        if (tooltipObject && itemNameText && itemValueText && itemEnlargedIcon)
        {
            itemNameText.text = item.itemName;
            itemValueText.text = item.value.ToString();
            itemEnlargedIcon.sprite = item.icon;

            tooltipObject.SetActive(true);
        }
    }

    private void HideTooltip()
    {
        if (tooltipObject)
        {
            tooltipObject.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CreateDraggingIcon();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item && draggingIcon)
        {
            draggingIcon.rectTransform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggingIconObject)
        {
            Destroy(draggingIconObject);
        }

        if (!IsPointerInUIObject())
        {
            // Drop item
            ClearSlot();
        }

        icon.transform.localPosition = Vector3.zero;

        // Check if the item was dropped on the store
        List<RaycastResult> hitObjects = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, hitObjects);

        foreach (var hit in hitObjects)
        {
            if (hit.gameObject.CompareTag("Store") && transform.parent.parent.GetComponent<Character_Inventory_UIPanel>())
            {
                Store store = hit.gameObject.GetComponent<Store>();
                if (store)
                {
                    Debug.LogWarning("Store");
                    store.Drop(item); // let the store handle it
                    Gameplay_SoundLibrary.instance.PlaySound("ChangeItem");
                }
            }

            if (hit.gameObject.CompareTag("Player") && transform.parent.parent.GetComponent<Store>())
            {
                Character_Inventory_UIPanel inv = hit.gameObject.GetComponent<Character_Inventory_UIPanel>();
                if (inv)
                {
                    Debug.LogWarning("Store");
                    inv.Drop(item, this); // let the store handle it

                }
            }

            if (hit.gameObject.CompareTag("Equip") && item.itemType == hit.gameObject.GetComponent<Inventory_Slot>().equipType && transform.parent.parent.GetComponent<Character_Inventory_UIPanel>())
            {
                // Equip item
                hit.gameObject.GetComponent<Inventory_Slot>().characterEquipment.EquipItem(item);
                hit.gameObject.GetComponent<Inventory_Slot>().AddItem(item);

                if (hit.gameObject.GetComponent<Inventory_Slot>().item)
                {
                    AddItem(hit.gameObject.GetComponent<Inventory_Slot>().item);
                }
                else
                {
                    ClearSlot();
                }

                Debug.LogWarning("Drop Equip");
            }

        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //
    }

    private bool IsPointerInUIObject()
    {
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    public void AddItem(Inventory_ItemBlueprint newItem)
    {
        if (!isEquipSlot || (isEquipSlot && newItem.itemType == equipType))
        {
            item = newItem;
            icon.sprite = item.icon;
            icon.enabled = true;
        }
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

     private void CreateDraggingIcon()
    {
        if (icon.sprite && !draggingIconObject)
        {
            draggingIconObject = new GameObject("Dragging Icon");
            draggingIcon = draggingIconObject.AddComponent<Image>();
            draggingIcon.sprite = icon.sprite;
            draggingIcon.rectTransform.sizeDelta = icon.rectTransform.sizeDelta * 1.2f; // Faça o ícone arrastável 20% maior
            draggingIcon.canvasRenderer.SetAlpha(0.75f); // Opcional: torná-lo um pouco transparente

            draggingIconObject.transform.SetParent(icon.canvas.transform);
            draggingIconObject.transform.SetAsLastSibling(); // Certifique-se de que ele seja renderizado no topo
            draggingIconObject.transform.localScale = Vector3.one; // Restaure a escala
        }
    }
}
