using Business.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private readonly ICarImageDal _carImageDal;
        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult Add(IFormFile file, CarImage carImage)
        {
            var result = BusinessRules.Run(CheckCarImagesCount(carImage.CarId));

            var query = GetByCarId(carImage.CarId).Data;

            if (result != null)
            {
                return result;
            }

            if(file == null)
            {
                carImage.ImagePath = "/Images/default.png";
                carImage.Date = DateTime.Now;
                _carImageDal.Add(carImage);
            }

            else
            {
                if (query.ImagePath.Contains("default"))
                {
                    carImage.ImagePath = FileHelper.Add("Images", file);
                    carImage.Date = DateTime.Now;
                    carImage.Id = query.Id;
                    _carImageDal.Update(carImage);
                }

                else
                {
                    carImage.ImagePath = FileHelper.Add("Images", file);
                    carImage.Date = DateTime.Now;
                    _carImageDal.Add(carImage);
                }
            }
            return new SuccessResult();
        }

        public IResult Delete(CarImage carImage)
        {
            var entity = _carImageDal.Get(p => p.Id == carImage.Id);

            if (entity == null)
            {
                return new ErrorResult();
            }

            FileHelper.Delete(entity.ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<CarImage> GetByCarId(int carId)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(p => p.CarId == carId));
        }

        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(p => p.Id == id));
        }

        public IResult Update(IFormFile file, CarImage carImage)
        {
            var entity = _carImageDal.Get(p => p.Id == carImage.Id);

            if (entity == null)
            {
                return new ErrorResult();
            }

            FileHelper.Delete(entity.ImagePath);
            entity.ImagePath = FileHelper.Add("Images", file);
            _carImageDal.Update(entity);
            return new SuccessResult();
        }

        private IResult CheckCarImagesCount(int carId)
        {
            var result = _carImageDal.GetAll(p => p.CarId == carId).Count;
            if (result >= 5)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
    }
}
