using System;
using System.Windows;
using System.Windows.Controls;
using Second.Models;

namespace Second.Views.SeasonDialog;

public partial class SeasonDialog : Window
{
    public Season Season { get; set; }
    public SeasonDialog(Season season )
    {
        InitializeComponent();
        season.EndDate = DateTime.Now;
        season.StartDate = DateTime.Now;
        DataContext = season;
        Season = season;
    }

    private void OK_Click(object sender, RoutedEventArgs e)
    {
        DateTime date = EndDatePicker.SelectedDate.Value;
        DateTime dateUtc = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, DateTimeKind.Utc);
        Season.EndDate = dateUtc;
        date = StartDatePicker.SelectedDate.Value;
        dateUtc = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, DateTimeKind.Utc);
        Season.StartDate = dateUtc;
        DialogResult = true;
        Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        // Закрываем окно с результатом DialogResult = false
        DialogResult = false;
        Close();
    }
    
}