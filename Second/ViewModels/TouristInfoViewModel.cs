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
using Second.Views.TouristInfoDialog;

namespace Second.ViewModels;

public class TouristInfoViewModel: Base, INotifyPropertyChanged
{
    private ObservableCollection<TouristInfo> _touristInfos = new ObservableCollection<TouristInfo>();
    private TouristInfo? _selectedTouristInfo = null;

        public AsyncRelayCommand AddTouristInfoCommand { get; private set; }
        public AsyncRelayCommand ChangeTouristInfoCommand { get; private set; }
        public AsyncRelayCommand DeleteTouristInfoCommand { get; private set; }

        public TouristInfo? SelectedTouristInfo
        {
            get => _selectedTouristInfo;
            set
            {
                _selectedTouristInfo = value;
                OnPropertyChanged(nameof(SelectedTouristInfo));
                OnPropertyChanged(nameof(CanEditOrDelete));
            }
        }

        public bool CanEditOrDelete => SelectedTouristInfo != null;

        public TouristInfoViewModel()
        {
            AddTouristInfoCommand = new AsyncRelayCommand(AddTouristInfoAsync);
            ChangeTouristInfoCommand = new AsyncRelayCommand(ChangeTouristInfoAsync, () => CanEditOrDelete);
            DeleteTouristInfoCommand = new AsyncRelayCommand(DeleteTouristInfoAsync, () => CanEditOrDelete);
        }

        private async Task AddTouristInfoAsync()
        {
            var touristInfo = new TouristInfo() {  };
            var dialog = new TouristInfoDialog(touristInfo);

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
                    {
                        var tourist = await db.Tourists.FirstAsync(item => item.TouristId == touristInfo.TouristId);
                        if (tourist != null)
                        {
                            touristInfo.Tourist = tourist;
                            await db.TouristInfos.AddAsync(touristInfo);
                            await db.SaveChangesAsync();
                            TouristInfos.Add(touristInfo);
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


        private async Task ChangeTouristInfoAsync()
        {
            var dialog = new TouristInfoDialog(SelectedTouristInfo);
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    await using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
                    {
                        var dtoFilled = Mapper.Map<TouristInfoDto>(SelectedTouristInfo);
                        var entityToUpdate = await db.TouristInfos.FirstOrDefaultAsync(item 
                            => item.TouristId == SelectedTouristInfo.TouristId);

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

        private async Task DeleteTouristInfoAsync()
        {
            if (MessageBox.Show("Are you sure you want to delete this tourist?", "Delete TouristInfo",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
                    {
                        var dtoFilled = Mapper.Map<TouristInfoDto>(SelectedTouristInfo);
                        var entityToUpdate = await db.TouristInfos.FirstOrDefaultAsync(item 
                            => item.TouristId == SelectedTouristInfo.TouristId);
                        if (entityToUpdate != null)
                        {
                            Mapper.Map(dtoFilled, entityToUpdate);
                            db.TouristInfos.Remove(entityToUpdate);
                            await db.SaveChangesAsync();
                            TouristInfos.Remove(SelectedTouristInfo);
                            SelectedTouristInfo = null;
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