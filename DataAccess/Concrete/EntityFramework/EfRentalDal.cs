using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.DTOs;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarRentalContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var listRentalDetails = (from rental in context.Rentals
                                         join car in context.Cars on 
                                         rental.CarId equals car.Id
                                         join user in context.Users on
                                         rental.CustomerId equals user.Id
                                         join brand in context.Brands on 
                                         car.BrandId equals brand.Id
                                         select new RentalDetailDto
                                         {
                                             Id = rental.Id,
                                             CarId = rental.CarId,
                                             FirstName = user.FirstName,
                                             LastName = user.LastName,
                                             BrandName = brand.Name,
                                             CarName = car.Name,
                                             RentDate = rental.RentDate,
                                             ReturnDate = rental.ReturnDate
                                         }).ToList();

                return listRentalDetails;
            }
        }
    }
}
