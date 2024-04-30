namespace AppClientes.WebAPI.Models.Profiles
{
    using AppClientes.Model.Entities;
    using AutoMapper;

    public class ClienteProfile : Profile
    {   
        public ClienteProfile()
        {
            CreateMap<ClienteUpdate, Cliente>();
        }
    }
}
