using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IAddressBL
    {
        bool AddAddress(AddressModel addressModel,int userid);

        bool UpdateAddress(AddressModel addressModel, int AddressId);

        bool DeleteAddress(int AddressId);
    }
}
