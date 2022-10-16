namespace Jiufen.Audio
{
    public class AudioJob
    {
        public AudioAction audioAction;
        public AudioType type;

        public AudioJob(AudioAction audioAction, AudioType audioType)
        {
            this.audioAction = audioAction ;
            this.type = audioType;
        }
    }
}