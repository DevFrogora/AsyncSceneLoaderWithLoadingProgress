using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSceneUI : MonoBehaviour
{
    public void OnLoobySceneClick()
    {
        Debug.Log("Click On Lobby btn");
        Loader.Load(Loader.Scene.LobbyScene);

    }
}
