using System.Web.Mvc;
using Hulen.BusinessServices.Interfaces;
using Hulen.WebCode.Attributes;
using Hulen.WebCode.MvcBase;
using Hulen.WebCode.ViewModels;

namespace Hulen.WebCode.Controllers
{
    [HulenAuthorize("PAGE_HOTEL")]
    public class HotelController : HulenController
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public ViewResult Index()
        {
            var model = new HotelViewModel { Hotels = _hotelService.GetAllHotels() };
            return View("Index", model);
        }

        public ViewResult Create()
        {
            return View("Create", new HotelViewModel());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Create(HotelViewModel model)
        {
            _hotelService.SaveNewHotel(model.Hotel);
            ViewData["Message"] = "Nytt hotell er lagret";
            return View("Create", model);
        }

        public ViewResult Edit(int id)
        {
            var model = new HotelViewModel { Hotel = _hotelService.GetOneHotelById(id) };
            return View("Edit", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Edit(HotelViewModel model)
        {
            _hotelService.UpdateHotel(model.Hotel);
            ViewData["Message"] = "Hotellet er oppdatert.";
            return View("Edit", model);
        }

        public ViewResult Delete(int id)
        {
            var model = new HotelViewModel {Hotel = _hotelService.GetOneHotelById(id)};
            return View("Delete", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Delete(HotelViewModel model)
        {
            _hotelService.DeleteHotel(model.Hotel);
            ViewData["Message"] = "Følgende informasjon er slettet fra databasen.";
            return View("Delete", model);
        }
    }
}