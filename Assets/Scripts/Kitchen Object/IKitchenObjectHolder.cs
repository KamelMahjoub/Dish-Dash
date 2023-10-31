using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectHolder
{
    public void SetKitchenObject(KitchenObject kitchenObject);

    public void ClearKitchenObject();

    public KitchenObject GetKitchenObject();

    public Transform GetKitchenObjectFollowTransform();

    public bool HasKitchenObject();
}