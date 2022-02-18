using FluentAssertions;
using FluentAssertions.Extensions;
using SV.RDW.Apps.Import;
using SV.RDW.Data.Entities.ImportJson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SV.RDW.Apps.MigrationHandlerTests
{
    public class RdwHttpClientTests
    {
        RdwHttpClient _sut = new RdwHttpClient();

        public RdwHttpClientTests()
        {
        }

        [Fact]
        public async Task GivenLimitAndFirstAdmissionDate_WhenGetVehicles_VehiclesReturnedAsync()
        {
            // Arrange
            var offset = 0;
            var firstAdmission = new DateTime(2022, 2, 1);

            // Act
            var result = await _sut.GetVehicles(offset, firstAdmission);

            // Assert
            result.Should().HaveCount(1000);
        }

        [Fact]
        public async Task GivenMultipleAdmissionDates_WhenGetVehicles_VehiclesReturnedAsync()
        {
            // Arrange
            var startDate = new DateTime(2022, 2, 1);
            var endDate = new DateTime(2022, 2, 7);
            var vehicles = new List<Voertuig>();

            // Act
            for (var date = startDate.Date; date.Date <= endDate.Date; date = date.AddDays(1))
            {
                var result = await _sut.GetVehicles(date);

                vehicles.AddRange(result);
            }

            // Assert
            vehicles.Should().HaveCount(7817);
        }

        [Fact]
		public async void GetVehiclesTiming()
		{
            Action performTestCoversion = async () => await GetVehiclesTimingInternal();
            performTestCoversion.ExecutionTime().Should().BeLessThanOrEqualTo(2000.Milliseconds());
        }

		private async Task GetVehiclesTimingInternal()
		{
            var firstAdmission = new DateTime(2022, 2, 1);

            // Act
            var result = await _sut.GetVehicles(firstAdmission);

            result.Should().HaveCount(1272);
        }
    }
}