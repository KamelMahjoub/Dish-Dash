using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    [SerializeField] private string objectName;
}
