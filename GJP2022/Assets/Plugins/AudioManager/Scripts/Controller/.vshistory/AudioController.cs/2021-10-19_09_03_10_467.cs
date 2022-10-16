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
        public Hashtable _JobsTable;
        #endregion 1.Fields

        #region 2.Methods

        #region 2.3.Helpers
        private void Log(string message)
        {
            if (!_debug) return;
            Debug.Log($"Audio Controller message: {message}");
        }
        #endregion 2.3.Helpers

        #region 2.4.UnityEvents
        #endregion 2.4.UnityEvents
        #endregion 2.Methods
    }
}
