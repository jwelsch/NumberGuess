using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace NumberGuess;

public class AttemptControl : TemplatedControl
{
    private ItemsControl? _itemsControl;

    /// <summary>
    /// Defines the <see cref="Characters"/> property.
    /// </summary>
    public static readonly StyledProperty<ObservableCollection<CharacterViewModel>> CharactersProperty =
        AvaloniaProperty.Register<AttemptControl, ObservableCollection<CharacterViewModel>>(
            nameof(Characters));

    /// <summary>
    /// Gets or sets the Characters.
    /// </summary>
    public ObservableCollection<CharacterViewModel> Characters
    {
        get => GetValue(CharactersProperty);
        set => SetValue(CharactersProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        _itemsControl = e.NameScope.Find<ItemsControl>(nameof(_itemsControl));
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);

        if (DataContext != null
            || DataContext is not AttemptControlViewModel viewModel
            || viewModel.Characters == null)
        {
            return;
        }

        foreach (var item in viewModel.Characters)
        {
            if (item is ObservableObject observable)
            {
                observable.PropertyChanged += Observable_PropertyChanged;
            }
        }

        viewModel.Characters.CollectionChanged += Characters_CollectionChanged;
    }

    private void Characters_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null)
        {
            foreach (var item in e.NewItems)
            {
                if (item is ObservableObject observable)
                {
                    observable.PropertyChanged += Observable_PropertyChanged;
                }
            }
        }
    }

    private void Observable_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        System.Diagnostics.Trace.WriteLine($"{nameof(AttemptControl)}.{nameof(Observable_PropertyChanged)}: {e.PropertyName}");
    }
}