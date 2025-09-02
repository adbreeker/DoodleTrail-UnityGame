using System;
using UnityEngine;

public class SoundType // configuration for AudioSourceBehavior
{
    // Flags
    public bool IsPersistent { get; private set; }
    public bool IsLooping { get; private set; }
    public bool IsRandomized { get; private set; }
    public bool IsPausable { get; private set; }

    // Values
    public float DestroyTime { get; private set; }
    public Tuple<float, float> PitchRange { get; private set; }

    public float VolumeMultiplier { get; private set; }

    public SoundType(
        bool isPersistent,
        bool isLooping,
        bool isRandomized,
        bool isPausable,
        float destroyTime,
        Tuple<float, float> pitchRange,
        float volumeMultiplier)
    {
        IsPersistent = isPersistent;
        IsLooping = isLooping;
        IsRandomized = isRandomized;
        IsPausable = isPausable;
        DestroyTime = destroyTime;
        PitchRange = pitchRange;
        VolumeMultiplier = volumeMultiplier;
    }
}

public class SoundTypes
{
    public static SoundType OneShot_SingleUse = new SoundType(
        isPersistent: false,
        isLooping: false,
        isRandomized: false,
        isPausable: true,
        destroyTime: 0f,
        pitchRange: null,
        volumeMultiplier: 1f
    );
    public static SoundType OneShot_MultiUse = new SoundType(
        isPersistent: false,
        isLooping: false,
        isRandomized: true,
        isPausable: true,
        destroyTime: 0f,
        pitchRange: Tuple.Create(0.9f, 1.1f),
        volumeMultiplier: 1f
    );
    public static SoundType OneShot_UI = new SoundType(
        isPersistent: true,
        isLooping: false,
        isRandomized: false,
        isPausable: false,
        destroyTime: 0f,
        pitchRange: null,
        volumeMultiplier: 1f
    );
    public static SoundType Looped = new SoundType(
        isPersistent: true,
        isLooping: true,
        isRandomized: false,
        isPausable: true,
        destroyTime: -1f,
        pitchRange: null,
        volumeMultiplier: 1f
    );
}
