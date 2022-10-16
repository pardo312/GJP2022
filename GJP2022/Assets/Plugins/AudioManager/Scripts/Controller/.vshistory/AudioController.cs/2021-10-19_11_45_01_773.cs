using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jiufen.Audio
{
    public class AudioController : MonoBehaviour
    {

        #region 1.Fields
        private AudioTrack[] m_audioTracks;
        public Hashtable m_audioTable;
        public bool debug;

        public static AudioController Instance;
        private AudioJobsController m_audioJobsController;
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
            m_audioJobsController.AddJob(new AudioJob(AudioAction.START, audioType));
        }
        public void StopAudio(AudioType audioType)
        {
            m_audioJobsController.AddJob(new AudioJob(AudioAction.STOP, audioType));
        }
        public void RestartAudio(AudioType audioType)
        {
            m_audioJobsController.AddJob(new AudioJob(AudioAction.RESTART, audioType));
        }
        #endregion 2.2.Audio Behaviours

        #region 2.3.Helpers
        private void Configure()
        {
            Instance = this;
            AudioLogger.m_debug = debug;

            m_audioTable = new Hashtable();
            m_audioJobsController.Init();
            GenerateAudioTable();
        }

        private void GenerateAudioTable()
        {
            foreach (AudioTrack tracks in m_audioTracks)
            {
                foreach (AudioObject audioObj in tracks._audioObject)
                {
                    if (m_audioTable.Contains(audioObj._audioType))
                    {
                        AudioLogger.LogError($"AudioTable is registering an already registered audio type: {audioObj._audioType}");
                    }
                    else
                    {
                        m_audioTable.Add(audioObj._audioType, tracks);
                        AudioLogger.Log($"Audio type {audioObj._audioType} has been registered in the audioTable");
                    }
                }
            }

        }

        private void Dispose()
        {
            m_audioJobsController.Dispose();
        }
        #endregion 2.3.Helpers
        #endregion 2.Methods
    }
}
