using JrAssessment.Model.Base;
using JrAssessment.Model.Database;
using JrAssessment.Model.Request;
using JrAssessment.Repository.SqLite;
using System.Net;

namespace JrAssessment.Core.Services
{
    public interface IGuestService
    {
        Task<ContentResponse<TblGuest>> RegisterGuestAsync(RegisterGuestRequest request);
    }

    public class GuestService : IGuestService
    {
        private readonly ISqLiteRepo<TblGuest> _guestRepo;
        public GuestService(ISqLiteRepo<TblGuest> guestRepo)
        {
            _guestRepo = guestRepo;
        }

        public async Task<ContentResponse<TblGuest>> RegisterGuestAsync(RegisterGuestRequest request)
        {
            var repeatedUsername = await _guestRepo.GetAsync(x => x.GuestUsername == request.GuestUsername);

            if (repeatedUsername != null)
            {
                return ContentResponse<TblGuest>.Add(HttpStatusCode.BadRequest, "Username has been used", null);
            }

            var repeatedEmail = await _guestRepo.GetAsync(x => x.GuestEmail == request.GuestEmail);

            if (repeatedEmail != null)
            {
                return ContentResponse<TblGuest>.Add(HttpStatusCode.BadRequest, "Email has been used", null);
            }

            if (request.GuestPassword != request.GuestConfirmedPassword)
            {
                return ContentResponse<TblGuest>.Add(HttpStatusCode.BadRequest, "Password not matched", null);
            }

            var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.GuestPassword);

            var newGuest = new TblGuest
            {
                GuestName      = request.GuestName,
                GuestEmail     = request.GuestEmail,
                GuestPhone     = request.GuestPhone,
                GuestBirthDate = request.GuestBirthDate,
                GuestUsername  = request.GuestUsername,
                GuestPassword  = hashPassword,
            };

            await _guestRepo.AddAsync(newGuest);

            return ContentResponse<TblGuest>.Add(HttpStatusCode.OK, "Success register account", newGuest);
        }

        //public async Task<ContentResponse<TblGuest>>
    }
}
