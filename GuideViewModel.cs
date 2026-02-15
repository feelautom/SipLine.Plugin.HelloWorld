using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using SipLine.Plugin.Sdk;
using SipLine.Plugin.HelloWorld.Resources;

namespace SipLine.Plugin.HelloWorld
{
    public class GuideViewModel : INotifyPropertyChanged
    {
        private readonly IPluginContext _context;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Title => Strings.GuideTitle;
        public string WelcomeMessage => Strings.GuideSubtitle;
        
        public string WhatIsPlugin => Strings.WhatIsPlugin;
        public string WhatIsPluginDesc => Strings.WhatIsPluginDesc;
        public string Capabilities => Strings.Capabilities;
        public string CapCallControl => Strings.CapCallControl;
        public string CapCallControlDesc => Strings.CapCallControlDesc;
        public string CapUiExtensions => Strings.CapUiExtensions;
        public string CapUiExtensionsDesc => Strings.CapUiExtensionsDesc;
        public string CapEvents => Strings.CapEvents;
        public string CapEventsDesc => Strings.CapEventsDesc;
        public string CapStorage => Strings.CapStorage;
        public string CapStorageDesc => Strings.CapStorageDesc;
        public string QuickStart => Strings.QuickStart;
        public string QuickStartStep1 => Strings.QuickStartStep1;
        public string QuickStartStep2 => Strings.QuickStartStep2;
        public string QuickStartStep3 => Strings.QuickStartStep3;
        public string ViewOnGithub => Strings.ViewOnGithub;
        public string ReadDocs => Strings.ReadDocs;
        public string SdkCardDesc => Strings.SdkCardDesc;
        public string DocsCardDesc => Strings.DocsCardDesc;

        public ICommand OpenSdkGithubCommand { get; }
        public ICommand OpenDocumentationCommand { get; }

        public GuideViewModel(IPluginContext context)
        {
            _context = context;

            OpenSdkGithubCommand = new SimpleCommand(() => OpenUrl("https://github.com/feelautom/SipLine.Plugin.Sdk"));
            OpenDocumentationCommand = new SimpleCommand(() => OpenUrl("https://sipline.feelautom.fr/docs/sdk"));
        }

        public void RefreshStrings()
        {
            OnPropertyChanged(string.Empty); // Notifie que TOUTES les propriétés ont changé
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OpenUrl(string url)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                _context.Logger.LogError(ex, "Impossible d'ouvrir l'URL : {Url}", url);
                _context.ShowSnackbar(Strings.ErrorOpeningLink, SnackbarSeverity.Error);
            }
        }
    }
}
