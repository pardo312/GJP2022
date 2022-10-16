using System.Collections;
using UnityEngine;

namespace Jiufen.Audio
{

    public class AudioJobStart : AudioJob
    {
        public AudioJobStart(string key, AudioJobOptions audioJobExtras = null) : base(key, audioJobExtras)
        {
            action = AudioAction.START;
        }

        public override IEnumerator RunAudioJob(AudioTrack track, AudioClip clip)
        {
            yield return base.RunAudioJob(track, clip);

            track.audioSource.Play();

            if (options.fadeIn != null)
            {
                var initVolume = options.fadeIn.initVolume != -1 ? options.fadeIn.initVolume : 0;
                var endVolume = options.fadeIn.endVolumen != -1 ? options.fadeIn.endVolumen : 1;

                yield return FadeAudio(track, options.fadeIn.fadeDuration, initVolume, endVolume, options.fadeIn.callback);
            }
        }
    }

}