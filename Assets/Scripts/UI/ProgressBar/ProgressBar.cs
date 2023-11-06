using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    //INTERFACES DONT SHOW UP IN THE INSPECTOR
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;
    
    private IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        
        if(hasProgress == null)
            Debug.Log("Game Object doesnt have a component that implements IHasProgress");
        
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        
        barImage.fillAmount = 0f;
        
        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
        
        if(e.progressNormalized is 0f or 1f)
            Hide();
        else
            Show();
    }
    
    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false); 
    }
}
