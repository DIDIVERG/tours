using System.Windows;
using Second.Models;

namespace Second.Views.TourDialog;

public partial class TourDialog : Window
{
    public TourDialog(Tour tour)
    {
        InitializeComponent();
        DataContext = tour;
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