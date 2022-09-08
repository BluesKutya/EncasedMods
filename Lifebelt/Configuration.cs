using BepInEx.Configuration;
using BepInEx.Logging;

namespace Lifebelt.Configurations
{
    public sealed class Configuration
    {

        private Configuration() {}

        public static volatile Configuration instance;

        public static Configuration getInstance() {
            return instance;
        }

        public static ManualLogSource Log { get; set; }

        private ConfigFile config { get; set; }

        public ConfigEntry<bool> isAllItemWeightZero { get; private set; }

        public ConfigEntry<bool> isRadiationDisabled { get; private set; }
        public ConfigEntry<bool> isFatigueDisabled { get; private set; }
        public ConfigEntry<bool> isHungerDisabled { get; private set; }
        public ConfigEntry<bool> isThirstDisabled { get; private set; }


        public Configuration(ConfigFile _config)
        {
            config = _config;

            isAllItemWeightZero = config.Bind("General", "IsAllItemWeightZero", true, "If enabled, all item weight zero when calculating the encumbrance.");

            isRadiationDisabled = config.Bind("General", "IsRadiationDisabled", true, "If enabled, all negative Radiation status changes will be ignored.");
            isFatigueDisabled = config.Bind("General", "IsFatigueDisabled", true, "If enabled, all negative Fatigue status changes will be ignored.");
            isHungerDisabled = config.Bind("General", "IsHungerDisabled", true, "If enabled, all negative Hunger status changes will be ignored.");
            isThirstDisabled = config.Bind("General", "IsThirstDisabled", true, "If enabled, all negative Thirst status changes will be ignored.");

        }

    }

}
