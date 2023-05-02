using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Second.Models;

namespace Second.ViewModels;

public class VoucherViewModel : Base, INotifyPropertyChanged
{
    
    
    private ObservableCollection<Voucher> _vouchers = new ObservableCollection<Voucher>();
    
    public ObservableCollection<Voucher> Vouchers
    {
        get => _vouchers;
        set
        {
            _vouchers = value;
            OnPropertyChanged(nameof(Vouchers));
        }
    }

    public async Task LoadVoucherAsync()
    {
        using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
        {
            Vouchers = new ObservableCollection<Voucher>(await db.Vouchers.ToListAsync());
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