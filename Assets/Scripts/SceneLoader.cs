using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AnimaRes
{
    public class SceneLoader : MonoBehaviour
    {
        public const string FIRST_SCENE = "Scene_1";
        public const string SECOND_SCENE = "Scene_2";
        public const string THIRD_SCENE = "Scene_3";

        public static int GetActiveSceneCount => AllAvailableGameObjectsByScene.Keys.Count;
        public static event Action<string> OnSceneLoaded = delegate { };
        public static event Action<string> OnSceneUnloaded = delegate { };
        public static Dictionary<string, GameObject[]> AllAvailableGameObjectsByScene = new Dictionary<string, GameObject[]>();

        private void OnEnable()
        {
            SceneManager.sceneLoaded += SceneLoadedHandler;
            SceneManager.sceneUnloaded += SceneUnloadedHandler;
        }

        public static void MoveGameObjectToScene(GameObject gameObject, string sceneName)
        {
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName(sceneName));
        }

        public static void Load(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        public static void Unload(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }

        private static void SceneLoadedHandler(Scene scene, LoadSceneMode mode)
        {
            if (AllAvailableGameObjectsByScene.ContainsKey(scene.name))
            {
                AllAvailableGameObjectsByScene[scene.name] = scene.GetRootGameObjects();
            }
            else
            {
                AllAvailableGameObjectsByScene.Add(scene.name, scene.GetRootGameObjects());
            }

            OnSceneLoaded?.Invoke(scene.name);
        }

        private static void SceneUnloadedHandler(Scene scene)
        {
            if (AllAvailableGameObjectsByScene.ContainsKey(scene.name))
            {
                AllAvailableGameObjectsByScene.Remove(scene.name);
            }

            OnSceneUnloaded?.Invoke(scene.name);
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= SceneLoadedHandler;
            SceneManager.sceneUnloaded -= SceneUnloadedHandler;
        }
    }
}