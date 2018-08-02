using BusinessTripModels;

namespace BusinessTripAdministration.Models
{
    public class UpdateTripModel
    {
        private int Id { get; set; }
        private Trip Trip { get; set; }

        public UpdateTripModel(int id, Trip t)
        {
            Trip = t;
            Id = id;
        }
    }
}
