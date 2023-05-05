using System.Windows;
using Second.Models;

namespace Second.Views.VoucherDialog;

public partial class VoucherDialog : Window
{
    public VoucherDialog(Voucher voucher)
    {
        InitializeComponent();
        DataContext = voucher;
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