using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jiufen.Audio
{
    public class AudioController : MonoBehaviour
    {

        #region 1.Fields
        public bool _debug;
        public AudioTrack[] _audioTracks;
        public Hashtable _audioTable;
        public Hashtable _jobsTable;

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

        #region 2.3.Helpers
        private void Configure()
        {
            Instace = this;
            _audioTable = new Hashtable();
            _jobsTable = new Hashtable();
            ConfigureAudioTable();
        }

        private void ConfigureAudioTable()
        {

        }

        private void Dispose()
        {

        }

        private void Log(string message)
        {
            if (!_debug) return;
            Debug.Log($"<color=yellow>Audio Controller message:</color> {message}");
        }
        #endregion 2.3.Helpers

        #endregion 2.Methods
    }
}
