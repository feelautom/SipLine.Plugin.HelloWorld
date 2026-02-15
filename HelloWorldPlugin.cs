using System.Windows.Input;
using Microsoft.Extensions.Logging;
using SipLine.Plugin.Sdk;
using SipLine.Plugin.Sdk.Enums;
using SipLine.Plugin.Sdk.Licensing;
using SipLine.Plugin.HelloWorld.Resources;

namespace SipLine.Plugin.HelloWorld
{
    public class HelloWorldPlugin : ISipLinePlugin
    {
        public string Id => "com.sipline.helloworld";
        public string Name => Strings.PluginName;
        public string Description => Strings.PluginDescription;
        public Version Version => new Version(1, 2, 0);
        public string Author => "FeelAutom";
        
        // Nouvelle méthode : Icône standard (enum)
        public PluginIcon? Icon => PluginIcon.Message;
        public string? IconPathData => null; // Plus besoin du SVG brut ici
        
        public string WebsiteUrl => "https://github.com/feelautom/SipLine.Plugin.Sdk";
        
        public PluginLicenseType LicenseType => PluginLicenseType.Integrated;
        
        public bool HasSettingsUI => false;
        public object? GetSettingsUI() => null;

        private IPluginContext? _context;
        private GuideViewModel? _viewModel;
        private PluginSidebarTab? _tab;

        public Task InitializeAsync(IPluginContext context)
        {
            _context = context;
            _context.Logger.LogInformation("Plugin {Name} initialized.", Name);

            // Note: context.RegisterResource(Strings.ResourceManager) n'est plus nécessaire !
            // La détection est automatique.

            // Créer le ViewModel
            _viewModel = new GuideViewModel(context);

            // Enregistrer l'élément dans la sidebar
            _tab = new PluginSidebarTab
            {
                Id = "guide-tab",
                Title = Strings.SidebarTitle,
                Tooltip = Strings.SidebarTooltip,
                Icon = PluginIcon.Message, // Utilisation de l'enum
                Order = 500, 
                ContentFactory = () => new GuideView { DataContext = _viewModel }
            };

            _context.RegisterSidebarTab(_tab);

            // S'abonner au changement de langue
            _context.OnLanguageChanged += (lang) =>
            {
                if (_tab != null)
                {
                    // Récupération via le nouveau service de localisation
                    _tab.Title = context.Localization.GetString("SidebarTitle");
                    _tab.Tooltip = context.Localization.GetString("SidebarTooltip");
                }
                _viewModel?.RefreshStrings();
            };

            // Optionnel : Garder le bouton toolbar pour la démo
            _context.RegisterToolbarButton(new PluginToolbarButton
            {
                Id = "hello-btn",
                Icon = PluginIcon.Star, // Icône différente pour le bouton
                Tooltip = "HelloWorld Test",
                Command = new SimpleCommand(() => 
                {
                    _context.ShowSnackbar(context.Localization.GetString("SnackbarWorking"), SnackbarSeverity.Success);
                })
            });

            return Task.CompletedTask;
        }

        public Task ShutdownAsync()
        {
            _context?.UnregisterSidebarTab("guide-tab");
            _context?.UnregisterToolbarButton("hello-btn");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }

    internal class SimpleCommand : ICommand
    {
        private readonly Action _action;
        public SimpleCommand(Action action) => _action = action;
        public bool CanExecute(object? parameter) => true;
        public void Execute(object? parameter) => _action();
        public event EventHandler? CanExecuteChanged { add { } remove { } }
    }
}
