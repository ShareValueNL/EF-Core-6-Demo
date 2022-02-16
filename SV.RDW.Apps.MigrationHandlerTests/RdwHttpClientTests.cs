using NUnit.Framework;
using SV.RDW.Apps.Import;
using System;
using System.Threading.Tasks;

namespace SV.RDW.Apps.MigrationHandlerTests
{
    public class RdwHttpClientTests
    {
        RdwHttpClient _sut = new RdwHttpClient();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GivenLimitAndFirstAdmissionDate_WhenGetVehicles_VehiclesReturnedAsync()
        {
            // Arrange
            var offset = 0;
            var firstAdmission = new DateTime(2022, 2, 1);

            // Act
            var result = await _sut.GetVehicles(offset, firstAdmission);

            // Assert
        }
    }
}