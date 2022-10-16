using System.Collections;
using UnityEngine;

namespace Jiufen.Audio
{
    public class AudioJob
    {
        public AudioAction action;
        public AudioType type;
        public FadeInfo fadeIn;
        public WaitForSeconds delay;

        public AudioJob(AudioAction audioAction, AudioType audioType, bool fade, float delay)
        {
            this.action = audioAction;
            this.type = audioType;
            this.fadeIn.fade = fade;
            this.delay = delay > 0f ? new WaitForSeconds(delay) : null;
        }
    }
    public struct FadeInfo
    {
        public bool fade;
        public float fadeDuration;
        public float fadeTargetVolume;
    }
}