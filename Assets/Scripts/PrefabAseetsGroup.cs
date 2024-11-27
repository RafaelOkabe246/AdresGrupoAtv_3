using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PrefabAseetsGroup : MonoBehaviour
{
    public List<AssetReference> assetReferences = new List<AssetReference>();

    public List<GameObject> InstantiatAssets(AsyncOperationHandle<GameObject> handle)
    {
        List<GameObject> instantiatePrefabs = new List<GameObject>();

        foreach (AssetReference asset in assetReferences)
        {
            handle = asset.InstantiateAsync(transform.position, Quaternion.identity);
            handle.Completed += handle =>
            {
                instantiatePrefabs.Add(handle.Result);
               
            };
        }

        return instantiatePrefabs;
    }
}
