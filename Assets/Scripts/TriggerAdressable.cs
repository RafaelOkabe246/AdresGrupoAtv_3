using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class TriggerAdressable : MonoBehaviour
{
    //public List<AssetReference> assetReferences =new List<AssetReference>();
    public List<PrefabAseetsGroup> assetReferences = new List<PrefabAseetsGroup>();


    List<GameObject> prefabs = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Call adressables
            if(prefabs.Count <= 0)
            {
                LoadAsset();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (prefabs.Count > 0)
            {
                UnloadAsset();
            }
        }
    }


    void LoadAsset()
    {
        AsyncOperationHandle<GameObject> handle = new AsyncOperationHandle<GameObject>();

        foreach (PrefabAseetsGroup asset in assetReferences)
        {
            prefabs = asset.InstantiatAssets(handle);
        }

        //foreach (AssetReference asset in assetReferences)
        //{
        //    handle = asset.InstantiateAsync(transform.position, Quaternion.identity);
        //    handle.Completed += handle =>
        //    {
        //        prefabs.Add(handle.Result);
        //    };
        //}

        //Code for just one prefab
        //handle = assetReferences.InstantiateAsync(transform.position, Quaternion.identity);
        //handle.Completed += handle => 
        //{ 
        //    prefabs.Add(handle.Result); 
        //};
    }

    void UnloadAsset()
    {
        foreach (GameObject prefab in prefabs)
        {
            Addressables.ReleaseInstance(prefab);
        }
        prefabs.Clear();
    }
}
