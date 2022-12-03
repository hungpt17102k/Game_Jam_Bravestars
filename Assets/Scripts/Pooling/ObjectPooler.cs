using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

/// <summary>
/// Item for object Pooling
/// </summary>
[System.Serializable]
public class PoolItem
{
    public string itemName;
    public GameObject objectToPool;
    public int amountToPool;
    public bool shouldExpand;
}

/// <summary>
/// Manages object Pooling.
/// </summary>
public class ObjectPooler : MonoBehaviour
{
    /// <summary> Items to pool</summary>
    public List<PoolItem> itemsToPool;

    /// <summary> list of list of pooled items" </summary>
    public List<List<GameObject>> pooledItemsList = new List<List<GameObject>>();

    void Awake()
    {
        CreateObjectPool();
    }

    /// <summary>
    /// Creates the object pool
    /// </summary>
    public void CreateObjectPool()
    {
        foreach (PoolItem item in itemsToPool)
        {
            List<GameObject> pooledObjects = new List<GameObject>();
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
            pooledItemsList.Add(pooledObjects);
        }

    }


    /// <summary>
    /// Gets the pooled object.
    /// </summary>
    /// <returns>The pooled object.</returns>
    /// <param name="itemName">Item name.</param>
    /// <param name="activeState">If set to <c>true</c> return the object in active state.</param>
    public GameObject GetPooledObject(string itemName, bool activeState = false)
    {
        //Find the given item.
        int itemIndex = itemsToPool.FindIndex(x => x.itemName == itemName);

        //If item is not present
        if (itemIndex == -1)
        {
            Debug.LogError("Item with this name not found. Compare the assigned name and the name being passed");
            return null;
        }

        //loop through the respective list and find an item which is not active.
        for (int i = 0; i < pooledItemsList[itemIndex].Count; i++)
        {
            if (!pooledItemsList[itemIndex][i].activeInHierarchy)
            {
                pooledItemsList[itemIndex][i].SetActive(activeState);
                return pooledItemsList[itemIndex][i];
            }
        }

        //if no items are available in the pool then add a new item
        if (itemsToPool[itemIndex].shouldExpand)
        {
            GameObject obj = (GameObject)Instantiate(itemsToPool[itemIndex].objectToPool);
            obj.SetActive(activeState);
            pooledItemsList[itemIndex].Add(obj);
            return obj;
        }

        return null;
    }

    /// <summary>
    /// Gets the pooled object with ObjectPoolItems enum
    /// </summary>
    public GameObject GetPooledObject(ObjectPoolItems item, bool activeState = false)
    {
        return GetPooledObject(item.ToString(), activeState);
    }


    /// <summary>
    /// Gets the pooled object with given position
    /// </summary>
    public GameObject GetPooledObject(string itemName, Vector3 pos, bool activeState = false)
    {
        GameObject obj = GetPooledObject(itemName, activeState);
        obj.transform.position = pos;
        return obj;
    }

    /// <summary>
    /// Gets the pooled object with ObjectPoolItems enum and given position
    /// </summary>
    public GameObject GetPooledObject(ObjectPoolItems itemName, Vector3 pos, bool activeState = false)
    {
        return GetPooledObject(itemName.ToString(), pos, activeState);
    }

    /// <summary>
    /// Gets the pooled object with given position and rotation
    /// </summary>
    public GameObject GetPooledObject(string itemName, Transform posRot, bool activeState = false)
    {

        GameObject obj = GetPooledObject(itemName, activeState);
        obj.transform.position = posRot.transform.position;
        obj.transform.rotation = posRot.transform.rotation;
        return obj;
    }

    /// <summary>
    /// Gets the pooled object with ObjectPoolItems enum and given position, rotation
    /// </summary>
    public GameObject GetPooledObject(ObjectPoolItems itemName, Transform posRot, bool activeState = false)
    {
        return GetPooledObject(itemName.ToString(), posRot, activeState);
    }

    /// <summary>
    /// Disable particular pooled items
    /// </summary>
    public void disablePooled(string itemName)
    {
        int itemIndex = itemsToPool.FindIndex(x => x.itemName == itemName);

        for (int i = 0; i < pooledItemsList[itemIndex].Count; i++)
        {
            if (pooledItemsList[itemIndex][i].activeInHierarchy)
            {
                pooledItemsList[itemIndex][i].SetActive(false);

            }
        }
    }

    /// <summary>
    /// Disable all pooled items
    /// </summary>
    public void disableAllPooled()
    {
        for (int i = 0; i < pooledItemsList.Count; i++)
        {
            for (int j = 0; j < pooledItemsList[i].Count; j++)
            {
                if (pooledItemsList[i][j].activeInHierarchy)
                {
                    pooledItemsList[i][j].SetActive(false);

                }
            }
        }
    }

}


[CustomEditor(typeof(ObjectPooler), true)]
public class ObjectPoolerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ObjectPooler objectPooler = (ObjectPooler)target;

        EditorGUILayout.LabelField("");
        EditorGUILayout.HelpBox("After assigning Items, Hit this button to get an enum of poolable items created. Makes method calls easy.\n ** NOTE : Make sure the name is a valid enum name **\n" +
                                "Example -  objectPooler.GetPooledObject(ObjectPoolItems.leftoverBlock, true);", MessageType.Info);

        if (GUILayout.Button("Generate ObjectPoolItems Enum"))
        {
            GenerateObjectPoolerItemsEnum();
        }
    }

    /// <summary>
    /// Creates the ObjectPoolItems enum at the ObjectPooler's location
    /// </summary>
    public void GenerateObjectPoolerItemsEnum()
    {
        //Get the list of Object Pool Items
        ObjectPooler objectPooler = (ObjectPooler)target;
        List<PoolItem> itemsToPool = objectPooler.itemsToPool;

        //Get the script's path
        MonoScript thisScript = MonoScript.FromMonoBehaviour(objectPooler);
        string ScriptFilePath = AssetDatabase.GetAssetPath(thisScript);

        //Create a path for the enum file
        string enumName = "ObjectPoolItems";
        string filePathAndName = ScriptFilePath.Replace(thisScript.name + ".cs", "") + "/" + enumName + ".cs";

        //Wrire the enum at above path
        using (StreamWriter streamWriter = new StreamWriter(filePathAndName))
        {
            streamWriter.WriteLine("public enum " + enumName);
            streamWriter.WriteLine("{");
            for (int i = 0; i < itemsToPool.Count; i++)
            {
                streamWriter.WriteLine("\t" + itemsToPool[i].itemName + ",");
            }
            streamWriter.WriteLine("}");
        }
        AssetDatabase.Refresh();

        Debug.Log("ObjectPoolItems  enum created/updated at " + ScriptFilePath);

    }
}