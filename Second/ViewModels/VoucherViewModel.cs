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
using Second.Views.VoucherDialog;

namespace Second.ViewModels;

public class VoucherViewModel : Base, INotifyPropertyChanged
{
    
    private ObservableCollection<Voucher> _vouchers = new ObservableCollection<Voucher>();
     private Voucher? _selectedVoucher = null;

        public AsyncRelayCommand AddVoucherCommand { get; private set; }
        public AsyncRelayCommand ChangeVoucherCommand { get; private set; }
        public AsyncRelayCommand DeleteVoucherCommand { get; private set; }

        public Voucher? SelectedVoucher
        {
            get => _selectedVoucher;
            set
            {
                _selectedVoucher = value;
                OnPropertyChanged(nameof(SelectedVoucher));
                OnPropertyChanged(nameof(CanEditOrDelete));
            }
        }

        public bool CanEditOrDelete => SelectedVoucher != null;

        public VoucherViewModel()
        {
            AddVoucherCommand = new AsyncRelayCommand(AddVoucherAsync);
            ChangeVoucherCommand = new AsyncRelayCommand(ChangeVoucherAsync, () => CanEditOrDelete);
            DeleteVoucherCommand = new AsyncRelayCommand(DeleteVoucherAsync, () => CanEditOrDelete);
        }

        private async Task AddVoucherAsync()
        {
            var voucher = new Voucher() { VoucherId = Vouchers.Max(item => item.VoucherId) + 1 };
            var dialog = new VoucherDialog(voucher);

            if (dialog.ShowDialog() == true)
            {
                using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
                {
                  var touristInfo = await db.TouristInfos.Include(t => t.Tourist)
                      .FirstOrDefaultAsync(item => item.TouristId == voucher.TouristId);
                  var season = await db.Seasons.Include(s => s.Tour)
                      .FirstOrDefaultAsync(item => item.SeasonId == voucher.SeasonId);
                  if (touristInfo == null || season == null)
                  {
                      
                      MessageBox.Show("Either season or tourist does not exist");
                  }
                  else
                  {
                      voucher.Season = season;
                      voucher.TouristInfo = touristInfo;
                      await db.Vouchers.AddAsync(voucher);
                      await db.SaveChangesAsync();
                      Vouchers.Add(voucher);
                  }
                   
                }
            }
        }


        private async Task ChangeVoucherAsync()
        {
            var dialog = new VoucherDialog(SelectedVoucher);
            if (dialog.ShowDialog() == true)
            {
                await using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
                {
                    var dtoFilled = Mapper.Map<VoucherDto>(SelectedVoucher);
                    var entityToUpdate = await db.Vouchers.FirstOrDefaultAsync(item => item.VoucherId == SelectedVoucher.VoucherId);

                    if (entityToUpdate != null)
                    {
                        Mapper.Map(dtoFilled, entityToUpdate);
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        MessageBox.Show("Voucher is null");
                    }
                }
            }
        }

        private async Task DeleteVoucherAsync()
        {
            if (MessageBox.Show("Are you sure you want to delete this voucher?", "Delete Voucher", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
                {
                    var dtoFilled = Mapper.Map<VoucherDto>(SelectedVoucher);
                    var entityToUpdate = await db.Vouchers.FirstOrDefaultAsync(item => item.VoucherId == SelectedVoucher.VoucherId);
                    if (entityToUpdate != null)
                    {
                        Mapper.Map(dtoFilled, entityToUpdate);
                        db.Vouchers.Remove(entityToUpdate);
                        await db.SaveChangesAsync();
                        Vouchers.Remove(SelectedVoucher);
                        SelectedVoucher = null;
                    }
                    else
                    {
                        MessageBox.Show("Voucher is null");
                    }
                }
            }
        }
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
            var VouchersSet = await db.Vouchers
                .Include(v => v.Season)
                .ThenInclude(s => s.Tour)
                .Include(v => v.TouristInfo)
                .ThenInclude(ti => ti.Tourist).ToListAsync();
            Vouchers = new ObservableCollection<Voucher>(VouchersSet);
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