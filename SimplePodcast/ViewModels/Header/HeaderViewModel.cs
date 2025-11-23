using System.Reactive;
using System.Reactive.Disposables.Fluent;
using ReactiveUI;
using SimplePodcast.Views;
using Ursa.Controls;

namespace SimplePodcast.ViewModels;

public sealed class HeaderViewModel : ViewModelBase
{
    public HeaderViewModel()
    {
        ChangeSettingsVisibilityCommand = ReactiveCommand.Create(() =>
        {
            IsSettingsVisible = !IsSettingsVisible;
        }, outputScheduler: RxApp.MainThreadScheduler).DisposeWith(Disposables);
        OpenAddSourceDialogCommand = ReactiveCommand.CreateFromTask(async (ct) =>
        {
            using var vm = new AddSourceDialogViewModel();
            var result = await OverlayDialog.ShowModal<AddSourceDialogView, AddSourceDialogViewModel>(
                vm,
                options: new OverlayDialogOptions
                {
                    Title = "Add new source for podcasts",
                    Mode = DialogMode.None,
                    Buttons = DialogButton.OKCancel,
                }, 
                token: ct
            );
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