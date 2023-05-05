using System;
using System.Windows;
using Second.Models;

namespace Second.Views.TouristInfoDialog;

public partial class TouristInfoDialog : Window
{
    public TouristInfo TouristInfo { get; set; }
    public TouristInfoDialog(TouristInfo touristInfo )
    {
        InitializeComponent();
        DataContext = touristInfo;
        TouristInfo = touristInfo;
    }

    private void OK_Click(object sender, RoutedEventArgs e)
    {
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