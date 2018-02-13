using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ReuseDropDownList.DataSource;

namespace ReuseDropDownList.Services
{
    public interface IZipCodeService : IDisposable
    {
        bool IsExists(Expression<Func<ZipCode, bool>> predicate);
        int TotalCount(Expression<Func<ZipCode, bool>> predicate);

        ZipCode FindOne(int id);
        ZipCode FindOneByPostalCode(int postalCode);

        IQueryable<ZipCode> FindAll();
        IQueryable<ZipCode> Find(Expression<Func<ZipCode, bool>> predicate);

        Dictionary<string, string> GetAllCities();
        Dictionary<string, string> GetAllCityDictionary();
        Dictionary<string, string> GetCountyByCityName(string cityName);
    }
}