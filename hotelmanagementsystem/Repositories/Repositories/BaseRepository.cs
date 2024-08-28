using BusinessObjects.Models;

namespace Repositories.Repositories
{
    public abstract class BaseRepository 
    {
        protected readonly HotelContext _hotelContext;
        public BaseRepository(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
    }
}
