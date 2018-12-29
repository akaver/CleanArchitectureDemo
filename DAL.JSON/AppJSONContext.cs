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
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DAL.JSON
{
    public class AppJSONContext : IDataContext
    {
        private readonly Dictionary<Type, object> _dataCache = new Dictionary<Type, object>();
        private readonly string _dataPath;

        public AppJSONContext(IOptionsMonitor<AppJSONContextOptions> options)
        {
            _dataPath = options.CurrentValue.DataPath;

            LoadData<Person>();
            LoadData<Contact>();
            LoadData<ContactType>();


            // Restore relationships
            foreach (var contact in DataSet<Contact>())
            {
                var person = DataSet<Person>().First(a => a.Id == contact.PersonId);
                if (person.Contacts == null) person.Contacts = new List<Contact>();
                person.Contacts.Add(contact);
                contact.Person = person;

                var contactType = DataSet<ContactType>().First(a => a.Id == contact.ContactTypeId);
                if (contactType.Contacts == null) contactType.Contacts = new List<Contact>();
                contactType.Contacts.Add(contact);
                contact.ContactType = contactType;
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

           return (List<TEntity>) _dataCache[typeof(TEntity)];
        }


        public int SaveChanges()
        {
            var res = 0;
            foreach (var key in _dataCache.Keys)
            {
                var jsonText = JsonConvert.SerializeObject(_dataCache[key]);
                res += jsonText.Length;
                File.WriteAllText(_dataPath + key.Name + ".json",
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