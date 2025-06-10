using JrAssessment.Model.Base;
using JrAssessment.Model.Database;
using JrAssessment.Model.Request;
using JrAssessment.Model.Response;
using JrAssessment.Repository.SqLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JrAssessment.Core.Services
{
    public interface IBookingService
    {
        //Task<ContentResponse<BookingResponse>> AddBookingAsync(AddBookingRequest request);
    }

    public class BookingService : IBookingService
    {
        private readonly ISqLiteRepo<TblBooking> _bookRepo;
        public BookingService(ISqLiteRepo<TblBooking> bookRepo)
        {
            _bookRepo = bookRepo;
        }

        //public async Task<ContentResponse<BookingResponse>> AddBookingAsync(AddBookingRequest request)
        //{
            
        //}
    }
}
