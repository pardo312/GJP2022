using System;
using System.Collections;
using UnityEngine;

namespace Jiufen.Audio
{
    public class AudioFadeInfo
    {

        public float fadeDuration;
        public float initVolume;
        public float endVolumen;
        public Action callback;

        public AudioFadeInfo(float _fadeDuration = 1, float _initVolume = -1, float _endVolume = -1, Action _callback = null)
        {
            fadeDuration = _fadeDuration;
            initVolume = _initVolume;
            endVolumen = _endVolume;
            callback = _callback;
        }
    }
}