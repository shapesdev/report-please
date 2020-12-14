using System;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine;

public class GameIntroPlayer
{
    private PlayableDirector introDirector;

    public GameIntroPlayer(PlayableDirector director)
    {
        introDirector = director;
    }

    public void PlayIntro(DateTime day, Text introDateText, AudioSource source)
    {
        if (introDirector != null)
        {
            TimelineAsset timelineAsset = (TimelineAsset)introDirector.playableAsset;

            foreach (PlayableBinding output in timelineAsset.outputs)
            {
                if (output.streamName == "Audio Track")
                {
                    introDirector.SetGenericBinding(output.sourceObject, source);
                }
            }
            introDirector.Play();
        }
        TextWriterHelper.instance.AddWriter(introDateText, day.ToString("MMMM dd, yyyy"), 0.08f);
    }
}
