#region License

/*
 
Copyright (c) 2012 Danko Kozar

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 
*/

#endregion License

using System;
using System.Collections.Generic;
using System.Reflection;
using eDriven.Core.Managers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace eDriven.Audio
{
    ///<summary>
    ///</summary>
    [AddComponentMenu("eDriven/Audio/AudioPlayerMapper")]

    /// <summary>
    /// AudioPlayerMapper class maps strings to AudioPlayers (via PowerMapper pattern)
    /// </summary>
    public class AudioPlayerMapper : AudioPlayer
    {
#if DEBUG
// ReSharper disable UnassignedField.Global
        /// <summary>
        /// Debug mode
        /// </summary>
        public static bool DebugMode;

// ReSharper restore UnassignedField.Global
#endif

        protected AudioPlayerMapper()
        {
            // constructor is protected
        }

        private static AudioPlayerMapper _defaultMapper;
        private static Dictionary<string, AudioPlayerMapper> _mappers;

        public bool Default;

        public string Id;

// ReSharper disable UnusedMember.Local
        [Obfuscation(Exclude = true)]
        void OnDisable()
// ReSharper restore UnusedMember.Local
        {
            _initialized = false;
            SystemManager.Instance.DisposingSignal.Disconnect(DisposingSlot);
        }

// ReSharper disable UnusedMember.Local
        [Obfuscation(Exclude = true)]
        void Start()
// ReSharper restore UnusedMember.Local
        {
            if (!Default && string.IsNullOrEmpty(Id))
                throw new Exception("AudioPlayerMapper error: Id not set for a non-default mapper");

            SystemManager.Instance.DisposingSignal.Connect(DisposingSlot, true);
        }

        private static void DisposingSlot(object[] parameters)
        {
#if DEBUG
            if (DebugMode)
            {
                Debug.Log("AudioPlayerMapper>DisposingSlot");
            }
#endif
            _mappers.Clear();
            _defaultMapper = null;
            _initialized = false;
        }

        private static bool _initialized;
        private static void Initialize()
        {
#if DEBUG
            if (DebugMode)
            {
                Debug.Log("Initializing AudioPlayerMapper");
            }
#endif
            _mappers = new Dictionary<string, AudioPlayerMapper>();

            Object[] mappers = FindObjectsOfType(typeof(AudioPlayerMapper));

#if DEBUG
            if (DebugMode)
            {
                Debug.Log(string.Format("   -> Found {0} AudioPlayerMappers", mappers.Length));
            }
#endif
            foreach (Object o in mappers)
            {
                AudioPlayerMapper mapper = (AudioPlayerMapper)o;

                if (mapper.Default)
                {
                    if (null != _defaultMapper)
                        Debug.LogWarning("Duplicated default AudioPlayerMapper");

                    _defaultMapper = mapper;
                }

                if (!string.IsNullOrEmpty(mapper.Id))
                {

                    if (_mappers.ContainsKey(mapper.Id))
                        Debug.LogWarning("Duplicated AudioPlayerMapper for: " + mapper.Id);
                    else
                        _mappers.Add(mapper.Id, mapper);
                }
            }

            _initialized = true;
        }

        public static bool IsMapping(string id)
        {
            if (!_initialized)
                Initialize();

            return _mappers.ContainsKey(id);
        }

        public static AudioPlayerMapper Get(string id)
        {
            if (!_initialized)
                Initialize();

            if (!_mappers.ContainsKey(id))
                return null;

            return _mappers[id];
        }

        public static bool HasDefault(string id)
        {
            return null == _defaultMapper;
        }

        public static AudioPlayerMapper GetDefault()
        {
            if (!_initialized)
                Initialize();

            if (null == _defaultMapper)
                throw new Exception("Default mapper not defined");

            return _defaultMapper;
        }

        public static AudioPlayerMapper GetWithFallback(string id)
        {
            if (IsMapping(id))
                return Get(id);

            return GetDefault();
        }
    }
}