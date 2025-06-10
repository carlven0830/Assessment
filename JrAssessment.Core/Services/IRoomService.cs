using JrAssessment.Core.Mapping;
using JrAssessment.Model.Base;
using JrAssessment.Model.Database;
using JrAssessment.Model.Enums;
using JrAssessment.Model.Request;
using JrAssessment.Model.Response;
using JrAssessment.Repository.SqLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JrAssessment.Core.Services
{
    public interface IRoomService
    {
        Task<ContentResponse<RoomResponse>> AddRoomAsync(AddRoomRequest request);
    }

    public class ProductService : IRoomService
    {
        private readonly ISqLiteRepo<TblRoom> _roomRepo;

        public ProductService(ISqLiteRepo<TblRoom> roomRepo)
        {
            _roomRepo = roomRepo;
        }

        public async Task<ContentResponse<RoomResponse>> AddRoomAsync(AddRoomRequest request)
        {
            var lastRoom = await _roomRepo.GetByOrderAsync(x => x.RoomNum, false);

            int nextNum = 1;

            if (lastRoom != null)
            {
                var numPart = lastRoom.RoomNum.Substring(1);

                if (int.TryParse(numPart, out int lastNum))
                {
                    nextNum = lastNum + 1;
                }
            }

            string generateRoomNum = $"R{nextNum:D3}";

            var newProduct = new TblRoom
            {
                RoomNum       = generateRoomNum,
                RoomType      = request.RoomType,
                PricePerNight = request.PricePerNight,
                Status        = StatusEnum.Available,
            };

            await _roomRepo.AddAsync(newProduct);

            var resp = newProduct.MapToRoomResp();

            return ContentResponse<RoomResponse>.Add(HttpStatusCode.OK, "Success add the room", resp);
        }

        //public async Task<ContentResponse<RoomResponse>> UpdateRoomAsync(UpdateRoomRequest request)
        //{
        //    var room = await _roomRepo.GetAsync(x => x.Id == request.Id);

        //    if (room == null)
        //    {
        //        return ContentResponse<RoomResponse>.Add(HttpStatusCode.BadRequest, "Room not found", null);
        //    }



        //    return ContentResponse<RoomResponse>.Add(HttpStatusCode.OK, "Success update the room", resp);
        //}
    }
}
