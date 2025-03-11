using System;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;

namespace planer_dnia;

public partial class MainWindow : Window
{
    private TextBox _zadanie;
    private ComboBox _kategoria;
    private int _row = 4;
    private Grid _grid;
    private int _kategoriaIndex;
    private int _tak = 0;
    private CheckBox _checkBox;
    private string _className;
    private TextBlock _podsumowanie;
    private TextBlock _podsumowanieText;
    private int _zadania = 0;
    public MainWindow()
    {
        InitializeComponent();
        _zadanie = this.FindControl<TextBox>("Zadanie");
        _kategoria = this.FindControl<ComboBox>("Kategoria");
        _grid = this.FindControl<Grid>("Grid");
        _podsumowanie = this.FindControl<TextBlock>("Podsumowanie");
        _podsumowanieText = this.FindControl<TextBlock>("PodsumowanieText");
    }

    private void DodajZadanie(object? sender, RoutedEventArgs e)
    {
        _zadania++;
        _className = _zadanie.Text;
        var rowDefinition = new RowDefinition
        {
            Height = GridLength.Auto
        };
        
        _grid.RowDefinitions.Add(rowDefinition);
        
        var zadanieNazwa = new TextBlock
        {
            Classes = { _className },
            Text = _zadanie.Text,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            FontWeight = FontWeight.Bold,
            [Grid.RowProperty] = _row-1,
            [Grid.ColumnProperty] = 0,
            Margin = new Thickness(5)
        };

        var kategoriaString = (_kategoria.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Błąd";
        if (kategoriaString == "Praca")
        {
            _kategoriaIndex = 0;
        } else if (kategoriaString == "Rozrywka")
        {
            _kategoriaIndex = 1;
        } else if (kategoriaString == "Relaks")
        {
            _kategoriaIndex = 2;
        } else if (kategoriaString == "Edukacja")
        {
            _kategoriaIndex = 3;
        } else if (kategoriaString == "Zakupy")
        {
            _kategoriaIndex = 4;
        } else
        {
            _kategoriaIndex = 0;
        }
        
        var comboBox = new ComboBox
        {
            Classes = { _className },
            Width = 150,
            Items = { "Praca", "Rozrywka", "Relaks", "Edukacja", "Zakupy" },
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            [Grid.RowProperty] = _row-1,
            [Grid.ColumnProperty] = 1,
            Margin = new Thickness(5)
        };
        comboBox.SelectedIndex = _kategoriaIndex;

        _checkBox = new CheckBox
        {
            Classes = { _className },
            Content = "Ukończone",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            FontWeight = FontWeight.Bold,
            [Grid.RowProperty] = _row-1,
            [Grid.ColumnProperty] = 2,
            Margin = new Thickness(5)
        };
        

        var usun = new Button
        {
            Name = _className,
            Classes = { _className },
            [Grid.RowProperty] = _row-1,
            [Grid.ColumnProperty] = 3,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Content = "Usuń",
            Margin = new Thickness(5)
        };
        usun.Click += UsunZadanie;

        _grid.Children.Add(zadanieNazwa);
        _grid.Children.Add(comboBox);
        _grid.Children.Add(_checkBox);
        _grid.Children.Add(usun);

        Wyswietl();
        
        _row++;
    }

    private void Wyswietl()
    {
        var podsumowanieText = "Ilość zadań: \n" + "Ukończone: ";
        _podsumowanieText.Text = podsumowanieText;
        Grid.SetRow(_podsumowanieText, _row);
        Grid.SetColumn(_podsumowanieText, 1);
        _podsumowanieText.IsVisible = true;
        
        Grid.SetRow(_podsumowanie, _row);
        Grid.SetColumn(_podsumowanie, 2);
        var text = $"{_zadania} \n {_tak}";
        _podsumowanie.Text = text;
        
    }
    
    private void UsunZadanie(object? sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        string className = button.Name;
        var elementyDoUsuniecia = _grid.Children
            .OfType<Control>()
            .Where(c => c.Classes.Contains(className))
            .ToList();
        
        foreach (var element in elementyDoUsuniecia)
        {
            _grid.Children.Remove(element);
        }

        _zadania--;
        Wyswietl();
    }
    
}