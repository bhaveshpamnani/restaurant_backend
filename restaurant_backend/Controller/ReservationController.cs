using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restaurant_backend.Data;
using restaurant_backend.Model;

namespace restaurant_backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationRepository _reservationRepository;

        public ReservationController(ReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        [HttpGet]
        public IActionResult GetAllReservation()
        {
            var reservation = _reservationRepository.GetAllReservation();
            return Ok(reservation);
        }

        [HttpGet("{ReservationID}")]
        public IActionResult GetReservationByID(int ReservationID)
        {
            var reservation = _reservationRepository.GetReservationByID(ReservationID);
            return Ok(reservation);
        }

        #region Create Reservation
        [HttpPost("ReserveTable")]
        public IActionResult ReserveTable([FromBody] ReservationModel reservationRequest)
        {
            var response = _reservationRepository.CreateReservation(reservationRequest);
            return Ok(response);
        }
        #endregion


        [HttpPut]
        public IActionResult UpdateReservation(ReservationUpdateModel reservationModel)
        {
            var value = _reservationRepository.UpdateReservation(reservationModel);

            if (value)
            {
                Console.WriteLine("Update successful.");
                return Ok(value);
            }
            else
            {
                Console.WriteLine("Update failed.");
                return BadRequest("Update failed.");
            }
        }
        
        

        [HttpDelete("{ReservationID}")]
        public IActionResult DeleteReservation(int ReservationID)
        {
            var value = _reservationRepository.DeleteReservation(ReservationID);
            return Ok(value);
        }
        
        [HttpGet("ByUserId/{UserID}")]
        public IActionResult GetReservationsByUserId(int UserID)
        {
            var reservations = _reservationRepository.GetReservationsByUserId(UserID);
            return Ok(reservations);
        }
        
        [HttpPut("ByUser")]
        public IActionResult UpdateByUserReservation(ReservationModel reservationModel)
        {
            var value = _reservationRepository.UpdateByUserReservation(reservationModel);

            if (value)
            {
                Console.WriteLine("Update successful.");
                return Ok(value);
            }
            else
            {
                Console.WriteLine("Update failed.");
                return BadRequest("Update failed.");
            }
        }
    }
}
