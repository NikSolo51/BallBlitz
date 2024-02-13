using System.Linq;
using CodeBase.Logic;
using CodeBase.Services.StaticData;
using CodeBase.SoundManager;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        private const string InitialPointTag = "InitialPoint";
        private const string InitialCameraPointTag = "CameraInitialPoint";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData) target;

            if (GUILayout.Button("Collect"))
            {
                levelData.LevelKey = SceneManager.GetActiveScene().name;
                SoundManagerMarker soundManagerMarker = FindObjectOfType<SoundManagerMarker>();
                levelData.SoundManagerData = new SoundManagerData(soundManagerMarker.sounds,soundManagerMarker.clips,
                    soundManagerMarker.soundManagerType);
                
                levelData.InitialCameraPosition =
                    GameObject.FindGameObjectWithTag(InitialCameraPointTag).transform.position;
            }

            EditorUtility.SetDirty(target);
        }
    }
}