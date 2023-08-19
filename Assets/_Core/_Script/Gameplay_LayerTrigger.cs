using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay_LayerTrigger : MonoBehaviour
{
    public string sortingLayer;

    private void OnTriggerExit2D(Collider2D other)
    {
        SpriteRenderer[] srs = other.gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in srs)
        {
            sr.sortingLayerName = sortingLayer;
        }
    }
}
