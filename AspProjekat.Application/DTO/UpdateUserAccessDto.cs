﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Application.DTO
{
    public class UpdateUserAccessDto
    {
        public int RoleId { get; set; }
        public IEnumerable<int> UseCaseIds { get; set; }
    }
}
