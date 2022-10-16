using System.Collections;
using UnityEngine;

namespace Jiufen.Audio
{
    public class AudioJob
    {
        public AudioAction action;
        public AudioType type;
        public bool fade;
        public WaitForSeconds delay;

        public AudioJob(AudioAction audioAction, AudioType audioType, bool fade, float delay)
        {
            this.action = audioAction;
            this.type = audioType;
            this.fade = fade;
            this.delay = delay > 0f ? new WaitForSeconds(delay) : null;
        }
    }
    public struct FadeInfo
    {
        public bool fadeIn;
        public bool fadeOut;

    }
}