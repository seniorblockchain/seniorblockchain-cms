using powerpage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powerpage.Services;
public interface ILocalizationService : IService<StringResource>
    {
        Task<StringResource> GetStringResource(string name, string value, int languageId);

        Task InsertDefaultResource(int languageId, string direction = "ltr");
    }
 
