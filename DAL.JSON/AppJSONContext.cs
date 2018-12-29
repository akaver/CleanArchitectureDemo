using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DAL.Core;
using DAL.JSON.Repositories;
using DAL.Repositories;
using Domain;
using Domain.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DAL.JSON
{
    public class AppJSONContext : IDataContext
    {
        private readonly Dictionary<Type, object> _dataCache = new Dictionary<Type, object>();
        private readonly string _dataPath = "/Users/Akaver/Magister/TarkvaraArhitektuur/CleanArchitectureDemo/Data/";

        public AppJSONContext()
        {
            LoadData<Person>();
            LoadData<Contact>();
            LoadData<ContactType>();

            foreach (var contact in DataSet<Contact>())
            {
                
            }
        }


        private void LoadData<TEntity>() where TEntity : BaseEntity
        {
            _dataCache[typeof(TEntity)] = JsonConvert.DeserializeObject<List<TEntity>>(
                File.ReadAllText(_dataPath + typeof(TEntity).Name + ".json"));
        }

        public List<TEntity> DataSet<TEntity>() where TEntity : BaseEntity
        {
            if (!_dataCache.ContainsKey(typeof(TEntity)))
            {
                _dataCache[typeof(TEntity)] = new List<TEntity>();
            }


            var res = (List<TEntity>) _dataCache[typeof(TEntity)];

            return res;

        }


        public int SaveChanges()
        {
            var res = 0;
            foreach (var key in _dataCache.Keys)
            {
                var jsonText = JsonConvert.SerializeObject(_dataCache[key]);
                res += jsonText.Length;
                File.WriteAllText(_dataPath+ key.Name+".json",
                    jsonText);
                
            }
            
            return res;
        }

        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(SaveChanges());
        }
    }
}