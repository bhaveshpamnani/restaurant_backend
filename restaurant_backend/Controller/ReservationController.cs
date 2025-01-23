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

        [HttpPost]
        public IActionResult CreateReservation(ReservationModel reservationModel)
        {
            var value = _reservationRepository.CreateReservation(reservationModel);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateReservation(ReservationModel reservationModel)
        {
            var value = _reservationRepository.UpdateReservation(reservationModel);
            return Ok(value);
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
    }
}
