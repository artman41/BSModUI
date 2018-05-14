using System.Collections.Generic;
using System.Linq;
using BSModUI.VersionChecker.Interfaces;
using BSModUI.VersionChecker.Misc;
using IllusionPlugin;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VRUI;

namespace BSModUI
{
    public class ModMenuPlugin : IGithubInfoPlugin
    {
        public string Name => "Beat Saber Mod UI";

        public string Version => "0.0.1";

        public string GithubAuthor => "kaaori";

        public string GithubProjName => "BSModUI";

        public string[] Filter { get; }

        private LatestPluginInfoEvent _onInfoSet;
        private IEnumerable<IGithubInfoPlugin> Plugins => IllusionInjector.PluginManager.Plugins.Where(o => o is IGithubInfoPlugin).Cast<IGithubInfoPlugin>(); //the plugins that can be updated
        public static readonly List<LatestPluginInfo> PluginInfos = new List<LatestPluginInfo>(); //list of all the plugins iterated through
        private VcInterop _versionCheckerInterop;

        public void OnApplicationStart()
        {
            SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {

            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                ModMenuUi.OnLoad();
                Init();
            }
        }

        public void OnApplicationQuit()
        {
            SceneManager.activeSceneChanged -= SceneManagerOnActiveSceneChanged;
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        }

        private void SceneManagerOnActiveSceneChanged(Scene arg0, Scene scene)
        {
        }

        public void OnLevelWasLoaded(int level)
        {
        }

        public void OnLevelWasInitialized(int level)
        {

        }

        void Init()
        {
            if (_onInfoSet == null)
            {
                // Initializes the event and adds listener
                _onInfoSet = new LatestPluginInfoEvent();
                _onInfoSet.AddListener(OnSet); 
            }
            if (_versionCheckerInterop == null)
            {
                // creates the interop object if it doesn't exist so we can use it
                var x = new GameObject(); 
                _versionCheckerInterop = x.AddComponent<VcInterop>();
            }

            //if the array contains some objects we must have already ran this
            if (PluginInfos.Count != 0) return; 
            foreach (var plugin in Plugins)
            { 
                // get the plugins which implement IVerCheckPlugins
                // Don't worry about this, it handles all the retrieval on creation and calls OnInfoSet when the version has been set
                new LatestPluginInfo(plugin, _versionCheckerInterop, _onInfoSet);
            }
        }

        public void OnUpdate()
        {
            // Debug util
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                SceneDumper.DumpScene();
            }
            if (Input.GetKeyDown(KeyCode.Home))
            {
            }
        }

        public void OnFixedUpdate()
        {
        }

        public void OnLateUpdate()
        {
        }

        private void OnSet(LatestPluginInfo info)
        {
            Utils.Log($"{info.Plugin.Name} is {(info.IsLatestVersion ? " " : " not ")} up to date");

            // adds it to the local array of Plugins
            PluginInfos.Add(info); 
        }
    }
}