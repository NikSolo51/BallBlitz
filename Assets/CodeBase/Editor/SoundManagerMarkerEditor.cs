using System.Collections.Generic;
using System.Linq;
using CodeBase.Services.StaticData;
using CodeBase.SoundManager;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(SoundManagerMarker))]
    public class SoundManagerMarkerEditor : UnityEditor.Editor
    {
        private Dictionary<string, LevelStaticData> _levels;
        private bool initData;

        private void OnValidate()
        {
            UpdateSounds();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("UpdateSounds"))
            {
                UpdateSounds();
            }
        }

        private void UpdateSounds()
        {
            LoadLevels();

            SoundManagerMarker soundManagerMarker = (SoundManagerMarker) target;
            LevelStaticData levelData = ForLevel(SceneManager.GetActiveScene().name);
            levelData.SoundManagerData._sounds = soundManagerMarker.sounds;
            levelData.SoundManagerData._clips = soundManagerMarker.clips;
            EditorUtility.SetDirty(target);
        }


        private void LoadLevels()
        {
            _levels = Resources.LoadAll<LevelStaticData>("StaticData/Levels").ToDictionary(x => x.LevelKey, x => x);
        }

        public LevelStaticData ForLevel(string sceneKey)
        {
            return _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
                ? staticData
                : null;
        }
    }
}