using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public UnityEngine.Audio.AudioMixer mainMixer;

    UnityEngine.Audio.AudioMixerSnapshot normalSnapshot;
    UnityEngine.Audio.AudioMixerSnapshot battleSnapshot;


    // Start is called before the first frame update
    void Start()
    {
        battleSnapshot = mainMixer.FindSnapshot("Battle");
        normalSnapshot = mainMixer.FindSnapshot("Normal");
    }



    // Update is called once per frame
    void Update()
    {
        if(AIBlackboard.instance.PlayerNearEnemy(60))
        {
            battleSnapshot.TransitionTo(1f);
        }
        else
        {
            normalSnapshot.TransitionTo(2.8f);
        }

        
    }
}
