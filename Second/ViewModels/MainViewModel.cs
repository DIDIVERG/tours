using System.Threading.Tasks;

namespace Second.ViewModels;

public class MainViewModel
{
    public PaymentViewModel PaymentViewModel { get; }
    public SeasonViewModel SeasonViewModel { get; }
    public TouristInfoViewModel TouristInfoView { get; }
    public TouristViewModel TouristViewModel { get; }
    public TourViewModel TourViewModel { get; }
    public VoucherViewModel VoucherViewModel { get; }

    public MainViewModel()
    {
        PaymentViewModel = new PaymentViewModel();
        SeasonViewModel = new SeasonViewModel();
        TouristInfoView = new TouristInfoViewModel();
        TouristViewModel = new TouristViewModel();
        TourViewModel = new TourViewModel();
        VoucherViewModel = new VoucherViewModel();
        LoadAsync();
    }

    private async Task LoadAsync()
    {
        await PaymentViewModel.LoadPaymentAsync();
        await SeasonViewModel.LoadSeasonAsync();
        await TouristViewModel.LoadTouristAsync();
        await TouristInfoView.LoadTouristInfoAsync();
        await TourViewModel.LoadTourAsync();
        await VoucherViewModel.LoadVoucherAsync();
    }
}