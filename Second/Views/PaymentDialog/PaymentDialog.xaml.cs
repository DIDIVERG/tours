using System;
using System.Windows;
using AutoMapper;
using Second.DTOs;
using Second.Models;

namespace Second.Views.PaymentDialog;

public partial class PaymentDialog : Window
{
    public Payment Payment { get; set; }
    public PaymentDialog(Payment payment )
    {
        InitializeComponent();
        payment.PaymentDate = DateTime.Now;
        DataContext = payment;
        Payment = payment;
    }

    private void OK_Click(object sender, RoutedEventArgs e)
    {
        DateTime date = DatePicker.SelectedDate.Value;
        DateTime dateUtc = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, DateTimeKind.Utc);
        Payment.PaymentDate = dateUtc;
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