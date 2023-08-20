using System.Collections.Generic;
using UnityEngine;

public class Player_UIPanelsManager : MonoBehaviour
{
    // Singleton instance
    public static Player_UIPanelsManager Instance { get; private set; }

    [SerializeField] private List<GameObject> objectsInList = new List<GameObject>();

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

   
    public void AddObjectToList(GameObject obj)
    {
        if (!objectsInList.Contains(obj))
            objectsInList.Add(obj);
    }

   
    public void RemoveObjectFromList(GameObject obj)
    {
        if (objectsInList.Contains(obj))
            objectsInList.Remove(obj);
    }

  
    public bool AreAllObjectsDeactivated()
    {
        foreach (GameObject obj in objectsInList)
        {
            if (obj.activeSelf)
                return false;
        }

        return true;
    }
}
