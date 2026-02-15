using System.Reflection;
using System.Resources;

namespace SipLine.Plugin.HelloWorld.Resources
{
    public static class Strings
    {
        private static ResourceManager? _resourceManager;
        public static ResourceManager ResourceManager => _resourceManager ??= new ResourceManager("SipLine.Plugin.HelloWorld.Resources.Strings", typeof(Strings).Assembly);

        public static string GuideTitle => ResourceManager.GetString("GuideTitle") ?? "Developer Guide";
        public static string GuideSubtitle => ResourceManager.GetString("GuideSubtitle") ?? "Welcome to the SipLine ecosystem!";
        public static string WhatIsPlugin => ResourceManager.GetString("WhatIsPlugin") ?? "What is a Plugin?";
        public static string WhatIsPluginDesc => ResourceManager.GetString("WhatIsPluginDesc") ?? "SipLine plugins are external modules (DLLs) that extend the softphone's functionality.";
        public static string Capabilities => ResourceManager.GetString("Capabilities") ?? "What you can do";
        public static string CapCallControl => ResourceManager.GetString("CapCallControl") ?? "Call Control";
        public static string CapCallControlDesc => ResourceManager.GetString("CapCallControlDesc") ?? "Answer, hang up, mute, hold.";
        public static string CapUiExtensions => ResourceManager.GetString("CapUiExtensions") ?? "UI Extensions";
        public static string CapUiExtensionsDesc => ResourceManager.GetString("CapUiExtensionsDesc") ?? "Add sidebar buttons, tabs, or full pages.";
        public static string CapEvents => ResourceManager.GetString("CapEvents") ?? "Events";
        public static string CapEventsDesc => ResourceManager.GetString("CapEventsDesc") ?? "React to incoming/outgoing calls.";
        public static string CapStorage => ResourceManager.GetString("CapStorage") ?? "Storage & Config";
        public static string CapStorageDesc => ResourceManager.GetString("CapStorageDesc") ?? "Save secure settings.";
        public static string QuickStart => ResourceManager.GetString("QuickStart") ?? "Quick Start";
        public static string QuickStartStep1 => ResourceManager.GetString("QuickStartStep1") ?? "1. Create a .NET 9 Class Library project";
        public static string QuickStartStep2 => ResourceManager.GetString("QuickStartStep2") ?? "2. Install the SipLine.Plugin.Sdk package";
        public static string QuickStartStep3 => ResourceManager.GetString("QuickStartStep3") ?? "3. Implement the ISipLinePlugin interface";
        public static string ViewOnGithub => ResourceManager.GetString("ViewOnGithub") ?? "View on GitHub";
        public static string ReadDocs => ResourceManager.GetString("ReadDocs") ?? "Read the Docs";
        public static string SidebarTitle => ResourceManager.GetString("SidebarTitle") ?? "SDK Guide";
        public static string SidebarTooltip => ResourceManager.GetString("SidebarTooltip") ?? "Developer documentation";
        public static string PluginName => ResourceManager.GetString("PluginName") ?? "Developer Guide";
        public static string PluginDescription => ResourceManager.GetString("PluginDescription") ?? "Discover how to create your own plugins for SipLine.";
        public static string SnackbarWorking => ResourceManager.GetString("SnackbarWorking") ?? "The HelloWorld plugin is still working!";
        public static string SdkCardDesc => ResourceManager.GetString("SdkCardDesc") ?? "The official NuGet package to develop your extensions.";
        public static string DocsCardDesc => ResourceManager.GetString("DocsCardDesc") ?? "Discover the APIs, events and best practices.";
        public static string ErrorOpeningLink => ResourceManager.GetString("ErrorOpeningLink") ?? "Error opening link.";
    }
}
