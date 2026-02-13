using System.Windows.Input;
using Microsoft.Extensions.Logging;
using SipLine.Plugin.Sdk;

namespace SipLine.Plugin.HelloWorld
{
    public class HelloWorldPlugin : ISipLinePlugin
    {
        public string Id => "com.sipline.helloworld";
        public string Name => "Hello World";
        public string Description => "Un plugin de démonstration qui affiche un message de bienvenue.";
        public Version Version => new Version(1, 0, 0);
        public string Author => "SipLine Team";
        public string IconPathData => "M12 2L2 7l10 5 10-5-10-5zM2 17l10 5 10-5M2 12l10 5 10-5";
        public string WebsiteUrl => "https://sipline.feelautom.fr";
        public bool HasSettingsUI => false;
        public object? GetSettingsUI() => null;

        private IPluginContext? _context;

        public Task InitializeAsync(IPluginContext context)
        {
            _context = context;
            _context.Logger.LogInformation("Plugin HelloWorld initialisé !");

            _context.RegisterToolbarButton(new PluginToolbarButton
            {
                Id = "hello-btn",
                IconPathData = IconPathData,
                Tooltip = "Dire Bonjour",
                Command = new SimpleCommand(() => 
                {
                    _context.ShowSnackbar("Bonjour de la part du plugin HelloWorld !", SnackbarSeverity.Success);
                })
            });

            return Task.CompletedTask;
        }

        public Task ShutdownAsync() => Task.CompletedTask;
        public void Dispose() { }
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