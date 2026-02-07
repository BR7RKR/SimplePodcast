using System;
using System.Reactive;
using System.Reactive.Disposables.Fluent;
using ReactiveUI;
using SimplePodcast.Views;
using Ursa.Controls;

namespace SimplePodcast.ViewModels;

public sealed class HeaderViewModel : ViewModelBase
{
    public HeaderViewModel() 
        : this(new NullReactiveSourcesService(), new NullNotificationManagerHost())
    {
        DesignTime.ThrowIfNotDesignTime();
    }
    
    public HeaderViewModel(IReactiveSourcesService sourcesService, INotificationManagerHost notificationManagerHost)
    {
        ChangeSettingsVisibilityCommand = ReactiveCommand.Create(() =>
        {
            IsSettingsVisible = !IsSettingsVisible;
        }, outputScheduler: RxApp.MainThreadScheduler).DisposeWith(Disposables);
        OpenAddSourceDialogCommand = ReactiveCommand.CreateFromTask(async (ct) =>
        {
            var notificationManager = notificationManagerHost.GetNotificationManager();
            using var vm = new AddSourceDialogViewModel();
            var result = await OverlayDialog.ShowModal<AddSourceDialogView, AddSourceDialogViewModel>(
                vm,
                options: new OverlayDialogOptions
                {
                    Title = "Add new source for podcasts",
                    Mode = DialogMode.None,
                    Buttons = DialogButton.OKCancel,
                    CanResize = false,
                }, 
                token: ct
            );

            if (result != DialogResult.OK)
            {
                return;
            }
            
            var data = vm.SelectedTab?.GetSourceData();
            
            if (data is null)
            {
                notificationManager.ShowError(
                    "Filed to read url:", 
                    "Provided string is not a valid url"
                );
                return;
            }

            try
            {
                await sourcesService.AddSourceAsync(data, ct);
                notificationManager.ShowSuccess("Source added successfully!");
            }
            catch (Exception e)
            {
                notificationManager.ShowError("Failed to add source:", e);
            }
        }, outputScheduler: RxApp.MainThreadScheduler).DisposeWith(Disposables);
    }
    
    public bool IsSettingsVisible
    {
        get;
        private set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public ReactiveCommand<Unit, Unit> ChangeSettingsVisibilityCommand { get; }
    public ReactiveCommand<Unit, Unit> OpenAddSourceDialogCommand { get; }
}