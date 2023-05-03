using System.Windows;
using Second.Models;

namespace Second.Views.PaymentDialog;

public partial class PaymentDialog : Window
{
    public PaymentDialog(Payment payment )
    {
        InitializeComponent();
        DataContext = payment;
    }
    private void OK_Click(object sender, RoutedEventArgs e)
    {
        // Закрываем окно с результатом DialogResult = true
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