using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PreparedRecipeSO : ScriptableObject
{
    public List<KitchenObjectSO> kitchenObjectSOList;
    public string recipeName;
}
