using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car { Id = 1, BrandId = 1, ColorId = 1, DailyPrice = 250, ModelYear = 2020, Description = "" },
                new Car { Id = 2, BrandId = 2, ColorId = 2, DailyPrice = 150, ModelYear = 2018, Description = ""},
                new Car { Id = 3, BrandId = 3, ColorId = 2, DailyPrice = 175, ModelYear = 2018, Description = ""},
                new Car { Id = 4, BrandId = 4, ColorId = 3, DailyPrice = 200, ModelYear = 2019, Description = ""},
                new Car { Id = 5, BrandId = 5, ColorId = 1, DailyPrice = 210, ModelYear = 2019, Description = ""}
            };
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            var result = _cars.SingleOrDefault(p => p.Id == car.Id);
            _cars.Remove(result);
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Car GetById(int id)
        {
            return _cars.SingleOrDefault(p => p.Id == id);
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            var result = _cars.SingleOrDefault(p => p.Id == car.Id);
            result.BrandId = car.BrandId;
            result.ColorId = car.ColorId;
            result.DailyPrice = car.DailyPrice;
            result.ModelYear = car.ModelYear;
            result.Description = car.Description;
        }
    }
}
