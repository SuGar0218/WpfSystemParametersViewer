using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Data;

namespace SystemParametersViewer;

public class MainViewModel
{
    public MainViewModel()
    {
        _properties = [];
        _parametersViewSource = new CollectionViewSource
        {
            Source = _properties
        };
        _parametersViewSource.Filter += OnParametersViewSourceFilter;
        AvailableTypes =
        [
            typeof(SystemParameters),
            typeof(SystemColors),
            typeof(SystemFonts),
            typeof(Environment)
        ];
        TargetType = AvailableTypes[0];
    }

    private readonly ObservableCollection<SystemParameterViewModel> _properties;
    private readonly CollectionViewSource _parametersViewSource;

    private Type? _TargetType;
    public Type? TargetType
    {
        get => _TargetType;
        set
        {
            if (_TargetType == value)
                return;

            _TargetType = value;
            _ = InitializeAsync();
        }
    }

    public IReadOnlyList<Type> AvailableTypes { get; }

    public ICollectionView Properties => _parametersViewSource.View;

    public string SearchingKeyword { get; set; } = string.Empty;

    public async Task InitializeAsync()
    {
        _properties.Clear();
        if (TargetType is null)
            return;

        foreach (SystemParameterViewModel parameter in await Task.Run(() => TargetType
            .GetProperties(BindingFlags.Public | BindingFlags.Static)
            .OrderBy(p => p.Name)
            .Select(p => new SystemParameterViewModel
            {
                Name = p.Name,
                Type = p.PropertyType,
                Value = p.GetValue(null),
                HelpLink =
                    "https://msdn.microsoft.com/query/dev17.query?" +
                    "appId=Dev17IDEF1&l=ZH-CN&k=" +
                    $"k({TargetType.FullName}.{p.Name});" +
                    $"k(DevLang-csharp)&rd=true"
            })))
        {
            _properties.Add(parameter);
        }
    }

    public void Search()
    {
        Properties.Refresh();
    }

    private void OnParametersViewSourceFilter(object sender, FilterEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(SearchingKeyword))
        {
            e.Accepted = true;
        }
        else
        {
            e.Accepted =
                e.Item is SystemParameterViewModel parameter &&
                parameter.Name.Contains(SearchingKeyword);
        }
    }
}
