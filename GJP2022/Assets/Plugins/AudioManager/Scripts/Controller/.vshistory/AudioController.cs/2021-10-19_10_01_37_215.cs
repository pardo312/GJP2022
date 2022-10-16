using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jiufen.Audio
{
    public partial class AudioController : MonoBehaviour
    {

        #region 1.Fields
        public bool m_debug;
        public AudioTrack[] m_audioTracks;
        public Hashtable m_audioTable;
        public Hashtable m_jobsTable;

        public AudioController Instace;
        #endregion 1.Fields

        #region 2.Methods

        #region 2.1.UnityEvents
        public void Awake()
        {
            if (Instace != null) return;
            else
            {
                Configure();
            }
        }
        public void OnDisable()
        {
            Dispose();
        }

        #endregion 2.1.UnityEvents

        #region 2.2.Audio Behaviours
        public void PlayAudio(AudioType audioType)
        {
            AddJob(new AudioJob(AudioAction.START, audioType));
        }
        public void StopAudio(AudioType audioType)
        {
            AddJob(new AudioJob(AudioAction.STOP, audioType));
        }
        public void RestartAudio(AudioType audioType)
        {
            AddJob(new AudioJob(AudioAction.RESTART, audioType));
        }
        #endregion 2.2.Audio Behaviours

        #region 2.3.Jobs Handling
        private void AddJob(AudioJob _audioJob)
        {
            RemoveConflictingJobs(_audioJob.type);

            //Add Job
            IEnumerator jobRunner = RunAudioJob(_audioJob);
            m_jobsTable.Add(_audioJob.type, _audioJob);
            Log($"Starting Job {_audioJob.type}")
        }

        private IEnumerator RunAudioJob(AudioJob audioJob)
        {
            throw new NotImplementedException();
        }

        private void RemoveConflictingJobs(AudioType _type)
        {
            if (m_jobsTable.Contains(_type))
            {
                RemoveJob(_type);
            }

            AudioType audioTypeConflict = AudioType.None;

            foreach (DictionaryEntry job in m_jobsTable)
            {
                AudioTrack currentTrack = (AudioTrack)m_audioTable[job.Key];
                AudioTrack newTrack = (AudioTrack)m_audioTable[_type];
                if (currentTrack._audioSource == newTrack._audioSource)
                {
                    LogError($"You have the same audio source for different audioTypes. Please check audioType [{job.Key}] and [{_type}] ");
                    audioTypeConflict = _type;
                }
            }
            if (audioTypeConflict != AudioType.None)
            {
                RemoveJob(audioTypeConflict);
            }
        }

        private void RemoveJob(AudioType audioType)
        {

        }

        #endregion 2.3.Jobs Handling

        #region 2.4.Helpers
        private void Configure()
        {
            Instace = this;
            m_audioTable = new Hashtable();
            m_jobsTable = new Hashtable();
            ConfigureAudioTable();
        }

        private void ConfigureAudioTable()
        {
            foreach (AudioTrack tracks in m_audioTracks)
            {
                foreach (AudioObject audioObj in tracks._audioObject)
                {
                    if (m_audioTable.Contains(audioObj._audioType))
                    {
                        LogError($"AudioTable is registering an already registered audio type: {audioObj._audioType}");
                    }
                    else
                    {
                        m_audioTable.Add(audioObj._audioType, tracks);
                        Log($"Audio type {audioObj._audioType} has been registered in the audioTable");
                    }
                }
            }

        }

        private void Dispose()
        {

        }

        private void Log(string message)
        {
            if (!m_debug) return;
            Debug.Log($"<color=yellow>Audio Controller message:</color> {message}");
        }
        private void LogError(string message)
        {
            if (!m_debug) return;
            Debug.Log($"<color=red>Audio Controller error message:</color> {message}");
        }
        #endregion 2.4.Helpers

        #endregion 2.Methods
    }
}
