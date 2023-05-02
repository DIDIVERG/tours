using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Microsoft.EntityFrameworkCore;
using Second.Models;

namespace Second.ViewModels;

public class TouristViewModel : Base, INotifyPropertyChanged
{
    
    
    private ObservableCollection<Tourist> _tourists = new ObservableCollection<Tourist>();
    
    public ObservableCollection<Tourist> Tourists
    {
        get => _tourists;
        set
        {
            _tourists = value;
            OnPropertyChanged(nameof(Tourists));
        }
    }

    public async Task LoadTouristAsync()
    {
        using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
        {
            Tourists = new ObservableCollection<Tourist>(await db.Tourists.ToListAsync());
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