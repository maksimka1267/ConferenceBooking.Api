using ConferenceBooking.Api.DTOs.Booking;
using ConferenceBooking.Api.Models.Entities;
using ConferenceBooking.Api.Repository.Interfaces;
using ConferenceBooking.Api.Services.Interfaces;
using Mapster;

namespace ConferenceBooking.Api.Services
{
    public class BookingManagementService : IBookingManagementService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IConferenceHallRepository _conferenceHallRepository;
        private readonly IAdditionalServiceRepository _additionalServiceRepository;
        private readonly IBookingServiceRepository _bookingServiceRepository;
        public BookingManagementService(IBookingRepository bookingRepository,
                            IConferenceHallRepository conferenceHallRepository,
                            IAdditionalServiceRepository additionalServiceRepository,
                            IBookingServiceRepository bookingServiceRepository)
        {
            _bookingRepository = bookingRepository;
            _conferenceHallRepository = conferenceHallRepository;
            _additionalServiceRepository = additionalServiceRepository;
            _bookingServiceRepository = bookingServiceRepository;
        }
        public async Task<IEnumerable<AvailableConferenceHallResponse>> GetAvailableAsync(DateTime start,DateTime end,int capacity)
        {
            var halls = await _conferenceHallRepository.GetAvailableAsync(
                start,
                end,
                capacity);

            return halls.Adapt<IEnumerable<AvailableConferenceHallResponse>>();
        }
        public async Task<BookingResponse> CreateAsync(CreateBookingRequest request)
        {
            var hall = await GetConferenceHallAsync(request.ConferenceHallId);

            await ValidateBookingAvailabilityAsync(
                hall.Id,
                request.StartTime,
                request.EndTime);

            var services = await GetServicesAsync(
                request.ServiceIds,
                hall.Id);

            var totalPrice = CalculateTotalPrice(
                hall,
                request.StartTime,
                request.EndTime,
                services);

            var booking = request.Adapt<Booking>();

            booking.Id = Guid.NewGuid();
            booking.TotalPrice = totalPrice;
            booking.CreatedAt = DateTime.UtcNow;
            booking.UpdatedAt = DateTime.UtcNow;

            await _bookingRepository.AddAsync(booking);
            await _bookingRepository.SaveChangesAsync();

            await SaveBookingServicesAsync(booking, services);

            booking.BookedServices = services.Select(x => new BookingService
            {
                BookingId = booking.Id,
                ServiceId = x.Id,
                Price = x.Price,
                Service = x
            }).ToList();

            return booking.Adapt<BookingResponse>();
        }
        private async Task<ConferenceHall> GetConferenceHallAsync(Guid hallId)
        {
            var hall = await _conferenceHallRepository.GetByIdAsync(hallId);

            if (hall is null)
                throw new KeyNotFoundException("Conference hall was not found.");

            return hall;
        }
        private async Task ValidateBookingAvailabilityAsync(Guid hallId,DateTime start,DateTime end)
        {
            var hasBooking = await _bookingRepository.HasOverlappingBookingAsync(
                hallId,
                start,
                end);

            if (hasBooking)
                throw new InvalidOperationException(
                    "Conference hall is already booked for the selected period.");
        }
        private async Task<List<AdditionalService>> GetServicesAsync(IEnumerable<Guid> ids,Guid hallId)
        {
            var services = await _additionalServiceRepository.GetByIdsAsync(ids);

            if (services.Count != ids.Count())
                throw new KeyNotFoundException("One or more services were not found.");

            if (services.Any(x => x.ConferenceHallId != hallId))
                throw new InvalidOperationException(
                    "Selected services do not belong to the conference hall.");

            return services;
        }
        private async Task SaveBookingServicesAsync(Booking booking, IEnumerable<AdditionalService> services)
        {
            foreach (var service in services)
            {
                await _bookingServiceRepository.AddAsync(new BookingService
                {
                    BookingId = booking.Id,
                    ServiceId = service.Id,
                    Price = service.Price
                });
            }

            await _bookingServiceRepository.SaveChangesAsync();
        }
        private decimal CalculateTotalPrice(ConferenceHall hall,DateTime start,DateTime end,IEnumerable<AdditionalService> services)
        {
            decimal total = 0m;

            for (var current = start; current < end; current = current.AddHours(1))
            {
                decimal hourlyRate = hall.HourlyRate;
                var hour = current.TimeOfDay;

                if (hour >= TimeSpan.FromHours(6) &&
                    hour < TimeSpan.FromHours(9))
                {
                    hourlyRate *= 0.9m;
                }
                else if (hour >= TimeSpan.FromHours(18) &&
                         hour < TimeSpan.FromHours(23))
                {
                    hourlyRate *= 0.8m;
                }
                else if (hour >= TimeSpan.FromHours(12) &&
                         hour < TimeSpan.FromHours(14))
                {
                    hourlyRate *= 1.15m;
                }

                total += hourlyRate;
            }

            total += services.Sum(x => x.Price);

            return decimal.Round(total, 2);
        }
    }
}
