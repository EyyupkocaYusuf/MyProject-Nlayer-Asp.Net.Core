﻿using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace AgriculturePresentation.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
           _imageService = imageService;
        }

        public IActionResult Index()
        {
           var values =  _imageService.TGetListAll();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddImage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddImage(Image image)
        {

            ImageValidator validationRules = new ImageValidator();
            ValidationResult result = validationRules.Validate(image);
            if (result.IsValid)
            {
                _imageService.TInsert(image);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View();
            }

        }

        [HttpGet]
        public IActionResult UpdateImage(int id)
        {
            var values = _imageService.TGetById(id);
            return View(values);
        }

        [HttpPost]
        public IActionResult UpdateImage(Image image)
        {

            ImageValidator validationRules = new ImageValidator();
            ValidationResult result = validationRules.Validate(image);
            if (result.IsValid)
            {
                _imageService.TUpdate(image);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View();
            }

        }

        public IActionResult DeleteImage(int id)
        {
            var values = _imageService.TGetById(id);
            _imageService.TDelete(values);
            return RedirectToAction("Index");
        }
    }
}
