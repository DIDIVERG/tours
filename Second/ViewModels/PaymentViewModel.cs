using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Second.Models;

namespace Second.ViewModels;

public class PaymentViewModel : Base,INotifyPropertyChanged
{

    private ObservableCollection<Payment> _payments = new ObservableCollection<Payment>();
    
    public ObservableCollection<Payment> Payments
    {
        get => _payments;
        set
        {
            _payments = value;
            OnPropertyChanged(nameof(Payments));
        }
    }

    public async Task LoadPaymentAsync()
    {
        using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
        {
            Payments = new ObservableCollection<Payment>(await db.Payments.ToListAsync());
        }
    }
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}