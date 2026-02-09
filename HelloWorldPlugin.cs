using System.Windows.Input;
using Microsoft.Extensions.Logging;
using SipLine.Plugin.Sdk;

namespace SipLine.Plugin.HelloWorld;

/// <summary>
/// Plugin de démonstration "Hello World".
/// Montre comment utiliser le SDK SipLine pour créer un plugin.
/// </summary>
public class HelloWorldPlugin : ISipLinePlugin
{
    public string Id => "hello-world";
    public string Name => "Hello World";
    public string Description => "Plugin de démonstration SipLine";
    public Version Version => new(1, 0, 0);
    public string Author => "SipLine Team";
    public string? WebsiteUrl => null;
    public string? IconPathData => "M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm-2 15l-5-5 1.41-1.41L10 14.17l7.59-7.59L19 8l-9 9z";
    public bool HasSettingsUI => false;

    private IPluginContext? _context;
    private int _callCount = 0;

    public Task InitializeAsync(IPluginContext context)
    {
        _context = context;

        // Log au démarrage
        _context.Logger.LogInformation("Hello World plugin initialisé!");
        _context.AddLog("Hello World plugin chargé avec succès", "Info");

        // Ajouter un bouton dans la toolbar
        _context.RegisterToolbarButton(new PluginToolbarButton
        {
            Id = "hello-btn",
            Tooltip = "Afficher un message Hello World",
            IconPathData = IconPathData ?? "",
            Order = 100,
            Command = new SimpleCommand(OnHelloButtonClick)
        });

        // S'abonner aux événements d'appel
        _context.SipService.OnCallIncoming += OnCallIncoming;
        _context.SipService.OnCallEnded += OnCallEnded;

        return Task.CompletedTask;
    }

    private void OnHelloButtonClick()
    {
        _context?.ShowSnackbar($"Hello World! Appels reçus: {_callCount}", SnackbarSeverity.Success);
        _context?.AddLog($"Bouton Hello cliqué - Total appels: {_callCount}", "Info");
    }

    private void OnCallIncoming(CallInfo call)
    {
        _callCount++;
        _context?.Logger.LogInformation("Appel entrant de {Caller} (total: {Count})",
            call.CallerNumber, _callCount);
    }

    private void OnCallEnded(CallEndedInfo info)
    {
        _context?.Logger.LogInformation("Appel terminé - Durée: {Duration}s",
            info.Duration.TotalSeconds);
    }

    public object? GetSettingsUI() => null;

    public Task ShutdownAsync()
    {
        _context?.Logger.LogInformation("Hello World plugin arrêté");

        if (_context != null)
        {
            // Supprimer le bouton de toolbar
            _context.UnregisterToolbarButton("hello-btn");

            // Se désabonner des événements
            _context.SipService.OnCallIncoming -= OnCallIncoming;
            _context.SipService.OnCallEnded -= OnCallEnded;
        }

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        // Rien à libérer pour ce plugin simple
    }
}

/// <summary>
/// Implémentation simple de ICommand pour les boutons.
/// </summary>
internal class SimpleCommand : ICommand
{
    private readonly Action _execute;

    public SimpleCommand(Action execute)
    {
        _execute = execute;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter) => _execute();
}
