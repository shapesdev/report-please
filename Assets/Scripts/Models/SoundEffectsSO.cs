using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound Effect", menuName = "Sound Effect")]
public class SoundEffectsSO : ScriptableObject
{
    public AudioClip[] stampPlaceSounds;
    public AudioClip[] inspectorSounds;
    public AudioClip[] stampOpenSounds;
    public AudioClip[] highlightSounds;
    public AudioClip[] paperDragSounds;
    public AudioClip citationSound;
    public AudioClip discrepancySound;
    public AudioClip turnPageSound;
}
