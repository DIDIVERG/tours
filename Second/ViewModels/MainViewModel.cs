using System.Threading.Tasks;

namespace Second.ViewModels;

public class MainViewModel
{
    public PaymentViewModel PaymentViewModel { get; }
    public SeasonViewModel SeasonViewModel { get; }
    public TouristInfoViewModel TouristInfoViewModel { get; }
    public TouristViewModel TouristViewModel { get; }
    public TourViewModel TourViewModel { get; }
    public VoucherViewModel VoucherViewModel { get; }
    public QueryViewModel QueryViewModel { get; }
    
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
        await PaymentViewModel.LoadPaymentAsync();
        await SeasonViewModel.LoadSeasonAsync();
        await TouristViewModel.LoadTouristAsync();
        await TouristInfoViewModel.LoadTouristInfoAsync();
        await TourViewModel.LoadTourAsync();
        await VoucherViewModel.LoadVoucherAsync();
    }
}