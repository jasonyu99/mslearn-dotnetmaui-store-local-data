using System.Collections.ObjectModel;
using System.Windows.Input;
using People.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace People;

public partial class MainPageViewModel : ObservableObject
{
    private readonly PersonRepository _personRepository;

    [ObservableProperty]
    private ObservableCollection<Person> people;

    [ObservableProperty]
    private string statusMessage;

    [ObservableProperty]
    private bool isRefreshing;

    [ObservableProperty]
    private string newPersonName;

    public ICommand AddPersonCommand { get; }
    public ICommand RefreshCommand { get; }

    public MainPageViewModel(PersonRepository personRepository)
    {
        _personRepository = personRepository;
        People = new ObservableCollection<Person>();

        AddPersonCommand = new AsyncRelayCommand(AddPersonAsync);
        RefreshCommand = new AsyncRelayCommand(RefreshPeopleAsync);
    }

    private async Task AddPersonAsync()
    {
        if (string.IsNullOrWhiteSpace(NewPersonName))
        {
            StatusMessage = "Please enter a valid name.";
            return;
        }

        await _personRepository.AddNewPersonAsync(NewPersonName);
        StatusMessage = _personRepository.StatusMessage;

        // Refresh the list after adding a new person
        await RefreshPeopleAsync();
        NewPersonName = string.Empty;
    }

    private async Task RefreshPeopleAsync()
    {
        IsRefreshing = true;

        await Task.Delay(2000);

        var peopleList = await _personRepository.GetAllPeopleAsync();
        People.Clear();

        foreach (var person in peopleList)
        {
            People.Add(person);
        }

        IsRefreshing = false;
    }
}
