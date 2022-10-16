namespace Jiufen.Audio
{
    public class AudioJob
    {
        public AudioAction action;
        public AudioType type;

        public AudioJob(AudioAction audioAction, AudioType audioType)
        {
            this.action = audioAction ;
            this.type = audioType;
        }
    }
}