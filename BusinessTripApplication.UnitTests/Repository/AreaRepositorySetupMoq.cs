using BusinessTripModels.Models;
using BusinessTripApplication.Repository;
using Moq;
using System.Collections.Generic;

namespace BusinessTripApplication.UnitTests.Repository
{
    public static class AreaRepositorySetupMoq
    {
        public static void FindAll(Mock<IAreaRepository> MockAreaRepository, IList<Area> areas)
        {
            MockAreaRepository.Setup(mock => mock.FindAll()).Returns(areas);
        }
    }
}
