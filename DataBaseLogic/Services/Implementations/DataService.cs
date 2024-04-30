
using DataBaseLogic.Entities;
using DataBaseLogic.Enums;
using DataBaseLogic.Models;
using DataBaseLogic.Repositories.Interfaces;
using DataBaseLogic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLogic.Services.Implementations
{
    public class DataService : IDataService
    {
        private readonly IBaseRepository<Abonent> _abonentRepository;

        private readonly IBaseRepository<PhoneNumber> _phoneRepository;

        private readonly IBaseRepository<Address> _addressRepository;

        private readonly IBaseRepository<Street> _streetRepository;

        public DataService(IBaseRepository<Abonent> abonentRepository, IBaseRepository<PhoneNumber> phoneRepository, IBaseRepository<Address> addressRepository, IBaseRepository<Street> streetRepository)
        {
            _abonentRepository = abonentRepository;

            _phoneRepository = phoneRepository;

            _addressRepository = addressRepository;

            _streetRepository = streetRepository;
        }

        public async Task<IQueryable<AbonentModel>> GetAbonentInfoAsync()
        {
            var abonents = await _abonentRepository.GetData();

            var phones = await _phoneRepository.GetData();

            var addresses = await _addressRepository.GetData();

            var streets = await _streetRepository.GetData();

            var result = (from abonent in abonents
                          join address in addresses on abonent.AddressId equals address.Id
                          join street in streets on address.StreetId equals street.Id
                          join phoneNumber in phones on abonent.Id equals phoneNumber.AbonentId into phoneNumbers
                          from phoneNumber in phoneNumbers.DefaultIfEmpty()
                          group new { abonent, address, street, phoneNumber } by new { abonent.Id, abonent.FullName, street.Name, address.HouseNumber } into grouped
                          select new AbonentModel
                          {
                              FullName = grouped.Key.FullName,
                              Street = grouped.Key.Name,
                              HouseNumber = grouped.Key.HouseNumber,
                              HomePhoneNumber = grouped.Max(x => x.phoneNumber.PhoneType == PhoneType.Home  ? x.phoneNumber.Number : null),
                              WorkPhoneNumber = grouped.Max(x => x.phoneNumber.PhoneType == PhoneType.Work ? x.phoneNumber.Number : null),
                              MobilePhoneNumber = grouped.Max(x => x.phoneNumber.PhoneType == PhoneType.Mobile ? x.phoneNumber.Number : null)
                          }).AsQueryable();

            return result;
        }

        public async Task<IQueryable<StreetModel>> GetStreetInfoAsync()
        {
            var abonents = await _abonentRepository.GetData();

            var addresses = await _addressRepository.GetData();

            var streets = await _streetRepository.GetData();

            var streetsInfo = streets
             .GroupJoin(addresses,
                        street => street.Id,
                        address => address.StreetId,
                        (street, addresses) => new { Street = street, Addresses = addresses })
             .SelectMany(sa => sa.Addresses, (sa, address) => new { sa.Street, Address = address })
             .GroupJoin(abonents,
                        saa => saa.Address.Id,
                        abonent => abonent.AddressId,
                        (saa, abonents) => new
                        {
                            saa.Street.Name,
                            NumberOfAbonents = abonents.Count()
                        })
             .GroupBy(result => result.Name)
             .Select(group => new StreetModel
             {
                 Name = group.Key,
                 AbonentsCount = group.Sum(x => x.NumberOfAbonents)
             });

            return streetsInfo;
        }
    }
}
