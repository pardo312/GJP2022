using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jiufen.Audio
{
    public class AudioController : MonoBehaviour
    {

        #region 1.Fields
        public bool m_debug;
        public AudioTrack[] m_audioTracks;
        public Hashtable m_audioTable;

        public AudioJobsController m_audioJobsController;
        public static AudioController Instance;
        #endregion 1.Fields

        #region 2.Methods

        #region 2.1.UnityEvents
        public void Awake()
        {
            if (Instance != null) return;
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

        #endregion 2.3.Jobs Handling

        #region 2.4.Helpers
        private void Configure()
        {
            Instance = this;
            m_audioTable = new Hashtable();
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
            Debug.Log($"<color=blue>Audio Controller message:</color> {message}");
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
