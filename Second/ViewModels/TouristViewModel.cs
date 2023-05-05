using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using Microsoft.EntityFrameworkCore;
using Second.DTOs;
using Second.Models;
using Second.Views.TouristDialog;

namespace Second.ViewModels;

public class TouristViewModel : Base, INotifyPropertyChanged
{
    
    
    private ObservableCollection<Tourist> _tourists = new ObservableCollection<Tourist>();
    private Tourist? _selectedTourist = null;

        public AsyncRelayCommand AddTouristCommand { get; private set; }
        public AsyncRelayCommand ChangeTouristCommand { get; private set; }
        public AsyncRelayCommand DeleteTouristCommand { get; private set; }
        
        public Tourist? SelectedTourist
        {
            get => _selectedTourist;
            set
            {
                _selectedTourist = value;
                OnPropertyChanged(nameof(SelectedTourist));
                OnPropertyChanged(nameof(CanEditOrDelete));
            }
        }

        public bool CanEditOrDelete => SelectedTourist != null;
        
        public TouristViewModel()
        {
            AddTouristCommand = new AsyncRelayCommand(AddTouristAsync);
            ChangeTouristCommand = new AsyncRelayCommand(ChangeTouristAsync, () => CanEditOrDelete);
            DeleteTouristCommand = new AsyncRelayCommand(DeleteTouristAsync, () => CanEditOrDelete);
        }

        private async Task AddTouristAsync()
        {
            var tourist = new Tourist() { TouristId = Tourists.Max(item => item.TouristId) + 1 };
            var dialog = new TouristDialog(tourist);

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
                    {
                        await db.Tourists.AddAsync(tourist);
                        await db.SaveChangesAsync();
                        Tourists.Add(tourist);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
               
            }
        }

       

        private async Task ChangeTouristAsync()
        {
            var dialog = new TouristDialog(SelectedTourist);
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    await using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
                    {
                        var dtoFilled = Mapper.Map<TouristDto>(SelectedTourist);
                        var entityToUpdate = await db.Tourists.FirstOrDefaultAsync(item 
                            => item.TouristId == SelectedTourist.TouristId);

                        if (entityToUpdate != null)
                        {
                            Mapper.Map(dtoFilled, entityToUpdate);
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            MessageBox.Show("Tourist is null");
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
              
            }
        }

        private async Task DeleteTouristAsync()
        {
            if (MessageBox.Show("Are you sure you want to delete this tourist?", "Delete Tourist", 
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
                    {
                        var dtoFilled = Mapper.Map<TouristDto>(SelectedTourist);
                        var entityToUpdate = await db.Tourists.FirstOrDefaultAsync(item 
                            => item.TouristId == SelectedTourist.TouristId);
                        if (entityToUpdate != null)
                        {
                            Mapper.Map(dtoFilled, entityToUpdate);
                            db.Tourists.Remove(entityToUpdate);
                            await db.SaveChangesAsync();
                            Tourists.Remove(SelectedTourist);
                            SelectedTourist = null;
                        }
                        else
                        {
                            MessageBox.Show("Tourist is null");
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
               
            }
        }
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