using BusinessTripModels.Models;

namespace BussinesTripModels.Models
{
    public class UpdateTripModel
    {
        public int Id { get; set; }
        public Trip Trip { get; set; }

        public UpdateTripModel(int id, Trip trip)
        {
            Trip = trip;
            Id = id;
        }
    }
}
