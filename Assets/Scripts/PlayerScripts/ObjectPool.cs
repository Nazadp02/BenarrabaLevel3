using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefabObject;
    [SerializeField] private int objectsNumberOnStart;  //numbers of objects that are charged in the scene for recycle

    private List<GameObject> objectsPool = new List<GameObject>();

    private void Start()
    {
        CreateObjects(objectsNumberOnStart);
    }

    /// <summary>
    /// //Create the objects needed at the beginning of the game
    /// </summary>
    /// <param name="numberOfObjects"></param>
    private void CreateObjects(int numberOfObjects)
    {
        for (int i = 0; i < objectsNumberOnStart; i++)
        {
            CreateNewObject();
        }
    }

    /// <summary>
    /// instantiate new object and add to the list
    /// </summary>
    /// <returns>Pool gameobject</returns>
    private GameObject CreateNewObject()
    {
        //Instantiate anywhere
        GameObject newObject = Instantiate(prefabObject);
        //Desactive
        newObject.SetActive(false);
        //Add to the list
        objectsPool.Add(newObject);

        return newObject;
    }

    /// <summary>
    /// Take form the list an available object, if it does not exit create a new one
    /// </summary>
    /// <returns></returns>
    public GameObject GetGameObject()
    {
        //Find in the objectsPool an object that is inactive in the game hierachy
        GameObject objectToGet = objectsPool.Find(x => x.activeInHierarchy == false);

        //If not exist, create one
        if (objectToGet == null)
        {
            objectToGet = CreateNewObject();
        }

        //Active the object
        objectToGet.SetActive(true);
        return objectToGet;
    }
}
