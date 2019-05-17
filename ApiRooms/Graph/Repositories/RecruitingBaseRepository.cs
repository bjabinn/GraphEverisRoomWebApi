using ApiRooms.Graph.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Graph.Repositories
{
    public class RecruitingBaseRepository<T> : LinqBaseRepository<T, RecruitingDbContext>
        where T : BaseEntity
    {
    }
}