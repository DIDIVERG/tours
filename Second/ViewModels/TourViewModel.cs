using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Second.DTOs;
using Second.Models;
using Second.Views.TourDialog;

namespace Second.ViewModels;

public class TourViewModel : Base, INotifyPropertyChanged
{
    
    private ObservableCollection<Tour> _tours = new ObservableCollection<Tour>();
     private Tour? _selectedTour = null;
     public AsyncRelayCommand AddTourCommand { get; private set; }
     public AsyncRelayCommand ChangeTourCommand { get; private set; }
    public AsyncRelayCommand DeleteTourCommand { get; private set; }
    public Tour? SelectedTour
        {
            get => _selectedTour;
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
                OnPropertyChanged(nameof(CanEditOrDelete));
            }
        }
    public bool CanEditOrDelete => SelectedTour != null;

        public TourViewModel()
        {
            AddTourCommand = new AsyncRelayCommand(AddTourAsync);
            ChangeTourCommand = new AsyncRelayCommand(ChangeTourAsync, () => CanEditOrDelete);
            DeleteTourCommand = new AsyncRelayCommand(DeleteTourAsync, () => CanEditOrDelete);
        }

        private async Task AddTourAsync()
        {
            var tourist = new Tour() { TourId = Tours.Max(item => item.TourId) + 1 };
            var dialog = new TourDialog(tourist);

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
                    {
                        await db.Tours.AddAsync(tourist);
                        await db.SaveChangesAsync();
                        Tours.Add(tourist);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        private async Task ChangeTourAsync()
        {
            var dialog = new TourDialog(SelectedTour);
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    await using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
                    {
                        var dtoFilled = Mapper.Map<TourDto>(SelectedTour);
                        var entityToUpdate = await db.Tours.FirstOrDefaultAsync(item
                            => item.TourId == SelectedTour.TourId);

                        if (entityToUpdate != null)
                        {
                            Mapper.Map(dtoFilled, entityToUpdate);
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            MessageBox.Show("Tour is null");
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
               
            }
        }

        private async Task DeleteTourAsync()
        {
            if (MessageBox.Show("Are you sure you want to delete this tourist?", "Delete Tour"
                    , MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
                    {
                        var dtoFilled = Mapper.Map<TourDto>(SelectedTour);
                        var entityToUpdate = await db.Tours.FirstOrDefaultAsync(item 
                            => item.TourId == SelectedTour.TourId);
                        if (entityToUpdate != null)
                        {
                            Mapper.Map(dtoFilled, entityToUpdate);
                            db.Tours.Remove(entityToUpdate);
                            await db.SaveChangesAsync();
                            Tours.Remove(SelectedTour);
                            SelectedTour = null;
                        }
                        else
                        {
                            MessageBox.Show("Tour is null");
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                
            }
        }
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