using System;
using BLL.Core;
using BLL.Services;

namespace BLL
{
    public interface IAppBLL: IBLL
    {
        IPersonService Persons { get; }
        IContactService Contacts { get; }
        IContactTypeService ContactTypes { get; }
    }
}