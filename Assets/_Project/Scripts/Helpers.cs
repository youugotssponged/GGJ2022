using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using static System.IO.Directory;
using static System.IO.Path;
using static UnityEngine.Application;
using static UnityEditor.AssetDatabase;

namespace GGJ2022 {

    public static class JStateTests
    {
        [MenuItem("TestFuncs/StateManagers/ChangeToMainMenuState")]
        public static void ChangeSceneToMainMenu()
        {
            GlobalSceneManager._Instance?.UpdateSceneManagerState(GlobalSceneManager.SceneManagerState.MAINMENU);
        }

        [MenuItem("TestFuncs/StateManagers/ChangeToSplashScreenState")]
        public static void ChangeSceneToSplashScreen()
        {
            GlobalSceneManager._Instance?.UpdateSceneManagerState(GlobalSceneManager.SceneManagerState.SPLASHSCREEN);
        }
    }

    public static class JToolsMenu 
    {
        [MenuItem("Tools/Setup/CreateDefaultFolders")]
        public static void CreateDefaultFolders()
        {
            var root = "_Project";

            string[] dirs = new string[]
            {
                "Scripts", "Scenes", "Prefabs",
                "Textures", "UI", "Materials", 
                "Audio", "Animation", "SaveFiles", 
                "Testing"
            };
            
            CreateDirectories(root, dirs);
            Refresh();
        }

        public static void CreateDirectories(string root, params string[] dirs) 
        {
            var fullPath = Combine(dataPath, root);
            foreach(var dir in dirs)
                CreateDirectory(Combine(fullPath, dir));
        }
    }


    public static class Helpers 
    {
        // Main Camera Cache Ref
        // Camera.main sadly is a poor ref for the engine to find a camera tagged with "Main Camera" as it's tag in the editor...
        // Storing this ref allows the camera to be called once.
        private static Camera _camera;
        public static Camera Camera 
        {
            get 
            {
                if(_camera == null) _camera = Camera.main;
                return _camera;
            }
        }

        // Cleaner way of co-routining wait for seconds,
        // as yeild return new WaitForSeconds causes GC allocations, especially when for looped or while looped over which does a bit of stack busting
        // 
        // Essentially cache's frequently used calls to WaitForSeconds() and reuses when needed during runtime
        // stopping the GC to keep on spawning coroutines to waitforseconds when you can just reuse them/respin them
        private static readonly Dictionary<float, WaitForSeconds> WaitDict = new Dictionary<float, WaitForSeconds>();
        public static WaitForSeconds GetWait(float time)
        {
            if(WaitDict.TryGetValue(time, out var wait)) 
                return wait;
            
            WaitDict[time] = new WaitForSeconds(time);
            return WaitDict[time];
        }

        // Friendly check for detecting if mouse is over UI (Generically any piece of Unity UI / Canvas / Rect element)
        // Can be used to POLL for when it is over a given UI - can be used in a component's Update() 
        private static PointerEventData _eventDataCurrentPos;
        private static List<RaycastResult> _results;
        public static bool isMousePointerOverUI()
        {
            _eventDataCurrentPos = 
                    new PointerEventData(EventSystem.current) 
                    { 
                        position = Input.mousePosition 
                    };

            _results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(_eventDataCurrentPos, _results);

            return _results.Count > 0;
        }

        // Transform Extension to delete tree except parent
        // In english destroy all sub objects on a component / object
        //
        // e.g  
        // public Transform Parent;
        // Parent.DeleteChildren()
        public static void DeleteChildren(this Transform t){
            foreach(Transform child in t)
                Object.Destroy(child.gameObject);
        }
    }
}