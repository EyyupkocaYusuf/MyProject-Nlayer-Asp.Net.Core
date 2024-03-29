﻿using AgriculturePresentation.Models;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace AgriculturePresentation.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public IActionResult Index()
        {
            var values = _serviceService.TGetListAll();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddService()
        {
            return View(new ServiceAddViewModel());
        }

        [HttpPost]
        public IActionResult AddService(ServiceAddViewModel model)
        {
            if(ModelState.IsValid)
            {
                _serviceService.TInsert(new Service
                {
                    Title = model.Title,
                    Description = model.Description,
                    Image = model.Image
                });

                return RedirectToAction("Index");
            }
            return View(model);
                     
        }

        public IActionResult DeleteService(int id)
        {
            var value = _serviceService.TGetById(id);
            _serviceService.TDelete(value);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateService(int id)
        {
            var values = _serviceService.TGetById(id);
            return View(values);
        }

        [HttpPost]
        public IActionResult UpdateService(Service service)
        {
            _serviceService.TUpdate(service);
                return RedirectToAction("Index");          

        }

    }
}
