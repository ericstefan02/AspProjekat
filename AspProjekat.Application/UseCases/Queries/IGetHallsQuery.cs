﻿using AspProjekat.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Application.UseCases.Queries
{
    public interface IGetHallsQuery:IQuery<PagedResponse<HallDto>, PagedSearch>
    {
    }
}
