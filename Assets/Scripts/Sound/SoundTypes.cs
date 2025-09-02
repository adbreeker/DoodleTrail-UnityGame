using System;
using UnityEngine;

public class SoundType // configuration for AudioSourceBehavior
{
    // Flags
    public bool IsPersistent { get; private set; }
    public bool IsLooping { get; private set; }
    public bool IsRandomized { get; private set; }
    public bool IsPausable { get; private set; }
    public bool IsMutable { get; private set; }

    // Values
    public Tuple<float, float> PitchRange { get; private set; }
    public float VolumeMultiplier { get; private set; }

    // Constructor
    public SoundType(
        bool isPersistent,
        bool isLooping,
        bool isRandomized,
        bool isPausable,
        bool isMutable,
        Tuple<float, float> pitchRange,
        float volumeMultiplier)
    {
        IsPersistent = isPersistent;
        IsLooping = isLooping;
        IsRandomized = isRandomized;
        IsPausable = isPausable;
        IsMutable = isMutable;
        PitchRange = pitchRange;
        VolumeMultiplier = volumeMultiplier;
    }

    //Sound Types
    public static SoundType GetType_OneShotSingleUse()
    {
        return new SoundType(
            isPersistent: false,
            isLooping: false,
            isRandomized: false,
            isPausable: true,
            isMutable: true,
            pitchRange: null,
            volumeMultiplier: 1f
        );
    }

    public static SoundType GetType_OneShotMultiUse()
    {
        return new SoundType(
            isPersistent: false,
            isLooping: false,
            isRandomized: true,
            isPausable: true,
            isMutable: true,
            pitchRange: Tuple.Create(0.9f, 1.1f),
            volumeMultiplier: 1f
        );
    }

    public static SoundType GetType_OneShotUI()
    {
        return new SoundType(
            isPersistent: true,
            isLooping: false,
            isRandomized: false,
            isPausable: false,
            isMutable: false,
            pitchRange: null,
            volumeMultiplier: 1f
        );
    }

    public static SoundType GetType_Looped()
    {
        return new SoundType(
            isPersistent: true,
            isLooping: true,
            isRandomized: false,
            isPausable: true,
            isMutable: true,
            pitchRange: null,
            volumeMultiplier: 1f
        );
    }
}
