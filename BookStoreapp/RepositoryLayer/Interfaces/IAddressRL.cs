using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IAddressRL
    {
        bool AddAddress(AddressModel addressModel, int userid);
        bool UpdateAddress(AddressModel addressModel, int AddressId);
        bool DeleteAddress(int AddressId);
    }
}
