using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Vidly.AppCode
{
    // This is custom code for encrypting our connection string 
    // present in the web.config. 
    // Sealed just for the sake of it.
    public sealed class AutoEncryptConnectionStrings
    {
        public static void AutoEncrypt()
        {
            // don't encrypt if debug=true in the web.config
            if (HttpContext.Current.IsDebuggingEnabled) return;

            var configSection = GetConfigurationSection();

            if (configSection.SectionInformation.IsProtected) return;

            configSection.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
            configSection.SectionInformation.ForceSave = true;
            configSection.CurrentConfiguration.Save();
        }

        public static void Decrypt()
        {
            var configSection = GetConfigurationSection();

            if (!configSection.SectionInformation.IsProtected) return;

            configSection.SectionInformation.UnprotectSection();
            configSection.SectionInformation.ForceSave = true;
            configSection.CurrentConfiguration.Save();
        }

        private static ConfigurationSection GetConfigurationSection()
        {
            Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~");

            var configSection = configuration.GetSection("connectionStrings");
            if (configSection.ElementInformation.IsLocked || configSection.SectionInformation.IsLocked) return null;

            return configSection;
        }
    }
}