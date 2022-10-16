namespace Jiufen.Audio
{
    public class AudioJob
    {
        public AudioAction action;
        public AudioType type;
        public bool fade;
        public float FadeTime;

        public AudioJob(AudioAction audioAction, AudioType audioType,bool fade, float fadeTime)
        {
            this.action = audioAction ;
            this.type = audioType;
            this.fade = fade  ;
            this.fadeTime = fadeTime;
        }
    }
}