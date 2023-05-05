using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Second.Models;

namespace Second.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    public PaymentViewModel PaymentViewModel { get; }
    public SeasonViewModel SeasonViewModel { get; }
    public TouristInfoViewModel TouristInfoViewModel { get; }
    public TouristViewModel TouristViewModel { get; }
    public TourViewModel TourViewModel { get; }
    public VoucherViewModel VoucherViewModel { get; }
    public QueryViewModel QueryViewModel { get; }
    public AsyncRelayCommand ActivateTriggerCommand { get; private set; }
    private int _selectedTabIndex;
    public int SelectedTabIndex
    {
        get { return _selectedTabIndex; }
        set
        {
            if (_selectedTabIndex != value)
            {
                _selectedTabIndex = value;
                OnPropertyChanged(nameof(SelectedTabIndex));
            }
        }
    }
    
    public MainViewModel()
    {
        PaymentViewModel = new PaymentViewModel();
        SeasonViewModel = new SeasonViewModel();
        TouristInfoViewModel = new TouristInfoViewModel();
        TouristViewModel = new TouristViewModel();
        TourViewModel = new TourViewModel();
        VoucherViewModel = new VoucherViewModel();
        ToursContextFactory factory = new ToursContextFactory();
        QueryViewModel = new QueryViewModel(factory.connectionString.ConnectionString);
        LoadAsync();
    }

    private async Task LoadAsync()
    {
        ActivateTriggerCommand = new AsyncRelayCommand(ActivateTriggerAsync);
        await PaymentViewModel.LoadPaymentAsync();
        await SeasonViewModel.LoadSeasonAsync();
        await TouristViewModel.LoadTouristAsync();
        await TouristInfoViewModel.LoadTouristInfoAsync();
        await TourViewModel.LoadTourAsync();
        await VoucherViewModel.LoadVoucherAsync();

    }
    private async Task ActivateTriggerAsync()
    {

        using (var db = new ToursContextFactory().CreateDbContext(Array.Empty<string>()))
        {
            VoucherViewModel.Vouchers = new ObservableCollection<Voucher>(await db.Vouchers.ToListAsync());
            TouristInfoViewModel.TouristInfos = new ObservableCollection<TouristInfo>(await db.TouristInfos.ToListAsync());
        }
        SelectedTabIndex = 4;
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