using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using powerpage.Entities;
using Microsoft.EntityFrameworkCore;
using powerpage.DataLayer.Context;
using powerpage.Entities.Identity;
using powerpage.Services.Identity;

namespace powerpage.Services;

public class LanguageService : Service<Language>, ILanguageService

{
    private readonly DbSet<Language> _languages;
    private readonly IUnitOfWork _uow;
    public LanguageService(IUnitOfWork uow, IRepositoryAsync<Language> repository) : base(repository)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));

        _languages = uow.Set<Language>();
    }

    public IEnumerable<Language> GetLanguages()
    {
        return _languages.ToList();
    }

    public Language GetLanguageByCulture(string culture)
    {
        return _languages.FirstOrDefault(x => x.Culture == culture);
    }


     
}
