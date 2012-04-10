using System;
using System.Collections.Generic;
using Amazon.SimpleDB.Model;
using Molibar.Infrastructure.Mapper;
using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.Domain.Repositories;

namespace Molibar.WebTracking.External.SimpleDb.Tracking
{

    public class FormEventRepository : IFormEventRepository
    {
        private readonly ISimpleDbProxy _simpleDbProxy;
        private readonly IEntityMapper _entityMapper;
        public const string DOMAIN_NAME = "FormEvents";

        public FormEventRepository(ISimpleDbProxy simpleDbProxy, IEntityMapper entityMapper)
        {
            _simpleDbProxy = simpleDbProxy;
            _entityMapper = entityMapper;
        }

        public void Initialize()
        {
            if (!_simpleDbProxy.DomainExists(DOMAIN_NAME))
            {
                _simpleDbProxy.CreateDomain(DOMAIN_NAME);
            }
        }


        public void Insert(FormEvent formEvent)
        {
            formEvent.Id = Guid.NewGuid().ToString();
            var replaceableAttributes = _entityMapper.Map<List<ReplaceableAttribute>>(formEvent);
            _simpleDbProxy.Put(DOMAIN_NAME, formEvent.Id, replaceableAttributes);
        }
    }
}
