using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AnimaRes
{
    public class SceneLoader : MonoBehaviour
    {
        public static event Action<string> OnSceneLoaded = delegate { };
        public static event Action<string> OnSceneUnloaded = delegate { };

        public static Dictionary<string, GameObject[]> AllAvailableGameObjectsByScene = new Dictionary<string, GameObject[]>();

        public static void MoveGameObjectToScene(GameObject gameObject, string sceneName)
        {
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName(sceneName));
        }

        public static void Load(string sceneName)
        {
            SceneManager.sceneLoaded += SceneLoadedHandler;
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        public static void Unload(string sceneName)
        {
            SceneManager.sceneUnloaded += SceneUnloadedHandler;
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
            SceneManager.sceneLoaded -= SceneLoadedHandler;
        }

        private static void SceneUnloadedHandler(Scene scene)
        {
            OnSceneUnloaded?.Invoke(scene.name);
            SceneManager.sceneUnloaded -= SceneUnloadedHandler;
        }
    }
}