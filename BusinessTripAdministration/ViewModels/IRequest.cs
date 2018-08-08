using System.Collections.Generic;
using BusinessTripModels.Models;

namespace BusinessTripAdministration.ViewModels
{
    interface IRequest
    {
        void ShowTrips(List<Trip> tripList);
    }
}
