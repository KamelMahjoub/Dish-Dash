using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public enum Scene
    {
        MainMenu,
        GameScene,
        LoadingScreen
    }
    
    
    public static Scene targetScene;
    
    public static void Load(Scene targetScene)
    {
        
        Loader.targetScene = targetScene;
        
        SceneManager.LoadScene(Scene.LoadingScreen.ToString());
        
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
