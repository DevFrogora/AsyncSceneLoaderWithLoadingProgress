using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LobbySceneUI : MonoBehaviour
{
    int mapSceneIndex = 1;
    public Image voiceImage;
    public Sprite voiceDisableSprite;
    public Sprite voiceEnableSprite;

    public Image speakerImage;
    public Sprite speakerEnableSprite;
    public Sprite speakerDisableSprite;

    enum VoiceStatus{
        Enable,
        Disable,
    };

    enum SpeakerStatus
    {
        Enable,
        Disable,
    };

    VoiceStatus voiceStatus;
    SpeakerStatus speakerStatus;

    private void Start()
    {
        speakerImage.sprite = speakerDisableSprite;
        speakerStatus = SpeakerStatus.Disable;
        voiceImage.sprite = voiceDisableSprite;
        voiceStatus = VoiceStatus.Disable;
    }


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


    public void OnVoiceBtnClick()
    {
        if(voiceStatus == VoiceStatus.Enable)
        {
            voiceImage.sprite = voiceDisableSprite;
            voiceStatus = VoiceStatus.Disable;
        }
        else
        {
            voiceImage.sprite = voiceEnableSprite;
            voiceStatus = VoiceStatus.Enable;
        }
    }

    public void OnSpeakerBtnClick()
    {
        if (speakerStatus == SpeakerStatus.Enable)
        {
            speakerImage.sprite = speakerDisableSprite;
            speakerStatus = SpeakerStatus.Disable;
        }
        else
        {
            speakerImage.sprite = speakerEnableSprite;
            speakerStatus = SpeakerStatus.Enable;
        }

        // instead of enum use bool and toogle it
    }


    bool ismapOpen= false;
    public Image bigMapImage;
    public void onMapClicked()
    {
        if(ismapOpen)
        {
            bigMapImage.gameObject.SetActive(false);
            ismapOpen = false;
            Debug.Log("map close");
            return;
        }
        bigMapImage.gameObject.SetActive(true);
        ismapOpen = true;
        Debug.Log("map open");
    }
}
