﻿using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfIntegrationDal : GenericRepository<Integration>, IIntegrationDal
    {
        public EfIntegrationDal(Context context) : base(context)
        {
        }
    }
}
