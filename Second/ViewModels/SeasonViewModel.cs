using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Second.DTOs;
using Second.Models;
using Second.Views.SeasonDialog;

namespace Second.ViewModels;

public class SeasonViewModel : Base, INotifyPropertyChanged
{
    
    private ObservableCollection<Season> _seasons = new ObservableCollection<Season>();
    private Season? _selectedSeason = null;

        public AsyncRelayCommand AddSeasonCommand { get; private set; }
        public AsyncRelayCommand ChangeSeasonCommand { get; private set; }
        public AsyncRelayCommand DeleteSeasonCommand { get; private set; }

        public Season? SelectedSeason
        {
            get => _selectedSeason;
            set
            {
                _selectedSeason = value;
                OnPropertyChanged(nameof(SelectedSeason));
                OnPropertyChanged(nameof(CanEditOrDelete));
            }
        }

        public bool CanEditOrDelete => SelectedSeason != null;

        public SeasonViewModel()
        {
            AddSeasonCommand = new AsyncRelayCommand(AddSeasonAsync);
            ChangeSeasonCommand = new AsyncRelayCommand(ChangeSeasonAsync, () => CanEditOrDelete);
            DeleteSeasonCommand = new AsyncRelayCommand(DeleteSeasonAsync, () => CanEditOrDelete);
        }

        private async Task AddSeasonAsync()
        {
            var season = new Season() { SeasonId = Seasons.Max(item => item.SeasonId) + 1 };
            var dialog = new SeasonDialog(season);

            if (dialog.ShowDialog() == true)
            {
                using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
                {
                    var tour = await db.Tours.FindAsync(season.TourId);
                    if (tour != null)
                    {
                        season.Tour = tour;
                        await db.Seasons.AddAsync(season);
                        await db.SaveChangesAsync();
                    }
                }
                Seasons.Add(season);
            }
        }


        private async Task ChangeSeasonAsync()
        {
            var dialog = new SeasonDialog(SelectedSeason);

            if (dialog.ShowDialog() == true)
            {
                await using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
                {
                    var dtoFilled = Mapper.Map<SeasonDto>(SelectedSeason);
                    var entityToUpdate = await db.Seasons.FirstOrDefaultAsync(item => item.SeasonId == SelectedSeason.SeasonId);

                    if (entityToUpdate != null)
                    {
                        Mapper.Map(dtoFilled, entityToUpdate);

                        await db.SaveChangesAsync();
                    }
                }
            }
        }

        private async Task DeleteSeasonAsync()
        {
            if (MessageBox.Show("Are you sure you want to delete this season?", "Delete Season", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
                {
                    var dtoFilled = Mapper.Map<SeasonDto>(SelectedSeason);
                    var entityToUpdate = await db.Seasons.FirstOrDefaultAsync(item => item.SeasonId == SelectedSeason.SeasonId);

                    if (entityToUpdate != null)
                    {
                        Mapper.Map(dtoFilled, entityToUpdate);
                        db.Seasons.Remove(entityToUpdate);
                    }
                    await db.SaveChangesAsync();
                }
                Seasons.Remove(SelectedSeason);
                SelectedSeason = null;
            }
        }
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