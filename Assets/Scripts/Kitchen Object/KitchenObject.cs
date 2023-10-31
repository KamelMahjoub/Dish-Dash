using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectHolder kitchenObjectParent;

    public KitchenObjectSO KitchenObjectSO => kitchenObjectSO;


    public IKitchenObjectHolder GetKitchenObjectParent() => kitchenObjectParent;

    public void SetKitchenObjectParent(IKitchenObjectHolder kitchenObjectParent)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
            
        this.kitchenObjectParent = kitchenObjectParent;
            
        if(kitchenObjectParent.HasKitchenObject())
            Debug.LogError("Counter already has a kitchen object");
            
        this.kitchenObjectParent.SetKitchenObject(this);
            
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
            
        transform.localPosition = Vector3.zero;  
    }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO,
        IKitchenObjectHolder kitchenObjectParent)
    {
        GameObject kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }

    
    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }
    
}
