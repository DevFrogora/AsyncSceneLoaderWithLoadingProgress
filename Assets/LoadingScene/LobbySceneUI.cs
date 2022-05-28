using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySceneUI : MonoBehaviour
{
    int mapSceneIndex = 1;

    public void MapSelection(int _mapSceneIndex)
    {
        mapSceneIndex = _mapSceneIndex + 1; // as all the map scene start after lobby scene i.e from 1th index
        Debug.Log(_mapSceneIndex);
    }
    public void OnPlayBtnClick()
    {
        Debug.Log(mapSceneIndex);
        if (mapSceneIndex == ((int)Loader.Scene.IslandScene))
        {
            Debug.Log("Click On Island Scene btn");
            Loader.Load(Loader.Scene.IslandScene);
        }

        if (mapSceneIndex == ((int)Loader.Scene.TrainingScene))
        {
            Debug.Log("Click On Training Scene btn");
            Loader.Load(Loader.Scene.TrainingScene);
        }

    }
}
