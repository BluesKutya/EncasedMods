using System.Reflection;
using HarmonyLib;
using BepInEx;
using Lifebelt.Configurations;
using BepInEx.Unity.IL2CPP;
using BepInEx.Logging;

namespace Lifebelt
{

    [BepInPlugin(GUID, "Lifebelt", "1.0.1")]
    public class Plugin : BasePlugin
    {
        private const string GUID = "blueskutya.encased.lifebelt";

        public static volatile Plugin instance;

        internal static ManualLogSource Logger { get; private set; }

        public static Plugin getInstance()
        {
            return instance;
        }

        public override void Load() {
            // Plugin startup logic

            Logger = Log;

            instance = this;

            //init the Configuration
            Configuration.instance = new Configuration(Config);
            Configuration.Log = Log;

            var harmony = new Harmony(GUID);

            var assembly = Assembly.GetExecutingAssembly();
            harmony.PatchAll(assembly);

            Log.LogInfo($"Plugin {GUID} is loaded!");
        }

    }

}
