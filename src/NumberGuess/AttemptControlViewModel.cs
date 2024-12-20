using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace NumberGuess
{
    public partial class AttemptControlViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<CharacterViewModel>? _characters;
    }
}
