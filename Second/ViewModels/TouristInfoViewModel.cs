using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Second.Models;

namespace Second.ViewModels;

public class TouristInfoViewModel: Base, INotifyPropertyChanged
{
    private ObservableCollection<TouristInfo> _touristInfos = new ObservableCollection<TouristInfo>();
    
    public ObservableCollection<TouristInfo> TouristInfos
    {
        get => _touristInfos;
        set
        {
            _touristInfos = value;
            OnPropertyChanged(nameof(TouristInfos));
        }
    }

    public async Task LoadTouristInfoAsync()
    {
        using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
        {
            TouristInfos = new ObservableCollection<TouristInfo>(await db.TouristInfos.ToListAsync());
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