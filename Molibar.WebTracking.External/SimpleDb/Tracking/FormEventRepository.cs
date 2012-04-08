using System;
using System.Collections.Generic;
using Amazon.SimpleDB.Model;
using Molibar.Infrastructure.Mapper.AutoMapper;
using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.Domain.Repositories;

namespace Molibar.WebTracking.External.SimpleDb.Tracking
{

    public class FormEventRepository : SimpleDbProxy, IFormEventRepository
    {
        private readonly EntityMapper _entityMapper;
        public const string DOMAIN_NAME = "FormEvents";

        public FormEventRepository(EntityMapper entityMapper) : base(DOMAIN_NAME)
        {
            _entityMapper = entityMapper;
        }

        public void Initialize()
        {
            if (!DomainExists(DOMAIN_NAME))
            {
                CreateDomain(DOMAIN_NAME);
            }
        }


        public void Insert(FormEvent formEvent)
        {
            formEvent.Id = Guid.NewGuid().ToString();
            var replaceableAttributes = _entityMapper.Map<List<ReplaceableAttribute>>(formEvent);
            Put(DOMAIN_NAME, formEvent.Id, replaceableAttributes);
        }
    }
}
