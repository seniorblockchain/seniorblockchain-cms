using powerpage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powerpage.Services;

public interface ILanguageService : IService<Language>
{
    IEnumerable<Language> GetLanguages();
    Language GetLanguageByCulture(string culture);


    //Task AddLanguage(string name, string culture);
    //Task RemoveLanguage(int Id);
    //Task EditLanguage(int Id);

}
