using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Second.Models;

namespace Second.ViewModels;

public class SeasonViewModel : Base, INotifyPropertyChanged
{
    
    private ObservableCollection<Season> _seasons = new ObservableCollection<Season>();
    
    public ObservableCollection<Season> Seasons
    {
        get => _seasons;
        set
        {
            _seasons = value;
            OnPropertyChanged(nameof(Seasons));
        }
    }

    public async Task LoadSeasonAsync()
    {
        using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
        {
            Seasons = new ObservableCollection<Season>(await db.Seasons.ToListAsync());
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