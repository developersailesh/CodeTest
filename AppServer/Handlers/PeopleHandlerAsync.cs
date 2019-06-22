namespace AppServer.Handlers
{
    using AppServer.Interfaces;
    using AppServer.Models;
    using AppServer.Query;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// This Handler consumes People Api and groupby PetDetails based on Gender.
    /// </summary>
    public class PeopleHandlerAsync : MediatR.IAsyncRequestHandler<PeopleQuery, IEnumerable<People>>
    {
        public  IPeopleClientService _iPeopleClientService;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public PeopleHandlerAsync(IPeopleClientService iPeopleClientService)
        {
            _iPeopleClientService = iPeopleClientService;
        }

        /// <summary>
        /// Query This Handler consumes People Api and groupby PetDetails based on Gender.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<IEnumerable<People>> Handle(PeopleQuery message)
        {
            IEnumerable<People> peopleData =  await _iPeopleClientService.GetPeopleData(_cancellationTokenSource.Token);

            var peopleDataGroupByGender = peopleData
                                            .GroupBy(x => x.gender,
                                                     (key, elements) =>
                                                        new {
                                                            gender = key,                                                  
                                                            petsCollection = elements.Where(k => k.petsCollection != null)
                                                            .SelectMany(o => o.petsCollection)
                                                            .OrderBy(s => s.name).ToList()
        });
            if (peopleDataGroupByGender.Any())
            {
                List<People> lstPeople = new List<People>();
                foreach (var peopleDetails in peopleDataGroupByGender)
                {
                    People people = new People();
                    people.gender = peopleDetails.gender;
                    people.petsCollection = peopleDetails.petsCollection;
                    lstPeople.Add(people);
                }

                return lstPeople;
            }
            else
            {
                return null;
            }
        }
    }
}
