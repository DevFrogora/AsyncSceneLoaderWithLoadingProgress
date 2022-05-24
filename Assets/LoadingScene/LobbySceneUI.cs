using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySceneUI : MonoBehaviour
{
    public void OnIslandSceneClick()
    {
        Debug.Log("Click On Island Scene btn");
        Loader.Load(Loader.Scene.IslandScene);
        
    }
}
