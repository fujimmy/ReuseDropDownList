using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ReuseDropDownList.DataSource;

namespace ReuseDropDownList.Services
{
    public class ZipCodeService : IZipCodeService
    {
        private Database1Entities db = new Database1Entities();

        public bool IsExists(Expression<Func<ZipCode, bool>> predicate)
        {
            return this.db.ZipCode.Any(predicate);
        }

        #region IZipCodeService 成員


        public int TotalCount(Expression<Func<ZipCode, bool>> predicate)
        {
            return this.db.ZipCode.Count(predicate);

        }

        public ZipCode FindOne(int id)
        {
            if (!this.IsExists(x => x.ID == id)) { return null; }
            return this.db.ZipCode.FirstOrDefault(x => x.ID == id);
        }

        public ZipCode FindOneByPostalCode(int postalCode)
        {
            if (!this.IsExists(x => x.PostalCode == postalCode))
            {
                return null;
            }
            return this.db.ZipCode.FirstOrDefault(x => x.PostalCode == postalCode);
        }

        public IQueryable<ZipCode> FindAll()
        {
            return this.db.ZipCode.AsQueryable();
        }

        public IQueryable<ZipCode> Find(Expression<Func<ZipCode, bool>> predicate)
        {
            return this.db.ZipCode.Where(predicate);
        }

        public Dictionary<string, string> GetAllCities()
        {
            var query = (from c in this.FindAll()
                         where c.IsEnabled
                         select new
                         {
                             CityCode = c.CitySort.ToString(),
                             CityName = c.City
                         })
                      .Distinct().OrderBy(x => x.CityCode);
            return query.ToDictionary(x => x.CityCode, x => x.CityName);
        }

        public Dictionary<string, string> GetAllCityDictionary()
        {
            var query = (from c in this.FindAll()
                         where c.IsEnabled
                         select new
                         {
                             CityCode = c.CitySort.ToString(),
                             CityName = c.City
                         })
                      .Distinct().OrderBy(x => x.CityCode);

            Dictionary<string, string> dict = new Dictionary<string, string>();

            foreach (var item in query)
            {
                if (dict.Keys.Count(x => x.Equals(item.CityName)).Equals(0))
                {
                    dict.Add(item.CityName, item.CityName);
                }
            }
            return dict;
        }

        public Dictionary<string, string> GetCountyByCityName(string cityName)
        {
            var query = (from c in this.FindAll()
                         where c.IsEnabled && c.City == cityName
                         select new
                         {
                             PostalCode = c.PostalCode,
                             CountyName = c.County,
                             Sort = c.PostalCode
                         })
                          .Distinct().OrderBy(x => x.Sort);
            return query.ToDictionary(x => x.PostalCode.ToString(), x => x.CountyName);
        }

        #endregion

        #region IDisposable 成員

        public void Dispose()
        {
            this.db.Dispose();
        }

        #endregion
    }
}