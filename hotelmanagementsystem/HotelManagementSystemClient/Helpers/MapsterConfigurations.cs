using Mapster;
using BusinessObjects.Models;
using HotelManagementSystemClient.Models.Accounts;

namespace HotelManagementSystemClient.Helpers
{
	public static class MapsterConfiguration
	{
		public static void RegisterMappings()
		{
			TypeAdapterConfig<RegisterViewModel, Customer>
				.NewConfig()
				.Map(dest => dest.FirstName, src => src.FirstName)
				.Map(dest => dest.LastName, src => src.LastName)
				.Map(dest => dest.Username, src => src.Username)
				.Map(dest => dest.Password, src => src.Password)
				.Map(dest => dest.Email, src => src.Email)
				.Map(dest => dest.PhoneNumber, src => src.PhoneNumber);
		}
	}
}



