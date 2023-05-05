using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Second.Commands;
using Second.DTOs;
using Second.Models;
using Second.Views.PaymentDialog;

namespace Second.ViewModels
{
    public class PaymentViewModel : Base, INotifyPropertyChanged
    {
        private ObservableCollection<Payment> _payments = new ObservableCollection<Payment>();

        private Payment? _selectedPayment = null;

        public AsyncRelayCommand AddPaymentCommand { get; private set; }
        public AsyncRelayCommand ChangePaymentCommand { get; private set; }
        public AsyncRelayCommand DeletePaymentCommand { get; private set; }

        public Payment? SelectedPayment
        {
            get => _selectedPayment;
            set
            {
                _selectedPayment = value;
                OnPropertyChanged(nameof(SelectedPayment));
                OnPropertyChanged(nameof(CanEditOrDelete));
            }
        }

        public bool CanEditOrDelete => SelectedPayment != null;

        public PaymentViewModel()
        {
            AddPaymentCommand = new AsyncRelayCommand(AddPaymentAsync);
            ChangePaymentCommand = new AsyncRelayCommand(ChangePaymentAsync, () => CanEditOrDelete);
            DeletePaymentCommand = new AsyncRelayCommand(DeletePaymentAsync, () => CanEditOrDelete);
        }

        private async Task AddPaymentAsync()
        {
            var payment = new Payment() { PaymentId = Payments.Max(item => item.PaymentId) + 1 };
            var dialog = new PaymentDialog(payment);

            if (dialog.ShowDialog() == true)
            {
                using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
                {
                    var voucher = await db.Vouchers.FindAsync(payment.VoucherId);
                    if (voucher != null)
                    {
                        payment.Voucher = voucher;
                        await db.Payments.AddAsync(payment);
                        await db.SaveChangesAsync();
                        Payments.Add(payment);
                    }
                }
            }
        }


        private async Task ChangePaymentAsync()
        {
            var dialog = new PaymentDialog(SelectedPayment);

            if (dialog.ShowDialog() == true)
            {
                await using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
                {
                    var dtoFilled = Mapper.Map<PaymentDto>(SelectedPayment);
                    var entityToUpdate = await db.Payments.FirstOrDefaultAsync(item => item.PaymentId == SelectedPayment.PaymentId);

                    if (entityToUpdate != null)
                    {
                        Mapper.Map(dtoFilled, entityToUpdate);

                        await db.SaveChangesAsync();
                    }
                }
            }
        }

        private async Task DeletePaymentAsync()
        {
            if (MessageBox.Show("Are you sure you want to delete this payment?", "Delete Payment", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
                {
                    var dtoFilled = Mapper.Map<PaymentDto>(SelectedPayment);
                    var entityToUpdate = await db.Payments.FirstOrDefaultAsync(item => item.PaymentId == SelectedPayment.PaymentId);

                    if (entityToUpdate != null)
                    {
                        Mapper.Map(dtoFilled, entityToUpdate);
                        db.Payments.Remove(entityToUpdate);
                        Payments.Remove(SelectedPayment);
                        SelectedPayment = null;
                    }
                    await db.SaveChangesAsync();
                }
            }
        }

        public ObservableCollection<Payment> Payments
        {
            get => _payments;
            set => SetField(ref _payments, value);
        }

        public async Task LoadPaymentAsync()
        {
            using (var db = ContextFactory.CreateDbContext(Array.Empty<string>()))
            {
                Payments = new ObservableCollection<Payment>(await db.Payments.ToListAsync());
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
}
