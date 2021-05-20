using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarRentalContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var listCarDetails = from car in context.Cars
                    join brand in context.Brands on
                        car.BrandId equals brand.Id
                    join color in context.Colors on
                        car.ColorId equals color.Id
                    join image in context.CarImages on
                        car.Id equals image.CarId
                    select new CarDetailDto
                    {
                        Id = car.Id,
                        BrandId = car.BrandId,
                        ColorId = car.ColorId,
                        CarName = car.Name,
                        ModelYear = car.ModelYear,
                        BrandName = brand.Name,
                        ColorName = color.Name,
                        DailyPrice = car.DailyPrice,
                        Description = car.Description,
                        ImagePath = image.ImagePath
                    };

                return filter == null ? listCarDetails.ToList() : listCarDetails.Where(filter).ToList();
            }
        }
    }
}
