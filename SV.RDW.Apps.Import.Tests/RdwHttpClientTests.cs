using FluentAssertions;
using FluentAssertions.Extensions;
using SV.RDW.Apps.Import;
using System;
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
            result.Should().NotBeEmpty();

        }

        [Fact]
		public void GetVehiclesTiming()
		{
			Action performTestCoversion = async () => await GetVehiclesTimingInternal();
			performTestCoversion.ExecutionTime().Should().BeLessThanOrEqualTo(100.Milliseconds());
		}

		private async Task GetVehiclesTimingInternal()
		{
            var offset = 0;
            var firstAdmission = new DateTime(2022, 2, 1);

            // Act
            var result = await _sut.GetVehicles(offset, firstAdmission);

            result.Should().NotBeEmpty();

        }
    }
}