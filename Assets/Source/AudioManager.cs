using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum SE
{
    dead,
    click,
    newRecord
}
public enum BGM
{
    title,
    normal,
}

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] AudioSource audioSource;
    Dictionary<SE, AudioClip> SEClips = new Dictionary<SE, AudioClip>();
    Dictionary<BGM, AudioClip> BGMCLips = new Dictionary<BGM, AudioClip>();

    private void SetData()
    {
        foreach (SE se in Enum.GetValues(typeof(SE)))
        {
            SEClips.Add(se, Resources.Load<AudioClip>($"sounds/SE{se}"));
        }
        foreach (BGM bgm in Enum.GetValues(typeof(BGM)))
        {
            BGMCLips.Add(bgm, Resources.Load<AudioClip>($"sounds/BGM{bgm}"));
        }
    }

    public void PlayBGM(BGM bgm)
    {
        if(BGMCLips.TryGetValue(bgm, out var clip))
            audioSource.clip = clip;
        ChangeBGMspeed(1.0f);
        audioSource.Play();
    }

    public void PlayBGMContinuously(BGM bgm)
    {
        var time = audioSource.time;
        if (BGMCLips.TryGetValue(bgm, out var clip))
            audioSource.clip = clip;
        audioSource.Play();
        audioSource.time = time;
    }
    public void StopBGM()
    {
        audioSource.Stop();
    }
    public void PlaySE(SE se)
    {
        if (SEClips.TryGetValue(se, out var clip))
            audioSource.PlayOneShot(clip);
    }
    public void ChangeBGMspeed(float timescale)
    {
        audioSource.pitch = timescale;
    }

    public override void Initialize()
    {
        base.Initialize();
        audioSource = GetComponent<AudioSource>();
        SetData();
    }
}
