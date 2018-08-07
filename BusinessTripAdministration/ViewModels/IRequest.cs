using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessTripModels;

namespace BusinessTripAdministration.ViewModels
{
    interface IRequest
    {
        void ShowTrips(List<Trip> tripList);
    }
}
