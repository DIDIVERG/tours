using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Second.Models;

namespace Second.ViewModels;

public class TourViewModel : Base, INotifyPropertyChanged
{
    
    private ObservableCollection<Tour> _tours = new ObservableCollection<Tour>();
    
    public ObservableCollection<Tour> Tours
    {
        get => _tours;
        set
        {
            _tours = value;
            OnPropertyChanged(nameof(Tours));
        }
    }

    public async Task LoadTourAsync()
    {
        using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
        {
            Tours = new ObservableCollection<Tour>(await db.Tours.ToListAsync());
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