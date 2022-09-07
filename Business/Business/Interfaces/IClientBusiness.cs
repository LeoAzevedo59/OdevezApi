﻿using Odevez.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Business.Interfaces
{
    public interface IClientBusiness
    {
        Task<List<ClientDTO>> ListByFilterAsync(int? clientId = 0, string name = null);
        Task<bool> InserirClient(ClientDTO client);
        Task<bool> LoginClient(long phoneNumber, string password);
    }
}
